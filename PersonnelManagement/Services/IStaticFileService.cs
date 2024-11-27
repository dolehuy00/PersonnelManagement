namespace PersonnelManagement.Services
{
    public interface IStaticFileService
    {
        Task<string> UploadImageAsync(IFormFile file, string fileName, string key);
    }
}
