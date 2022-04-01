using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApi.Models
{
    public class Image
    {
        public int Id { get; set; }
       
      
        public string ImageUrl { get; set; }

        public int FizikPiriId { get; set; }

       
        public virtual FizikPiri FizikPiri { get; set; }

    }
}
