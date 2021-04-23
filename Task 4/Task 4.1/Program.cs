using System;
using System.IO;
using System.Globalization;
using System.Threading;

namespace Task_4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter directory");
            var userDir = Console.ReadLine();
            var dw = new DirWatcher(userDir);
            Console.WriteLine("Enter mode:");
            Console.WriteLine("1: watching");
            Console.WriteLine("2: backup");
            int userMode;
            try {
                while(true){
                    if(Int32.TryParse(Console.ReadLine(), out userMode)){
                        switch(userMode){
                            case 1: 
                                Console.WriteLine("Start watcing");
                                dw.StartWatch();
                                break;
                            case 2:
                                string[] backupFolderName = dw.BackupDirFoldersName();
                                foreach (var folder in backupFolderName){
                                    Console.WriteLine(folder);
                                }
                                string dateToBackup = Console.ReadLine();
                                dw.Backup(dateToBackup);
                                Console.WriteLine("Backup complete");
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                    }
                    else Console.WriteLine("Invalid option");
                }
            }
            catch(Exception e) {Console.WriteLine(e.Message);}
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

        public void StartWatch(){
            using var watcher = new FileSystemWatcher(PathDirToWatch);

            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Changed += OnChanged;
            watcher.Error += OnError;

            watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            while(true){
                Thread.Sleep(1);
            }
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
                var di = Directory.CreateDirectory(backupDirPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            CopyDir(PathDirToWatch, backupDirPath + "\\" +  DateTime.Now.ToString("G", dtfi));
        }

        public string[] BackupDirFoldersName() {
            DirectoryInfo[] foldersInfo = new DirectoryInfo(backupDirPath).GetDirectories();
            string[] preparedNames = new string[foldersInfo.Length];
            for(int i = 0; i < foldersInfo.Length; i++){
                preparedNames[i] = foldersInfo[i].ToString().Remove(0, backupDirPath.Length + 1);
            }
            return preparedNames;
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

        private void OnError(object sender, ErrorEventArgs e) => throw e.GetException();
    }
}
