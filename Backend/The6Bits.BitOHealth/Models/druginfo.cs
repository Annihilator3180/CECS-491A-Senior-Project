using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class drugInfo
    {
    
        public List<string> spl_product_data_elements { get; set; }
        public List<string> boxed_warning { get; set; }
        public List<string> indications_and_usage { get; set; }
        public List<string> warnings_and_cautions { get; set; }
        public List<string> pregnancy { get; set; }
        //public List<string> pediatric_use { get; set; }
        //public List<string> geriatric_use { get; set; }
        //public List<string> drug_abuse_and_dependence { get; set; }
        //public List<string> controlled_substance { get; set; }
        //public List<string> abuse { get; set; }
        //public List<string> dependence { get; set; }
        //public List<string> overdosage { get; set; }
        //public List<string> description { get; set; }
        public List<string> inactive_ingredient { get; set; }
        //public List<string> clinical_pharmacology { get; set; }
        //public List<string> mechanism_of_action { get; set; }
        //public List<string> pharmacodynamics { get; set; }
        //public List<string> pharmacokinetics { get; set; }
        //public List<string> nonclinical_toxicology { get; set; }
        //public List<string> carcinogenesis_and_mutagenesis_and_impairment_of_fertility { get; set; }
        //public List<string> how_supplied { get; set; }
        public List<string> information_for_patients { get; set; }
        //public List<string> spl_unclassified_section { get; set; }
        //public List<string> spl_medguide { get; set; }
        //public List<string> package_label_principal_display_panel { get; set; }
        //public string set_id { get; set; }
        //public string effective_time { get; set; }
        public string version { get; set; }
        public Openfda openfda { get; set; }
        public bool isFavorited { get; set; }
        public FavoriteDrug favoriteDrug { get; set; }
    }


    public class Openfda
    {
        //public List<string> application_number { get; set; }
        public List<string> brand_name { get; set; }
        public List<string> generic_name { get; set; }
        //public List<string> manufacturer_name { get; set; }
        public List<string> product_ndc { get; set; }
        //public List<string> product_type { get; set; }
        //public List<string> route { get; set; }
        //public List<string> substance_name { get; set; }
        //public List<string> rxcui { get; set; }
        //public List<string> spl_id { get; set; }
        //public List<string> spl_set_id { get; set; }
        //public List<string> package_ndc { get; set; }
        //public List<string> original_packager_product_ndc { get; set; }
        //public List<string> nui { get; set; }
        //public List<string> pharm_class_epc { get; set; }
        //public List<string> pharm_class_cs { get; set; }
        //public List<string> pharm_class_pe { get; set; }
        //public List<string> unii { get; set; }
    }

    public class drugInfos
    {
        public List<drugInfo> results { get; set; }
    }



}

