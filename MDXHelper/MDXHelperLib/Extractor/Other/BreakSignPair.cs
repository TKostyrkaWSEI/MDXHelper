using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    public class BreakSignPair
    {
        public int id { get; set; }
        public char sign_start { get; set; }
        public char sign_stop { get; set; }

        public BreakSignPair(int c_id, char c_sign_start, char c_sign_stop)
        {
            id = c_id;
            sign_start = c_sign_start;
            sign_stop = c_sign_stop;
        }
    }
}
