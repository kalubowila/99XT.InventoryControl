using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace _99XT.InventoryControl.Utility
{
    public static class FileHandler
    {
        private static string file = GetApplicationRoot() + @"\Resources\AppKeys.json";

        public static string GetValueByKey(string key)
        {
            using (StreamReader r = new StreamReader(file))
            {
                var details = JObject.Parse(r.ReadToEnd());
                return details[key].ToString();
            }
        }

        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(99XT.InventoryControl)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}
