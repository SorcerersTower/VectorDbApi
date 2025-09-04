using VectorDbApi.Models;
using VectorDbApi.Services;
using System.Text.Json;

namespace VectorDbApi
{
    public class FilterHelper
    {
        private MongoDBService mongoDb;

        public FilterHelper (MongoDBService m)
        {
            mongoDb = m;
        }
        public string FilterByVenue(string jsonString, string venueId)
        {
            PineConeModel pineModel = null;

            pineModel = System.Text.Json.JsonSerializer.Deserialize<PineConeModel>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            List<Match> matches = pineModel.matches;

            List<VenueProduct> venueProducts = mongoDb.GetAllVenueProducts();

            List<VenueProduct> filteredVP = venueProducts.FindAll(x => x.VenueId == venueId);
            List<Match> venueFiltered = new List<Match>();
            foreach (Match match in matches) 
            {
                VenueProduct? vp = filteredVP.Find(x => x.ProductId == match.metadata.ProductId);
                if(vp != null)
                {
                    venueFiltered.Add(match);
                }
            }

           
            pineModel.matches = venueFiltered;

            string json = System.Text.Json.JsonSerializer.Serialize<PineConeModel>(pineModel, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return json;
        }


        public string FilterForDistinct(string value, string venueId)
        {
            PineConeModel pineModel = null;

            pineModel = System.Text.Json.JsonSerializer.Deserialize<PineConeModel>(value, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            List<Match> matches = pineModel.matches;
            List<Match> filteredList = matches.DistinctBy(x => x.metadata.ProductId).Take(3).ToList();

            pineModel.matches = filteredList;


            //if (venueId != "0" && venueId != null)
            //    mongoDb.WriteMatch(pineModel.matches, venueId);
            //else
            //    mongoDb.WriteMatch(pineModel.matches, "0");

            string json = System.Text.Json.JsonSerializer.Serialize<PineConeModel>(pineModel, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return json;
        }
    }
}
