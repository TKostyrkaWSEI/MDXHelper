using MDXHelperData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using amo = Microsoft.AnalysisServices;

namespace MDXHelperApp
{
    public class DBCommunicator : IDBCommunicator
    {
        private readonly AppDbContext dbc;
        private Project prj = null;
        private ObjCube cb = null;

        public DBCommunicator(AppDbContext _dbc)
        {
            dbc = _dbc;
        }

        public void SetProjectAndCube(
            string ProjectName,
            string CubeName
            )
        {
            AddOrUpdateProject(new Project() {
                ProjectName = ProjectName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            });
            prj = dbc.Project
                .Single(b => b.ProjectName == ProjectName)
                ;

            AddOrUpdateObjCube(new ObjCube() {
                Name = CubeName,
                ProjectId = prj.Id,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            });
            cb = dbc.ObjCube
                .Single(b => b.Name == CubeName && b.ProjectId == prj.Id)
                ;
        }

        public void AddScript(
            string str
            )
        {
            if (cb == null) { return; }
            AddObjCalcScript(new ObjCalcScript
                                            {
                                                IsActive = 1,
                                                CalculationScriptText = str,
                                                DateCreated = DateTime.Now,
                                                DateModified = DateTime.Now,
                                                CubeId = cb.Id
                                            }
            );

            return;
        }

        public ObjCalcScript GetScript()
        {
            if (cb == null) { return null; }
            ObjCalcScript cs = dbc.ObjCalcScript
                .Where(b => b.Cube.Name == cb.Name &&
                        b.Cube.Project.ProjectName == prj.ProjectName &&
                        b.IsActive == 1)
                .Single()
                ;

            return cs;
        }

        private void AddOrUpdateProject(Project obj)
        {
            Project result = dbc.Project
                .SingleOrDefault(b => b.ProjectName == obj.ProjectName);

            if (result == null)
            { dbc.Project.Add(obj); }
            else
            {
                result.ProjectDescription = obj.ProjectDescription;
                result.DateModified = DateTime.Now;
            }

            dbc.SaveChanges();
        }

        private void AddOrUpdateObjCube(ObjCube obj)
        {
            ObjCube result = dbc.ObjCube
                .SingleOrDefault(b => b.Name == obj.Name && b.ProjectId == obj.ProjectId);

            if (result == null)
            {   dbc.ObjCube.Add(obj); }
            else
            {
                result.DateModified = DateTime.Now;
            }

            dbc.SaveChanges();
        }

        private void AddObjCalcScript(ObjCalcScript obj)
        {
            dbc.ObjCalcScript
                .Where(b => b.CubeId == obj.CubeId)
                .ToList()
                .ForEach(c => c.IsActive = 0);

            dbc.ObjCalcScript
                .Add(obj);

            dbc.SaveChanges();
        }
        
        public void AddObjActionToDB(List<ObjAction> ls)
        {
            dbc.ObjAction.RemoveRange(dbc.ObjAction.Where(s => s.CubeId == cb.Id));
            ls.ForEach(x => { x.CubeId = cb.Id; });
            ls.ForEach(x => dbc.ObjAction.Add(x));
            dbc.SaveChanges();
        }

        public void AddObjKpiToDB(List<ObjKpi> ls)
        {
            dbc.ObjKpi.RemoveRange(dbc.ObjKpi.Where(s => s.CubeId == cb.Id));
            ls.ForEach(x => { x.CubeId = cb.Id; });
            ls.ForEach(x => dbc.ObjKpi.Add(x));
            dbc.SaveChanges();
        }

        public void AddObjMeasureToDB(List<ObjMeasure> ls)
        {
            dbc.ObjMeasure.RemoveRange(dbc.ObjMeasure.Where(s => s.CubeId == cb.Id));
            ls.ForEach(x => { x.CubeId = cb.Id; });
            ls.ForEach(x => dbc.ObjMeasure.Add(x));
            dbc.SaveChanges();
        }

        public void AddMdxScopeListToDB(ObjCalcScript cspt, List<MdxScope> esc)
        {
            dbc.MdxScope.RemoveRange(dbc.MdxScope.Where(s => s.ObjCalcScriptId == cspt.Id));

            esc.ForEach(x => dbc.MdxScope.Add(x));
            dbc.SaveChanges();
        }

        public void AddMdxMemberMeasureListToDB(ObjCalcScript cspt, List<MdxMember> emm)
        {
            dbc.MdxMember.RemoveRange(dbc.MdxMember.Where(s => s.ObjCalcScriptId == cspt.Id));

            emm.ForEach(x => dbc.MdxMember.Add(x));
            dbc.SaveChanges();
        }

        public void AddMdxSetListToDB(ObjCalcScript cspt, List<MdxSet> es)
        {
            dbc.MdxSet.RemoveRange(dbc.MdxSet.Where(s => s.ObjCalcScriptId == cspt.Id));

            es.ForEach(x => dbc.MdxSet.Add(x));
            dbc.SaveChanges();
        }

        public void AddMdxUnclassifiedCalcListToDB(ObjCalcScript cspt, List<MdxUnclassifiedCalc> muc)
        {
            dbc.MdxUnclassifiedCalc.RemoveRange(dbc.MdxUnclassifiedCalc.Where(s => s.ObjCalcScriptId == cspt.Id));

            muc.ForEach(x => dbc.MdxUnclassifiedCalc.Add(x));
            dbc.SaveChanges();
        }
    }
}

//try
//{
//    dbc.SaveChanges();
//}
//catch (DbEntityValidationException e)
//{
//    foreach (var eve in e.EntityValidationErrors)
//    {
//        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//            eve.Entry.Entity.GetType().Name, eve.Entry.State);
//        foreach (var ve in eve.ValidationErrors)
//        {
//            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                ve.PropertyName, ve.ErrorMessage);
//        }
//    }
//    throw;
//}

