using PassportOperator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassportOperatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MATCPassport _passport = new PassportOperator.MATCPassport();
            _passport.UNIU = "05100ДУ701146";
            _passport.Address = "dgdfgdfg";
            _passport.CreateDate = "666666666666666";
            _passport.Okrug = "ggggggggggg";
            _passport.Raion = "ddddddddddddd";
            _passport.GU = "!@#!@#!@#3453534534";
            _passport.Closed_loop = "!@#!@#!@#3453534534";
            _passport.Reconstruction = "!@#!@#!@#3453534534";
            _passport.Electricity_connection = "!@#!@#!@#3453534534";
            _passport.Type_of_surface = "!@#!@#!@#3453534534";
            _passport.Sidewalk_width = "!@#!@#!@#3453534534";
            _passport.Traffic = "!@#!@#!@#3453534534";
            _passport.State = "!@#!@#!@#3453534534";
            _passport.Visibility = "Хорошая";
            _passport.Foto = new FileInfo("e://Jellyfish.jpg").OpenRead();
            _passport.Plan = new FileInfo("e://Penguins.jpg").OpenRead();
            MATCPassport_InformationField field = new MATCPassport_InformationField();
            field.Arrow = "left";
            field.Name = "gdfgdfg";
            field.Transliteration = "dfgdfgdfgdfgd";
            _passport.Information_fields.Add(field);
            MATCPassportExporter Ex1 = new MATCPassportExporter();
            Func< MATCPassport,string> ggg = delegate (MATCPassport passport) { return passport.UNIU + "_паспорт_" + DateTime.Now.ToString("yyyyMMdd"); };
            Ex1.ExportToDOCX(_passport,new DirectoryInfo("E:\\"),ggg);



        }
    }
}
