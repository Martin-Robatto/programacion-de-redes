namespace Protocol
{
    public class HeaderConstants
    {
        public const string REQUEST = "REQ";
        public const string RESPONSE = "RES";
        public const int DIRECTION_LENGTH = 3;
        public const int COMMAND_LENGTH = 2;
        public const int DATA_LENGTH = 4;
        public const int HEADER_LENGTH = DIRECTION_LENGTH + COMMAND_LENGTH + DATA_LENGTH;
        public const int STATUS_CODE_LENGTH = 3;
        public const int FILE_NAME_LENGTH = 4;
        public const int FILE_SIZE_LENGTH = 8;
        public const int FILE_HEADER_LENGTH = FILE_NAME_LENGTH + FILE_SIZE_LENGTH;
        public const int MAX_PACKET_SIZE = 32768; 
    }
}