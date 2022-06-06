using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ERP.Exceptions.ErpExceptions;
using ERP.BaseLib.Serialization;

namespace ERP.Business.Filtering
{
    public class FilterList
    {
        public FilterList(string Serialized)
        {
            list = Json.Deserialize<Dictionary<string, string>>(Serialized);
        }

        public FilterList()
        {
        }

        public enum FilterOption
        { Minimum, Maximum }

        public IEnumerable<KeyValuePair<string, string>> Elements { get => list; }

        private Dictionary<string, string> list { get; } = new();

        public void Add(string PropertyName, Regex Regex)
        {
            if (!list.ContainsKey(PropertyName))
            {
                list.Add(PropertyName, Regex.ToString());
            }
        }

        public void Add(string PropertyName, int Min, int Max)
        {
            if (Min > Max) { throw new ErpException("Min may not be greater than Max"); }
            List<List<int>> minimumNum = new List<List<int>>();
            List<List<int>> maximumNum = new List<List<int>>();

            string rgxresult = "";

            int[] minstr = Min.ToString().Replace(".", "").Replace(",", "").Select(o => int.Parse(o.ToString())).ToArray();
            int[] maxstr = Max.ToString().Replace(".", "").Replace(",", "").Select(o => int.Parse(o.ToString())).ToArray();

            //Generating Numbers for Maximum
            if (minstr.Length < maxstr.Length)
            {
                for (int i = 0; i < maxstr.Length; i++)
                {
                    maximumNum.Add(new());
                    for (int j = 0; j < maxstr.Length; j++)
                    {
                        int num = maxstr[j];
                        if (i != maxstr.Length)
                        {
                            if (i == j)
                            {
                                num = num - 1;
                            }
                            if (i < j)
                            {
                                num = 9;
                            }
                        }
                        if (num < 0) { num = 0; }
                        maximumNum[i].Add(num);
                    }
                }
            }
            else
            {
                for (int i = 0; i < maxstr.Length + maxstr.Length - 1; i++)
                {
                    maximumNum.Add(new());
                    for (int j = 0; j < maxstr.Length; j++)
                    {
                        int num = maxstr[j];
                        if (i < maxstr.Length - 1)
                        {
                            if (j == 0)
                            {
                                num = maxstr[j] - 1;
                            }
                            else
                            {
                                num = 9;
                            }
                        }
                        if (i >= maxstr.Length)
                        {
                            int altI = i - maxstr.Length + 1;
                            if (altI != maxstr.Length + maxstr.Length - 1)
                            {
                                if (altI > j)
                                {
                                    num = maxstr[j];
                                }
                                if (altI == j)
                                {
                                    num = maxstr[j] - 1;
                                }
                                if (altI < j)
                                {
                                    num = 9;
                                }
                            }
                            else
                            {
                                num = maxstr[j];
                            }
                        }
                        if (num < 0) { num = 0; }
                        maximumNum[i].Add(num);
                    }
                }
            }

            //Generating Numbers for Minimum
            if (minstr.Length < maxstr.Length)
            {
                for (int i = 0; i < minstr.Length; i++)
                {
                    minimumNum.Add(new());
                    for (int j = 0; j < minstr.Length; j++)
                    {
                        int num = minstr[j];
                        if (i != minstr.Length - 1)
                        {
                            if (i == j)
                            {
                                num = num + 1;
                            }
                            if (i < j)
                            {
                                num = 0;
                            }
                        }
                        if (num < 0) { num = 0; }
                        minimumNum[i].Add(num);
                    }
                }
            }
            else
            {
                for (int i = 0; i < minstr.Length + minstr.Length - 1; i++)
                {
                    minimumNum.Add(new());
                    for (int j = 0; j < minstr.Length; j++)
                    {
                        int num = minstr[j];
                        if (i < minstr.Length - 1)
                        {
                            if (i > j)
                            {
                                num = minstr[j];
                            }
                            if (i == j)
                            {
                                num = minstr[j] + 1;
                            }
                            if (i < j)
                            {
                                num = 0;
                            }
                        }
                        if (i >= minstr.Length)
                        {
                            int altI = i - minstr.Length + 1;
                            if (altI != minstr.Length + minstr.Length - 1)
                            {
                                if (j == 0)
                                {
                                    num = minstr[j] + 1;
                                }
                                else
                                {
                                    num = 0;
                                }
                            }
                            else
                            {
                                num = minstr[j];
                            }
                        }
                        if (num < 0) { num = 0; }
                        minimumNum[i].Add(num);
                    }
                }
            }

            //Generating String
            if (minstr.Length < maxstr.Length)
            {
                foreach (List<int> list in maximumNum)
                {
                    if (rgxresult.EndsWith("]")) { rgxresult += "|"; }
                    foreach (int i in list)
                    {
                        rgxresult += $"[{0}-{i}]";
                    }
                }
                foreach (List<int> list in minimumNum)
                {
                    if (rgxresult.EndsWith("]")) { rgxresult += "|"; }
                    foreach (int i in list)
                    {
                        rgxresult += $"[{i}-{9}]";
                    }
                }
            }
            else
            {
                for (int i = 0; i < minimumNum.Count; i++)
                {
                    if (rgxresult.EndsWith("]")) { rgxresult += "|"; }
                    for (int j = 0; j < minimumNum[i].Count; j++)
                    {
                        rgxresult += $"[{minimumNum[i][j]}-{maximumNum[i][j]}]";
                    }
                }
            }

            rgxresult = $"\\G({rgxresult})$";

            try
            {
                Add(PropertyName, new Regex(rgxresult));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(string PropertyName, string SearchString)
        {
            try
            {
                Add(PropertyName, new Regex($"\\G({SearchString.Replace("[", "\\[").Replace("(", "\\(").Replace("]", "\\]").Replace(")", "\\)").Replace(".", "\\.").Replace("*", ".*")})$"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(string PropertyName, int Value, FilterOption FilterOption)
        {
            List<List<int>> minimumNum = new List<List<int>>();
            List<List<int>> maximumNum = new List<List<int>>();

            string rgxresult = "";

            int[] minstr = Value.ToString().Replace(".", "").Replace(",", "").Select(o => int.Parse(o.ToString())).ToArray();
            int[] maxstr = Value.ToString().Replace(".", "").Replace(",", "").Select(o => int.Parse(o.ToString())).ToArray();

            if (FilterOption == FilterOption.Maximum)
            {
                //Generating Numbers for Maximum
                if (minstr.Length < maxstr.Length)
                {
                    for (int i = 0; i < maxstr.Length; i++)
                    {
                        maximumNum.Add(new());
                        for (int j = 0; j < maxstr.Length; j++)
                        {
                            int num = maxstr[j];
                            if (i != maxstr.Length)
                            {
                                if (i == j)
                                {
                                    num = num - 1;
                                }
                                if (i < j)
                                {
                                    num = 9;
                                }
                            }
                            if (num < 0) { num = 0; }
                            maximumNum[i].Add(num);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < maxstr.Length + maxstr.Length - 1; i++)
                    {
                        maximumNum.Add(new());
                        for (int j = 0; j < maxstr.Length; j++)
                        {
                            int num = maxstr[j];
                            if (i < maxstr.Length - 1)
                            {
                                if (j == 0)
                                {
                                    num = maxstr[j] - 1;
                                }
                                else
                                {
                                    num = 9;
                                }
                            }
                            if (i >= maxstr.Length)
                            {
                                int altI = i - maxstr.Length + 1;
                                if (altI != maxstr.Length + maxstr.Length - 1)
                                {
                                    if (altI > j)
                                    {
                                        num = maxstr[j];
                                    }
                                    if (altI == j)
                                    {
                                        num = maxstr[j] - 1;
                                    }
                                    if (altI < j)
                                    {
                                        num = 9;
                                    }
                                }
                                else
                                {
                                    num = maxstr[j];
                                }
                            }
                            if (num < 0) { num = 0; }
                            maximumNum[i].Add(num);
                        }
                    }
                }
            }

            if (FilterOption == FilterOption.Minimum)
            {
                //Generating Numbers for Minimum
                if (minstr.Length < maxstr.Length)
                {
                    for (int i = 0; i < minstr.Length; i++)
                    {
                        minimumNum.Add(new());
                        for (int j = 0; j < minstr.Length; j++)
                        {
                            int num = minstr[j];
                            if (i != minstr.Length - 1)
                            {
                                if (i == j)
                                {
                                    num = num + 1;
                                }
                                if (i < j)
                                {
                                    num = 0;
                                }
                            }
                            if (num < 0) { num = 0; }
                            minimumNum[i].Add(num);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < minstr.Length + minstr.Length - 1; i++)
                    {
                        minimumNum.Add(new());
                        for (int j = 0; j < minstr.Length; j++)
                        {
                            int num = minstr[j];
                            if (i < minstr.Length - 1)
                            {
                                if (i > j)
                                {
                                    num = minstr[j];
                                }
                                if (i == j)
                                {
                                    num = minstr[j] + 1;
                                }
                                if (i < j)
                                {
                                    num = 0;
                                }
                            }
                            if (i >= minstr.Length)
                            {
                                int altI = i - minstr.Length + 1;
                                if (altI != minstr.Length + minstr.Length - 1)
                                {
                                    if (j == 0)
                                    {
                                        num = minstr[j] + 1;
                                    }
                                    else
                                    {
                                        num = 0;
                                    }
                                }
                                else
                                {
                                    num = minstr[j];
                                }
                            }
                            if (num < 0) { num = 0; }
                            minimumNum[i].Add(num);
                        }
                    }
                }
            }

            //Generating String
            if (FilterOption == FilterOption.Minimum)
            {
                foreach (List<int> list in minimumNum)
                {
                    if (rgxresult.EndsWith("]")) { rgxresult += "|"; }
                    rgxresult += $"[{0}-{9}]*";
                    foreach (int i in list)
                    {
                        rgxresult += $"[{i}-{9}]";
                    }
                }
            }
            else
            {
                foreach (List<int> list in maximumNum)
                {
                    if (rgxresult.EndsWith("?")) { rgxresult += "|"; }
                    foreach (int i in list)
                    {
                        rgxresult += $"[{0}-{i}]?";
                    }
                }
            }

            rgxresult = $"\\G({rgxresult})$";

            try
            {
                Add(PropertyName, new Regex(rgxresult));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Serialize()
        {
            return Json.Serialize(list);
        }
    }
}