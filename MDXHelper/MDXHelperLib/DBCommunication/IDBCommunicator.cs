using MDXHelperData.Data;
using Microsoft.AnalysisServices;
using System.Collections.Generic;

namespace MDXHelperApp
{
    public interface IDBCommunicator
    {
        void SetProjectAndCube(
            string ProjectName,
            string CubeName
            );

        void AddScript(
           string str
           );

        ObjCalcScript GetScript();

        void AddObjActionToDB(List<ObjAction> ls);
        void AddObjKpiToDB(List<ObjKpi> ls);
        void AddObjMeasureToDB(List<ObjMeasure> ls);

        void AddMdxScopeListToDB(ObjCalcScript cspt, List<MdxScope> esc);
        void AddMdxMemberMeasureListToDB(ObjCalcScript cspt, List<MdxMember> emm);
        void AddMdxSetListToDB(ObjCalcScript cspt, List<MdxSet> es);
        void AddMdxUnclassifiedCalcListToDB(ObjCalcScript cspt, List<MdxUnclassifiedCalc> muc);
    }
}
