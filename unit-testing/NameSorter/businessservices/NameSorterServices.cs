using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using NameSorter.model;

namespace NameSorter.BusinessServices
{
    public class NameSorterServices : INameSorter
    {
        #region CRUD
        public void Sort(string path)
        {
            try
            {
                string[] allNames = File.ReadAllLines(path);
                List<Persons> lp = listOfPersons(allNames).OrderBy(a => a.LastName).ThenBy(a => a.score).ToList();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lp.Count; i++)
                {
                    Console.WriteLine(lp[i].LongName);
                    sb.AppendLine(lp[i].LongName);
                }
                saveResult(sb.ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException == null ? ex.Message : ex.Message + " - " + ex.InnerException.Message;
                Console.WriteLine("Failed sort name : " + msg);
            }
        }

        public List<Persons> listOfPersons(string[] listNames)
        {
            List<Persons> lp = new List<Persons>();
            int i = 0;
            foreach(string names in listNames)
            {
                Persons p = new Persons();
                Decimal scr = scoreResult(names, i);
                List<string> ls = names.Split(' ').ToList();
                p.LastName = ls.LastOrDefault();
                p.LongName = names;
                p.score = scr;
                lp.Add(p);
                i++;
            }

            return lp;
        }

        public void saveResult(string result)
        {
            System.IO.StreamWriter fileSort = new System.IO.StreamWriter("sorted-names-list" + ".txt");
            fileSort.WriteLine(result);
            fileSort.Close();
        }

        public Decimal scoreResult(string name, int index)
        {
            Decimal scr = name.ToLower().AsEnumerable().Select(a => a - 96).Sum();
            scr *= index + 1;
            return scr;
        }
        #endregion
    }
}