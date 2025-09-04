namespace ReevelaVectorDbApi.Models
{
    public class ImageMatch
    {
        public string _id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        public string Imageid { get; set; }

        public string Venueid { get; set; }

        public string VenueName { get; set; }

        public string Score { get; set; }

        public DateTime CreatedDate { get; set; } 
    }
}
