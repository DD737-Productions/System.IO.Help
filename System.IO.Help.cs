using System.IO;

namespace System.IO.Help // If you want to use this, you should include this
{

    public static class Help // Class designed for helping you with System.IO
    {

        public static class Opening // Sub class designed for opening files
        {

            public static FileStream OpenOrCreate(string path) // Opens this file or, if needed, creates and opens it
            {

                FileStream fs;

                string dire = Path.GetDirectoryName(path);

                if (!Directory.Exists(dire))
                    Creating.CreateDirectory(dire);

                fs = JustTryOpen(path);

                if (fs == null)
                    fs = Creating.CreateFile(path);

                return fs;

            }

            public static FileStream JustTryOpen(string path) // Just tries to open the file
            {

                FileStream fs = null;

                if (File.Exists(path))
                    fs = File.Open(path, FileMode.Open);

                return fs;

            }

        }

        public static class Creating // Sub class designed for creating things
        {

            public static DirectoryInfo CreateDirectory(string path) // Creates this directory
            {

                if (File.Exists(path))
                    path = Path.GetDirectoryName(path);

                return Directory.CreateDirectory(path);

            }

            public static FileStream CreateFile(string path) // Creates this file and, if needed, this directory
            {

                string dire = Path.GetDirectoryName(path);

                if (!Existing.Directory(dire))
                    CreateDirectory(dire);

                return File.Create(path);

            }

            public static void CreateFileWithoutStream(string path) // Creates this file but closes the returned System.IO.FileStream
            {

                string dire = Path.GetDirectoryName(path);

                if (!Directory.Exists(dire))
                    CreateDirectory(dire);

                FileStream fs = CreateFile(path);

                if (fs != null)
                    fs.Close();

            }

        }

        public static class Reading // Sub class designed for reading out of files
        {

            public static StreamReader GetReaderOf(string path) // Gets a System.IO.StreamReader for this file
            {
                FileStream fs = Opening.JustTryOpen(path);

                if (fs == null)
                    return null;

                return new StreamReader(fs);
            }

            public static string[] GetFileAsArray(string path) // Returns the file as a string[]
            {

                Queue<string> Array = new Queue<string>();

                StreamReader r = GetReaderOf(path);

                string line = "";

                while ((line = r.ReadLine()) != null)
                    Array.Enqueue(line);

                r.Close();

                return Array.ToArray();

            }

            public static string GetFileAsString(string path) // Returns the file as a string
            {

                string r = "";

                foreach (string s in GetFileAsArray(path))
                    r += s;

                return r;

            }

            public static string GetFileAsStringFormatted(string path) // Returns the file as a string, but every line ist seperatet by a "\n"
            {

                string r = "";

                string[] lines = GetFileAsArray(path);

                if (lines == null)
                    return null;

                for (int i = 0; i < lines.Length; i++)
                {
                    r += lines[i];

                    if (i < lines.Length - 1)
                        r += "\n";
                }

                return r;

            }

        }

        public static class Writing // Sub class designed for writing in files
        {

            public static StreamWriter GetWriterOf(string path) // Get a System.IO.StreamWriter for this file
            {

                FileStream fs = Opening.JustTryOpen(path);

                if (fs == null)
                    return null;

                return new StreamWriter(fs);

            }

            public static StreamWriter GetWriterOf(string path, bool append) // Same as System.IO.Help.Writing.GetWriterOf(string path) but you can specify wether you'd like to append or not
            {

                if (!File.Exists(path))
                    return null;

                return new StreamWriter(path, append);

            }

            public static void OverwriteFile(string path, string text) // Overwrites this file
            {

                StreamWriter w = GetWriterOf(path, false);

                w.Write(text);

                w.Close();

            }

            public static void AppendWritingInFile(string path, string text) // Appends the text to this file
            {

                StreamWriter w = GetWriterOf(path, true);

                w.Write(text);

                w.Close();

            }

        }

        public static class Existing //Sub class designed for checking if something exists
        {

            public static bool Directory (string path) // Does this directory exist?
            {

                if (File(path))
                    path = Path.GetDirectoryName(path);

                return IO.Directory.Exists(path);

            }

            public static bool File (string path) // Does this file exist? (NOTE: there must be an existing directory for a clear answer)
            {

                return File.Exists(path);

            }

        }

        public static class Pathing // Sub class using System.IO.Path
        {

            public static string UpperDirectory (string path) // The higher directory of the file / directory
            {

                return Path.GetDirectoryName(path);

            }

            public static string FileName (string path) // The name of the file in this path (NOTE: the extension is included)
            {

                if (!Existing.File(path))
                    return null;

                return Path.GetFileName(path);

            }

            public static string Extension (string path) // Just the extension of the file in this path
            {

                if (!Existing.File(path))
                    return null;

                return Path.GetExtension(path);

            }

            public static string FileNameWithoutExtension (string path) // Just the name of the file in this path
            {

                if (!Existing.File(path))
                    return null;

                return Path.GetFileNameWithoutExtension(path);

            }

            public static string FullPath (string relativePath) // The full path of this relative path
            {

                if (!Existing.File(relativePath))
                    return null;

                return Path.GetFullPath(relativePath);

            }

            public static string PathRoot (string path) // The very root of this path
            {

                if (!Existing.Directory(path))
                    return null;

                return Path.GetPathRoot(path);

            }

            public static string RandomFile () // A random file or directory on this system
            {

                return Path.GetRandomFileName();

            }

            public static string GetNewTemoraryFile () // A new temporary file
            {

                return Path.GetTempFileName();

            }

            public static string TemporaryDirectoty () // The temporary directory of this user (NOTE: this method is within Pathing because it uses System.IO.Path)
            {
                
                return Path.GetTempPath();

            }

        }
        
        public static class Info // Sub class for storing infos
        {

            public static string DesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); // Directory in which the users desktop is saved
            public static string WindowsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Windows); // Directory in which the users windows folder is saved
            public static string AdminToolsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.AdminTools); // Directory in which the users admin tools are saved

            public static string Pictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // Users pictures
            public static string Music = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic); // Users music
            public static string Documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Users documents
            public static string Videos = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos); // Users videos

            public static string DirectoryOfThisProgram = AppDomain.CurrentDomain.FriendlyName; // The directory in which this running program is saved

        }

    }

}
