using System;
using System.Collections.Generic;
using System.Text;

namespace httpClient
{
    class Output
    {
        public decimal SumResult { get; set; }
        public int MulResult { get; set; }
        public decimal[] SortedInputs { get; set; }
        public Output()
        {
            MulResult = 1;
            SumResult = 0;
            SortedInputs = new decimal[0];
        }
        public static bool equalsOutputs(Output first, Output second)
        {
            if (first.SumResult != second.SumResult) return false;
            if (first.MulResult != second.MulResult) return false;
            if (first.SortedInputs.Length != second.SortedInputs.Length) return false;
            for (int i = 0; i < first.SortedInputs.Length; i++)
            {
                if (first.SortedInputs[i] != second.SortedInputs[i]) return false;
            }
            return true;
        }
    }
}
