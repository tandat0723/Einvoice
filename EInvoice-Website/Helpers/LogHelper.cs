using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CRM
{
    public static class LogHelper
    {
        public static void SaveLog(string msg )
        {
            try
            {
                string folder = HttpContext.Current.Server.MapPath("~/Logs/");
                if (System.IO.Directory.Exists(folder) == false) System.IO.Directory.CreateDirectory(folder);

                string  path = Path.Combine(HttpContext.Current.Server.MapPath("~/Logs/"), DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path,true))
                {
                    file.WriteLine(msg);  
                }

            }
            catch(Exception ex) { }
        }
    }

}