using MDXHelperData.Data;
using Microsoft.AnalysisServices;
using System;

namespace MDXHelperApp
{
    public class ServerPicker : IPicker
    {
        public LoaderOutput Load(BaseConfig config)
        {
            ServerConfig cg = new ServerConfig(config.Params);
            Server srv = new Server();
            srv.Connect(cg.SsasConnString());

            Database db = srv.Databases.FindByName(cg.SsasDatabaseName());
            Cube cb = db.Cubes.FindByName(cg.SsasCubeName());
            Command cm = cb.MdxScripts[0].Commands[0];

            //  Populate LoaderOutput
            //  ------------------------------------------------------------------------
            LoaderOutput lo = new LoaderOutput
            {
                CalculationScript = cm.Text
            };

            foreach (MeasureGroup mg in cb.MeasureGroups)
            {
                foreach (Measure m in mg.Measures)
                {
                    lo.MeasureList.Add(new ObjMeasure()
                    {
                        Name = m.Name,
                        MeasureGroup = mg.Name,
                        DisplayFolder = m.DisplayFolder,
                        FormatString = m.FormatString,
                        Source = m.Source.ToString(),
                        SourceCollation = m.Source.Collation,
                        SourceDataType = m.Source.DataType.ToString(),
                        SourceDataSize = m.Source.DataSize,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    });
                }
            }

            foreach (Microsoft.AnalysisServices.Action a in cb.Actions)
            {
                lo.ActionList.Add(new ObjAction
                {
                    Name = a.Name,
                    Caption = a.Caption,
                    CaptionIsMdx = a.CaptionIsMdx,
                    Condition = a.Condition,
                    Description = a.Description,
                    Target = a.Target,
                    TargetType = a.TargetType.ToString(),
                    Type = a.Type.ToString(),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
                );
            }

            foreach (Kpi k in cb.Kpis)
            {
                lo.KpiList.Add(new ObjKpi
                {
                    Name = k.Name,
                    Weight = k.Weight,
                    StatusGraphic = k.StatusGraphic,
                    TrendGraphic = k.TrendGraphic,
                    AssociatedMeasureGroup = k.AssociatedMeasureGroup.Name,
                    DisplayFolder = k.DisplayFolder,
                    Value = k.Value,
                    Trend = k.Trend,
                    Goal = k.Goal,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
                );
            }

            srv.Disconnect();
            return lo;
        }
    }


}
