using MediatR;
using Microsoft.AspNetCore.Http;
using MotoRental.Application.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MotoRental.Application.Commands
{
    public class UploadCnhCommand : IRequest<Unit>
    {
        public Guid DeliveryPersonId { get; set; }
        public IFormFile Image { get; set; }
    }

    public class UploadCnhCommandHandler : IRequestHandler<UploadCnhCommand, Unit>
    {
        private readonly IFileStorageService _fileStorageService;

        public UploadCnhCommandHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<Unit> Handle(UploadCnhCommand request, CancellationToken cancellationToken)
        {
            if (request.Image == null || request.Image.Length == 0)
                throw new ArgumentException("Invalid file.");
            var filePath = await _fileStorageService.UploadFileAsync(request.Image, "cnh");
            return Unit.Value;
        }
    }
}
