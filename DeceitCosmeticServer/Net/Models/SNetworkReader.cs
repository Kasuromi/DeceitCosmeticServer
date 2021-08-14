using DeceitCosmeticServer.Net.Interfaces;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace DeceitCosmeticServer.Net.Models {
    public class SNetworkReader {
        public SNetworkReader(byte[] data) {
            _data = data;
            _readerIndex = 0;
        }
        public byte[] ReadBytes(int length) {
            byte[] buf = new byte[length];
            Array.Copy(_data, _readerIndex, buf, 0, length);
            _readerIndex += length;
            return buf;
        }
        public byte ReadByte() {
            byte val = _data[_readerIndex];
            _readerIndex += Marshal.SizeOf<byte>();
            return val;
        }
        public unsafe bool ReadBool() {
            byte val = ReadByte();
            return *(bool*)&val;
        }
        public short ReadShort() => BitConverter.ToInt16(ReadBytes(Marshal.SizeOf<short>()).Reverse().ToArray(), 0);
        public int ReadInt() => BitConverter.ToInt32(ReadBytes(Marshal.SizeOf<int>()).Reverse().ToArray(), 0);
        public long ReadLong() => BitConverter.ToInt64(ReadBytes(Marshal.SizeOf<long>()).Reverse().ToArray(), 0);
        public T ReadDeserializable<T>() where T : INetworkDeserializable => (T)((T)FormatterServices.GetUninitializedObject(typeof(T))).Deserialize(this);
        public string ReadString() {
            short size = ReadShort();
            return Encoding.UTF8.GetString(ReadBytes(size));
        }
        private readonly byte[] _data = Array.Empty<byte>();
        private int _readerIndex = 0;
    }
}
