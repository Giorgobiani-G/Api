using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApi.Models
{
    public class ContactInfo
    {
        [Key]
        public int ContactId { get; set; }
        public int Info { get; set; }
        public int FizikPiriId { get; set; }
        FizikPiri FizikPiri { get; set; }
    }
}
