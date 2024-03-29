﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;
namespace The6Bits.BitOHealth.DAL
{
    public interface IDrugDataSet
    {


        public Task<List<DrugName>> GetGenericDrugName(string drugName);
        public Task<List<DrugName>> GetBrandDrugName(string drugName);
        public Task<drugInfo> GetDrugInfo(string brand_name);
        public Task<drugInfo> GetDrugInfoGeneric(string generic_name);
    }
}
