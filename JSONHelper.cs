using Newtonsoft.Json;
using VectorDbApi.Models;
using System.Reflection;
using System;
using System.Text.Json;
using SixLabors.ImageSharp;

namespace VectorDbApi
{
    public class JSONHelper
    {
        public JSONHelper() { }

        public static string GenerateRequestJson(VectorDBValues vectorData)
        {
            var vector = vectorData.Vector;

            // Define metadata for the vector
            var metadata = new Dictionary<string, string>
            {
                { "Category", vectorData.Category.ToString()},
                { "ProductId", vectorData.ProductId.ToString() },
                { "VenueId", vectorData.VenueId.ToString() },
                { "ImageName", vectorData.ImageId.ToString() },

            };

            // Create an anonymous object that matches the JSON structure
            var jsonObj = new
            {
                vectors = new[]
                {
                    new
                    {
                        values = vector,
                        metadata = metadata,
                        id = Guid.NewGuid().ToString()
                    }
                }
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            // Output the JSON string
            return json;
        }


        public static string GenerateQueryJson(float[] vectorData)
        {
            var vector = vectorData;

    

            // Create an anonymous object that matches the JSON structure
            var jsonObj = new
            {
                includeValues = false.ToString(),
                includeMetadata = true.ToString(),
                vector = vector,
                topK = 1000.ToString(),
     
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            // Output the JSON string
            return json;
        }

        public static string GenerateQueryFilterByVenueIdJson(float[] vectorData, string venueId)
        {
            var vector = vectorData;

            var filter = new Dictionary<string, string>
            {
                {"VenueId", venueId },
            };

            // Create an anonymous object that matches the JSON structure
            var jsonObj = new
            {
                includeValues = false.ToString(),
                includeMetadata = true.ToString(),
                vector = vector,
                topK = 25.ToString(),
                filter = filter
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            // Output the JSON string
            return json;
        }


        public static string GenerateQueryFilterByImageNameJson(float[] vectorData, string imageName)
        {
            var vector = vectorData;

            var filter = new Dictionary<string, string>
            {
                {"imageName", imageName },
            };

            // Create an anonymous object that matches the JSON structure
            var jsonObj = new
            {
                includeValues = false.ToString(),
                includeMetadata = true.ToString(),
                vector = vector,
                topK = 25.ToString(),
                filter = filter
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            // Output the JSON string
            return json;
        }

        public static string GenerateGetAllQuery()
        {
            float[] vector = GenerateEmptyVector();
            // Create an anonymous object that matches the JSON structure
            var jsonObj = new
            {
                includeValues = false.ToString(),
                includeMetadata = true.ToString(),
                @namespace = "",
                topK = 1000.ToString(),
                vector = vector
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            // Output the JSON string
            return json;
        }

        public static string GenerateDeleteByImageNameQuery(string imageName)
        {
            var filter = new Dictionary<string, string>
            {
                {"ImageName", imageName },
            };

            var jsonObj = new
            {
                deleteAll = false.ToString(),
                @namespace = "",
                filter = filter
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            return json;
        }

        public static string GenerateDeleteByProductIdQuery(string productId)
        {
            var filter = new Dictionary<string, string>
            {
                {"ProductId", productId },
            };

            var jsonObj = new
            {
                deleteAll = false.ToString(),
                @namespace = "",
                filter = filter
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            return json;
        }

        public static string GenerateDeleteAll()
        {


            var jsonObj = new
            {
                deleteAll = true.ToString(),
                @namespace = ""
            };

            // Serialize the anonymous object to JSON using Newtonsoft.Json
            var json = JsonConvert.SerializeObject(jsonObj);

            return json;
        }

        private static float[] GenerateEmptyVector()
        {
            float[] vector = new float[1000];

            for(int i=0; i<1000; i++) 
            {
                vector[i] = 0.0f;
            }

            return vector;
        }

    }
}
