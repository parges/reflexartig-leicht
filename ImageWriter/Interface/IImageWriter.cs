using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ImageWriter.Interface
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file, string path);
        /*IActionResult Download(string path);*/
    }
}
