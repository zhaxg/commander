using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Commander.Services
{
    public static class FileManager
    {
        public static bool ByteArrayToFile(string fileName, byte[] content)
        {
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(content, 0, content.Length);
                }
                return true;
            }
            catch (Exception o)
            {
                Console.WriteLine(o);
            }
            return false;
        }

        public static void UncompressFile(string fileName, byte[] content)
        {
            byte[] array = new byte[4096];
            using (FileStream fileStream = File.Create(fileName))
            {
                using (GZipStream gZipStream = new GZipStream(new MemoryStream(content), CompressionMode.Decompress, false))
                {
                    int count;
                    while ((count = gZipStream.Read(array, 0, array.Length)) > 0)
                    {
                        fileStream.Write(array, 0, count);
                    }
                }
            }
        }

        public static string NonExclusiveReadAllText(string path)
        {
            return FileManager.NonExclusiveReadAllText(path, Encoding.Default);
        }

        public static string NonExclusiveReadAllText(string path, Encoding encoding)
        {
            string result;
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, encoding))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            { 
                throw ex;
            }
            return result;
        }
    }
}
