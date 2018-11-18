using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList_Library
{
    /// <summary>
    /// class that extends string
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// extension method to split a string into several lines
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String[] SplitLines(this String str, Boolean removeEmptyEntries)
        {
            char[] separators = { '\n', '\r' };
            return str.Split(separators, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }


    }
}
