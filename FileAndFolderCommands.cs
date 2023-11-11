using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp36;

public static class FileAndFolderCommands
{
    /// <summary>
    /// Добавление файла
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="name"></param>
    public static void AddFile(string rootPath, string name)
    {
        var newFilePath = Path.Combine(rootPath, name);
        using var createdFile = File.Create(newFilePath);
        Console.WriteLine($"Файл {newFilePath} создан!");
    }

    /// <summary>
    /// Удаление файла
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFile(string path)
    {
        var currentTryCount = 0;
        var maxTryCount = 5;

        while (maxTryCount > currentTryCount)
        {
            try
            {
                File.Delete(path);
                Console.WriteLine($"Файл {path} удален!");
                return;
            }
            catch (Exception)
            {
                currentTryCount++;
                Thread.Sleep(1000);
            }
        }
       
    }

    /// <summary>
    /// Добавить директорию
    /// </summary>
    /// <param name="rootPath"></param>
    /// <param name="name"></param>
    public static void AddFolder(string rootPath, string name)
    {
        var newDirPath = Path.Combine(rootPath, name);
        var dirInfo = Directory.CreateDirectory(newDirPath);
        dirInfo.Refresh();
      
        Console.WriteLine($"Директория {newDirPath} создана!");
    }

    /// <summary>
    /// Удалить директорию
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFolder(string path)
    {
        var currentTryCount = 0;
        var maxTryCount = 5;

        while (maxTryCount > currentTryCount)
        {
            try
            {
                Directory.Delete(path, true);
                Console.WriteLine($"Директория {path} удалена!");
                return;
            }
            catch (Exception)
            {
                currentTryCount++;
                Thread.Sleep(1000);
            }
        }
    }
}