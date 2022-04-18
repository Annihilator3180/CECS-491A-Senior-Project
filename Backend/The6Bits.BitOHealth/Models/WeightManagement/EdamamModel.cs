
namespace The6Bits.BitOHealth.Models
{
    public class Nutrients
    {
        public double ENERC_KCAL { get; set; }
        public double PROCNT { get; set; }
        public double FAT { get; set; }
        public double CHOCDF { get; set; }
        public double FIBTG { get; set; }
    }

    public class Food
    {
        public string? foodId { get; set; }
        public string label { get; set; }
        public Nutrients nutrients { get; set; }
        public string category { get; set; }
        public string categoryLabel { get; set; }
        public string image { get; set; }
        public string brand { get; set; }
        public string foodContentsLabel { get; set; }
        public List<ServingSize> servingSizes { get; set; }
    }

    public class Parsed
    {
        public Food food { get; set; }
    }

    public class ServingSize
    {
        public string uri { get; set; }
        public string label { get; set; }
        public double quantity { get; set; }
    }

    public class Qualifier
    {
        public string uri { get; set; }
        public string label { get; set; }
    }

    public class Qualified
    {
        public List<Qualifier> qualifiers { get; set; }
        public double weight { get; set; }
    }

    public class Measure
    {
        public string uri { get; set; }
        public string label { get; set; }
        public double weight { get; set; }
        public List<Qualified> qualified { get; set; }
    }

    public class Hint
    {
        public Food food { get; set; }
        public List<Measure> measures { get; set; }
    }

    public class Next
    {
        public string title { get; set; }
        public string href { get; set; }
    }

    public class Links
    {
        public Next next { get; set; }
    }

    public class Root
    {
        public string text { get; set; }
        public List<Parsed> parsed { get; set; }
        public List<Hint> hints { get; set; }
        public Links _links { get; set; }
    }



}
