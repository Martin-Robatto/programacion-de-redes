using Exceptions;
using System.IO;
using System.Threading.Tasks;

namespace FileLogic
{
    public class FileStreamManager
    {
        public FileStreamManager() { }
        
        public async Task WriteAsync(string fileName, byte[] data)
        {
            if (File.Exists(fileName))
            {
                await using (var fileStream = new FileStream(fileName, FileMode.Append))
                {
                    await fileStream.WriteAsync(data, 0, data.Length);
                }
            }
            else
            {
                await using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await fileStream.WriteAsync(data, 0, data.Length);
                }
            }
        }

        public async Task<byte[]> ReadAsync(string path, long offset, int length)
        {
            var data = new byte[length];

            await using (var fileStream = new FileStream(path, FileMode.Open))
            {
                fileStream.Position = offset;
                var bytesRead = 0;
                while (bytesRead < length)
                {
                    var read = await fileStream.ReadAsync(data, bytesRead, length - bytesRead);
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