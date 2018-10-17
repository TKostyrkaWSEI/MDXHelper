using MDXHelperData.Data;
using System.Collections.Generic;

namespace MDXHelperApp
{
    public interface IExtractor
    {
        List<Calculation> SplitToCalculations(ObjCalcScript cspt);
        List<MdxScope> ExtractScopes(ObjCalcScript cspt, ref List<Calculation> calcs);
        List<MdxMember> ExtractMeasureMembers(ObjCalcScript cspt, ref List<Calculation> calcs);
        List<MdxSet> ExtractSets(ObjCalcScript cspt, ref List<Calculation> calcs);
    }
}
