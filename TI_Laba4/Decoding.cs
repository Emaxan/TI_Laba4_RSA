using System.IO;

namespace TI_Laba4
{
    class Decoding
    {
        public static string Work()
        {
            return DeCrypting();
        }

        private static string DeCrypting()
        {
            var res = string.Empty;
            var file = new FileStream(Controller.GetPath(), FileMode.Open, FileAccess.Read, FileShare.None, 32, FileOptions.SequentialScan);
            var reader = new BinaryReader(file);
            var writer = new BinaryWriter(new FileStream(Processor.GetNewName(Controller.GetPath()), FileMode.Create, FileAccess.Write,
                FileShare.None, 32, FileOptions.WriteThrough));
            try
            {
                var pos = 0;
                while (pos < file.Length)
                {
                    var c = reader.ReadUInt16();
                    //var pow = Processor.fast_exp(c, Controller.GetKc(), Controller.GetR());
                    var pow = Processor.Pows(c, Controller.GetKc(), Controller.GetR());
                    if (pos < 256) res += pow + " ";
                    writer.Write((byte)(pow));
                    pos += 2;
                }
            }
            finally
            {
                writer.Close();
                reader.Close();
            }
            return "Decoding Complete!\n" + res;
        }
    }
}
