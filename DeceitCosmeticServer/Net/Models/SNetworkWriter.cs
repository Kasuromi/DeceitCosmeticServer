using DeceitCosmeticServer.Net.Interfaces;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DeceitCosmeticServer.Net.Models {
    public class SNetworkWriter {
        public SNetworkWriter(long size) {
            _data = new byte[size];
            _writerIndex = 0;
        }
        public byte[] GetData() => _data.ToArray();

        public void WriteBytes(byte[] data) {
            for (int i = 0; i < data.Length; i++) {
                _data[_writerIndex] = data[i];
                _writerIndex++;
            }
        }
        public void WriteByte(byte val) {
            _data[_writerIndex] = val;
            _writerIndex += Marshal.SizeOf<byte>();
        }
        public unsafe void WriteBool(bool val) => WriteByte(*(byte*)&val);
        public void WriteShort(short val) => WriteBytes(BitConverter.GetBytes(val).Reverse().ToArray());
        public void WriteInt(int val) => WriteBytes(BitConverter.GetBytes(val).Reverse().ToArray());
        public void WriteLong(long val) => WriteBytes(BitConverter.GetBytes(val).Reverse().ToArray());
        public void WriteDouble(double val) => WriteBytes(BitConverter.GetBytes(val).Reverse().ToArray());
        public void WriteSerializable<T>(T val) where T : INetworkSerializable => WriteBytes(val.Serialize());
        public void WriteString(string val) {
            WriteShort((short)val.Length);
            WriteBytes(Encoding.UTF8.GetBytes(val));
        }
        private readonly byte[] _data;
        private int _writerIndex;
    }
}
