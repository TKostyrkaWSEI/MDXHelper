using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.AdomdClient;

namespace MDXHelperApp
{
    public interface IPicker
    {
        LoaderOutput Load(BaseConfig config);
    }
}
