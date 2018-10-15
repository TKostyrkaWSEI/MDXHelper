using Microsoft.AnalysisServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCDemo2App.DemoMethods
{
    class DemoClass
    {
        Server srv = new Server();
        public DemoClass()
        {
            srv.Connect("WL352168\\KOKOSZMD");
        }

        public void DemoMethod01()
        {
            foreach (Database d in srv.Databases)
            {
                PrintLine(d.Name, 1);
                foreach (Cube c in d.Cubes)
                {
                    PrintLine(c.Name, 2);
                    foreach (MeasureGroup mg in c.MeasureGroups)
                    {
                        PrintLine(mg.Name, 3);
                        foreach (Measure mr in mg.Measures)
                        {
                            PrintLine(mr.Name, 4);
                        }
                    }
                }
            }
        }
        public void DemoMethod02()
        {
            Database d = srv.Databases.FindByName("ContosoDCDemo");
            Cube c = d.Cubes.FindByName("OperationA");
            MeasureGroup mg = c.MeasureGroups.FindByName("Inventory");

            foreach (Measure m in mg.Measures)
            {
                //  https://blog.crossjoin.co.uk/2005/06/30/measure-expressions/

                Console.WriteLine(
                    $"{m.Name,-40}, " +
                    $"{m.Visible, -5}, " +
                    $"{m.Source,-40}, " +
                    $"{m.AggregateFunction,-10}, " +
                    $"{m.MeasureExpression}"
                    );
            }
        }
        public void DemoMethod03()
        {
            Database d = srv.Databases.FindByName("ContosoDCDemo");
            Cube c = d.Cubes.FindByName("OperationA");
            MdxScript mx = c.MdxScripts[0];
            string cs = mx.Commands[0].Text;

            Console.WriteLine($"scripts in cube: {c.MdxScripts.Count}");
            Console.WriteLine($"commands in script: {c.MdxScripts[0].Commands.Count}");
            Console.WriteLine($"{new String('-', 100)}\n");

            Console.WriteLine(cs);
        }
        public void DemoMethod04()
        {
            Database d = srv.Databases.FindByName("ContosoDCDemo");
            Cube c = d.Cubes.FindByName("OperationA");

            Console.WriteLine("kpis:");
            foreach (Kpi k in c.Kpis)
            {
                Console.WriteLine($"\t{k.Name}");
            }

            Console.WriteLine("actions:");
            foreach (Microsoft.AnalysisServices.Action a in c.Actions)
            {
                Console.WriteLine($"\t{a.Name}");
            }
        }

        void PrintLine(string s, int i = 0)
        {
            Console.WriteLine($"{new String(' ', 5 * i)}{s}");
        }
    }
}
