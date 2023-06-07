using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GridView_Editing_With_SP
{
    public class ReadCS
    {
        public static string ConStr
        {
            get { return ConfigurationManager.ConnectionStrings["cs"].ConnectionString; }
        }
    }
}