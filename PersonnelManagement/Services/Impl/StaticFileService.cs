
using PersonnelManagement.DTO;
using System.Net.Http.Headers;

namespace PersonnelManagement.Services.Impl
{
    public class StaticFileService : IStaticFileService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StaticFileService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string fileName, string key)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file provided.", nameof(file));
            }

            var client = _httpClientFactory.CreateClient("WebStorageClient");

            using var content = new MultipartFormDataContent();

            // Thêm file
            using var fileStream = file.OpenReadStream();
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "file", fileName);

            // Thêm key
            content.Add(new StringContent(key), "key");

            // Gửi request tới Web Storage API
            var response = await client.PostAsync("/api/file/upload-image", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error uploading file: {response.ReasonPhrase}");
            }

            var storageResult = await response.Content.ReadFromJsonAsync<StorageResponseDTO>();
            if (storageResult == null)
            {
                throw new Exception("Error parsing storage response.");
            }

            return storageResult.FileUrl; // Trả về đường dẫn file
        }
    }
}
