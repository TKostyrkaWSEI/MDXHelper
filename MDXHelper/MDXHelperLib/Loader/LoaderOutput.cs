using System.Collections.Generic;
using MDXHelperData.Data;
using Microsoft.AnalysisServices;

namespace MDXHelperApp
{
    public class LoaderOutput
    {
        public string CalculationScript;
        public List<ObjAction> ActionList = new List<ObjAction>();
        public List<ObjKpi> KpiList = new List<ObjKpi>();
        public List<ObjMeasure> MeasureList = new List<ObjMeasure>();
    }
}
