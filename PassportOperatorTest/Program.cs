using PassportOperator;
using System;
using System.IO;

namespace PassportOperatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MATCPassport _passport = new PassportOperator.MATCPassport
            {
                UNIU = "05100ДУ701146",
                Address = "dgdfgdfg",
                CreateDate = "666666666666666",
                Okrug = "ggggggggggg",
                Raion = "ddddddddddddd",
                GU = "!@#!@#!@#3453534534",
                Closed_loop = "!@#!@#!@#3453534534",
                Reconstruction = "!@#!@#!@#3453534534",
                Electricity_connection = "!@#!@#!@#3453534534",
                Type_of_surface = "!@#!@#!@#3453534534",
                Sidewalk_width = "!@#!@#!@#3453534534",
                Traffic = "!@#!@#!@#3453534534",
                State = "!@#!@#!@#3453534534",
                Visibility = "Хорошая",
                Foto = new FileInfo("e://Jellyfish.jpg").OpenRead(),
                Plan = new FileInfo("e://Penguins.jpg").OpenRead()
            };
            MATCPassport_InformationField field = new MATCPassport_InformationField
            {
                Arrow = "left",
                Name = "gdfgdfg",
                Transliteration = "dfgdfgdfgdfgd"
            };
            _passport.Information_fields.Add(field);
            MATCPassportExporter Ex1 = new MATCPassportExporter();
            Func<MATCPassport, string> ggg = delegate (MATCPassport passport) { return passport.UNIU + "_паспорт_" + DateTime.Now.ToString("yyyyMMdd"); };
            Ex1.ExportToDOCX(_passport, new DirectoryInfo("E:\\"), ggg);



        }
    }
}
