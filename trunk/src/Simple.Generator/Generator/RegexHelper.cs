﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simple.Generator
{
    public static class RegexHelper
    {
        public const string ListRegexString = @"\(?(\s*(?<value>\w+)\s*,)*(\s*(?<value>\w+)\s*)\)?";
        
        private static Regex _listRegex = new Regex(ListRegexString.ToRegexFormat(true), RegexOptions.Compiled);
        public static Regex ListRegex { get { return _listRegex; } }



        static Regex _spaces1 = new Regex(@"\s+");
        static Regex _spaces2 = new Regex(@"(\w)([\[\(\{])");
        static Regex _spaces3 = new Regex(@"([\]\)\}])(\w)");

        public static string CorrectInput(this string x)
        {
            x = _spaces1.Replace(x, " ");
            x = _spaces2.Replace(x, EvaluateSpaces1);
            x = _spaces3.Replace(x, EvaluateSpaces2);

            return x;
        }

        public static string ToRegexFormat(this string x, bool mustBeFirst)
        {
            return (mustBeFirst ? "^" : "") + @"\s*" + x + @"(\s|$)";
        }

        private static string EvaluateSpaces1(Match match)
        {
            return match.Groups[1] + " " + match.Groups[2];
        }

        private static string EvaluateSpaces2(Match match)
        {
            return match.Groups[1] + " " + match.Groups[2];
        }
    }
}
