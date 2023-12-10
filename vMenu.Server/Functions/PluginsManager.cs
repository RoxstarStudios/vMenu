using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

using static CitizenFX.Core.Native.API;

using CitizenFX.Core;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading;

namespace vMenu.Server.Functions
{
    public class PluginsManager : BaseScript
    {
        public PortableExecutableReference[] References { get; set; }
        public PluginsManager()
        {
            Debug.WriteLine("Initializing Plugins");

            string PluginsDirectory = $"{GetResourcePath(GetCurrentResourceName())}/Plugins";

            if (!Directory.Exists(PluginsDirectory))
            {
                Directory.CreateDirectory(PluginsDirectory);
                Debug.WriteLine("Plugins Folder did not exist. Created one for you!");
            }

            var PluginFolders = Directory.EnumerateDirectories(PluginsDirectory);

            foreach (string PluginFolderLoc in PluginFolders)
            {
                string PluginFolderName = PluginFolderLoc.Substring(PluginFolderLoc.LastIndexOf(Path.DirectorySeparatorChar) + 1);

                if (!File.Exists($"{PluginFolderLoc}/plugin.ini"))
                {
                    Debug.WriteLine($"/Plugins/{PluginFolderName}/ did not have a \"/plugin.ini\" file. Skipping...");
                    continue;
                }

                if (!Directory.Exists($"{PluginFolderLoc}/Client"))
                {
                    Debug.WriteLine($"/Plugins/{PluginFolderName}/ did not have a \"/Client\" folder. Skipping...");
                    continue;
                }

                if (!File.Exists($"{PluginFolderLoc}/Client/Plugin.cs"))
                {
                    Debug.WriteLine($"/Plugins/{PluginFolderName}/ did not have a \"/Client/Plugin.cs\" file. Skipping...");
                    continue;
                }

                if (!Directory.Exists($"{PluginFolderLoc}/Server"))
                {
                    Debug.WriteLine($"/Plugins/{PluginFolderName}/ did not have a \"/Server\" folder. Skipping...");
                    continue;
                }

                if (!File.Exists($"{PluginFolderLoc}/Server/Plugin.cs"))
                {
                    Debug.WriteLine($"/Plugins/{PluginFolderName}/ did not have a \"/Server/Plugin.cs\" file. Skipping...");
                    continue;
                }

                Debug.WriteLine($"/Plugins/{PluginFolderName}/ has been found! Initializing Plugin...");

                InitializePlugin(PluginFolderName, PluginFolderLoc);
            }
        }

        public void InitializePlugin(string PluginFolderName, string PluginFolderLoc)
        {
            string PluginServerFileContent = File.ReadAllText($"{PluginFolderLoc}/Server/Plugin.cs");

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 1");

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(PluginServerFileContent, new CSharpParseOptions(LanguageVersion.CSharp8));

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 2");

            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 3");

            var compilation = CSharpCompilation.Create("vMenu.Plugins.Server.Plugin", [syntaxTree])
                .AddReferences(MetadataReference.CreateFromFile(typeof(string).Assembly.Location))
                .AddSyntaxTrees(syntaxTree);

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 4");

            SemanticModel model = compilation.GetSemanticModel(syntaxTree);

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 5");

            UsingDirectiveSyntax usingSystem = root.Usings[0];

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 6");

            NameSyntax systemName = usingSystem.Name;

            Debug.WriteLine($"/Plugins/{PluginFolderName}/ Debug 7 - Finish");
        }
    }
}
