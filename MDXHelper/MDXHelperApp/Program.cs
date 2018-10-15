using Newtonsoft.Json;
using System;
using System.IO;

namespace MDXHelperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Ladowanie();
        }

        static void Ladowanie()
        {
            //string path_tie = @"C:\Users\kostytom\Documents\GitHub\MDXHelper\_DCDemoFiles\DCDemo03_Parser\CfgFiles\Config_ServerA.txt";
            string path_tie = @"C:\Users\kostytom\Documents\GitHub\MDXHelper\_DCDemoFiles\DCDemo03_Parser\CfgFiles\Config_ServerB.txt";

            string nl = new string('-', 50);
            Console.WriteLine(nl);

            string json = File.ReadAllText(path_tie);
            Console.WriteLine(json);

            ProcessorInput procInput = JsonConvert.DeserializeObject<ProcessorInput>(json);
            Processor prs = new Processor();

            prs.SetConfig(procInput);
            prs.LoadCubeObjects();
            prs.SplitScript();

            Console.WriteLine(nl);
            Console.WriteLine("<-- end -->");
            Console.ReadLine();
        }

    }

}