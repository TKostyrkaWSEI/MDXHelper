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
    class Extractor: IExtractor
    {
        public Extractor(IDivider _prs)
        {
            prs = _prs;
        }

        private readonly IDivider prs;
        private readonly BreakSignPair brackets = new BreakSignPair(1, "("[0], ")"[0]);

        private readonly List<BreakSignPair> bracketPairs = new List<BreakSignPair>
        {
            new BreakSignPair(1, "("[0], ")"[0]),
            new BreakSignPair(1, "{"[0], "}"[0])
        };

        private readonly List<BreakSignPair> breakPairs = new List<BreakSignPair>
            {
                new BreakSignPair(1, "["[0], "]"[0])
            ,   new BreakSignPair(2, "\""[0], "\""[0])
            ,   new BreakSignPair(3, "'"[0], "'"[0])
            };

        public List<Calculation> SplitToCalculations(ObjCalcScript cspt)
        {
            List<Calculation> calculations = prs
                .Divide(cspt.CalculationScriptText, breakPairs, ';', bracketPairs)
                .Select(x => new Calculation(x, SplitToProperties(x)))
                .ToList()
                ;

            return calculations;
        }

        private List<string> SplitToProperties(string calculationText)
        {
            List<string> props = prs.Divide(calculationText + ",",
                                                breakPairs,
                                                ',',
                                                bracketPairs);
            return props;
        }

        public List<MdxScope> ExtractScopes(ObjCalcScript cspt, ref List<Calculation> calcs)
        {
            List<Tuple<int, int>> tpls = new List<Tuple<int, int>>();
            List<MdxScope> scopes = new List<MdxScope>();

            int state = 0;
            int start = 0;

            for (int i = 0; i < calcs.Count; i++)
            {
                switch (calcs[i].calc_type)
                {
                    case CalculationType.MdxScopeBegin:
                        {
                            state++;
                            if (state == 1) { start = i; }
                            break;
                        }
                    case CalculationType.MdxScopeEnd:
                        {
                            state--;
                            if (state == 0) { tpls.Add(new Tuple<int, int>(start, i)); }
                            break;
                        }
                }
            }

            foreach (Tuple<int, int> t in tpls.OrderByDescending(o => o.Item1))
            {
                string s =
                    string.Join(Environment.NewLine,
                        calcs
                            .Skip(t.Item1)
                            .Take(t.Item2 - t.Item1 + 1)
                            .Select(x => x.fullcode + ";")
                            );
                scopes.Add(new MdxScope
                                {   Code = s,
                                    ObjCalcScriptId = cspt.Id,
                                    DateCreated = DateTime.Now,
                                    DateModified = DateTime.Now
                                });

                calcs.RemoveRange(t.Item1, t.Item2 - t.Item1 + 1);
            }

            return scopes;
        }

        public List<MdxMember> ExtractMeasureMembers(ObjCalcScript cspt, ref List<Calculation> calcs)
        {
            List<MdxMember> cms = calcs
                .Where(x => x.calc_type == CalculationType.MdxMember)
                .Select(x => new CalcMember(x))
                .Select(x => new MdxMember
                {
                    IsMeasure = x.flag_ms,
                    FullCode = x.prop_ex,
                    Name = x.prop_nm,
                    MeasureGroup = x.prop_mg,
                    DisplayFolder = x.prop_df,
                    NonEmptyBehavior = x.prop_ne,
                    FormatString = x.prop_fs,
                    Caption = x.prop_cp,
                    Visible = x.prop_vs,
                    SolveOrder = x.prop_so,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    ObjCalcScriptId = cspt.Id
                })
                .ToList();

            calcs.RemoveAll(x => x.calc_type == CalculationType.MdxMember);

            return cms;
        }

        public List<MdxSet> ExtractSets(ObjCalcScript cspt, ref List<Calculation> calcs)
        {
            List<MdxSet> cms = calcs
                .Where(x => x.calc_type == CalculationType.MdxSet)
                .Select(x => new CalcSet(x))
                .Select(x => new MdxSet
                {
                    Name = x.prop_nm,
                    FullCode = x.prop_ex,
                    Caption = x.prop_cp,
                    DisplayFolder = x.prop_df,
                    IsSession = x.flag_is,
                    IsDynamic = x.flag_id,
                    IsHidden = x.flag_ih,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    ObjCalcScriptId = cspt.Id
                })
                .ToList();

            calcs.RemoveAll(x => x.calc_type == CalculationType.MdxSet);
            return cms;
        }


    }
}
