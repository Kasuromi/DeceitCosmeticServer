using DeceitCosmeticServer.Net.Interfaces;
using DeceitCosmeticServer.Net.Models;
using System.Diagnostics;

namespace DeceitCosmeticServer.Net.Messages {
    public class SMessage : INetworkSerializable, INetworkDeserializable {
        public virtual MessageId MessageId => 0;
        public ushort RequestId;

        public virtual long CalculateSize() => 3;

        public virtual INetworkDeserializable Deserialize(SNetworkReader reader) {
            MessageId messageId = (MessageId)reader.ReadByte();
            Debug.Assert(MessageId == messageId, $"Deserializing message of type {MessageId}, but found {messageId}");
            RequestId = (ushort)reader.ReadShort();
            return this;
        }

        public virtual byte[] Serialize() {
            _writer = new SNetworkWriter(CalculateSize());
            _writer.WriteByte((byte)MessageId);
            _writer.WriteShort((short)RequestId);
            return _writer.GetData();
        }
        protected SNetworkWriter _writer = null;
    }
}
