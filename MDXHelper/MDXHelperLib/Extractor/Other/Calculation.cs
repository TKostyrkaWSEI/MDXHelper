using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MDXHelperApp
{
    public class Calculation
    {
        public string fullcode { get; set; }
        public DateTime DateCreated;
        public DateTime DateModified;
        public CalculationType calc_type { get; }
        public List<string> props { get; }

        public Calculation(string c_fullcode, List<string> c_props)
        {
            fullcode = c_fullcode;
            props = c_props;
            
            if      (strBS.IsMatch(c_fullcode)) { calc_type = CalculationType.MdxScopeBegin; }
            else if (strES.IsMatch(c_fullcode)) { calc_type = CalculationType.MdxScopeEnd; }
            else if (strST.IsMatch(c_fullcode)) { calc_type = CalculationType.MdxSet; }
            else if (strCM.IsMatch(c_fullcode)) { calc_type = CalculationType.MdxMember; }
            else { calc_type = CalculationType.MdxOther; }

            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        protected Regex strBS = new Regex(@"^(SCOPE)", RegexOptions.IgnoreCase);
        protected Regex strES = new Regex(@"^(END\s*SCOPE)", RegexOptions.IgnoreCase);
        protected Regex strCM = new Regex(@"^(CREATE\s*MEMBER\s*CURRENTCUBE.)", RegexOptions.IgnoreCase);
        protected Regex strST = new Regex(@"^(CREATE\s*(SESSION)?(DYNAMIC|STATIC)?(HIDDEN)?\s*SET)", RegexOptions.IgnoreCase);
    }

    class CalcSet : Calculation
    {
        private Regex st_rgx = new Regex(@"^(CREATE\s*(SESSION)?\s*(DYNAMIC|STATIC)?\s*(HIDDEN)?\s*SET\s*.*[\n\r]*\.[\n\r]*\[?(?<CalcSet>[^\]]*)\]?\s*AS)", RegexOptions.IgnoreCase);
        private Regex cp_rgx = new Regex(@"^(CAPTION(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex df_rgx = new Regex(@"^(DISPLAY_FOLDER(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);

        private Regex is_rgx = new Regex(@"^(CREATE\s*(SESSION).*[\n\r]*SET)", RegexOptions.IgnoreCase);
        private Regex id_rgx = new Regex(@"^(CREATE\s*(DYNAMIC).*[\n\r]*SET)", RegexOptions.IgnoreCase);
        private Regex ih_rgx = new Regex(@"^(CREATE\s*(HIDDEN).*[\n\r]*SET)", RegexOptions.IgnoreCase);

        public string prop_nm { get; }
        public string prop_ex { get; }
        public string prop_cp { get; }
        public string prop_df { get; }

        public int flag_is { get; }
        public int flag_id { get; }
        public int flag_ih { get; }

        public CalcSet(Calculation m) : base(m.fullcode, m.props)
        {
                string st = props
                .Where(x => st_rgx.IsMatch(x))
                .FirstOrDefault();
                
            string pat1 = @"^(CREATE\s*(SESSION)?\s*(DYNAMIC|STATIC)?\s*(HIDDEN)?\s*SET\s*CURRENTCUBE.)";
                string pat2 = @"\sAS";

            prop_ex = st_rgx.Replace(st, "");
            prop_nm = Regex.Replace(Regex.Replace(
                st_rgx.Match(st).Value,
                pat1, "", RegexOptions.IgnoreCase),
                pat2, "", RegexOptions.IgnoreCase)
                .Trim()
                ;

            prop_cp = props.Where(x => cp_rgx.IsMatch(x)).Select(x => Regex.Replace(x, cp_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();
            prop_df = props.Where(x => df_rgx.IsMatch(x)).Select(x => Regex.Replace(x, df_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();

            flag_is = is_rgx.IsMatch(st) ? 1 : 0;
            flag_id = id_rgx.IsMatch(st) ? 1 : 0;
            flag_ih = ih_rgx.IsMatch(st) ? 1 : 0;
        }
    }

    class CalcMember : Calculation
    {
        private Regex mb_rgx = new Regex(@"^(\bCREATE\s*MEMBER\s*.*[\n\r]*\.[\n\r]*\[?(?<CalcMeasure>[^\]]*)\]?\s*AS)", RegexOptions.IgnoreCase);
        private Regex ms_rgx = new Regex(@"^(\bCREATE\s*MEMBER\s*.*\[?Measures\]?[\n\r]*\.[\n\r]*\[?(?<CalcMeasure>[^\]]*)\]?\s*AS)", RegexOptions.IgnoreCase);

        private Regex mg_rgx = new Regex(@"^(ASSOCIATED_MEASURE_GROUP(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex df_rgx = new Regex(@"^(DISPLAY_FOLDER(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex ne_rgx = new Regex(@"^(NON_EMPTY_BEHAVIOR(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex fs_rgx = new Regex(@"^(FORMAT_STRING(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex cp_rgx = new Regex(@"^(CAPTION(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex vs_rgx = new Regex(@"^(VISIBLE(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        private Regex so_rgx = new Regex(@"^(SOLVE_ORDER(\s+)?=(\s+)?)", RegexOptions.IgnoreCase);
        
        

        public string prop_nm { get; }
        public string prop_ex { get; }
        public string prop_mg { get; }
        public string prop_df { get; }
        public string prop_ne { get; }
        public string prop_fs { get; }
        public string prop_cp { get; }
        public int prop_vs { get; }
        public int prop_so { get; }

        public int flag_ms { get; }

        public CalcMember(Calculation m) : base(m.fullcode, m.props)
        {
            string st = props
                .Where(x => mb_rgx.IsMatch(x))
                .FirstOrDefault();

            string pat1 = @"CREATE\s*MEMBER\s*CURRENTCUBE."; //.\[Measures\]
            string pat2 = @"\sAS";

            prop_ex = mb_rgx.Replace(st, "");
            prop_nm = Regex.Replace(Regex.Replace(
                mb_rgx.Match(st).Value,
                pat1, "", RegexOptions.IgnoreCase),
                pat2, "", RegexOptions.IgnoreCase)
                .Trim()
                ;

            prop_mg = props.Where(x => mg_rgx.IsMatch(x)).Select(x => Regex.Replace(x, mg_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();
            prop_df = props.Where(x => df_rgx.IsMatch(x)).Select(x => Regex.Replace(x, df_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();
            prop_ne = props.Where(x => ne_rgx.IsMatch(x)).Select(x => Regex.Replace(x, ne_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();
            prop_fs = props.Where(x => fs_rgx.IsMatch(x)).Select(x => Regex.Replace(x, fs_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();
            prop_cp = props.Where(x => cp_rgx.IsMatch(x)).Select(x => Regex.Replace(x, cp_rgx.ToString(), "", RegexOptions.IgnoreCase)).FirstOrDefault();

            int vs;
            bool vsbl = int.TryParse(props.Where(x => vs_rgx.IsMatch(x))
                                                    .Select(x => Regex.Replace(x, vs_rgx.ToString(), "", RegexOptions.IgnoreCase))
                                                    .FirstOrDefault(),
                           out vs);
            prop_vs = vsbl ? vs : 1;

            int so;
            bool sobl = int.TryParse(props.Where(x => so_rgx.IsMatch(x))
                                                    .Select(x => Regex.Replace(x, so_rgx.ToString(), "", RegexOptions.IgnoreCase))
                                                    .FirstOrDefault(),
                           out so);
            prop_so = sobl ? so : 0;

            flag_ms = ms_rgx.IsMatch(st) ? 1 : 0;
        }

    }
}
