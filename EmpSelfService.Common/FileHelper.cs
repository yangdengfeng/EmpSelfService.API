using System;
using System.Text;
using System.IO;

namespace EmpSelfService.Common
{
    public class FileHelper
    {
        private static object Locked = new object();

        public static string GetWebDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static bool FileExists(string filePathName)
        {
            return File.Exists(filePathName);
        }

        public static bool DirectoryExists(string filePath)
        {
            return Directory.Exists(filePath);
        }

        public static void CreatFilePath(string filePath)
        {
            lock (Locked)
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
            }
        }

        public static void FileDel(string filePathName)
        {
            if (File.Exists(filePathName))
                File.Delete(filePathName);
        }

        public static void DirectoryDel(string filePath)
        {
            if (Directory.Exists(filePath))
                Directory.Delete(filePath, true);
        }

        public static string GetFileContent(string fileName)
        {
            string fileContent;
            Encoding encoding = Encoding.UTF8;
            StreamReader sr = new StreamReader(fileName, encoding);
            fileContent = sr.ReadToEnd();
            sr.Close();
            return fileContent;
        }

        public static void WriteFileContent(string fileName, string fileContent)
        {
            lock (Locked)
            {
                Encoding encoding = Encoding.UTF8;
                StreamWriter sw = new StreamWriter(fileName, false, encoding);
                sw.Write(fileContent);
                sw.Flush();
                sw.Close();
            }
        }

        public static DirectoryInfo[] GetDirectory(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return dirInfo.GetDirectories();
        }

        public static FileInfo[] GetFileList(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return dirInfo.GetFiles();
        }

        public static int GetFileCount(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return dirInfo.GetFiles().Length;
        }

        public static string GetFileExtension(string file)
        {
            return Path.GetExtension(file).ToLower().Replace(".", "");
        }

        public static long GetFileSize(string file)
        {
            if (File.Exists(file))
            {
                FileInfo fi = new FileInfo(file);
                return fi.Length;
            }
            return 0;
        }

        public static void AppendAllText(string pathFile, string fileInfo)
        {
            lock (Locked)
            {
                File.AppendAllText(pathFile, fileInfo, Encoding.UTF8);
            }
        }

        public static DateTime GetFileCreatTime(string filePathName)
        {
            DateTime dt = DateTime.Now;
            if (File.Exists(filePathName))
                dt = File.GetCreationTime(filePathName);
            return dt;
        }

        public static DateTime GetFileModifyTime(string filePathName)
        {
            DateTime dt = DateTime.Now;
            if (File.Exists(filePathName))
                dt = File.GetLastWriteTime(filePathName);
            return dt;
        }

        public static void CreatFile(string filePatchName, string fileContent, string encoding = "utf-8")
        {
            if (File.Exists(filePatchName))
                File.Delete(filePatchName);
            FileStream myFs = new FileStream(filePatchName, FileMode.Create);
            StreamWriter mySw = new StreamWriter(myFs, Encoding.GetEncoding(encoding));
            mySw.Write(fileContent);
            mySw.Close();
            myFs.Close();   
        }

        /// <summary>
        ///  把一个文件夹中的内容复制到另一个文件夹
        /// </summary>
        /// <param name="sources">源路径</param>
        /// <param name="dest">新路径</param>
        public static void CopyFile(string sources, string dest)
        {
            DirectoryInfo dinfo = new DirectoryInfo(sources);
            //注，这里面传的是路径，并不是文件，所以不能保含带后缀的文件                
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                //目标路径destName = 目标文件夹路径 + 原文件夹下的子文件(或文件夹)名字                
                //Path.Combine(string a ,string b) 为合并两个字符串                     
                String destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    //如果是文件就复制       
                    File.Copy(f.FullName, destName, true);//true代表可以覆盖同名文件                     
                }
                else
                {
                    //如果是文件夹就创建文件夹然后复制然后递归复制              
                    Directory.CreateDirectory(destName);
                    CopyFile(f.FullName, destName);
                }
            }
        }
    }
}
