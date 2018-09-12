using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{

    public class ServerConfig : BaseConfig
    {
        public string SsasConnString() { return Params["SsasCS"]; }
        public string SsasDatabaseName() { return Params["SsasDB"]; }
        public string SsasCubeName() { return Params["SsasCB"]; }

        public ServerConfig(Dictionary<string, string> _Params) : base(SourceType.Server, _Params) { }
    }
}
