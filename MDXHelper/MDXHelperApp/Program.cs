using Newtonsoft.Json;
using System;
using System.IO;

namespace MDXHelperApp
{

    //  todo:    ------------------------------------------
    //
    //  - posprzątać Namespace'y (jest HelperApp w Lib)
    //  - wywalić Calculate z other calcs
    //  - pomyśleć nad zrzutem do dokumentacji
    //  -   SCOPE_ISOLATION
    //  ---------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {
            Ladowanie();
        }

        static void Ladowanie()
        {
            string path_tie = @"C:\Users\tomek\Desktop\DCDemoMDX\Config_ServerAWDW1.txt";

            string nl = new string('-', 50);
            Console.WriteLine(nl);

            string json = File.ReadAllText(path_tie);
            Console.WriteLine(json);

            ProcessorInput procInput = JsonConvert.DeserializeObject<ProcessorInput>(json);
            Processor prs = new Processor();

            prs.LoadCubeObjects(procInput);
            prs.SplitScript(procInput);

            Console.WriteLine(nl);
            Console.WriteLine("<-- end -->");
            Console.ReadLine();
        }

    }

}