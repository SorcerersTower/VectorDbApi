using MongoDB.Bson;

namespace ReevelaVectorDbApi.Services
{
    public class VenueProduct
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }

        public string CarouselUrl { get; set; }

        public string VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenueUrl { get; set; }

    }
}
