using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EIP.Common.Core.Utils
{
    /// <summary>
    ///     文件操作
    /// </summary>
    public static class FileUtil
    {
        #region 编码方式
        /// <summary>
        ///     编码方式
        /// </summary>
        private static readonly Encoding Encoding = Encoding.GetEncoding("utf-8");
        #endregion

        #region 递归取得文件夹下文件
        /// <summary>
        ///     递归取得文件夹下文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="list"></param>
        public static void GetFiles(string dir,
            List<string> list)
        {
            GetFiles(dir, list, new List<string>());
        }

        /// <summary>
        ///     递归取得文件夹下文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="list"></param>
        /// <param name="fileExtsions"></param>
        public static void GetFiles(string dir,
            List<string> list,
            List<string> fileExtsions)
        {
            //添加文件   
            var files = Directory.GetFiles(dir);
            if (fileExtsions.Count > 0)
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file);
                    if (extension != null && fileExtsions.Contains(extension))
                    {
                        list.Add(file);
                    }
                }
            }
            else
            {
                list.AddRange(files);
            }
            //如果是目录，则递归  
            var directories = new DirectoryInfo(dir).GetDirectories();
            foreach (var item in directories)
            {
                GetFiles(item.FullName, list, fileExtsions);
            }
        }
        #endregion

        #region 写入文件
        /// <summary>
        ///     写入文件
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(string filePath, 
            string content)
        {
            try
            {
                var directoryPath = GetDirectoryFromPath(filePath);
                if (!string.IsNullOrWhiteSpace(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                var fs = new FileStream(filePath, FileMode.Create);
                var encode = Encoding;
                //获得字节数组  
                var data = encode.GetBytes(content);
                //开始写入  
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流  
                fs.Flush();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 读取文件
        /// <summary>
        ///     读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            return ReadFile(filePath, Encoding);
        }

        /// <summary>
        ///     读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath,
            Encoding encoding)
        {
            if (!File.Exists(filePath)) return string.Empty;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, encoding))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        #endregion

        #region 复制文件夹
        /// <summary>
        ///     复制文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="destinationPath">目标路径</param>
        public static void CopyDirectory(String sourcePath, 
            String destinationPath)
        {
            var info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);
            foreach (var fsi in info.GetFileSystemInfos())
            {
                var destName = Path.Combine(destinationPath, fsi.Name);

                if (fsi is FileInfo) //如果是文件，复制文件  
                    File.Copy(fsi.FullName, destName);
                else //如果是文件夹，新建文件夹，递归  
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDirectoryFromPath(string path)
        {
            return path.Substring(0, path.LastIndexOf(@"\", StringComparison.Ordinal));
        }
        #endregion

        #region 删除文件夹
        /// <summary>
        ///     删除文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void DeleteFolder(string directoryPath)
        {
            foreach (var d in Directory.GetFileSystemEntries(directoryPath))
            {
                if (File.Exists(d))
                {
                    var fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d); //删除文件     
                }
                else
                    DeleteFolder(d); //删除文件夹  
            }
            Directory.Delete(directoryPath); //删除空文件夹  
        }
        #endregion

        #region 清空文件夹
        /// <summary>
        ///     清空文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void ClearFolder(string directoryPath)
        {
            foreach (var d in Directory.GetFileSystemEntries(directoryPath))
            {
                if (File.Exists(d))
                {
                    var fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d); //删除文件     
                }
                else
                    DeleteFolder(d); //删除文件夹  
            }
        }
        #endregion

        #region 取得文件大小，按适当单位转换
        /// <summary>
        ///     取得文件大小，按适当单位转换
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileSize(string filepath)
        {
            var result = "0KB";
            if (File.Exists(filepath))
            {
                var size = new FileInfo(filepath).Length;
                var filelength = size.ToString().Length;
                if (filelength < 4)
                    result = size + "byte";
                else if (filelength < 7)
                    result = Math.Round(Convert.ToDouble(size / 1024d), 2) + "KB";
                else if (filelength < 10)
                    result = Math.Round(Convert.ToDouble(size / 1024d / 1024), 2) + "MB";
                else if (filelength < 13)
                    result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024), 2) + "GB";
                else
                    result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024 / 1024), 2) + "TB";
                return result;
            }
            return result;
        }
        #endregion

        #region 文件转换成流
        /// <summary>
        ///     文件转换成流
        /// </summary>
        /// <param name="physicalPath"></param>
        /// <returns></returns>
        public static Stream GetStreamFrom(string physicalPath)
        {
            if (!File.Exists(physicalPath))
                return null;
            return new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(@path))
            {
                //如果存在则删除
                File.Delete(@path);
            }
        }
        #endregion
    }
}