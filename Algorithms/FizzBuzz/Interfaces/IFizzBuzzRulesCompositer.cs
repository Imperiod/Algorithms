using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.FizzBuzz.Interfaces
{
    public interface IFizzBuzzRulesCompositer
    {
        public string Compose(ulong element);
    }
}
