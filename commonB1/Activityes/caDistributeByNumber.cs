using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace commonB1.Activityes
{

    public sealed class CaDistributeByNumber : CodeActivity<FileInfo>
    {
        // Определите входной аргумент действия типа string
        public InArgument<string> FileNumberRegExp { get; set; }
        public InArgument<string> FullFilePath { get; set; }
        public InArgument<bool> FileNamePriority { get; set; }
        public InArgument<bool> DirectoryNamePriorityFirst { get; set; }
        public SortedDictionary<string, List<FileInfo>> List { get; set; }
        public OutArgument<string> Number { get; set; }

        // Если действие возвращает значение, создайте класс, производный от CodeActivity<TResult>
        // и верните значение из метода Execute.
        protected override FileInfo Execute(CodeActivityContext context)
        {
            Regex regex = new Regex(FileNumberRegExp.Get(context));
            // Получите значение входного аргумента Text во время выполнения
            string _FullFilePath = context.GetValue(this.FullFilePath);
            bool _FileNamePriority = FileNamePriority.Get(context);
            FileInfo _FileInfo = new FileInfo(_FullFilePath);

            MatchCollection mCollectionDirectory = regex.Matches(_FileInfo.DirectoryName);
            MatchCollection mCollectionName = regex.Matches(_FileInfo.Name);
            string _DirectoryNum;
            string _FileNameNum;

            switch (mCollectionDirectory.Count)
            {
                case 0:
                    { _DirectoryNum = null; }
                    break;
                case 1:
                    { _DirectoryNum = mCollectionDirectory[0].Value; }
                    break;
                default:
                    if (DirectoryNamePriorityFirst.Get(context))
                    { _DirectoryNum = mCollectionDirectory[0].Value; }
                    else
                    { _DirectoryNum = mCollectionDirectory[mCollectionDirectory.Count - 1].Value; }
                    break;
            }

            if (mCollectionName.Count > 0)
            { _FileNameNum = mCollectionName[0].Value; }
            else
            { _FileNameNum = null; }

            if (!string.IsNullOrEmpty(_FileNameNum))
            {
                if (_FileNamePriority)
                { Number.Set(context, _FileNameNum); }
                else
                { Number.Set(context, _DirectoryNum); }
            }
            else
            {
                if (!string.IsNullOrEmpty(_DirectoryNum))
                { Number.Set(context, _DirectoryNum); }
                else
                { throw new System.Activities.ValidationException("Имя файла не содержит регулярноно выражения."); }

            }
            ;
            return _FileInfo;
        }
    }
}
