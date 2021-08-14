using DeceitCosmeticServer.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DeceitCosmeticServer.Net.Models {
    public class SUserProfileParam : INetworkSerializable, INetworkDeserializable {
        public string Name;
        private byte NativeType;
        public object Value;
        protected SUserProfileParam() {}
        public static SUserProfileParam New<T>(string name, T value) {
            if (!_managedToParamType.ContainsKey(typeof(T)))
                throw new Exception($"Cannot serialize param managed type {typeof(T).Name}");
            return new SUserProfileParam { 
                Name = name,
                NativeType = _managedToParamType[typeof(T)],
                Value = value
            };
        }
        public long CalculateSize() =>
            Marshal.SizeOf(Value.GetType()) + 1 + 2 + Encoding.UTF8.GetByteCount(Name);

        public byte[] Serialize() {
            _writer = new SNetworkWriter(CalculateSize());
            _writer.WriteString(Name);
            _writer.WriteByte(NativeType);
            switch(NativeType) {
                case 0:
                    _writer.WriteByte((byte)Value);
                    break;
                case 1:
                    _writer.WriteShort((short)Value);
                    break;
                case 2:
                    _writer.WriteInt((int)Value);
                    break;
                case 3:
                    if(Value is double val)
                        _writer.WriteDouble(val);
                    else
                        _writer.WriteLong((long)Value);
                    break;
                default:
                    throw new Exception("exhausted native types");
            }
            return _writer.GetData();
        }

        public unsafe INetworkDeserializable Deserialize(SNetworkReader reader) {
            Name = reader.ReadString();
            NativeType = reader.ReadByte();
            switch(NativeType) {
                case 0:
                    Value = reader.ReadByte();
                    break;
                case 1:
                    Value = reader.ReadShort();
                    break;
                case 2:
                    Value = reader.ReadInt();
                    break;
                case 3:
                    long val = reader.ReadLong();
                    if((val & 0x4000000000000000) == 0x4000000000000000) {
                        Value = *(double*)&val;
                    } else {
                        Value = val;
                    }
                    break;
                default:
                    throw new Exception("exhausted native types");
            }
            return this;
        }

        private SNetworkWriter _writer = null;
        private static readonly Dictionary<Type, byte> _managedToParamType = new() {
            { typeof(byte), 0 },
            { typeof(ushort), 1 },
            { typeof(short), 1 },
            { typeof(uint), 2 },
            { typeof(int), 2 },
            { typeof(ulong), 3 },
            { typeof(long), 3 },
            { typeof(double), 3 }
        };
    }
}
