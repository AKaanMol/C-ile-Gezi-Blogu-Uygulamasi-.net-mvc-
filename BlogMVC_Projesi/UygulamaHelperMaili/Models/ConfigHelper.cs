using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UygulamaHelperMaili.Models
{
    public class ConfigHelper
    {

        public string Get1(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }


        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }
    }
}