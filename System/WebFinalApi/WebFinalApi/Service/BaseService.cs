using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebFinalApi.Service
{
    public class BaseService
    {
        
        /// <summary>
        /// Insert语句生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public string InsertSqlGenerate<T>(T dataModel,IEnumerable<string> fileNames)
        {
            string sqlResult = " VALUES ";
            var type = dataModel.GetType();//获取属性集合 
            PropertyInfo[] pi = type.GetProperties();//获取属性集合   
            string cellTitles = string.Empty;
            string cellValues = string.Empty;
            foreach (PropertyInfo p in pi)
            {
                if (fileNames.Contains(p.Name))
                {
                    try
                    {
                        cellTitles += p.Name + " ,";
                        cellValues += $" @{p.Name} ,";
                    }
                    catch
                    {
                    }
                }
            }
            return $"({cellTitles.TrimEnd(',')}) {sqlResult} ({cellValues.TrimEnd(',')})";
        }

        /// <summary>
        /// SELECT语句生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public string SelectSqlGenerate<T>(T dataModel ,IEnumerable<string> fileNames)
        {
            var type = dataModel.GetType();//获取属性集合 
            PropertyInfo[] pi = type.GetProperties();//获取属性集合   
            List<string> allConditon = new List<string>();
            foreach (PropertyInfo p in pi)
            {
                if (fileNames.Contains(p.Name))
                {
                    try
                    {
                        allConditon.Add($" {p.Name} = @{p.Name} ");
                    }
                    catch
                    {
                    }
                }
            }
            return $" WHERE {string.Join("AND", allConditon)}" ;
        }

        public string UpdateSqlGenerate<T>(T dataModel, IEnumerable<string> updateFileNames, IEnumerable<string> accordingFileNames)
        {
            var type = dataModel.GetType();//获取属性集合 
            PropertyInfo[] pi = type.GetProperties();//获取属性集合   
            List<string> allConditon = new List<string>();

            List<string> allConditon2 = new List<string>();

            foreach (PropertyInfo p in pi)
            {
                try
                {
                    if (updateFileNames.Contains(p.Name))
                    {

                        allConditon.Add($" {p.Name} = @{p.Name} ");

                    }
                    if (accordingFileNames.Contains(p.Name))
                    {
                        allConditon2.Add($" {p.Name} = @{p.Name} ");
                    }
                }
                catch
                {
                }
            }
            return $" SET {string.Join(",", allConditon)} WHERE {string.Join("AND", allConditon2)}";
        }

    }
}