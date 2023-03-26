// ReSharper disable InconsistentNaming

namespace TeisterMask.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Diagnostics.Metrics;
    using System.Text;
    using System.Xml.Serialization;
    using ImportDto;
    using TeisterMask.Data.Models;
    using System.Globalization;
    using Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Projects");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProjectTaskDto[]), xmlRoot);
            using StringReader xmlReader = new StringReader(xmlString);
            ImportProjectTaskDto[] oDtos = (ImportProjectTaskDto[])xmlSerializer.Deserialize(xmlReader);
            List<Project> projects = new List<Project>();

            foreach (var oDto in oDtos)
            {
                if (!IsValid(oDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                bool isValidOpenDate = DateTime.TryParseExact(oDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDate);
                if (!isValidOpenDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? dueDate = null;
                if (!string.IsNullOrWhiteSpace(oDto.DueDate))
                {
                    bool isValidDueDate = DateTime.TryParseExact(oDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime currentDate);
                    if (!isValidDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    dueDate = currentDate;
                }


                Project project = new Project()
                {
                    Name = oDto.Name,
                    OpenDate = openDate,
                    DueDate = dueDate,
                };
                List<Task> tasks = new List<Task>();

                foreach (var taskDto in oDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    bool isValidTaskOpenDate = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openTaskDate);
                    if (!isValidTaskOpenDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    bool isValidTaskDueDate = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime deuTaskDate);
                    if (!isValidTaskDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (openTaskDate < openDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (dueDate.HasValue && deuTaskDate > dueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task task = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = openTaskDate,
                        DueDate = deuTaskDate,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType,
                    };
                    tasks.Add(task);
                }
                project.Tasks = tasks;
                projects.Add(project);
                sb.AppendLine(string.Format(SuccessfullyImportedProject, oDto.Name, tasks.Count));
            }
            context.Projects.AddRange(projects);
            context.SaveChanges();
            
            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportEmployeeTaskDto[] odDtos = JsonConvert.DeserializeObject<ImportEmployeeTaskDto[]>(jsonString);
            List<Employee> employees = new List<Employee>();

            foreach (var odDto in odDtos)
            {
                if (!IsValid(odDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee()
                {
                    Username = odDto.Username,
                    Email = odDto.Email,
                    Phone = odDto.Phone,
                };

                foreach (var dtoTask in odDto.Tasks.Distinct())
                {
                    Task currentTask = context.Tasks.Find(dtoTask);
                    if (currentTask == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    EmployeeTask newEt = new EmployeeTask()
                    {
                        Task = currentTask
                    };
                    employee.EmployeesTasks.Add(newEt);
                }
                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username,
                    employee.EmployeesTasks.Count));
            }
            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}