using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApi.Models
{
    public class FizikPiri
    {


        
        public int FizikPiriId { get; set; }
        
        public string Saxeli { get; set; }
        [DataType(DataType.Text)]
        public string SaxeliLatinuri { get; set; }
        public string Gvari { get; set; }
        public string GvariLatinuri { get; set; }

        public string Piradoba { get; set; }
        public DateTime DabTarigi { get; set; }

        public string Misamarti { get; set; }


        public List<ContactInfo> ContactInfos { get; set; }
        public List<ConnectedPerson> ConnectedPersons { get; set; }

        public virtual Image Image { get; set; }
    }

    //Fluent Api
    public class FizikPiriValodator : AbstractValidator<FizikPiri>

    {
        public FizikPiriValodator()
        {
            RuleFor(x => x.Saxeli).NotEmpty();
            RuleFor(x => x.Gvari).NotEmpty();
            RuleFor(x => x.Piradoba).Length(10)
                .WithMessage("piradi nomeri unda shedgobodes 10 cifrisgan");
            RuleFor(x => x.DabTarigi).LessThan(DateTime.Today)
                .WithMessage("dabtarigi arunda ikos momvalshi");
        }
    
    }
}
