using LearnApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using IFormFile = Microsoft.AspNetCore.Http.IFormFile;
using System.Drawing;
using Image = LearnApi.Models.Image;
using System.Threading;

namespace LearnApi.Controllers
{
    [Route("v1/Image")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly CitizenDbContext _dbContext;
        private readonly IWebHostEnvironment _env;
        public ImagesController(CitizenDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _env = webHostEnvironment;
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var filepath = (from db in _dbContext.Images
                            where db.FizikPiriId == Id
                            select db.ImageUrl).FirstOrDefault();

            if (System.IO.File.Exists(filepath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filepath);
                return File(b, "image/png");
            }

            return null;
        }

        [HttpPost]
        public string Post(IFormFile formFile, int id)
        {
            try
            {
                if (formFile.Length > 0)
                {
                    string url = _env.WebRootPath + "\\Uploads\\";
                    if (!Directory.Exists(url))
                    {
                        Directory.CreateDirectory(url);
                    }
                    var exist = (from f in _dbContext.Images
                                 where f.FizikPiriId == id
                                 select f).Any();
                    if (!exist)
                    {
                        using FileStream fileStream = System.IO.File.Create(url + formFile.FileName + id.ToString());
                        formFile.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    Image image = new()
                    {
                        FizikPiriId = id,
                        ImageUrl = url + formFile.FileName + id
                    };

                    _dbContext.Images.Add(image);
                    _dbContext.SaveChanges();

                    return "upload done";
                }
                else
                {
                    return "Failed";
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            Image image = (from dl in _dbContext.Images
                           where dl.FizikPiriId == id
                           select dl).FirstOrDefault();

            if (System.IO.File.Exists(image.ImageUrl))
            {
                System.IO.File.Delete(image.ImageUrl);
            }

            _dbContext.Images.Remove(image);
            _dbContext.SaveChanges();
            return "Deleted";
        }
    }
}

