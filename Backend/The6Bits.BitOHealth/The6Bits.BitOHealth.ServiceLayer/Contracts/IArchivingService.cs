using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.ServiceLayer.Contracts
{
    public interface IArchivingService
    {
        void ArchiveScheduler(DateTime executionTime);

        void Archive();
    }
}
