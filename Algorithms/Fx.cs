using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    /// <summary>
    /// Functional class
    /// </summary>
    public static class Fx
    {
        /// <summary>
        /// If one of args is 0, func return 0
        /// </summary>
        /// <param name="n">List of nums, which great then 0</param>
        /// <returns>GCD for list n</returns>
        public static ulong GCD(List<ulong> n)
        {
            return Task.Run(() => gcd(n)).Result;

            static ulong gcd(List<ulong> n)
            {
                //Core
                if (n.Count > 2)
                    n[1] = gcd(n.Where(w => w != n[0]).ToList());

                //Protector
                if (n[0] == 0 || n[1] == 0)
                    return 0;

                //Protector
                if (n[1] > n[0])
                {
                    n[0] = n[0] + n[1];
                    n[1] = n[0] - n[1];
                    n[0] = n[0] - n[1];
                }

                ulong r = n[0] % n[1];

                return r == 0 ? n[1] : gcd(new List<ulong>() { n[1], r });
            }
        }
    }
}
