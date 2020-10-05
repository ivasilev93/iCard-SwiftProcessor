using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using SwiftTransferProcessor.Data.Models;
using System.Linq;
using SwiftTransferProcessor.Common;
using Data.SwiftTransferProcessor.Data;

namespace SwiftTransferProcessor
{
    public class FileProcessor : IDisposable
    {
        private EventWaitHandle eventWaitHandle = new AutoResetEvent(false);
        private Thread worker;
        private Queue<string> fileNamesQueue;
        private readonly object locker = new object();

        public FileProcessor()
        {
            this.fileNamesQueue = new Queue<string>();
            worker = new Thread(Work);
            worker.Start();
        }

        public void EnqueueFilePath(string filePath)
        {
            lock (locker)
            {
                fileNamesQueue.Enqueue(filePath);
            }

            eventWaitHandle.Set();
        }

        private void Work()
        {
            while (true)
            {
                string filePath = null;

                lock (locker)
                    if (fileNamesQueue.Count > 0)
                    {
                        filePath = fileNamesQueue.Dequeue();

                        if (filePath == null) return;
                    }

                if (filePath != null)
                {
                    ProcessFile(filePath);
                }
                else
                {
                    eventWaitHandle.WaitOne();
                }
            }
        }

        private void ProcessFile(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);

            // spit, format, ect...
            var transactions = fileContent.Split(new char[] { '\u0001', '\u0003' })
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x)).ToArray();

            string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1, filePath.LastIndexOf(".") - (filePath.LastIndexOf('\\') + 1));

            #region try parse data into objects...

            bool successfulParse = true;
            var fileRecord = new FileRecord();
            fileRecord.Name = fileName;
            fileRecord.TimeOfArrival = DateTime.Now;

            foreach (var tran in transactions)
            {
                try
                {
                    var transaction = TransferParser.ParseToTransferModel(tran);
                    fileRecord.Transactions.Add(transaction);
                }
                catch (ArgumentException exception)
                {
                    if (!Directory.Exists(Paths.FailedToParsePath))
                    {
                        Directory.CreateDirectory(Paths.FailedToParsePath);
                    }

                    successfulParse = false;
                    File.Move(filePath, Paths.FailedToParsePath + fileName + ".txt");
                    Console.WriteLine($"{DateTime.Now} - Failed to log File: {fileName}, because {exception.Message}! \n");

                    break;
                }
            }

            #endregion

            if (successfulParse)
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.EnsureCreated();
                    context.FileRecords.Add(fileRecord);
                    context.SaveChanges();
                }

                if (!Directory.Exists(Paths.SuccessfullyParsedPath))
                {
                    Directory.CreateDirectory(Paths.SuccessfullyParsedPath);
                }

                File.Move(filePath, Paths.SuccessfullyParsedPath + fileName + ".txt");
                Console.WriteLine($"{DateTime.Now} - Successfully logged File: {fileName}! \n");
            }
        }

        public void Dispose()
        {
            EnqueueFilePath(null);
            worker.Join();
            eventWaitHandle.Close();
        }
    }
}
