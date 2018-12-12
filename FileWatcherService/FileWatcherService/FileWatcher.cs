using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace FileWatcherService
{
    public partial class FileWatcher : ServiceBase
    {
        private Logger _logger;
        public FileWatcher()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            _logger = new Logger();
            var loggerThread = new Thread(_logger.Start);
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            _logger.Stop();
            Thread.Sleep(millisecondsTimeout: 1000);
        }
    }

    internal class Logger
    {
        readonly FileSystemWatcher _watcher;
        private readonly object _obj = new object();
        private bool _enabled = true;
        private const string FolderForFileWatcher = "D:";

        public Logger()
        {
            _watcher = new FileSystemWatcher(FolderForFileWatcher);
            _watcher.Deleted += Watcher_Deleted;
            _watcher.Created += Watcher_Created;
            _watcher.Changed += Watcher_Changed;
            _watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (_enabled)
            {
                Thread.Sleep(millisecondsTimeout: 1000);
            }
        }
        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _enabled = false;
        }

        //Files renaming 
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            var fileEvent = "renamed into " + e.FullPath;
            var filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        //Files changing
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            const string fileEvent = "changes";
            var filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        //Files creating
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            const string fileEvent = "created";
            var filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        //Files creating
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            const string fileEvent = "deleted";
            var filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj)
            {
                using (var writer = new StreamWriter("D:\\templog.txt"))
                {
                    writer.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} file {filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }
    }
}
