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
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; }

        [MaxLength(255)]
        public string ProjectDescription { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ICollection<ObjCube> Cubes { get; set; }
    }
}
