using DeceitCosmeticServer.Net.Objects;

namespace DeceitCosmeticServer.Net.Messages {
    public class SUpdateUserProfile : SMessage {
        public override MessageId MessageId => MessageId.SUpdateUserProfile;
        public SUserProfile UserProfile;
        public bool IsCurrentUser;

        public override long CalculateSize() =>
            base.CalculateSize() +
            UserProfile.CalculateSize() + 1;

        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteSerializable(UserProfile);
            _writer.WriteBool(IsCurrentUser);
            return _writer.GetData();
        }
    }
}
