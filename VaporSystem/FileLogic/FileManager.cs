using System;
using System.Collections.Generic;
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
        
        public static bool IsValidExtension(string path)
        {
            IList<string> validExtensions = new List<string>() {"PNG", "JPG", "JPEG"};
            string[] filePathAttributes = path.Split(".");
            string fileExtension = filePathAttributes[filePathAttributes.Length-1];
            return validExtensions.Contains(fileExtension.ToUpper());
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