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
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base(@"Data Source =.; Initial Catalog = MDXHelperAppDb; Integrated Security = true")
        //public AppDbContext() : base(@"Data Source =ITK\DV16; Initial Catalog = MDXHelperAppDb; Integrated Security = true")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());        
        }
        public DbSet<Project> Project { get; set; }
        public DbSet<ObjCube> ObjCube { get; set; }
        public DbSet<ObjCalcScript> ObjCalcScript { get; set; }
        public DbSet<ObjAction> ObjAction { get; set; }
        public DbSet<ObjKpi> ObjKpi { get; set; }
        public DbSet<ObjMeasure> ObjMeasure { get; set; }
        public DbSet<MdxMember> MdxMember { get; set; }
        public DbSet<MdxSet> MdxSet { get; set; }
        public DbSet<MdxScope> MdxScope { get; set; }
        public DbSet<MdxUnclassifiedCalc> MdxUnclassifiedCalc { get; set; }
    }

}