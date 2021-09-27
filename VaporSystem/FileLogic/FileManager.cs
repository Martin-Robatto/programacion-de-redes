using System;
using System.IO;
using Exceptions;
using Protocol;

namespace FileLogic
{
    public class FileManager
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static string GetFileName(string path)
        {
            if (!FileExists(path))
            {
                throw new NotFoundException("File");
            }
            return new FileInfo(path).Name;
        }

        public static long GetFileSize(string path)
        {
            if (!FileExists(path))
            {
                throw new NotFoundException("File");
            }
            return new FileInfo(path).Length;
        }
        
        public static long GetParts(long fileSize)
        {
            var parts = fileSize / HeaderConstants.MAX_PACKET_SIZE;
            return parts * HeaderConstants.MAX_PACKET_SIZE == fileSize ? parts : parts + 1;
        }
    }
}