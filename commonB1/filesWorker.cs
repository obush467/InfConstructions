using commonB1.Perestanovki;
using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace commonB1
{
    public class FilesWorker
    {
        public List<FileInfo> Files = new List<FileInfo>();

        public delegate Collection<FileInfo> IntFilter(int i);
        public void StartFilesOperate()
        {
            filesOperate Act1 = new filesOperate();
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            Act1.DeleteDuplicates = false;
            Act1.DeleteEmptyFolders = true;
            Act1.DirectoryNamePriorityFirst = false;
            Act1.FileNamePriority = true;
            Act1.InDirectory = (new InArgument<string>("e:\\121"));
            Act1.OutDirectory = (new InArgument<string>("E:\\222"));
            Act1.InsertedFileNamePart = "_";
            Act1.FileNumberRegExp = "\\d{5}[Г|Д]У\\d{6}";
            Act1.RenamedFilesRegExp = "(\\d{5}[Д|Г]У\\d{6})(?:(:?_паспорт_согл_(:?\\d+_)*НЕВЕРНЫЙ(:?_\\d+)*)|(:?_паспорт_согл(:?_\\d+)*)|(:?_паспорт(:?_\\d+)*)|(:?_паспорт_первый(:?_\\d+)*)|(:?_паспорт_согл_ИЛИОН(:?_\\d+)*)|(:?_макет(:?_\\d+)*)|(:?_макет_исправленный(:?_\\d+)*)|(:?_макет_согл(:?_\\d+)*)|(:?_паспорт_макет_согл(:?_\\d+)*)|(:?_паспорт_макет(:?_\\d+)*)|(:?_карта(:?_\\d+)*)|(:?_карта_ЕГКО(:?_\\d+)*)|(:?_панорама(:?_\\d+)*)|(:?_акт_ВО(:?_\\d+)*)|(:?_элКО(:?_\\d+)*)|(:?_повторный_осмотр(:?_\\d+)*)|_фото|_фото_ИЛИОН|_фото_экспертов)*(?:_\\d{8,14})(?:_\\d+)*\\..{3,4}";
            Act1.RenamedFilesExtensionsRegExp = "jpg";
            Act1.NotRenamedFilesRegExp = "(\\d{5}[Д|Г]У\\d{6})(?:(:?_паспорт_согл_(:?\\d+_)*НЕВЕРНЫЙ(:?_\\d+)*)|(:?_паспорт_согл(:?_\\d+)*)|(:?_паспорт(:?_\\d+)*)|(:?_паспорт_первый(:?_\\d+)*)|(:?_паспорт_согл_ИЛИОН(:?_\\d+)*)|(:?_макет(:?_\\d+)*)|(:?_макет_исправленный(:?_\\d+)*)|(:?_макет_согл(:?_\\d+)*)|(:?_паспорт_макет_согл(:?_\\d+)*)|(:?_паспорт_макет(:?_\\d+)*)|(:?_карта(:?_\\d+)*)|(:?_карта_ЕГКО(:?_\\d+)*)|(:?_панорама(:?_\\d+)*)|(:?_акт_ВО(:?_\\d+)*)|(:?_элКО(:?_\\d+)*)|(:?_повторный_осмотр(:?_\\d+)*)|_фото|_фото_ИЛИОН|_фото_экспертов)*(?:_\\d{8,14})(?:_\\d+)*\\..{3,4}";
            Act1.NotRenamedFilesExtensionsRegExp = "jpg";
            CompileExpressions(Act1);
            WorkflowApplication wfApp =
            new WorkflowApplication(Act1)
            {
                Completed = delegate (WorkflowApplicationCompletedEventArgs e)
                {
                    syncEvent.Set();
                },

                Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
                {
                    Console.WriteLine(e.Reason);
                    syncEvent.Set();
                },

                OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
                {
                    Console.WriteLine(e.UnhandledException.ToString());
                    return UnhandledExceptionAction.Terminate;
                }
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

        public List<List<String>> PermutationsCalculate_string(List<String> Permutating)
        {
            PermutationsCalculator Calc = new PermutationsCalculator();
            return Permutating.ConvertAll<List<String>>(new Converter<String, List<String>>(Conv));
        }

        public List<List<object[]>> PermutationsCalculate_list(List<String> Permutating)
        {
            PermutationsCalculator Calc = new PermutationsCalculator();
            return Permutating.ConvertAll<List<object[]>>(new Converter<String, List<object[]>>(ConvO));
        }

        public List<List<String>> PermutationsCalculate_full(List<String> Permutating)
        {
            ByCountComparer bc = new ByCountComparer();
            List<List<String>> _result = CombinationList(Permutating);
            _result.Sort(0, _result.Count, bc);
            return _result;
        }

        public async void Loadqq()
        { await LoadAsync("U:\\Паспорта_и_Макеты", "\\d{5}ДУ\\d{6}"); }

        /// <summary>
        /// Загружает в список FileInfo отобранные по регулярке файлы
        /// </summary>
        /// <param name="DirectoryName">имя обрабатываемой папки</param>
        /// <returns></returns>
        public List<FileInfo> Load(string DirectoryName, string Pattern)
        {
            DirectoryInfo dir = new DirectoryInfo(DirectoryName);
            List<DirectoryInfo> r = dir.GetDirectories("*", SearchOption.TopDirectoryOnly)
             .Where(Name => ((new Regex(Pattern)).IsMatch(Name.Name))).ToList();
            foreach (DirectoryInfo d in r) { Load(d.FullName, Pattern); }
            try
            {
                List<FileInfo> ttt2;
                ttt2 = dir.GetFiles("*", SearchOption.TopDirectoryOnly)
                                .Where(Name => ((new Regex(Pattern)).IsMatch(Name.Name)) && (Name.DirectoryName != "DfsrPrivate"))
                                .ToList();
                Files.AddRange(ttt2);
                return ttt2;

            }
            catch (UnauthorizedAccessException)
            { return null; }
        }

        public Task<List<FileInfo>> LoadAsync(string FileName, string Pattern)
        {
            return Task.Run(() =>

            {
                return Load(FileName, Pattern);

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
                    { p -= 1; }
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
                    { p -= 1; }
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
    public class ByCountComparer : IComparer<List<String>>
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
