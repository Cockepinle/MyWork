using System.Diagnostics;

namespace ConsoleApp36
{
    public static class Program
    {
        static void Main()
        {
            Console.Clear();
            var pathInfo = Folder.ShowDrives();
            Arrow.SetMinAndMaxValues(pathInfo.Min, pathInfo.Max - 1);

            var cashInfoAboutFiles = pathInfo;

            while (true)
            {
                var position = Arrow.Select(cashInfoAboutFiles);
              
                if (position == -1)
                {
                    Console.Clear();
                    Console.WriteLine("Работа программы завершена");
                    return;
                }
                   

                if (position == 1)
                {
                    cashInfoAboutFiles = pathInfo = Folder.ShowInformation(cashInfoAboutFiles);
                    continue;
                }


                string value = pathInfo.Values[position - pathInfo.Min];
                var fileAttr = File.GetAttributes(value);
                if (!fileAttr.HasFlag(FileAttributes.Directory))
                {
                    Process.Start(value);
                    continue;
                }


                cashInfoAboutFiles = pathInfo = Folder.ShowFolderAndFiles(value);
                Arrow.SetMinAndMaxValues(pathInfo.Min, pathInfo.Max);
            }
        }
        
    }
}