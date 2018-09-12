using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDXHelperData.Data
{
    public class ObjAction
    {
        [Key]
        public int Id { get; set; }

        [Index("NUIX_ActionName", 1, IsUnique = true)]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Caption { get; set; }

        public bool CaptionIsMdx { get; set; }

        [MaxLength(255)]
        public string Condition { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Target { get; set; }

        [MaxLength(255)]
        public string TargetType { get; set; }

        [MaxLength(255)]
        public string Type { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [Index("NUIX_ActionName", 2, IsUnique = true)]
        [ForeignKey("Cube")]
        [Required]
        public int CubeId { get; set; }
        public ObjCube Cube { get; set; }
    }
}
