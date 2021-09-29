namespace Protocol
{
    public class ResponseData
    {
        public int StatusCode { get; set; }
        public int Function { get; set; }
        public string Data { get; set; }

        public ResponseData()
        {
            Data = string.Empty;
        }
    }
}
