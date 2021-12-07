using System;
using System.Collections.Generic;
using System.Text;

namespace httpServer
{
    class Output:IComparable
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
        

        public int CompareTo(object obj)
        {
            Output second = obj as Output;
            int result = 1;
            if (this.MulResult != second.MulResult || this.SumResult != second.SumResult)
                result = -1;
            for (int i = 0; i < this.SortedInputs.Length; i++)
            {
                if (this.SortedInputs[i] != second.SortedInputs[i])
                    result = -1;
            }
            return result;

        }
    }
}
