using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DotNetBucketMvc.Common
{
    public class AppConfig
    {
        public static string AWSAccessKey
        {
            get
            {
              
                return  ConfigurationManager.AppSettings["AWSAccessKey"];

            }
        }
           public static string AWSSecretKey
        {
            get
            {
              
                return ConfigurationManager.AppSettings["AWSSecretKey"];
            }
        }
           public static string AWSProfileName
        {
            get
            {
              
                return  ConfigurationManager.AppSettings["AWSProfileName"];
            }
        }

         public static bool S3Enable
        {
            get
            {
              
                return Convert.ToBoolean( ConfigurationManager.AppSettings["S3Enable"]);

            }
        }
      
    }
}