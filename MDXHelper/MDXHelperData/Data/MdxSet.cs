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
    public class MdxSet
    {
        [Key]
        public int Id { get; set; }

        [Index("NUIX_MdxSetNameAndCalculationScript", 1, IsUnique = true)]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string FullCode { get; set; }

        [MaxLength(255)]
        public string Caption { get; set; }
        [MaxLength(255)]
        public string DisplayFolder { get; set; }

        [Required]
        public int IsSession { get; set; }
        [Required]
        public int IsDynamic { get; set; }
        [Required]
        public int IsHidden { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("CalculationScript")]
        [Index("NUIX_MdxSetNameAndCalculationScript", 2, IsUnique = true)]
        [Required]
        public int ObjCalcScriptId { get; set; }
        public ObjCalcScript CalculationScript { get; set; }
    }
}
