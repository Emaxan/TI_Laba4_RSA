using System;
using System.Collections.Generic;
using System.Numerics;

namespace TI_Laba4
{
    class Processor
    {
        private static readonly List<ulong> SimpleNumber = new List<ulong>();
        private static ulong _count = 2;

        public static ulong GetSimple()
        {
            var rand = new Random();
            var i = rand.Next(SimpleNumber.Count/5);
            return SimpleNumber[i];
        }

        public static ulong Gcdex(ulong a, ulong b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            BigInteger x1, y1;
            var d = Gcdex((b % a), a, out x1, out y1);
            x = (y1 - (b / a) * x1);
            y = x1;
            return d;
        }

        public static ulong Gcd(ulong a, ulong b)
        {
            while (true)
            {
                if (a == 0) return b;
                var a1 = a;
                a = (b%a);
                b = a1;
            }
        }

        public static ulong MultInverse(ulong a, ulong modulo)
        {
            BigInteger x, y;
            Gcdex(a, modulo, out x, out y);
            return (ulong)((x % modulo + modulo) % modulo);
        }

        public static void SimpleInit(ulong count)
        {
            for (var i = _count; i < count; i++)
            {
                SimpleNumber.Add(i);
            }

            for (var i = 0; i < SimpleNumber.Count; i++)
            {
                var cur = SimpleNumber[i];
                SimpleNumber.RemoveAll(elem => (elem % cur == 0 && elem != cur));
            }

            _count = count;
        }

        public static bool IsSimple(ulong number)
        {
            if (number == 0 || number == 1) return false;
            if (number < _count) return SimpleNumber.Contains(number);
            SimpleInit(number + 10);
            return SimpleNumber.Contains(number);
        }

        public static ulong EulerFunc(ulong x)
        {
            if (x == 1) return 1;
            if (IsSimple(x)) return x - 1;
            ulong i = 2;
            while (x % i != 0) i++;
            BigInteger x1, y1;
            var gcd = Gcdex(i, x / i, out x1, out y1);
            return (EulerFunc(i)*EulerFunc(x/i)*gcd/EulerFunc(gcd));
        }

        public static ulong EulerFunc(ulong p, ulong q)
        {
            return (p - 1)*(q - 1);
        }

        public static ulong fast_exp(ulong a, ulong z, ulong m)
        {
            var a1 = a;
            var z1 = z;
            ulong x = 1;
            while (z1!=0)
            {
                while (z1 % 2 == 0)
                {
                    z1 = z1/2;
                    a1 = (a1*a1)%m;
                } 
                z1--;
                x = (x*a1)% m;
            } 
            return x;
        }

        public static ulong Mul(ulong a, ulong b, ulong m)
        {
            if (b == 0 || a == 0)
                return 0;
            if (b == 1)
                return a;
            if (b % 2 == 0)
                return (2 * Mul(a, b / 2, m)) % m;
            return (Mul(a, b - 1, m) + a) % m;
        }

        public static ulong Pows(ulong a, ulong b, ulong m)
        {
            if (b == 0)
                return 1;
            if (b % 2 == 0)
            {
                var t = Pows(a, b / 2, m);
                return Mul(t, t, m) % m;
            }
            return (Mul(Pows(a, b - 1, m), a, m)) % m;
        }

        public static string GetNewName(string name)
        {
            return name.Insert(name.LastIndexOf('.'), "_");
        }
    }
}
