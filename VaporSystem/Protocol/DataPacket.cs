namespace Protocol
{
    public class DataPacket
    {
        public Header Header { get; set; }
        public string Payload { get; set; }
        
        public DataPacket() { }
    }
}