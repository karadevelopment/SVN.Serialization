using ProtoBuf;
using System.IO;

namespace SVN.Serialization.Protobuf
{
    public static class Extensions
    {
        public static byte[] SerializeProtobuf<T>(this T instance)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, instance);
                return stream.ToArray();
            }
        }

        public static T DeserializeProtobuf<T>(this byte[] input)
        {
            using (var stream = new MemoryStream(input))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }
    }
}