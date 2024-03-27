using abuseloader.handler;
using abuseloader.handler.console;
using abuseloader.handler.program;
using Salaros.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abuseloader
{
    internal class consoleWindow
    {
        public static StringWriter writer = new StringWriter();

        static void Main(string[] args)
        {
            discord.startPresence();
            Console.Title = $"@buse loader | V{variables.niceVersion}";
            consoleManager.moveWindowToCenter();
            fontChecker.checkFont("gamesense");
            discord.updatePresence("@buse loader", "Clearing old files", "abuse", "abuse");
            downloadManager.clearMinecraft();
            downloadManager.clearCs();
            //Console.SetOut(writer);

            consoleWriter.writeLogo();
            discord.updatePresence("@buse loader", "Checking api", "abuse", "abuse");
            Console.WriteLine("");
            Console.WriteLine("");
            // removed

            if (fstream.isPackaged())
            {
                consoleManager.centerText("Please unzip @buse");
                consoleManager.keepOpen();
                Environment.Exit(0);
            }
            discord.updatePresence("@buse loader", "Checking folder structure", "abuse", "abuse");
            if (!fstream.folderExists(Application.StartupPath + "/programs"))
            {
                fstream.createFolder(Application.StartupPath + "/programs");
            }
            if (!fstream.folderExists(Application.StartupPath + "/programs/counterstrike"))
            {
                fstream.createFolder(Application.StartupPath + "/programs/counterstrike");
            }
            if (!fstream.folderExists(Application.StartupPath + "/programs/minecraft"))
            {
                fstream.createFolder(Application.StartupPath + "/programs/minecraft");
            }
            discord.updatePresence("@buse loader", "Checking api key", "abuse", "abuse");
            if (fstream.fileExists(variables.downloadSettings))
            {
                ConfigParser cfg = new ConfigParser(variables.downloadSettings);
                variables.downloadKey = cfg.GetValue("loader", "key");
                variables.keylink = $"link/key/{variables.downloadKey}";
                // removed
                writeOptions();
            }
            else
            {
                consoleManager.centerText("Please add downloadkey.cfg");
                consoleManager.keepOpen();
                Environment.Exit(0);
            }
        }

        public static void writeOptions()
        {
            discord.updatePresence("@buse loader", "Idle", "abuse", "abuse");
            Console.Clear();
            consoleWriter.writeLogo();
            Console.WriteLine("");
            Console.WriteLine("");
            consoleWriter.optionsWriter("Minecraft");
            consoleWriter.optionsWriter("Counterstrike");
            var option = Console.ReadLine();
            if (option == "1")
            {
                Console.Clear();
                consoleWriter.writeLogo();
                Console.WriteLine("");
                Console.WriteLine("");
                consoleManager.centerText("Contacting @buse server");
                downloadManager.getMcReady();
                //Console.ReadLine();
            }
            if (option == "2")
            {
                Console.Clear();
                consoleWriter.writeLogo();
                Console.WriteLine("");
                Console.WriteLine("");
                consoleManager.centerText("Contacting @buse server");
                if (Process.GetProcessesByName("@buse cs2").Length == 0)
                {
                    DialogResult result = MessageBox.Show("Auto start counterstrike?", "@buse", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes) { downloadManager.getCsReady(true); }
                    if (result == DialogResult.No) { downloadManager.getCsReady(false); }
                }
                else
                {
                    downloadManager.getCsReady(false);
                }
                //Console.ReadLine();
            }
            if (option != "1" || option != "2")
            {
                consoleWriter.resetOptions();
                writeOptions();
            }
        }
    }
}
