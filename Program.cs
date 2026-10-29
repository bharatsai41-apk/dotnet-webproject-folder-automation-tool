using System;
using System.Diagnostics;
using System.IO;

public class Folder_Structure_Creator : IFolder_Structure_Creator
{
    public string Folder_Creator(string project_name)
    {
        // Step 1: Define the commands
        string webApiCmd = $"new webapi -n {project_name}.Api";
        string infraCmd = $"new classlib -n {project_name}.Infrastructure";
        string domainCmd = $"new classlib -n {project_name}.Domain";
        string hubCmd = $"new classlib -n {project_name}.Hub";
        string slnCmd = $"new sln -n {project_name}";

        // Step 2: Check if the directory exists
        if (Directory.Exists(project_name))
        {
            throw new Exception("A directory exists with the current name.");
        }

        // Step 3: Create and get the absolute path of the root directory
        Directory.CreateDirectory(project_name);
        string targetPath = Path.GetFullPath(project_name);

        // Step 4: Execute project generation
        RunDotNetCommand(slnCmd, targetPath);
        RunDotNetCommand(webApiCmd, targetPath);
        RunDotNetCommand(infraCmd, targetPath);
        RunDotNetCommand(domainCmd, targetPath);
        RunDotNetCommand(hubCmd, targetPath);

        // Step 5: Dynamically search for the created solution file (.sln OR .slnx)
        string[] solutionFiles = Directory.GetFiles(targetPath, $"{project_name}.sln*");
        if (solutionFiles.Length == 0)
        {
            throw new Exception($"Could not find a generated solution file for {project_name} in {targetPath}");
        }
        // Extract just the filename (e.g., "test.slnx")
        string actualSolutionName = Path.GetFileName(solutionFiles[0]);

        // Step 6: Reference the dynamically resolved solution file name
        RunDotNetCommand($"sln \"{actualSolutionName}\" add \"{project_name}.Api/{project_name}.Api.csproj\"", targetPath);
        RunDotNetCommand($"sln \"{actualSolutionName}\" add \"{project_name}.Infrastructure/{project_name}.Infrastructure.csproj\"", targetPath);
        RunDotNetCommand($"sln \"{actualSolutionName}\" add \"{project_name}.Domain/{project_name}.Domain.csproj\"", targetPath);
        RunDotNetCommand($"sln \"{actualSolutionName}\" add \"{project_name}.Hub/{project_name}.Hub.csproj\"", targetPath);

        return $"Successfully created full clean-architecture structure at: {targetPath}";
    }


    private void RunDotNetCommand(string arguments, string workingDirectory)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "dotnet",
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process process = Process.Start(startInfo)
            ?? throw new Exception($"Failed to start dotnet command: {arguments}");

        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            string error = process.StandardError.ReadToEnd();
            throw new Exception($"Dotnet CLI Error ({arguments}): {error}");
        }
    }
}

public partial class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter your project name: ");
        string? input = Console.ReadLine();
        string project_name = input?.Trim() ?? "";

        if (string.IsNullOrEmpty(project_name) || project_name.Length > 150)
        {
            throw new Exception("Project name length should be between 1 and 150 characters.");
        }

        // Clean up previous failed 'test' directories if they exist before running
        if (Directory.Exists(project_name))
        {
            Directory.Delete(project_name, true);
        }

        Folder_Structure_Creator fsc = new Folder_Structure_Creator();
        string successMessage = fsc.Folder_Creator(project_name);

        Console.WriteLine(successMessage);
    }
}

public interface IFolder_Structure_Creator
{
    string Folder_Creator(string project_name);
}
