using System.Configuration;
using SlavaGu.ConsoleAppLauncher;

namespace AQA.helpers
{
    public class FileStarter
    {
        private string pathToExeFile;


        public FileStarter()
        {
            pathToExeFile = ConfigurationManager.AppSettings.Get("exeFile");
        }

        public string LaunchProcess(string parameters)
        {
            return ConsoleApp.Run(pathToExeFile,
                parameters).Output.Trim();
        }
    }
}