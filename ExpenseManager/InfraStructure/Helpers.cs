using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseManager.InfraStructure
{
    public class Helpers
    {
        public static string FullAccessProfile
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FullAccessProfile"];
            }
        }             
    }
}