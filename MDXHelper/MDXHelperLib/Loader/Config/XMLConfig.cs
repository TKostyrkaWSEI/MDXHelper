using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    public class XMLConfig : BaseConfig
    {
        public string FileLocation() { return Params["FileLocation"]; }
        public string Path() { return Params["Path"]; }
        
        public XMLConfig(Dictionary<string, string> _Params) : base(SourceType.XML, _Params) { }
    }
}
