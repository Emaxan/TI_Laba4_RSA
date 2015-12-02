using System.IO;

namespace TI_Laba4
{
    class Controller
    {
        public static void SetEvents(Model.PropertyChanged p, Model.PropertyChanged q, Model.PropertyChanged r,
            Model.PropertyChanged ko, Model.PropertyChanged kc)
        {
            Model.PChanged += p;
            Model.QChanged += q;
            Model.RChanged += r;
            Model.KoChanged += ko;
            Model.KcChanged += kc;
        }

        public static int InitializeModel(string path, params string[] argv)
        {
            if (!Equals(path, string.Empty) && new FileInfo(path).Exists) SetPath(path);
            else return 3;
            byte res = 0;
            for (var i = 0; i < argv.Length; i += 2)
            {
                switch (argv[i].ToLower())
                {
                    case "p":
                        ulong p;
                        if (ulong.TryParse(argv[i + 1], out p))
                        {
                            SetP(p);
                            res |= 1;
                        }
                        break;
                    case "q":
                        ulong q;
                        if (ulong.TryParse(argv[i + 1], out q))
                        {
                            SetQ(q);
                            res |= 2;
                        }
                        break;
                    case "r":
                        ulong r;
                        if (ulong.TryParse(argv[i + 1], out r))
                        {
                            SetR(r);
                            res |= 4;
                        }
                        break;
                    case "ko":
                        ulong ko;
                        if (ulong.TryParse(argv[i + 1], out ko))
                        {
                            SetKo(ko);
                            res |= 8;
                        }
                        break;
                    case "kc":
                        ulong kc;
                        if (ulong.TryParse(argv[i + 1], out kc))
                        {
                            SetKc(kc);
                            res |= 16;
                        }
                        break;
                    case "state":
                        int state;
                        if (int.TryParse(argv[i + 1], out state))
                            SetState((State)state);
                        break;
                }
            }
            ulong x1, x2;
            switch (Model.State)
            {
                case State.Coding:
                    if ((res & 19) == 19)
                    {
                        if (GetP()*GetQ() < 256 || GetP()*GetQ() > 65537) return 2;
                        if (!Processor.IsSimple(GetP())) return 7;
                        if (!Processor.IsSimple(GetQ())) return 8;
                        //if (Processor.Gcdex(GetP()*GetQ(), GetKc(), out x1, out x2) != 1) return 9;
                        return 0;
                    }
                    return 1;
                case State.Decoding:
                    if ((res & 20) == 20)
                    {
                        if (GetR() < 256 || GetR() > 65537) return 6;
                        //if (Processor.Gcdex(GetR(), GetKc(), out x1, out x2) != 1) return 9;
                        return 0;
                    }
                    return 4;
                case State.Hack:
                    if ((res & 12) == 12)
                    {
                        if (GetR() < 256 || GetR() > 65537) return 6;
                        //if (Processor.Gcdex(GetR(), GetKo(), out x1, out x2) != 1) return 10;
                        return 0;
                    }
                    return 5;
                default:
                    return 100;
            }
        }

        public static ulong GetKo()
        {
            return Model.Ko;
        }
        public static void SetKo(ulong ko)
        {
            Model.Ko = ko;
        }

        public static ulong GetKc()
        {
            return Model.Kc;
        }
        public static void SetKc(ulong kc)
        {
            Model.Kc = kc;
        }

        public static ulong GetP()
        {
            return Model.P;
        }
        public static void SetP(ulong p)
        {
            Model.P = p;
        }

        public static ulong GetQ()
        {
            return Model.Q;
        }
        public static void SetQ(ulong q)
        {
            Model.Q = q;
        }

        public static ulong GetR()
        {
            return Model.R;
        }
        public static void SetR(ulong r)
        {
            Model.R = r;
        }

        public static State GetState()
        {
            return Model.State;
        }
        public static void SetState(State state)
        {
            Model.State = state;
        }

        public static string GetPath()
        {
            return Model.FilePath;
        }
        public static void SetPath(string path)
        {
            Model.FilePath = path;
        }

        public static string ReturnResult()
        {
            string result;
            switch (GetState())
            {
                case State.Coding:
                    result = Coding.Work();
                    break;
                case State.Decoding:
                    result = Decoding.Work();
                    break;
                case State.Hack:
                    result = Hack.Work();
                    break;
                default:
                    result = "Error! Incorrect data";
                    break;
            }
            return result;
        }
    }
}
