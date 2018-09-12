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
    public class MdxUnclassifiedCalc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(4000)]
        public string FullCode { get; set; }

        [ForeignKey("CalculationScript")]
        [Required]
        public int ObjCalcScriptId { get; set; }
        public ObjCalcScript CalculationScript { get; set; }
    }
}
