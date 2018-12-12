using System.ComponentModel;
using System.ServiceProcess;

namespace FileWatcherService
{
    [RunInstaller(true)]
    public partial class FileWatcherServiceInstaller : System.Configuration.Install.Installer
    {
        public FileWatcherServiceInstaller()
        {
            InitializeComponent();
            var serviceInstaller = new ServiceInstaller();
            var processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "FileWatcherService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
