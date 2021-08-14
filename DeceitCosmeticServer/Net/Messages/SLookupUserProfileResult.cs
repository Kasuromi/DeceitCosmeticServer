using DeceitCosmeticServer.Net.Objects;

namespace DeceitCosmeticServer.Net.Messages {
    public class SLookupUserProfileResult : SMessage {
        public override MessageId MessageId => MessageId.SLookupUserProfileResult;
        public SUserProfile UserProfile;

        public override long CalculateSize() =>
            base.CalculateSize() +
            UserProfile.CalculateSize();

        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteSerializable(UserProfile);
            return _writer.GetData();
        }
    }
}
