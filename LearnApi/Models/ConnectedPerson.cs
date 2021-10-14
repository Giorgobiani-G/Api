using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApi.Models
{
    public class ConnectedPerson
    {

        public int ConnectedPersonId { get; set; }

        public int PersonTobeConnecedId  { get; set; }
        public string RelationType { get; set; }
        public int FizikPiriId { get; set; }

        public FizikPiri FizikPiri { get; set; }

    }
}
