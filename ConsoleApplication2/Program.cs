using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commonB1;
using commonB1.Perestanovki;
using commonB1.Config;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Activities.Expressions;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq.Expressions;

namespace ConsoleApplication2
{
    class Program
    {
        

        static void Main(String[] args)
        {
           
            StandartNames _standartNames = ((commonB1_config)ConfigurationManager.OpenExeConfiguration(0).Sections["commonB1_config"]).standartNames;
            Keywords _keywords = ((commonB1_config)ConfigurationManager.OpenExeConfiguration(0).Sections["commonB1_config"]).keywords;
            filesWorker _fw= new filesWorker();
            List<string> snl = new List<string>();
                foreach (StandartName sn in _standartNames)
            { snl.Add(sn.Name); };
            List<string> snk = new List<string>();
            foreach (Keyword sn in _keywords)
            { snk.Add(sn.Name); }
            _fw.PermutationsCalculate_string(snl);
            _fw.StartFilesOperate();
        }
       
    }
}



