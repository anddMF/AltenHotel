using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Entities
{
    public class AppSettings
    {
        //public static AppConfiguration AppConfiguration { get; set; }
        public static string ConnectionString { get; set; }
    }

    public class AppConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
