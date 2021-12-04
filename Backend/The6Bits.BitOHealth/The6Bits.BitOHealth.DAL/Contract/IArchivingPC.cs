﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IArchivingPC
    {
        bool Archive(IList<string> logs);

        bool Compress();
    }
}
