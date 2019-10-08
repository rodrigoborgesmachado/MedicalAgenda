using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Agenda_de_Saude.Util
{
    /// <summary>
    /// Class to implement file's configuration 
    /// </summary>
    class CL_Files
    {
        #region Atributes
        StreamWriter writeFile = null;
        /// <summary>
        /// FileManager openned for writtring
        /// </summary>
        public StreamWriter WriteFile
        {
            get
            {
                return this.writeFile;
            }
        }

        /// <summary>
        /// FileManager opened for reading
        /// </summary>
        StreamReader fileRead = null;

        public StreamReader FileRead
        {
            get
            {
                return fileRead;
            }
        }

        string filesdir;
        /// <summary>
        /// Caminho do arquivo
        /// </summary>
        public string FilesDir
        {
            get
            {
                return filesdir;
            }
        }

        public static string camDirBase = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal, System.Environment.SpecialFolderOption.Create);

        /// <summary>
        /// Directory off logs
        /// </summary>
        public static string camDirLog = camDirBase + "Log/";

        /// <summary>
        /// Directory of basic files from tests
        /// </summary>
        public static string camDirArqs = camDirBase + "Files/";

        /// <summary>
        /// Directory from database
        /// </summary>
        public static string camDirBD = camDirArqs + "Base/";
        public static string camArqBD = camDirBD + "pckdb.bd3";

        #endregion Atributes

        #region Construtores

        public CL_Files(string filedir)
        {
            this.filesdir = filedir;
        }

        #endregion Construtores

        #region Methods

        /// <summary>
        /// Method how delete files
        /// </summary>
        /// <param name="directory">Directory from the archive that will be delete</param>
        /// <returns>True - Sucess; False - Error</returns>
        public static bool DeleteArchive(string directory)
        {
            if (Exists(directory))
            {
                try
                {
                    File.Delete(directory);
                }
                catch
                {
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Method that write on the end of the file and put a line on the end
        /// </summary>
        /// <param name="messagem">Message to be writen</param>
        public void WriteOnTheEndWithLine(string messagem)
        {
            try
            {
                File.AppendAllText(FilesDir, messagem + "\n");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Method that write on the end of the file and don't put a line on the end
        /// </summary>
        /// <param name="messagem">Message to be writen</param>
        public void WriteOnTheEnd(string messagem)
        {
            try
            {
                File.AppendAllText(FilesDir, messagem);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Method main directories of the system
        /// </summary>
        public static void DropMainDiretories()
        {
            // verify if the directory exists, if is true delete that
            if (Directory.Exists(camDirLog))
                Directory.Delete(camDirLog, true);

            if (Directory.Exists(camDirBD))
                Directory.Delete(camDirBD, true);

            if (Directory.Exists(camDirArqs))
                Directory.Delete(camDirArqs, true);

        }

        /// <summary>
        /// Method that create the main directories
        /// </summary>
        public static void CreateMainDirectories()
        {
            // Create the logs's and reposts directoies
            Directory.CreateDirectory(camDirLog);

            // Create the file's directory
            Directory.CreateDirectory(camDirArqs);

            // Create database's directorie
            Directory.CreateDirectory(camDirBD);
        }

        /// <summary>
        /// Method how copie the file A to the directorie B
        /// </summary>
        /// <param name="from">File to be copied</param>
        /// <param name="to">File of destiny</param>
        /// <returns>True - Sucess; False - Fail</returns>
        public static bool CopyFile(string from, string to)
        {
            if (!Exists(from))
            {
                return false;
            }

            File.Copy(from, to, true);
            return true;
        }

        /// <summary>
        /// Method that replace the name of the file
        /// </summary>
        /// <param name="arq">File to be renamed</param>
        /// <param name="newname">New name of the file</param>
        /// <returns>True - Sucess; False- Fail</returns>
        public static bool Rename(string arq, string newname)
        {
            return CopyFile(arq, newname);
        }

        /// <summary>
        /// Method that read the archive and returns a List of string with witch line
        /// </summary>
        /// <param name="messageError">String by references that put some possible error during the reader</param>
        /// <returns>A list of strings os witch line in the archive</returns>
        public List<string> ReadArchive(out string messageError)
        {
            messageError = "";
            List<string> linhas = new List<string>();

            if (!Exists(FilesDir))
            {
                messageError = "FileManager não existe";
            }
            StreamReader reader = new StreamReader(filesdir);

            while (!reader.EndOfStream)
            {
                try
                {
                    string linha = reader.ReadLine();
                    linhas.Add(linha);
                }
                catch (Exception e)
                {
                    messageError = "Ocorreu um erro: " + e.Message;
                    return null;
                }

            }
            reader.Close();
            return linhas;
        }

        /// <summary>
        /// Method that verify the exists of the file
        /// </summary>
        /// <param name="Directory">directory of the file</param>
        /// <returns>True - Exists; False - Don't exists</returns>
        static public bool Exists(string Directory)
        {
            try
            {
                return System.IO.File.Exists(Directory);
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Method that write on the log
        /// </summary>
        /// <param name="message"></param>
        public static void WriteOnTheLog(string message)
        {
            string directory_ach = camDirLog;

            if (DateTime.Now.Day < 10)
            {
                directory_ach += "0" + DateTime.Now.Day;
            }
            else
            {
                directory_ach += DateTime.Now.Day;
            }

            if (DateTime.Now.Month < 10)
            {
                directory_ach += "0" + DateTime.Now.Month;
            }
            else
            {
                directory_ach += DateTime.Now.Month;
            }


            directory_ach += DateTime.Now.Year + ".log";

            CL_Files file = new CL_Files(directory_ach);
            file.WriteOnTheEnd(DateTime.Now.ToString() + "- " + message + "\n");
            file = null;
        }

        /// Method to generate a random text 
        /// </summary>
        /// <param name="size">Size of the text</param>
        /// <returns>Random Text</returns>
        public static string RandomText(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            System.Threading.Thread.Sleep(100);
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        #endregion Methods        
    }
}