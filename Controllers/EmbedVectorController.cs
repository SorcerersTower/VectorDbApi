using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using VectorDbApi.Models;
using VectorDbApi.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace VectorDbApi.Controllers
{

    [ApiController] 
    [Route("api/[controller]")]
    public class EmbedVectorController : Controller
    {
        private EmbeddingService embeddingService;
        private PineConeService pineConeService;
        private FilterHelper filterHelper;
     

        public EmbedVectorController(EmbeddingService service, PineConeService pcs, MongoDBService mongo)
        {
            embeddingService = service;
            pineConeService = pcs;
            filterHelper = new FilterHelper(mongo);
          
        }

        //Creates an image embedding and saves the images that is being uploaded 
        [HttpPost]
        [Route("{id}/{category}/{venueId}/{imageName}")]
        public string EmbedImage(string id, string category, string venueId, string imageName)
        {
 

            Image<Rgb24> image = null;

            if (Request.Form.Files[0].Length > 0)
            {
                var file = Request.Form.Files[0];
                image = Image.Load<Rgb24>(file.OpenReadStream());
            }

            float[] embedding  = embeddingService.CreateImageEmbedding(image);

            VectorDBValues vecVal = new VectorDBValues();
            vecVal.Vector = embedding;
            vecVal.Category = category;
            vecVal.ProductId = id;
            vecVal.VenueId = venueId;
            vecVal.ImageId = imageName;

            pineConeService.UpsertVectorData(vecVal);
             

            return "Image is stored";
        }

        //Matches the Image embedding with those already existing in the database
        [HttpPost]
        [Route("/api/ImageMatch/{venueId}")]
        public string QueryImage(string venueId)
        {
            Image<Rgb24> image = null;

            if (Request.Form.Files[0].Length > 0)
            {
                var file = Request.Form.Files[0];
                image = Image.Load<Rgb24>(file.OpenReadStream());
            }

            float[] embedding = embeddingService.CreateImageEmbedding(image);
            string result = pineConeService.QueryDatabase(embedding).Result;
            
            if(venueId != "0" && venueId != null && venueId != "" )
                result = filterHelper.FilterByVenue(result, venueId);

            result = filterHelper.FilterForDistinct(result, venueId);

            return result;
        }

        //Matches the Image embedding with those already existing in the database
        [HttpPost]
        [Route("{venueId}")]
        public string QueryImageByVenueId(string venueId)
        {
            Image<Rgb24> image = null;

            if (Request.Form.Files[0].Length > 0)
            {
                var file = Request.Form.Files[0];
                image = Image.Load<Rgb24>(file.OpenReadStream());
            }

            float[] embedding = embeddingService.CreateImageEmbedding(image);
            string result = pineConeService.QueryDatabaseFilterByVenueId(embedding, venueId).Result;

            //result = JSONHelper.FilterForDistinct(result);

            return result;
        }

        //Matches the Image embedding with those already existing in the database
        [HttpPost]
        [Route("/api/QueryImage/{imageName}")]
        public string QueryImageByImageName(string imageName)
        {
            Image<Rgb24> image = null;

            if (Request.Form.Files[0].Length > 0)
            {
                var file = Request.Form.Files[0];
                image = Image.Load<Rgb24>(file.OpenReadStream());
            }

            float[] embedding = embeddingService.CreateImageEmbedding(image);
            string result = pineConeService.QueryDatabaseByImageName(embedding, imageName).Result;

            //result = JSONHelper.FilterForDistinct(result);

            return result;
        }

        [HttpPost]
        [Route("/api/DeleteImage/{imageName}")]
        public string RemoveImageFromProduct(string imageName)
        {
            pineConeService.DeleteImageBasedOnImageName(imageName);

            //result = JSONHelper.FilterForDistinct(result);

            return "";
        }


        [HttpPost]
        [Route("/api/DeleteProduct/{productId}")]
        public void DeleteVectorBasedOnProductId(string productId)
        {

            pineConeService.DeleteImageBasedOnProductId(productId);
        }





        [HttpPost]
        [Route("/api/AllRows")]
        public string GetAllRows()
        {
 
            string result = pineConeService.GetAllEntries().Result;

            return result;
        }



    }
}
