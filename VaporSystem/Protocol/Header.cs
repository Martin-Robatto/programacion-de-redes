using System;
using System.Text;

namespace Protocol
{
    public class Header
    {
        private byte[] _direction;
        private byte[] _command;
        private byte[] _dataLength;
        
        public string Direction { get; set; }
        public int Command { get; set; }
        public int DataLength { get; set; }
        
        public Header() { }

        public Header(byte[] buffer)
        {
            DecodeData(buffer);
        }

        public Header(string direction, int command, int dataLength)
        {
            _direction = Encoding.UTF8.GetBytes(direction);
            _command = Encoding.UTF8.GetBytes(command.ToString("D2"));
            _dataLength = Encoding.UTF8.GetBytes(dataLength.ToString("D4"));
        }

        public byte[] GetRequest()
        {
            var header = new byte[HeaderConstants.HEADER_LENGTH];
            Array.Copy(_direction, 0, header, 0, 3);
            Array.Copy(_command, 0, header, HeaderConstants.DIRECTION_LENGTH, 2);
            Array.Copy(_dataLength, 0, header, HeaderConstants.DIRECTION_LENGTH + HeaderConstants.COMMAND_LENGTH, 4);
            return header;
        }
        
        public void DecodeData(byte[] data)
        {
            Direction = Encoding.UTF8.GetString(data, 0, HeaderConstants.DIRECTION_LENGTH);
            Command = Int32.Parse(Encoding.UTF8.GetString(data, HeaderConstants.DIRECTION_LENGTH, HeaderConstants.COMMAND_LENGTH));
            DataLength = Int32.Parse(Encoding.UTF8.GetString(data, HeaderConstants.DIRECTION_LENGTH + HeaderConstants.COMMAND_LENGTH, HeaderConstants.DATA_LENGTH));
        }
    }
}