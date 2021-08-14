namespace DeceitCosmeticServer.Net.Interfaces {
    public interface INetworkSerializable {
        long CalculateSize();
        byte[] Serialize();
    }
}
