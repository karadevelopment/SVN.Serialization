using System.IO;
using System.IO.Compression;

namespace SVN.Serialization.Compression
{
    public static class Extensions
    {
        public static byte[] GZipCompress(this byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        inputStream.CopyTo(zipStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }

        public static byte[] GZipDecompress(this byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        zipStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }

        public static void AddFilesToZip(this FileStream stream, params (string name, byte[] data)[] files)
        {
            using (var zipStream = new ZipArchive(stream, ZipArchiveMode.Update))
            {
                foreach (var (name, data) in files)
                {
                    var entry = zipStream.GetEntry(name) ?? zipStream.CreateEntry(name);

                    using (var entryStream = entry.Open())
                    {
                        entryStream.Write(data, default(int), data.Length);
                    }
                }
            }
        }
    }
}