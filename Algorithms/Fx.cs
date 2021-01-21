using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Fx
    {
        /// <summary>
        /// If one of args is 0, func return 0
        /// </summary>
        /// <param name="f">First num</param>
        /// <param name="s">Second num</param>
        /// <returns>GCD for first and second num</returns>
        public static ulong GCD(ulong f, ulong s)
        {
            return Task.Run(() => gcd(f, s)).Result;

            static ulong gcd(ulong f, ulong s)
            {
                //Protector
                if (f == 0 || s == 0)
                    return 0;

                //Protector
                if (s > f)
                {
                    f = f + s;
                    s = f - s;
                    f = f - s;
                }

                ulong r = f % s;

                return r == 0 ? s : gcd(s, r);
            }
        }
    }
}
