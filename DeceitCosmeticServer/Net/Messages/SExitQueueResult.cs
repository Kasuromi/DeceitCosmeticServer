namespace DeceitCosmeticServer.Net.Messages {
    public class SExitQueueResult : SMessage {
        public override MessageId MessageId => MessageId.SExitQueueResult;
        public bool Succeeded;

        public override long CalculateSize() =>
            base.CalculateSize() + 1;

        public override byte[] Serialize() {
            base.Serialize();
            _writer.WriteBool(Succeeded);
            return _writer.GetData();
        }
    }
}
