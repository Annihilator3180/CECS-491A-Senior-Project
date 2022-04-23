using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models.WeightManagement
{
    public class WeightManagerResponse : IWeightManagerResponse
    {
        public object Result { get; set; }
        public bool? IsError { get; set; }

        public bool? UserError { get; set; }

        public WeightManagerResponse(object? value)
        {
            Result = value;
        }

        public WeightManagerResponse(object? value, bool isError)
        {
            Result = value;
            IsError = isError;
        }

        public WeightManagerResponse(object? value, bool isError, bool userError)
        {
            Result = value;
            IsError = isError;
            UserError = userError;
        }
    }
}
