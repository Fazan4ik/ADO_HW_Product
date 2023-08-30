using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ADO_17._08._2023_1_.views
{
    public record ProductGroup
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;
        public String Picture { get; set; } = null!;
        public string Delete { get; set; }
    }
}
