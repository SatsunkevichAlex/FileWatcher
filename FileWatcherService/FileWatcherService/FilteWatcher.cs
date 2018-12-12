using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherService
{
    public partial class FilteWatcher : ServiceBase
    {
        public FilteWatcher()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }

    internal class Logger
    {
        private FileSystemWatcher watcher;
        private object obj = new object();
        private bool enabled = true;
        private const string FolderForWatching = "D:";

        public Logger()
        {
            watcher = new FileSystemWatcher(FolderForWatching);
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_Renamed(object sender, RenemedEventArgs e)
        {
            string fileEvent = "renamed into " + e.FilePath;
        }
    }
}
