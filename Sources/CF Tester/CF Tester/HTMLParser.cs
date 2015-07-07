namespace NotACompany.CF_Tester
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class HTMLParser
    {
        /// <summary>
        /// Gets inner HTML string from the first found element with the given class name (e.g. class="sample-test").
        /// </summary>
        /// <param name="sourceString">An HTML string to parse.</param>
        /// <param name="className">Class name.</param>
        /// <returns>An HTML string.</returns>
        public static string getInnerHTML(string sourceString, string className)
        {
            Match match = new Regex(className).Match(sourceString);

            if (!match.Success) return "";

            int lb = match.Index + className.Length;
            for (; lb < sourceString.Length && !(sourceString[lb - 1] == '>' && sourceString[lb - 2] != '\\'); lb++);
            int rb = lb, tmp, bracketCounter = 1;
            
            while (bracketCounter > 0)
            {
                while (rb < sourceString.Length && !(sourceString[rb] == '<' && sourceString[rb - 1] != '\\')) rb++;

                tmp = rb + 1;
                while (tmp < sourceString.Length && !(sourceString[tmp] == '>' && sourceString[tmp - 1] != '\\')) tmp++;

                if (sourceString[rb + 1] == '/') bracketCounter--;
                else if (sourceString[tmp - 1] != '/') bracketCounter++;

                rb++;
            }

            while (!(sourceString[--rb] == '>' && sourceString[rb - 1] != '\\')) ;

            return sourceString.Substring(lb, rb - lb + 1);
        }

        /// <summary>
        /// Gets a list of inner HTML strings from all elements with the given class name (e.g. class="sample-test").
        /// </summary>
        /// <param name="sourceString">An HTML string to parse.</param>
        /// <param name="className">Class name.</param>
        /// <returns>An HTML string.</returns>
        public static List<string> getInnerHTMLList(string sourceString, string className)
        {
            List<string> result = new List<string>();

            MatchCollection matches = new Regex(className).Matches(sourceString);
            foreach (Match match in matches)
            {
                int lb = match.Index + className.Length;
                for (; lb < sourceString.Length && !(sourceString[lb - 1] == '>' && sourceString[lb - 2] != '\\'); lb++) ;
                int rb = lb, tmp, bracketCounter = 1;

                while (bracketCounter > 0)
                {
                    while (rb < sourceString.Length && !(sourceString[rb] == '<' && sourceString[rb - 1] != '\\')) rb++;

                    tmp = rb + 1;
                    while (tmp < sourceString.Length && !(sourceString[tmp] == '>' && sourceString[tmp - 1] != '\\')) tmp++;

                    if (sourceString[rb + 1] == '/') bracketCounter--;
                    else if (sourceString[tmp - 1] != '/') bracketCounter++;

                    rb++;
                }

                while (!(sourceString[--rb] == '>' && sourceString[rb - 1] != '\\')) ;
                result.Add(sourceString.Substring(lb, rb - lb + 1));
            }

            return result;
        }

        /// <summary>
        /// Replaces all "<br />" tags with "\n"
        /// </summary>
        /// <param name="sourceString">Source string.</param>
        /// <returns>A processed string.</returns>
        public static string replaceBrTag(string sourceString)
        {
            return new Regex(@"<[\s]*br[\s]*/[\s]*>").Replace(sourceString, "\n");
        }
    }
}
