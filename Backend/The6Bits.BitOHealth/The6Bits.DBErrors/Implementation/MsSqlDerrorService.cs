using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.DBErrors
{
    public class MsSqlDerrorService : IDBErrors
    {
        public string DBErrorCheck(int ErrorNumber)
        {
            if (ErrorNumber == -2)
            {
                return "Database Time Out Error";
            }
            else if (ErrorNumber == 1105)
            {
                return "Database Full";
            }
            else if (ErrorNumber == 4060)
            {
                return "Database Offline";
            }
            else
            {
                return ErrorNumber.ToString()+"Database Other Error ";
            }
        }
    }
}
