using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDXHelperData.Data
{
    public class ObjCube
    {
        [Key]
        public int Id { get; set; }

        [Index("NUIX_Cube", 1, IsUnique = true)]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("Project")]
        [Index("NUIX_Cube", 2, IsUnique = true)]
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<ObjCalcScript> CalculationScripts { get; set; }
        public ICollection<ObjAction> Actions { get; set; }
        public ICollection<ObjKpi> Kpis { get; set; }
        public ICollection<ObjMeasure> Measures { get; set; }
    }
}
