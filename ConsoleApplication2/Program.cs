using commonB1;
using commonB1.Config;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ConsoleApplication2
{
    class Program
    {


        static void Main(String[] args)
        {

            StandartNames _standartNames = ((commonB1_config)ConfigurationManager.OpenExeConfiguration(0).Sections["commonB1_config"]).standartNames;
            Keywords _keywords = ((commonB1_config)ConfigurationManager.OpenExeConfiguration(0).Sections["commonB1_config"]).keywords;
            FilesWorker _fw = new FilesWorker();
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



