using Newtonsoft.Json;
using System;
using System.IO;

namespace MDXHelperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path_folder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\MDXDoc\SampleMDXConfigs\"));
  
            //Ladowanie(path_folder + @"Config_ServerA.txt");
            //Ladowanie(path_folder + @"Config_ServerB.txt"    );
            Ladowanie(path_folder + @"Config_ServerAWDW.txt" );
            //Ladowanie(path_folder + @"Config_XMLFile.txt"    );
            //Ladowanie(path_folder + @"Config_XMLHttp.txt"    );

            Console.WriteLine("<-- end -->");
            Console.ReadLine();
        }

        static void Ladowanie(string path_tie)
        {
            string nl = new string('-', 50);
            Console.WriteLine(nl);

            string json = File.ReadAllText(path_tie);
            Console.WriteLine(json);

            ProcessorInput procInput = JsonConvert.DeserializeObject<ProcessorInput>(json);
            IProcessor prs = new Processor();

            prs.SetConfig(procInput);
            prs.LoadCubeObjects();
            prs.SplitScript();

            Console.WriteLine(nl);
        }

    }

}