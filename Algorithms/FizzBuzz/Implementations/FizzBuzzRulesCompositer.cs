using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.FizzBuzz.Interfaces;

namespace Algorithms.FizzBuzz.Implementations
{
    public class FizzBuzzRulesCompositer : IFizzBuzzRulesCompositer
    {
        List<IFizzBuzzRule> _rules = new List<IFizzBuzzRule>();

        public FizzBuzzRulesCompositer()
        {
            _rules = new List<IFizzBuzzRule>();
            IFizzBuzzRule fizzRule = new FizzRule(byte.MaxValue);
            IFizzBuzzRule buzzRule = new BuzzRule(byte.MaxValue - 1);
            _rules.Add(fizzRule);
            _rules.Add(buzzRule);
            _rules = _rules.OrderByDescending(o => o.Priority).ToList();
        }
        public string Compose(ulong element)
        {
            string result = "";
            foreach (var rule in _rules)
            {
                if (rule.Feet(element))
                {
                    result += rule.GetReplacement();
                }
            }

            result = result == "" ? element.ToString() : result;
            return result;
        }
    }
}
