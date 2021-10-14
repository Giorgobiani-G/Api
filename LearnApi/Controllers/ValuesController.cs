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

namespace LearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly CitizenDbContext _dbContext ;
        private readonly IWebHostEnvironment _env;
        public ValuesController(CitizenDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._dbContext = dbContext;
            this._env = webHostEnvironment;
        }

        //        [HttpGet]
        public string Get([FromForm] int i)
        {
            var v = (from db in _dbContext.Images
                     where db.Id == i
                     select db.Content).FirstOrDefault();
            string imageBase64Data = Convert.ToBase64String(v);
            string ulr = string.Format("data:image/gif;base64,{0}",
 imageBase64Data);


            string st = _env.WebRootPath + "\\" + ulr;
            return ulr;
        }

        //public System.Drawing.Image Get([FromForm] string i)
        //{
        //    var v = (from db in _dbContext.Images
        //             where db.Id == Convert.ToInt32(i)
        //             select db.Content).FirstOrDefault().ToArray<byte>();
        //    //byte[] imageBase64Data = System.Text.Encoding.Default.GetBytes(v.ToString());

        //    MemoryStream ms = new(v, 0, v.Length);

        //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);


        //    image.Save(@"C:\Users\admin\Desktop\image\im.png", System.Drawing.Imaging.ImageFormat.Png);


        //    return image;
        //}

        [HttpPost]
        public string Post([FromForm] IFormFile photo, [FromForm] int FizikPiriId)
        {
            using (var ms = new MemoryStream())
            {

                photo.CopyTo(ms);

                
                Image image = new Image();
                image.Content = ms.ToArray();
                image.FizikPiriId = FizikPiriId;

                _dbContext.Images.Add(image);
                _dbContext.SaveChanges();
                return "saved";
            }
            
        }

        [HttpPut]
        public string Put([FromForm] IFormFile photo, [FromForm] int id)
        {
            using (var ms = new MemoryStream())
            {

                photo.CopyTo(ms);


                Image image = new Image();
                image.Content = ms.ToArray();
                image.Id = id;


                _dbContext.Entry(image).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                return "edited";
            }



            

            }

        [HttpDelete]
        public string Delete([FromForm] int id)
        {
            Image image = (from dl in _dbContext.Images
                           where dl.Id == id
                           select dl).FirstOrDefault();


            _dbContext.Images.Remove(image);
            _dbContext.SaveChanges();
            return "Deleted";
        }
    }
}
