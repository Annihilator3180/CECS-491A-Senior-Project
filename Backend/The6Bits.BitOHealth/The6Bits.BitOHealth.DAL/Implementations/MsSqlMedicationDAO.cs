﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL
{
    public class MsSqlMedicationDAO : IRepositoryMedication<string>
    {
        private string _connectString;
        public MsSqlMedicationDAO(string connectstring)
        {
            _connectString = connectstring;
        }
    }
}