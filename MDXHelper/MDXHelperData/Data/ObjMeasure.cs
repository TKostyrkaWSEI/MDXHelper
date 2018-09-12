using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDXHelperData.Data
{
    public class ObjMeasure
    {
        [Key]
        public int MeasureId { get; set; }

        [Index("NUIX_MeasureName", 1, IsUnique = true)]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string MeasureGroup { get; set; }

        [MaxLength(255)]
        public string DisplayFolder { get; set; }

        [MaxLength(255)]
        public string FormatString { get; set; }

        [MaxLength(255)]
        public string Source { get; set; }

        [MaxLength(255)]
        public string SourceCollation { get; set; }

        [MaxLength(255)]
        public string SourceDataType { get; set; }

        public int SourceDataSize { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [Index("NUIX_MeasureName", 2, IsUnique = true)]
        [ForeignKey("Cube")]
        [Required]
        public int CubeId { get; set; }
        public ObjCube Cube { get; set; }
    }
}
