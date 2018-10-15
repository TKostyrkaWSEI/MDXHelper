using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

using MDXHelperData.Data;

namespace MDXHelperApp
{
    public class Processor
    {
        private readonly ILoader scriptLoader;
        private readonly IExtractor extractor;
        private readonly IDBCommunicator dbc;
        private BaseConfig Config { get; set; }

        public Processor()
        {
            IContainer container = (new IocConfig()).GetContainer();
            ILifetimeScope scope = container.BeginLifetimeScope();

            scriptLoader = scope.Resolve<ILoader>();
            extractor = scope.Resolve<IExtractor>();
            dbc = scope.Resolve<IDBCommunicator>();
        }

        public void SetConfig(ProcessorInput procInpt)
        {
            Config = new BaseConfig(procInpt.SourceType, procInpt.ConfigParams);
            dbc.SetProjectAndCube(procInpt.ProjectName, procInpt.CubeName);
        }

        public void LoadCubeObjects()
        {         
            LoaderOutput lo = scriptLoader.GetLoaderOutput(Config);

            dbc.AddScript(lo.CalculationScript);
            dbc.AddObjActionToDB(lo.ActionList);
            dbc.AddObjKpiToDB(lo.KpiList);
            dbc.AddObjMeasureToDB(lo.MeasureList);
        }

        public void SplitScript()
        {
            ObjCalcScript cs = dbc.GetScript();
            //  split
            List<Calculation> cspts = extractor.SplitToCalculations(cs);

            //  extract
            List<MdxScope> esc = extractor.ExtractScopes(cs, ref cspts);
            List<MdxMember> emm = extractor.ExtractMeasureMembers(cs, ref cspts);
            List<MdxSet> est = extractor.ExtractSets(cs, ref cspts);

            List<MdxUnclassifiedCalc> muc = cspts.Select(x => new MdxUnclassifiedCalc
            {
                FullCode = x.fullcode,
                ObjCalcScriptId = cs.Id
            }).ToList();

            //  add to db
            dbc.AddMdxScopeListToDB(cs, esc);
            dbc.AddMdxMemberMeasureListToDB(cs, emm);
            dbc.AddMdxSetListToDB(cs, est);
            dbc.AddMdxUnclassifiedCalcListToDB(cs, muc);

        }
    }
}
