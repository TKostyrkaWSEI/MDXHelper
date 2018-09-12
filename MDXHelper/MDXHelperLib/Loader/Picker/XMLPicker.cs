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
    public class XMLPicker : IPicker
    {
        public LoaderOutput Load(BaseConfig config)
        {
            LoaderOutput lo = new LoaderOutput();

            XMLConfig cg = new XMLConfig(config.Params);
            XDocument dc = XDocument.Load(cg.Path());
            XmlNamespaceManager nm = new XmlNamespaceManager(new NameTable());

            nm.AddNamespace("x", @"http://schemas.microsoft.com/analysisservices/2003/engine");
            string xPath = @"/x:Cube/x:MdxScripts/x:MdxScript/x:Commands/x:Command/x:Text";

            XElement e = dc.XPathSelectElements(xPath, nm).Single();
            lo.CalculationScript = e.Value;

            return lo;
        }
    }
}
