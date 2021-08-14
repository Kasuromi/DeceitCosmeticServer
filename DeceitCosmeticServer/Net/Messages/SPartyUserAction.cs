using DeceitCosmeticServer.Net.Models.Enums;
using System;

namespace DeceitCosmeticServer.Net.Messages {
    public class SPartyUserAction : SMessage {
        public override MessageId MessageId => MessageId.SPartyUserAction;
        public Guid PartyGuid;
        public EPartyUserAction Action;
        public override long CalculateSize() =>
            base.CalculateSize() + 16 + 1 + 8 + 2;
        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteBytes(PartyGuid.ToByteArray());
            _writer.WriteByte((byte)Action);
            _writer.WriteLong(1);
            _writer.WriteShort(0);
            return _writer.GetData();
        }
    }
}
