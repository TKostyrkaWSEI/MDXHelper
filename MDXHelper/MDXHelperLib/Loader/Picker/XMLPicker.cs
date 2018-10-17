using System.Linq;

using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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
