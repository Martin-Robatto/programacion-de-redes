namespace Protocol
{
    public class DataPacket
    {
        public Header Header { get; set; }
        public string Payload { get; set; }
        public int StatusCode { get; set; }
        
        public DataPacket() { }
    }
}