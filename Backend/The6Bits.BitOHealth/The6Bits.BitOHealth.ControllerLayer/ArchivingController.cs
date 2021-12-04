using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.ServiceLayer.Contracts;

namespace The6Bits.BitOHealth.ControllerLayer
{
    public class ArchivingController
    {
        private readonly IArchivingService _service;

        public ArchivingController(IArchivingService service) {
            _service = service;
        }
        public bool Archive()
        {

            _service.ArchiveScheduler(DateTime.UtcNow);
            return true;
        }
    }
}
