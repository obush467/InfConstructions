using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commonB1;
using commonB1.Perestanovki;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Activities.Expressions;
using System.Collections.ObjectModel;

namespace ConsoleApplication2
{
    class Program
    {
        public static  List<FileInfo>  Files = new List<FileInfo>();

        static void Main(String[] args)
        {
           lll();
        }
        public delegate Collection<FileInfo> IntFilter(int i);
        IntFilter RRRRR = new IntFilter((int g) => new Collection<FileInfo>());
        public static void lll()
        {
            commonB1.filesOperate Act1 = new commonB1.filesOperate();
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            Act1.DeleteDuplicates= false;
            Act1.DeleteEmptyFolders = true;
            Act1.DirectoryNamePriorityFirst = false;
            Act1.FileNamePriority = true;
            Act1.InDirectory=(new InArgument<string>("e:\\121"));
            Act1.OutDirectory = (new InArgument<string>("E:\\222"));
            Act1.InsertedFileNamePart = "_";
            Act1.FileNumberRegExp = "\\d{5}[Г|Д]У\\d{6}";
            Act1.RenamedFilesRegExp = "(\\d{5}[Д|Г]У\\d{6})(?:(:?_паспорт_согл_(:?\\d+_)*НЕВЕРНЫЙ(:?_\\d+)*)|(:?_паспорт_согл(:?_\\d+)*)|(:?_паспорт(:?_\\d+)*)|(:?_паспорт_первый(:?_\\d+)*)|(:?_паспорт_согл_ИЛИОН(:?_\\d+)*)|(:?_макет(:?_\\d+)*)|(:?_макет_исправленный(:?_\\d+)*)|(:?_макет_согл(:?_\\d+)*)|(:?_паспорт_макет_согл(:?_\\d+)*)|(:?_паспорт_макет(:?_\\d+)*)|(:?_карта(:?_\\d+)*)|(:?_карта_ЕГКО(:?_\\d+)*)|(:?_панорама(:?_\\d+)*)|(:?_акт_ВО(:?_\\d+)*)|(:?_элКО(:?_\\d+)*)|(:?_повторный_осмотр(:?_\\d+)*)|_фото|_фото_ИЛИОН|_фото_экспертов)*(?:_\\d{8,14})(?:_\\d+)*\\..{3,4}";
            Act1.RenamedFilesExtensionsRegExp = "jpg";
            Act1.NotRenamedFilesRegExp = "(\\d{5}[Д|Г]У\\d{6})(?:(:?_паспорт_согл_(:?\\d+_)*НЕВЕРНЫЙ(:?_\\d+)*)|(:?_паспорт_согл(:?_\\d+)*)|(:?_паспорт(:?_\\d+)*)|(:?_паспорт_первый(:?_\\d+)*)|(:?_паспорт_согл_ИЛИОН(:?_\\d+)*)|(:?_макет(:?_\\d+)*)|(:?_макет_исправленный(:?_\\d+)*)|(:?_макет_согл(:?_\\d+)*)|(:?_паспорт_макет_согл(:?_\\d+)*)|(:?_паспорт_макет(:?_\\d+)*)|(:?_карта(:?_\\d+)*)|(:?_карта_ЕГКО(:?_\\d+)*)|(:?_панорама(:?_\\d+)*)|(:?_акт_ВО(:?_\\d+)*)|(:?_элКО(:?_\\d+)*)|(:?_повторный_осмотр(:?_\\d+)*)|_фото|_фото_ИЛИОН|_фото_экспертов)*(?:_\\d{8,14})(?:_\\d+)*\\..{3,4}";
            Act1.NotRenamedFilesExtensionsRegExp = "jpg";
            CompileExpressions(Act1);
            WorkflowApplication wfApp =
            new WorkflowApplication(Act1);
            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
                {
                    syncEvent.Set();
                };

                wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
                {
                    Console.WriteLine(e.Reason);
                    syncEvent.Set();
                };

                wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
                {
                    Console.WriteLine(e.UnhandledException.ToString());
                    return UnhandledExceptionAction.Terminate;
                };

                wfApp.Run();

                syncEvent.WaitOne();
            }

        static void CompileExpressions(Activity activity)
        {
            // activityName is the Namespace.Type of the activity that contains the  
            // C# expressions.  
            string activityName = activity.GetType().ToString();

            // Split activityName into Namespace and Type.Append _CompiledExpressionRoot to the type name  
            // to represent the new type that represents the compiled expressions.  
            // Take everything after the last . for the type name.  
            string activityType = activityName.Split('.').Last() + "_CompiledExpressionRoot";
            // Take everything before the last . for the namespace.  
            string activityNamespace = string.Join(".", activityName.Split('.').Reverse().Skip(1).Reverse());

            // Create a TextExpressionCompilerSettings.  
            TextExpressionCompilerSettings settings = new TextExpressionCompilerSettings
            {
                Activity = activity,
                Language = "C#",
                ActivityName = activityType,
                ActivityNamespace = activityNamespace,
                RootNamespace = null,
                GenerateAsPartialClass = false,
                AlwaysGenerateSource = true,
                ForImplementation = false
            };

            // Compile the C# expression.  
            TextExpressionCompilerResults results =
                new TextExpressionCompiler(settings).Compile();

            // Any compilation errors are contained in the CompilerMessages.  
            if (results.HasErrors)
            {
                throw new Exception("Compilation failed.");
            }

            // Create an instance of the new compiled expression type.  
            ICompiledExpressionRoot compiledExpressionRoot =
                Activator.CreateInstance(results.ResultType,
                    new object[] { activity }) as ICompiledExpressionRoot;

            // Attach it to the activity.  
            CompiledExpressionInvoker.SetCompiledExpressionRoot(
                activity, compiledExpressionRoot);
        }




        public void PermutationsCalculate()
        {
            PermutationsCalculator Calc = new commonB1.Perestanovki.PermutationsCalculator();
            String[] arr = new String[3];
            //String selectReg = "\\d{5}[Г|Д]У\\d{6}";
            //String NotRenamed = "(\\d{5}ГУ\\d{6})(?:)*(?:_\\d{8,14})*(?:_\\d+)*\\.";
            List<String> KeyWords = new List<string>();
            KeyWords.Add("паспорт");
            KeyWords.Add("макет");
            KeyWords.Add("карта");
            KeyWords.Add("панорама");
            KeyWords.Add("фото");
            KeyWords.Add("карточка");
            KeyWords.Add("акт");
            KeyWords.Add("МАЦ");
            KeyWords.Add("ИЛИОН");
            KeyWords.Add("экспертов");
            KeyWords.Add("согл");
            KeyWords.Add("первый");
            KeyWords.Add("старый");
            KeyWords.Add("новый");
            KeyWords.Add("последний");
            KeyWords.Add("верный");
            KeyWords.Add("неверный");
            KeyWords.Add("ЕГКО");
            KeyWords.Add("ВО");
            KeyWords.Add("устранение");
            KeyWords.Add("проверка");
            KeyWords.Add("элко");
            KeyWords.Add("электронная");
            List<String> StandartNames = new List<String>();
            StandartNames.Add("_паспорт_согл");
            StandartNames.Add("_паспорт_согл_НЕВЕРНЫЙ");
            StandartNames.Add("_паспорт_согл_ИЛИОН");
            StandartNames.Add("_макет_согл");
            StandartNames.Add("_паспорт");
            StandartNames.Add("_паспорт_первый");
            StandartNames.Add("_макет");
            StandartNames.Add("_паспорт_макет_согл");
            StandartNames.Add("_паспорт_макет");
            StandartNames.Add("_карта");
            StandartNames.Add("_карта_ЕГКО");
            StandartNames.Add("_фото_устранение_ИЛИОН");
            StandartNames.Add("_фото_устранение_МАЦ");
            StandartNames.Add("_фото_устранение");
            StandartNames.Add("_фото_экспертов");
            StandartNames.Add("_фото_проверка_ДКР");
            StandartNames.Add("_элКО");
            StandartNames.Add("_панорама");
            StandartNames.Add("_акт_ВО");
            var ttt = StandartNames.ConvertAll<List<String>>(new Converter<String, List<String>>(Conv));
            var ttt1 = StandartNames.ConvertAll<List<object[]>>(new Converter<String, List<object[]>>(ConvO));
            byCountComparer bc = new byCountComparer();
            //List<List<String>> yyy = CombinationList(KeyWords);
            //yyy.Sort(0, yyy.Count, bc);
            //List<String> FileList = new List<String>();     
            //ppp();

            Task t1 = new Task(Loadqq);
            t1.Start();
            t1.Wait();

            Console.ReadLine();


        }

        static async void Loadqq()
        { var t = await LoadAsync("U:\\Паспорта_и_Макеты"); }

        static List<FileInfo> Load(string FileName)
        {
                DirectoryInfo dir = new DirectoryInfo(FileName);
                List<DirectoryInfo> r = dir.GetDirectories("*", SearchOption.TopDirectoryOnly)
                 .Where(Name => ((new Regex("\\d{5}ДУ\\d{6}")).IsMatch(Name.Name))).ToList();
                foreach (DirectoryInfo d in r) { Load(d.FullName); }
                try
                {
                    List<FileInfo> ttt2;
                    ttt2 = dir.GetFiles("*", SearchOption.TopDirectoryOnly)
                                    .Where(Name => ((new Regex("\\d{5}ДУ\\d{6}")).IsMatch(Name.Name)) && (Name.DirectoryName != "DfsrPrivate"))
                                    .ToList();
                    Files.AddRange(ttt2);
                    return ttt2;

                }
                catch (UnauthorizedAccessException)
                { return null; }
        }
        
        static Task<List<FileInfo>> LoadAsync(string FileName)
        {
            return Task.Run(() =>

            {
                return Load(FileName);

            }
            );
        }
        public static List<String> Conv(String vConv)
        {
            String[] wA = vConv.Replace("_", " ").Trim().Split(Char.Parse(" "));
            PermutationsCalculator PC = new PermutationsCalculator();
            return PC.PermutationsCalculate_StringsList(wA.ToList());
        }

        public static List<object[]> ConvO(String vConv)
        {
            String[] wA = vConv.Replace("_", " ").Trim().Split(Char.Parse(" "));
            PermutationsCalculator PC = new PermutationsCalculator();
            return PC.PermutationsCalculate_List(wA.ToList());
        }
        public static List<List<String>> CombinationList(List<String> list)
        {
            int i;
            int p;
            int k2 = list.Count - 1;
            int n = list.Count;
            int m;
            int[] A = new int[100];
            List<List<String>> a2 = new List<List<String>>();
            for (int k = 1; k <= k2; k++)
            {
                for (i = 1; i <= k; i++)
                {
                    A[i] = i;
                }
                p = k;
                while (p >= 1)
                {
                    List<String> a1 = new List<String>();
                    for (m = 1; m <= k; m++)
                    {
                        a1.Add(list[A[m] - 1]);
                    }
                    a2.Add(a1);


                    if (A[k] == n)
                    { p = p - 1; }
                    else
                    { p = k; }

                    if (p >= 1)
                    {
                        for (i = k; i >= p; i--)
                        { A[i] = A[p] + i - p + 1; }
                    }
                }
            }
            a2.Add(list);
            return a2;




        }
        public static List<List<int>> Combination(int MaxCount)
        {

            int i;
            int p;
            int k2 = MaxCount - 1;
            int n = MaxCount;
            int m;
            int[] A = new int[100];
            List<List<int>> a2 = new List<List<int>>();
            for (int k = 1; k <= k2; k++)
            {
                for (i = 1; i <= k; i++)
                {
                    A[i] = i;
                }
                p = k;
                while (p >= 1)
                {
                    List<int> a1 = new List<int>();
                    for (m = 1; m <= k; m++)
                    {
                        a1.Add(A[m] - 1);
                    }
                    a2.Add(a1);


                    if (A[k] == n)
                    { p = p - 1; }
                    else
                    { p = k; }

                    if (p >= 1)
                    {
                        for (i = k; i >= p; i--)
                        { A[i] = A[p] + i - p + 1; }
                    }
                }
            }
            return a2;
        }
    }
    public class byCountComparer : IComparer<List<String>>
    {
        public int Compare(List<String> l1, List<String> l2)
        {
            if (l1.Count == l2.Count) { return 0; }
            else
            {
                if (l1.Count > l2.Count) { return -1; }
                else { return 1; }
            }
        }

    }
}



