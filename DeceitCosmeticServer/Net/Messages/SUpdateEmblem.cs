using DeceitCosmeticServer.Net.Interfaces;
using DeceitCosmeticServer.Net.Models;

namespace DeceitCosmeticServer.Net.Messages {
    public class SUpdateEmblem : SMessage {
        public override MessageId MessageId => MessageId.SUpdateEmblem;
        public short EmblemId;

        public override long CalculateSize() =>
            base.CalculateSize() + 2;

        public override INetworkDeserializable Deserialize(SNetworkReader reader) {
            base.Deserialize(reader);
            EmblemId = reader.ReadShort();
            return this;
        }

        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteShort(EmblemId);
            return _writer.GetData();
        }
    }
}
