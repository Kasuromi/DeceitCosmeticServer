using DeceitCosmeticServer.Net.Interfaces;
using DeceitCosmeticServer.Net.Models;
using System.Linq;

namespace DeceitCosmeticServer.Net.Messages {
    public class SSyncOnlineProfileAttributes : SMessage {
        public override MessageId MessageId => MessageId.SSyncOnlineProfileAttributes;
        public SUserProfileParam[] Attributes;

        public override long CalculateSize() =>
            base.CalculateSize() +
            Attributes.Select((x) => x.CalculateSize()).Sum();

        public override INetworkDeserializable Deserialize(SNetworkReader reader) {
            base.Deserialize(reader);
            Attributes = new SUserProfileParam[reader.ReadShort()];
            for (int i = 0; i < Attributes.Length; i++) {
                Attributes[i] = reader.ReadDeserializable<SUserProfileParam>();
            }
            return this;
        }
    }
}
