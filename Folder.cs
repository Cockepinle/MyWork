using System.Net.Http.Headers;
using System.Xml.Schema;

namespace ConsoleApp36;

/// <summary>
/// Статический класс для работы с файлами и директориями
/// </summary>
public static class Folder
{
    /// <summary>
    /// Выводим папки и файлы которые находятся внутри директории по пути, который передаем в качестве параметра
    /// </summary>
    /// <param name="path">Путь до директории которую нужно проанализировать и вывести ее файлы и директории</param>
    /// <returns></returns>
    public static PathInfo ShowFolderAndFiles(string path)
    {
        Console.Clear();
        string[] dirs = Array.Empty<string>();
        string[] files= Array.Empty<string>(); 
        try
        {
            dirs = Directory.GetDirectories(path);
            files = Directory.GetFiles(path);

            foreach (var dir in dirs)
            {
                var directoryInfo = new DirectoryInfo(dir);
                Console.WriteLine($"\t {directoryInfo.Name}, последнее изменение: {directoryInfo.LastWriteTime} - путь: {directoryInfo.FullName}");
            }

            foreach (var file in files)
            {
                if (dirs.Contains(file))continue;

                var fileInfo = new FileInfo(file);
                Console.WriteLine($"\t {fileInfo.Name}, {fileInfo.Length} байтов, последнее изменение: {fileInfo.LastWriteTime} - путь: {fileInfo.FullName}");
            }
        }
        catch (Exception e)
        {
            Console.Clear();
            Console.WriteLine($"\t {e.Message}");
        }

        var pathInfo = new PathInfo
        {
            Min = 0,
            Max = dirs.Length + files.Length - 1,
            Values = new List<string>()
        };
        pathInfo.Values.AddRange(dirs);
        pathInfo.Values.AddRange(files);
        pathInfo.Values.Distinct();

        WriteCommands();
        return pathInfo;
    }

    public static PathInfo ShowDrives()
    {
        Console.Clear();
        var dis = DriveInfo.GetDrives();
        var consoleRows = 0;

        try
        {
            Console.WriteLine("\t Выберите диск из имеющихся на ваших устройствах:");
            consoleRows++;
            Console.WriteLine("\t ------------------------------------------------");
            consoleRows++;

            foreach (var di in dis)
                Console.WriteLine($"\t Диск {di.Name} имеется в системе и его тип {di.DriveType}.");
        }
        catch (IOException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }

        var pathInfo = new PathInfo()
        {
            Min = consoleRows,
            Max = dis.Length + consoleRows,
            Values = dis.Select(item => item.Name).ToList()
        };

        WriteCommands();
        return pathInfo;
    }

    private static void WriteCommands()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("1 - добавить файл \t 2 - удалить файл \t 3 - добавить директорию \t 4 - удалить \t 5 - завершить");
    }

  
    public static PathInfo ShowInformation(PathInfo info)
    {
        var directory = Path.GetDirectoryName(info.Values[0]); // получаем имя директории
        var root = Path.GetPathRoot(info.Values[0]); // получаем имя корневой папки (диска)

        PathInfo pathInfo;

        // 
        if (directory != root)
        {
            pathInfo = Folder.ShowFolderAndFiles(directory);
            Arrow.SetMinAndMaxValues(pathInfo.Min, pathInfo.Max);
        }
        else
        {
            pathInfo = Folder.ShowDrives();
            Arrow.SetMinAndMaxValues(pathInfo.Min, pathInfo.Max - 1);
        }

        return pathInfo;
    }
}