using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries.Service
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IEnumerable<IFormFile> files, string src)
        {
            var savedImages = new List<string>();

            var imageDirectory = Path.Combine("wwwroot", "images", src);

            if (!Directory.Exists(imageDirectory))
                Directory.CreateDirectory(imageDirectory);

            foreach (var file in files)
            {
                if (file.Length == 0) continue;

                var imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var root = Path.Combine(imageDirectory, imageName);

                using var stream = new FileStream(root, FileMode.Create);
                await file.CopyToAsync(stream);

                savedImages.Add($"/images/{src}/{imageName}");
            }

            return savedImages;
        }




        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
