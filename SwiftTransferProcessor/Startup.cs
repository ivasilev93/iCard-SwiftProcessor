using SwiftTransferProcessor.Common;
using System;
using System.IO;

namespace SwiftTransferProcessor
{
    public class Startup
    {
        private static FileProcessor fileProcessor;
        
        static void Main(string[] args)
        {
            fileProcessor = new FileProcessor();

            using (FileSystemWatcher watcher = new FileSystemWatcher(Paths.ReadingPath, FileType.TextFiles))
            {
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += OnChanged;
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;

                if(fileProcessor != null)
                {
                    fileProcessor.Dispose();
                }
            }
        }

        // Im using onchanged event, because i dont know how long the programs that writes the file takes,
        //and i want to read it only when its done writing
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                fileProcessor.EnqueueFilePath(e.FullPath);
            }
        }

    }

}
