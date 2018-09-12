using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    class BreakState
    {
        public BreakState()
        {
            BreakFlag = false;
            BreakSignId = -1;
            BracketCount = 0;
        }

        public bool BreakFlag { get; set; }
        public int BreakSignId { get; set; }
        public uint BracketCount { get; set; }
    }
}
