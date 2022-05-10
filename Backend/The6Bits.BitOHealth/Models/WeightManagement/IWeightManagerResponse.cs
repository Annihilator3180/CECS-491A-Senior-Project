using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models.WeightManagement
{
    public interface IWeightManagerResponse
    {
        public object Result { get; set; }
        public bool? IsError { get; set; }
        public bool? UserError { get; set; }

    }
}
