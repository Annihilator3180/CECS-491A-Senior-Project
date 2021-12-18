using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ControllerLayer
{
    public class ArchivingController
    {


        public bool Archive()
        {
            ArchivingManager AM = new ArchivingManager();
            return AM.Archive();
        }
    }
}
