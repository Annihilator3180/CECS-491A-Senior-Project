﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer.Contracts;
using The6Bits.BitOHealth.ServiceLayer.Implementations;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class ArchivingManager
    {
        private IArchivingService _archivingService;

        public bool Archive()
        {
            _archivingService = new ArchivingService(new WindowsArchivingDAO(), new SqlArchivingDAO());
            return true;
        }
    }
}
