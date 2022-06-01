using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Fibonacci
    {
        private static HashSet<(BigInteger? key, BigInteger? value)> storage = new HashSet<(BigInteger? key, BigInteger? value)>()
        {
            (0, 0),
            (1, 1)
        };

        public async static Task<BigInteger> Get(BigInteger fibonacciNumber)
        {
            var memo = storage.FirstOrDefault(f => f.key == fibonacciNumber);
            if (memo.key is null)
            {
                storage.Add((fibonacciNumber, await Get(fibonacciNumber - 1) + await Get(fibonacciNumber - 2)));
            }

            return storage.First(f => f.key == fibonacciNumber).value!.Value;
        }
    }
}
