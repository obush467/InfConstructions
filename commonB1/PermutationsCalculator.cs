namespace commonB1
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Perestanovki
    {
        public class PermutationsCalculator
        {
            public List<object[]> ans = new List<object[]>();
            public void Swap(object[] arr, int i, int j)//поменять местами i-ый и j-ый элемент
            {
                object temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            public bool IsCorrect(int[] arr)//проверка на моноцикличность
            {
                int first = arr[0];
                int temp = first;
                int i = 0;
                while (i < arr.Length && first != arr[temp - 1])
                {
                    i++;
                    temp = arr[temp - 1];
                }
                return i == arr.Length - 1 && first == arr[temp - 1];
            }
            public void Generate(int k, int n, object[] arr)//генерация всех перестановок длины n
            {
                if (k == n)
                {

                    this.ans.Add((object[])arr.Clone());

                }
                else
                {
                    for (int j = k; j < arr.Length; j++)
                    {
                        Swap(arr, k, j);
                        Generate(k + 1, n, arr);
                        Swap(arr, k, j);
                    }
                }
            }

            public virtual string[] PermutationsCalculate_StringsArray(string[] args)
            {

                int n = args.Length;
                this.Generate(0, n, args);
                string[] stringAns = new string[ans.Count];
                for (int i = 0; i < ans.Count; i++)
                {
                    StringBuilder temp = new StringBuilder();
                    for (int j = 0; j < n; j++)
                    {
                        temp.Append(ans[i][j]);
                        temp.Append(' ');
                    }
                    stringAns[i] = temp.ToString();
                }
                return stringAns;

            }

            public virtual string[][] PermutationsCalculate_Array(string[] args)
            {
                int n = args.Length;
                this.Generate(0, n, args);
                string[][] stringAns = new string[ans.Count][];
                for (int i = 0; i < ans.Count; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        stringAns[i][j] = ans[i][j].ToString();
                    }
                }
                return stringAns;
            }

            public virtual List<String> PermutationsCalculate_StringsList(List<String> list)
            {
                List<String> wl = new List<String>();
                String[] wArr = list.ToArray();
                foreach (String ListItem in PermutationsCalculate_StringsArray(wArr))
                {
                    wl.Add(ListItem);
                }
                return wl;
            }


            public virtual List<object[]> PermutationsCalculate_List(List<String> list)
            {
                String[] wArr = list.ToArray();
                int n = wArr.Length;
                this.Generate(0, n, wArr);
                return ans;
            }
        }
    }
}