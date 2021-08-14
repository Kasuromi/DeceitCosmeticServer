using DeceitCosmeticServer.Net.Models;
using System.Text;

namespace DeceitCosmeticServer.Net.Messages {
    public class SAuthenticateOAuthClient : SMessage {
        public override MessageId MessageId => MessageId.SAuthenticateOAuthClient;
        public long GameVersion;
        public string Username;

        public override long CalculateSize() =>
            base.CalculateSize() +
            2 + 0 + 2 + 0 + 8 +
            2 + Encoding.UTF8.GetByteCount(Username) +
            10 + 16 + 16;

        public override SMessage Deserialize(SNetworkReader reader) {
            base.Deserialize(reader);
            reader.ReadString();
            reader.ReadString();
            GameVersion = reader.ReadLong();
            Username = reader.ReadString();
            reader.ReadBytes(10);
            reader.ReadBytes(16);
            reader.ReadBytes(16);
            return this;
        }
    }
}
