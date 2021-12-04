using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.ControllerLayer
{
    class Class1
    {
        void CheckTime()
        {
            var t = new Timer(TimerCallback);
            DateTime utctime = DateTime.Now;
            DateTime nine = DateTime.Today.AddHours(21.0); 

            int ms = (int)((fourOClock - now).TotalMilliseconds);
            if (utctime == nine)
            {


            }
        }
    }
}
