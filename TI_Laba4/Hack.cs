namespace TI_Laba4
{
    class Hack
    {
        public static string Work()
        {
            var result = string.Empty;
            Controller.SetKc(Processor.MultInverse(Controller.GetKo(), Processor.EulerFunc(Controller.GetR())));
            result = Decoding.Work();
            return result;
        }
    }
}
