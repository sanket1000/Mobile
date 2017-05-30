using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;

namespace Core.API.Helpers
{
    public class RowVersionComparer : IComparer<System.Data.Linq.Binary>
    {
       public int Compare(Binary x, Binary y)
        {
            long x1 = Convert.ToInt64(x.RowVersionToString());
            long y1 = Convert.ToInt64(y.RowVersionToString());

            return x1 > y1 ? 1 : -1;
        }
    }
}
