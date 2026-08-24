# VectorDbApi

An ASP.NET Core Web API for image-based brand/object recognition. It generates vector embeddings from uploaded images using a ResNet50 (ONNX) model, then stores and queries those embeddings in Pinecone to find visual matches — the backend powering a Flutter mobile app with Shopify catalog integration.

## Tech Stack

- **.NET / ASP.NET Core** — Web API host (`Program.cs`, `Controllers`)
- **ONNX Runtime** (`Microsoft.ML.OnnxRuntime`) — runs the `resnet50-v2-7.onnx` model to generate image embeddings
- **SixLabors.ImageSharp** — image loading/decoding
- **Pinecone** (`PineConeService`) — vector database for storing and querying embeddings
- **MongoDB** (`MongoDBService`) — supporting data used for filtering results (by venue/category)
- **Swagger / Swashbuckle** — API exploration in development

## API Endpoints

| Method | Route | Description |
|---|---|---|
| POST | `/api/EmbedVector/{id}/{category}/{venueId}/{imageName}` | Creates an image embedding and stores it (with product/category/venue metadata) in Pinecone |
| POST | `/api/ImageMatch/{venueId}` | Embeds an uploaded image and queries Pinecone for matches, optionally filtered by venue and de-duplicated |
| POST | `/api/EmbedVector/{venueId}` | Embeds an uploaded image and queries Pinecone for matches filtered by venue ID |
| POST | `/api/QueryImage/{imageName}` | Embeds an uploaded image and queries Pinecone for matches filtered by image name |
| POST | `/api/DeleteImage/{imageName}` | Removes a stored image's vector by image name |
| POST | `/api/DeleteProduct/{productId}` | Removes all stored vectors associated with a product ID |
| POST | `/api/AllRows` | Returns all entries currently stored in the vector database |

## Getting Started

### Prerequisites

- .NET SDK
- A Pinecone account/index and API key
- A MongoDB connection string

### Configuration

Set the following in `appsettings.json` / `appsettings.Development.json` (or environment variables):

- Pinecone API key and index/environment settings
- MongoDB connection string

### Run locally

```bash
dotnet restore
dotnet run
```

In development, Swagger UI is available at `/swagger` for exploring and testing endpoints.

## Project Structure

```
Controllers/       API endpoints (EmbedVectorController)
Services/          EmbeddingService, PineConeService, MongoDBService
Models/            Data models (e.g. VectorDBValues)
ImageModels/        resnet50-v2-7.onnx model used for embeddings
FilterHelper.cs     Filters query results by venue / distinct entries
JSONHelper.cs       JSON formatting helpers
Program.cs          App startup and DI configuration
```
