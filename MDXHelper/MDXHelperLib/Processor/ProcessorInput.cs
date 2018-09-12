using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    public class ProcessorInput
    {
        public SourceType SourceType;
        public string ProjectName;
        public string CubeName;
        public Dictionary<string, string> ConfigParams;

        public ProcessorInput(SourceType _SourceType,   string _ProjectName, string _CubeName, Dictionary<string, string> _ConfigParams)
        {
            SourceType = _SourceType;
            ConfigParams = _ConfigParams;
            ProjectName = _ProjectName;
            CubeName = _CubeName;
        }
    }
}
