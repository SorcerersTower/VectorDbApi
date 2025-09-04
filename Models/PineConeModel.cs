namespace ReevelaVectorDbApi.Models
{
    public class Match
    {
        public string id { get; set; }
        public double score { get; set; }
        public List<object> values { get; set; }
        public Metadata metadata { get; set; }

    }

    public class Metadata
    {
        public string Category { get; set; }
        public string ProductId { get; set; }
        public string VenueId { get; set; }

        public string ImageName { get; set; }
    }

    public class PineConeModel
    {
        public List<object> results { get; set; }
        public List<Match> matches { get; set; }
        public string @namespace { get; set; }
    }
}
