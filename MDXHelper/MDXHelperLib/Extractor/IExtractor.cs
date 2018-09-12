using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MDXHelperData.Data;
using System.Data.Entity.Validation;

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
