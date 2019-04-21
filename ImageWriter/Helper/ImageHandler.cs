using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploader.Helper
{
    public interface IImageHandler
    {
        Task<string> UploadImage(IFormFile file, string path);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<string> UploadImage(IFormFile file, string path)
        {
            var result = await _imageWriter.UploadImage(file, path);
            return result;
        }
    }
}
