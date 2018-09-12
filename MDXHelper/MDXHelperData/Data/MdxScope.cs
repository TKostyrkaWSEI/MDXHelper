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
    public class MdxScope
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("CalculationScript")]
        [Required]
        public int ObjCalcScriptId { get; set; }
        public ObjCalcScript CalculationScript { get; set; }

        //ObjCalcScriptId
    }
}
