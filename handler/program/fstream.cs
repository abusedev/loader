using System;
using System.IO;

namespace abuseloader.handler.program
{
    internal class fstream
    {
        public static TextWriter writer;

        /// <param name="path">Path to folder</param>
        public static bool folderExists(string path)
        {

            bool exists = Directory.Exists(path);

            if (exists)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <param name="path">Path to file</param>
        public static bool fileExists(string path)
        {
            bool exists = File.Exists(path);

            if (exists)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <param name="path">Path to file</param>
        public static void deleteFile(string path)
        {
            if (fileExists(path))
            {
                File.Delete(path);
            }
        }

        public static void renameFile(string path, string newName)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.MoveTo(newName);
            }
        }

        /// <param name="path">Path to folder</param>
        public static void createFolder(string path)
        {
            if (!folderExists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <param name="path">Path to folder</param>
        public static void deleteFolder(string path)
        {
            if (folderExists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// Checks if the executable is packaged with winrar or zipped
        /// </summary>
        public static bool isPackaged()
        {
            if (Path.GetFileName(Path.GetDirectoryName(Environment.CurrentDirectory)) == "Temp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void checkAppData()
        {
            if (!folderExists(variables.appdata))
            {
                Directory.CreateDirectory(variables.appdata);
            }
        }

        /// <param name="exception">Exception message</param>
        /// <param name="function">Name of function that threw an error</param>
        public static void writeLog(string exception, string function)
        {
            checkAppData();

            writer = File.CreateText(variables.appdata + @"\loader.txt");
            writer.WriteLine("# @buse log");
            writer.WriteLine($"# Caught exception at: {DateTime.Now}");
            writer.WriteLine($"# @buse version: {variables.version}");
            writer.WriteLine();
            writer.WriteLine($"# Function: {function}");
            writer.WriteLine($"# Exception: {exception}");
            writer.Flush();
            writer.Close();
        }
    }
}
