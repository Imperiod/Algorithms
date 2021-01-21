using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Fx
    {
        public static ulong GCD(ulong f, ulong s)
        {
            return Task.Run(() => gcd(f, s)).Result;

            static ulong gcd(ulong f, ulong s)
            {
                //Protector
                if (s > f)
                {
                    f += s;
                    s = f - s;
                    f -= s;
                }

                ulong r = f % s;

                return r == 0 ? s : gcd(s, r);
            }
        }
    }
}
