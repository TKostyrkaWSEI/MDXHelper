using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    public interface IDivider
    {
        List<string> Divide(    string str,
                                List<BreakSignPair> breakPairs,
                                char divideSign
                                );

        List<string> Divide(string str,
                                List<BreakSignPair> breakPairs,
                                char divideSign,
                                List<BreakSignPair> bracketPairs
                                );
    }

    class BracketPair
    {
        public BreakSignPair bracketPair { get; set; }
        public uint bracketCount { get; set; }
    }

    class Divider: IDivider
    {
        public List<string> Divide( string str,
                                    List<BreakSignPair> breakPairs,
                                    char divideSign
                                    )
        {
            List<string> chunks = new List<string>();
            List<int> brks = new List<int>() { -1 };

            BreakState st = new BreakState();

            for (int i = 0; i < str.Length; i++)
            {
                switch (st.BreakFlag)
                {
                    case true:
                        if (str[i] == breakPairs
                                        .Where(x => x.id == st.BreakSignId)
                                        .Select(x => x.sign_stop)
                                        .First()
                                        )
                        {
                            st.BreakFlag = false;
                            st.BreakSignId = -1;
                        }
                        break;

                    case false:
                        if (str[i] == divideSign) { brks.Add(i); }
                        else if (str[i] != divideSign)
                        {
                            foreach (BreakSignPair cp in breakPairs)
                            {
                                if (cp.sign_start == str[i])
                                {
                                    st.BreakFlag = true;
                                    st.BreakSignId = cp.id;
                                }
                            }
                        }
                        break;
                }
            }

            for (int i = 1; i < brks.Count(); i++)
            {
                chunks.Add(str.Substring(brks[i - 1] + 1, brks[i] - brks[i - 1] - 1).Trim());
            }
            return chunks;
        }

        public List<string> Divide(
            string str,
            List<BreakSignPair> breakPairs,
            char divideSign,
            List<BreakSignPair> bracketPairs
            )
        {
            List<string> strChunks = new List<string>();
            List<int> brks = new List<int>() { -1 };

            BreakState st = new BreakState();
            List<BracketPair> bp = bracketPairs.Select(x => new BracketPair() { bracketPair = x, bracketCount = 0}).ToList();

            for (int i = 0; i < str.Length; i++)
            {
                switch (st.BreakFlag)
                {
                    //  If break is opened -> close or do nothing
                    case true:
                        if (str[i] == breakPairs
                                        .Where(x => x.id == st.BreakSignId)
                                        .Select(x => x.sign_stop)
                                        .First()
                                        )
                        {
                            st.BreakFlag = false;
                            st.BreakSignId = -1;
                        }
                        break;

                    //  If break is closed
                    case false:

                        BracketPair b_start = bp.Where(z => z.bracketPair.sign_start == str[i]).Select(x => x).FirstOrDefault();
                        BracketPair b_stop = bp.Where(z => z.bracketPair.sign_stop == str[i]).Select(x => x).FirstOrDefault();

                        int bracketSum = bp.Select(z => (int)z.bracketCount).ToList().Sum();

                        if (bracketSum == 0 && str[i] == divideSign) { brks.Add(i); }
                        else if (b_start != null) { b_start.bracketCount++; }
                        else if (b_stop != null) { b_stop.bracketCount--; }
                        else
                        {
                            foreach (BreakSignPair bsp in breakPairs)
                            {
                                if (str[i] == bsp.sign_start)
                                {
                                    st.BreakFlag = true;
                                    st.BreakSignId = bsp.id;
                                }
                            }
                        }

                        break;
                }
            }

            for (int i = 1; i < brks.Count(); i++)
            {
                strChunks.Add(str.Substring(brks[i - 1] + 1, brks[i] - brks[i - 1] - 1).Trim());
            }
            return strChunks;
        }
    }
}
