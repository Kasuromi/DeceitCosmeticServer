using DeceitCosmeticServer.Net.Objects;
using System;

namespace DeceitCosmeticServer.Net.Messages {
    public class SAuthenticateClientResult : SMessage {
        public override MessageId MessageId => MessageId.SAuthenticateClientResult;
        public SUserProfile UserProfile;

        public override long CalculateSize() =>
            base.CalculateSize() +
            1 + 8 + UserProfile.CalculateSize() +
            10 + 1 + 16 + 2 + 8;

        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteBool(false);
            _writer.WriteBytes(new byte[8]);
            _writer.WriteSerializable(UserProfile);
            _writer.WriteBytes(new byte[10]);
            _writer.WriteByte(1);
            _writer.WriteBytes(Guid.NewGuid().ToByteArray());
            _writer.WriteShort(0);
            _writer.WriteLong(0);
            return _writer.GetData();
        }

    }
}
