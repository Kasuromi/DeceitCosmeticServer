using System.Text;

namespace DeceitCosmeticServer.Net.Messages {
    public class SEnterQueueResult : SMessage {
        public override MessageId MessageId => MessageId.SEnterQueueResult;
        public bool Succeeded;
        public bool IsRanked;
        public string Message;

        public override long CalculateSize() =>
            base.CalculateSize() +
            1 + 1 + 2 + Encoding.UTF8.GetByteCount(Message);

        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteBool(Succeeded);
            _writer.WriteBool(IsRanked);
            _writer.WriteString(Message);
            return _writer.GetData();
        }
    }
}
