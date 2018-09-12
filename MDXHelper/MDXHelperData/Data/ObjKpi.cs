using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDXHelperData.Data
{
    public class ObjKpi
    {
        [Key]
        public int Id { get; set; }

        [Index("NUIX_KpiName", 1, IsUnique = true)]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Weight { get; set; }

        [MaxLength(255)]
        public string StatusGraphic { get; set; }

        [MaxLength(255)]
        public string TrendGraphic { get; set; }

        [MaxLength(255)]
        public string AssociatedMeasureGroup { get; set; }

        [MaxLength(255)]
        public string DisplayFolder { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }

        [MaxLength(4000)]
        public string Trend { get; set; }

        [MaxLength(4000)]
        public string Goal { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [Index("NUIX_KpiName", 2, IsUnique = true)]
        [ForeignKey("Cube")]
        [Required]
        public int CubeId { get; set; }
        public ObjCube Cube { get; set; }
    }
}
