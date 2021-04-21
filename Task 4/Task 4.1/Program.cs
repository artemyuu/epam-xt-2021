using System;
using System.IO;
using System.Globalization;

namespace Task_4._1
{
    class Program
    {
        //args[0] - mode
        //args[1] - watched folder
        //args[2] - date
        static void Main(string[] args)
        {
            var dw = new DirWatcher(args[1]);
            if(args[0] == "-w"){
                dw.startWatch();
            }
            if(args[0] == "-b")
            {
                dw.Backup(args[2]);
            }
        }
    }

    class DirWatcher{
        private string backupDirPath;
        public string PathDirToWatch {get; private set;}
        public DirectoryInfo Dir {get; private set;}
        public DirWatcher(string pathDirToWatch){
            PathDirToWatch = pathDirToWatch;
            Dir = new DirectoryInfo(PathDirToWatch);
            backupDirPath = Dir.Parent.FullName + "backup";
        }

        public void startWatch(){
            using var watcher = new FileSystemWatcher(PathDirToWatch);

            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Changed += OnChanged;
            watcher.Error += OnError;

            watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public void Backup(string backupDate){
            Directory.Delete(Dir.FullName, true);
            Directory.CreateDirectory(Dir.FullName);
            CopyDir(backupDirPath + "\\" + backupDate, Dir.FullName);
        }

        private void OnChanged(object sender, FileSystemEventArgs e){
            
            //get Ru date type format
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.TimeSeparator = ".";
            //
            if(!Directory.Exists(backupDirPath)){
                Directory.CreateDirectory(backupDirPath);
            }
            CopyDir(PathDirToWatch, backupDirPath + "\\" +  DateTime.Now.ToString("G", dtfi));
        }

        private void CopyDir(string srcDirPath, string destDirPath){
            DirectoryInfo dir = new DirectoryInfo(srcDirPath);
            DirectoryInfo[] dirs = dir.GetDirectories();    
            if(!Directory.Exists(destDirPath)){
                Directory.CreateDirectory(destDirPath);  
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirPath, file.Name);
                file.CopyTo(tempPath, true);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirPath, subdir.Name);
                CopyDir(subdir.FullName, tempPath);
            }
        }

        private void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private void PrintException(Exception ex){
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}
