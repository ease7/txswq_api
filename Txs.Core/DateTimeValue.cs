using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txs.Core
{
    public static class DateTimeValue
    {

        static DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static double ToSeconds(this DateTime time)
        {
            return ToMilliseconds(time) / 1000;
        }

        public static DateTime ToDateTime(this long milliseconds)
        {
            return StartTime.AddMilliseconds(milliseconds);
        }

        public static long ToMilliseconds(this DateTime time)
        {
            return (time.Ticks - StartTime.Ticks) / 10000;
        }
    }
}
