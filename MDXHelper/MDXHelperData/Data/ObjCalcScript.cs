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
    public class ObjCalcScript
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IsActive { get; set; }

        [Required]
        public string CalculationScriptText { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("Cube")]
        [Required]
        public int CubeId { get; set; }
        public ObjCube Cube { get; set; }

        public ICollection<MdxMember> MeasureMembers { get; set; }
        public ICollection<MdxSet> Sets { get; set; }
        public ICollection<MdxScope> Scopes { get; set; }
        public ICollection<MdxUnclassifiedCalc> UnclassifiedCalcs { get; set; }
    }
}
