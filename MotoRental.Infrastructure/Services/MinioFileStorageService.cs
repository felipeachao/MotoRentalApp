using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using MotoRental.Application.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MotoRental.Infrastructure.Services
{
    public class MinioFileStorageService : IFileStorageService
    {
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;

        public MinioFileStorageService(MinioClient minioClient, string bucketName)
        {
            _minioClient = minioClient;
            _bucketName = bucketName;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string directory)
        {
            try
            {
                bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
                if (!found)
                {
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                }

                var objectName = $"{directory}/{Guid.NewGuid()}_{file.FileName}";
                using (var stream = file.OpenReadStream())
                {
                    await _minioClient.PutObjectAsync(new PutObjectArgs()
                        .WithBucket(_bucketName)
                        .WithObject(objectName)
                        .WithStreamData(stream)
                        .WithObjectSize(file.Length)
                        .WithContentType(file.ContentType));
                }

                return $"{_bucketName}/{objectName}";
            }
            catch (MinioException e)
            {
                throw new ApplicationException("Error occurred while uploading the file to MinIO", e);
            }
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            try
            {
                var objectName = fileUrl.Substring(fileUrl.IndexOf("/") + 1); 
                await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_bucketName).WithObject(objectName));
            }
            catch (MinioException e)
            {
                throw new ApplicationException("Error occurred while deleting the file from MinIO", e);
            }
        }
    }
}
