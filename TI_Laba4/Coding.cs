using System.IO;

namespace TI_Laba4
{
    class Coding
    {
        public static string Work()
        {
            Controller.SetR(Controller.GetP() * Controller.GetQ());
            var i = Processor.EulerFunc(Controller.GetP(), Controller.GetQ());
            Controller.SetKo(Processor.MultInverse(Controller.GetKc(), i));
            return Crypting();
        }

        private static string Crypting()
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
                    var c = reader.ReadByte();
                    //var pow = Processor.fast_exp(c, Controller.GetKo(), Controller.GetR());
                    var pow = Processor.Pows(c, Controller.GetKo(), Controller.GetR());
                    if (pos < 256) res += pow + " ";
                    writer.Write((ushort)(pow));
                    pos++;
                }
            }
            finally
            {
                writer.Close();
                reader.Close();
            }
            return "Crypting Complete!\n" + res;
        }
    }
}
