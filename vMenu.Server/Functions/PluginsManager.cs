using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

using static CitizenFX.Core.Native.API;

using CitizenFX.Core;

using System.Linq;
using System.Threading;
using vMenu.Server.Services;

namespace vMenu.Server.Functions
{
    public class PluginsManager : BaseScript
    {
        public PluginsManager()
        {
            Debug.WriteLine("Initializing Plugins Manager");

            string PluginsDirectory = $"{GetResourcePath(GetCurrentResourceName())}/Plugins";

            if (!Directory.Exists(PluginsDirectory))
            {
                Directory.CreateDirectory(PluginsDirectory);
                LoggingService.Instance.Warning("Plugins Folder did not exist. Created one for you!");
            }

            var PluginFolders = Directory.EnumerateDirectories(PluginsDirectory);

            foreach (string PluginFolderLoc in PluginFolders)
            {
                string PluginFolderName = PluginFolderLoc.Substring(PluginFolderLoc.LastIndexOf(Path.DirectorySeparatorChar) + 1);

                if (!File.Exists($"{PluginFolderLoc}/plugin.ini"))
                {
                    LoggingService.Instance.Warning($"/Plugins/{PluginFolderName}/ did not have a \"/plugin.ini\" file. Skipping...");
                    continue;
                }

                if (!Directory.Exists($"{PluginFolderLoc}/Client"))
                {
                    LoggingService.Instance.Warning($"/Plugins/{PluginFolderName}/ did not have a \"/Client\" folder. Skipping...");
                    continue;
                }

                if (!File.Exists($"{PluginFolderLoc}/Client/Plugin.cs"))
                {
                    LoggingService.Instance.Warning($"/Plugins/{PluginFolderName}/ did not have a \"/Client/Plugin.cs\" file. Skipping...");
                    continue;
                }

                if (!Directory.Exists($"{PluginFolderLoc}/Server"))
                {
                    LoggingService.Instance.Warning($"/Plugins/{PluginFolderName}/ did not have a \"/Server\" folder. Skipping...");
                    continue;
                }

                if (!File.Exists($"{PluginFolderLoc}/Server/Plugin.cs"))
                {
                    LoggingService.Instance.Warning($"/Plugins/{PluginFolderName}/ did not have a \"/Server/Plugin.cs\" file. Skipping...");
                    continue;
                }

                LoggingService.Instance.Success($"/Plugins/{PluginFolderName}/ has been found! Initializing Plugin...");

                InitializePlugin(PluginFolderName, PluginFolderLoc);
            }
        }

        public void InitializePlugin(string PluginFolderName, string PluginFolderLoc)
        {
            string PluginServerFileContent = File.ReadAllText($"{PluginFolderLoc}/Server/Plugin.cs");

            LoggingService.Instance.Info($"/Plugins/{PluginFolderName}/ Debug 1");

            // Initialize Plugins Here

            LoggingService.Instance.Info($"/Plugins/{PluginFolderName}/ Debug 7 - Finish");
        }
    }
}
