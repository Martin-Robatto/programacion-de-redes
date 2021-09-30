using Exceptions;
using System.IO;

namespace FileLogic
{
    public class FileStreamManager
    {
        public static void Write(string fileName, byte[] data)
        {
            if (File.Exists(fileName))
            {
                using (var fileStream = new FileStream(fileName, FileMode.Append))
                {
                    fileStream.Write(data, 0, data.Length);
                }
            }
            else
            {
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    fileStream.Write(data, 0, data.Length);
                }
            }
        }

        public static byte[] Read(string path, long offset, int length)
        {
            var data = new byte[length];

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                fileStream.Position = offset;
                var bytesRead = 0;
                while (bytesRead < length)
                {
                    var read = fileStream.Read(data, bytesRead, length - bytesRead);
                    if (read == 0)
                    {
                        throw new NotReadableFileException();
                    }
                    bytesRead += read;
                }
            }

            return data;
        }
    }
}