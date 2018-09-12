using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    public class BaseConfig
    {
        public SourceType SourceType { get; set; }
        public Dictionary<string, string> Params { get; set; }

        public BaseConfig(SourceType _SourceType, Dictionary<string, string> _Params)
        {
            SourceType = _SourceType;
            Params = _Params;
        }
    }
}
