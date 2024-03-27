// uses Tasks for the console animation in ./console/consoleProgress, normally there would only be two functions, minecraft and cs2 func
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using System;
using System.IO;
using abuseloader.handler.console;
using System.Threading;
using System.Threading.Tasks;

namespace abuseloader.handler.program
{
    internal class downloadManager
    {
        public static string programFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static string steamPath = Path.Combine(programFolder, "Steam", "steam.exe");

        public static void getCsReady(bool steam)
        {
            var task = startCounterStrike(steam);
            consoleProgress.consolethingy.ForTask(task);
        }

        public static void getMcReady()
        {
            var task = startMinecraft();
            consoleProgress.consolethingy.ForTask(task);
        }

        public static void clearMinecraft()
        {
            discord.updatePresence("@buse loader", "Clearing old minecraft files", "abuse", "abuse");
            fstream.deleteFile(Application.StartupPath + "/programs/minecraft/@buse.exe"); fstream.deleteFile(Application.StartupPath + "/programs/minecraft/minecraft.exe");
        }

        public static void clearCs()
        {
            discord.updatePresence("@buse loader", "Clearing old cs2 files", "abuse", "abuse");
            fstream.deleteFile(Application.StartupPath + "/programs/counterstrike/abuse.zip");
            fstream.deleteFile(Application.StartupPath + "/programs/counterstrike/@buse cs2.runtimeconfig.json");
            fstream.deleteFile(Application.StartupPath + "/programs/counterstrike/@buse cs2.deps.json");
            fstream.deleteFile(Application.StartupPath + "/programs/counterstrike/@buse cs2.exe");
            fstream.deleteFile(Application.StartupPath + "/programs/counterstrike/@buse cs2.dll");
            fstream.deleteFolder(Application.StartupPath + "/programs/counterstrike/runtimes");
        }

        public static Task<int> startCounterStrike(bool steam)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName("@buse cs2"))
                    {
                        process.Kill();
                    }
                    clearCs();
                    using (var client = new WebClient())
                    {
                        discord.updatePresence("@buse loader", "Downloading cs2 files", "abuse", "abuse");
                        client.DownloadFile($"link{variables.downloadKey}", Application.StartupPath + "/programs/counterstrike/abuse.zip");
                    }
                    discord.updatePresence("@buse loader", "Extracting cs2 files", "abuse", "abuse");
                    ZipFile.ExtractToDirectory(Application.StartupPath + "/programs/counterstrike/abuse.zip", Application.StartupPath + "/programs/counterstrike");
                    fstream.deleteFile(Application.StartupPath + "/programs/counterstrike/abuse.zip");
                    if (steam)
                    {
                        discord.updatePresence("@buse loader", "Launching counter strike", "abuse", "abuse");
                        if (fstream.folderExists("C:/Program Files (x86)/Steam"))
                        {
                            var pInfo = new ProcessStartInfo
                            {
                                FileName = steamPath,
                                Arguments = $"steam://rungameid/730"
                            };

                            var p = Process.Start(pInfo);
                            p.Start();
                        }
                    }
                    discord.updatePresence("@buse loader", "Launching cs2", "abuse", "abuse");
                    Process.Start(Application.StartupPath + "/programs/counterstrike/@buse cs2.exe");
                    discord.destroyPresence();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    fstream.writeLog(ex.Message, "Start Counter Strike");
                    MessageBox.Show("An error occured handling some files. @buse counterstrike api might be offline and @buse loader has to exit", "@buse", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                return 42;
            });
        }

        public static Task<int> startMinecraft()
        {
            return Task<int>.Factory.StartNew(() =>
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName("@buse"))
                    {
                        process.Kill();
                    }
                    clearMinecraft();
                    using (var client = new WebClient())
                    {
                        discord.updatePresence("@buse loader", "Downloading minecraft", "abuse", "abuse");
                        client.DownloadFile($"link/cdn/external", Application.StartupPath + "/programs/minecraft/@buse.exe");
                    }
                    discord.updatePresence("@buse loader", "Launching minecraft", "abuse", "abuse");
                    Process.Start(Application.StartupPath + "/programs/minecraft/@buse.exe");
                    discord.destroyPresence();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    fstream.writeLog(ex.Message, "Start Minecraft");
                    MessageBox.Show("An error occured handling some files. @buse counterstrike api might be offline and @buse loader has to exit", "@buse", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                return 42;
            });
        }

        public static Task<int> testTask()
        {
            return Task<int>.Factory.StartNew(() =>
            {
                startCounterStrike(false);
                return 42;
            });
        }
    }
}
