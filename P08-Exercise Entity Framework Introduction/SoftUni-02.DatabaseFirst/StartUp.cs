using SoftUni.Data;
namespace SoftUni;


public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContexto = new SoftUniContext();
        Console.WriteLine("Connection Success");
    }
}