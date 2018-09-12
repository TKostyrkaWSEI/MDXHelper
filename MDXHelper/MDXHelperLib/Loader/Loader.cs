using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MDXHelperApp
{
    class Loader : ILoader
    {
        private IPicker Picker;

        public LoaderOutput GetLoaderOutput(BaseConfig config)
        {

            LoaderOutput lo = new LoaderOutput();
            switch (config.SourceType)
            {
                case SourceType.XML:
                    {
                        Picker = new XMLPicker();
                        break;
                    }
                case SourceType.Server:
                    {
                        Picker = new ServerPicker();
                        break;
                    }
                default:
                    {
                        throw new Exception("Niepoprawny typ danych w pliku konfiguracyjnym (SourceType)");
                    }
            }

            lo = Picker.Load(config as BaseConfig);
            lo.CalculationScript = CleanScript(lo.CalculationScript);

            return lo;
        }

        private string CleanScript(string CalcScript)
        {
            //  remove comments
            //  -----------------------------------------------

            var blockComments = @"/\*(.*?)\*/";
            var lineComments1 = @"//(.*?)(\r?\n|$)";
            var lineComments2 = @"--(.*?)(\r?\n|$)";

            CalcScript = Regex.Replace(CalcScript, blockComments + "|" + lineComments1 + "|" + lineComments2, "", RegexOptions.Singleline);

            //  trim & remove empty lines
            //  -----------------------------------------------

            List<string> calcLines = CalcScript
                .Split(Environment.NewLine.ToCharArray())
                .Select(x => x.Trim())
                .Where(x => x.Length != 0)
                .ToList()
                ;

            CalcScript = string.Join(Environment.NewLine, calcLines);

            return CalcScript;
        }
    }
}
