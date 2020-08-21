using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo.Common
{
    public class CalculateService:ICalculateService
    {
        public int DoMathWork(int val)
        {
            var ret = 2 * val - 1;
            return ret;
        }
    }
}
