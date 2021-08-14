using DeceitCosmeticServer.Net.Objects;
using System;

namespace DeceitCosmeticServer.Net.Messages {
    public class SSyncParty : SMessage {
        public override MessageId MessageId => MessageId.SSyncParty;
        public Guid PartyGuid;
        public SUserProfile UserProfile;
        public override long CalculateSize() =>
            base.CalculateSize() + 16 + 2 + UserProfile.CalculateSize();
        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteBytes(PartyGuid.ToByteArray());
            _writer.WriteShort(1);
            _writer.WriteSerializable(UserProfile);
            return _writer.GetData();
        }
    }
}
