
using ReevelaVectorDbApi.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SixLabors.ImageSharp;

namespace ReevelaVectorDbApi.Services
{
    public class PineConeService
    {
        private readonly string baseUrl = "imagematch-53d5537.svc.eu-west4-gcp.pinecone.io";
        private readonly string apiKey = "9ae0c0ee-de0d-42ab-bf88-b311c9d700e9";

        private HttpRequestMessage CreateRequestFundament()
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("Api-Key", apiKey);

            return request;
        }

        public async void UpsertVectorData(VectorDBValues vecVal)
        {
           
            var client = new HttpClient();
            HttpRequestMessage request = CreateRequestFundament();


            request.RequestUri = new Uri("https://" + baseUrl + "/vectors/upsert");
            request.Content = new StringContent(JSONHelper.GenerateRequestJson(vecVal));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> QueryDatabase(float[] vectorData)
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();


            request.RequestUri = new Uri("https://" + baseUrl + "/query");
            request.Content = new StringContent(JSONHelper.GenerateQueryJson(vectorData));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            return body;
        }

        public async Task<string> QueryDatabaseFilterByVenueId(float[] vectorData, string venueId)
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();


            request.RequestUri = new Uri("https://" + baseUrl + "/query");
            request.Content = new StringContent(JSONHelper.GenerateQueryFilterByVenueIdJson(vectorData, venueId));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();    
            }

            return  body;
        }

        public async Task<string> QueryDatabaseByImageName(float[] vectorData, string imageName)
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();


            request.RequestUri = new Uri("https://" + baseUrl + "/query");
            request.Content = new StringContent(JSONHelper.GenerateQueryFilterByImageNameJson(vectorData, imageName));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            return body;
        }

        public async void DeleteImageBasedOnImageName(string imageName)
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();

            request.RequestUri = new Uri("https://" + baseUrl + "/vectors/delete");
            request.Content = new StringContent(JSONHelper.GenerateDeleteByImageNameQuery(imageName));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

       
        }

        public async void DeleteImageBasedOnProductId(string productId)
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();

            request.RequestUri = new Uri("https://" + baseUrl + "/vectors/delete");
            request.Content = new StringContent(JSONHelper.GenerateDeleteByProductIdQuery(productId));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }
        }

        public async void DeleteAllVectors()
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();

            request.RequestUri = new Uri("https://" + baseUrl + "/vectors/delete");
            request.Content = new StringContent(JSONHelper.GenerateDeleteAll());
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }
        }



        public async Task<string> GetAllEntries()
        {
            var client = new HttpClient();
            var body = "";
            HttpRequestMessage request = CreateRequestFundament();

            request.RequestUri = new Uri("https://" + baseUrl + "/query");
            request.Content = new StringContent(JSONHelper.GenerateGetAllQuery());
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }

            return body;
        }

 
    }
}
