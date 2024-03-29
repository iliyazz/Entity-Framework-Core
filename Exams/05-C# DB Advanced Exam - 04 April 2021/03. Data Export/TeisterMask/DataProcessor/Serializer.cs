﻿namespace TeisterMask.DataProcessor
{
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using ExportDto;
    using Microsoft.VisualBasic;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models.Enums;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Projects");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportProjectDto[]), xmlRoot);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            var projects = context
                .Projects
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectDto()
                {
                    Name = p.Name,
                    HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                    TasksCount = p.Tasks.Count,
                    Tasks = p.Tasks
                        .Select(t => new ExportProjectTaskDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .OrderBy(t => t.Name)
                        .ToArray()
                })
                .OrderByDescending(p => p.Tasks.Length)
                .ThenBy(p => p.Name)
                .ToArray();
            xmlSerializer.Serialize(writer, projects, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .ToArray()
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(et => et.Task.OpenDate >= date)
                        .ToArray()
                        .OrderByDescending(t => t.Task.DueDate)
                        .ThenBy(t => t.Task.Name)
                        .Select(et => new
                        {
                            TaskName = et.Task.Name,
                            OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = et.Task.LabelType.ToString(),
                            ExecutionType = et.Task.ExecutionType.ToString(),
                        })

                        .ToArray()
                })
                .OrderByDescending(e => e.Tasks.Length)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToArray();
            string output = JsonConvert.SerializeObject(employees, Formatting.Indented);
            return output;
        }
    }
}