using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class CommonServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public CommonServices(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            string filename = "";
            string path = "";
            bool iscopied = false;
            try
            {
               
                if (file.Length >0)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    filename = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, filename);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                return filename;
            }
            catch (Exception)
            {
                throw;
            }
            return filename;
        }
    }
}
