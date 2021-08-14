using DeceitCosmeticServer.Net.Models;

namespace DeceitCosmeticServer.Net.Interfaces {
    public interface INetworkDeserializable {
        INetworkDeserializable Deserialize(SNetworkReader reader);
    }
}
