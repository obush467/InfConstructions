using System.Collections.Generic;
using System.Linq;
using System.Activities;
using System.IO;
using System.Text.RegularExpressions;

namespace commonB1.Activityes
{

    public sealed class caLoadInDirectoryFiles : CodeActivity
    {
        // Определите входной аргумент действия типа string
        public InArgument<string> InDirectory { get; set; }
        public InArgument<string> FileNumberRegExp { get; set; }

        public OutArgument<List<FileInfo>> List { get; set; }


        // Если действие возвращает значение, создайте класс, производный от CodeActivity<TResult>
        // и верните значение из метода Execute.
        protected override void Execute(CodeActivityContext context)
        {
            // Получите значение входного аргумента Text во время выполнения
           var _InDirectory = context.GetValue(this.InDirectory);
            DirectoryInfo _wDir = new DirectoryInfo(_InDirectory);
            Regex reg = new Regex(FileNumberRegExp.Get(context));
            List<FileInfo> filelist = (_wDir.GetFiles("*",SearchOption.AllDirectories).Where(path => reg.IsMatch(path.Name)).ToList());
            List.Set(context, filelist);
           // return filelist;

        }
    }
}
