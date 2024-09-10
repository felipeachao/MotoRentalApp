using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MotoRental.Application.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string directory);
        Task DeleteFileAsync(string fileUrl);
    }
}
