using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace httpServer
{
    class Calculator
    {
        public static Output calculate(Input input)
        {
            var output = new Output();
            foreach (var num in input.Sums)
            {
                output.SumResult += num;
            }
            output.SumResult *= input.K;
            output.MulResult = 1;
            foreach (var num in input.Muls)
            {
                output.MulResult *= num;
            }
            var unitedArray = new decimal[input.Muls.Length + input.Sums.Length];
            decimal[] decMuls = Array.ConvertAll(input.Muls, x => (decimal)x);
            input.Sums.CopyTo(unitedArray, 0);
            decMuls.CopyTo(unitedArray, input.Sums.Length);
            output.SortedInputs = unitedArray.OrderBy(i => i).ToArray();
            return output;

        }
    }
}
