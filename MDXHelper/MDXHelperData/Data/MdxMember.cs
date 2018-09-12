using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDXHelperData.Data
{
    public class MdxMember
    {
        [Key]
        public int Id { get; set; }

        [Index("NUIX_MdxMemberMeasureNameAndCalculationScript", 1, IsUnique = true)]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string FullCode { get; set; }

        [Required]
        public int IsMeasure { get; set; }

        [MaxLength(255)]
        public string Caption { get; set; }

        [Required]
        public int SolveOrder { get; set; }

        [MaxLength(4000)]
        public string MeasureGroup { get; set; }
        [MaxLength(255)]
        public string DisplayFolder { get; set; }
        [MaxLength(255)]
        public string NonEmptyBehavior { get; set; }
        [MaxLength(255)]
        public string FormatString { get; set; }
        [Required]
        public int Visible { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("CalculationScript")]
        [Index("NUIX_MdxMemberMeasureNameAndCalculationScript", 2, IsUnique = true)]
        [Required]
        public int ObjCalcScriptId { get; set; }
        public ObjCalcScript CalculationScript { get; set; }
    }
}
