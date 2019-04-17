using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebFinalApi.Service
{
    public class BaseService
    {
        
        public string GenerateInsertSql<T>(T dataModel,IEnumerable<string> fileNames)
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
            return $"{cellTitles.TrimEnd(',')} {sqlResult}  {cellValues.TrimEnd(',')}";
        }

    }
}