namespace TI_Laba4
{
    public enum State
    {
        Coding = 0,
        Decoding = 1,
        Hack = 2
    }

    internal class Model
    {
        public delegate void PropertyChanged(ulong q);

        public static event PropertyChanged QChanged;
        public static event PropertyChanged PChanged;
        public static event PropertyChanged RChanged;
        public static event PropertyChanged KcChanged;
        public static event PropertyChanged KoChanged;

        private static ulong _p, _q, _r, _kc, _ko;

        public static string FilePath { get; set; }
        public static State State { get; set; }

        public static ulong Q
        {
            get { return _q; }
            set
            {
                _q = value;
                if (QChanged != null)
                    QChanged(value);
            }
        }
        public static ulong R
        {
            get { return _r; }
            set
            {
                _r = value;
                if (RChanged != null)
                    RChanged(value);
            }
        }
        public static ulong Kc
        {
            get { return _kc; }
            set
            {
                _kc = value;
                if (KcChanged != null)
                    KcChanged(value);
            }
        }
        public static ulong Ko
        {
            get { return _ko; }
            set
            {
                _ko = value;
                if (KoChanged != null)
                    KoChanged(value);
            }
        }
        public static ulong P
        {
            get { return _p; }
            set
            {
                _p = value;
                if (PChanged != null)
                    PChanged(value);
            }
        }
    }
}
