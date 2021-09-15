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
    }
}