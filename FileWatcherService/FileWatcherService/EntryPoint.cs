﻿using System.ServiceProcess;

namespace FileWatcherService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FileWatcher()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
