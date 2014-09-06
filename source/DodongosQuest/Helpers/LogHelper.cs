using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace xnaHelper.Helpers
{
    public class LogHelper
    {
        public static void LogInfo(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
