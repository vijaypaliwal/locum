using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

// Author: Brother Bill, brotherbill@mail.com
// Copyright 2012: May make copies and changes freely.
namespace AutoComplete.Controllers
{
    public static class MyString
    {
        /// <summary>
        /// Does haystack contain needle at position? Case Ignorant.
        /// </summary>
        public static bool MatchesWith(this string haystack, string needle, string position)
        {
            bool result = false;

            // If needle is too big, doesn't match
            if (needle.Length <= haystack.Length)
            {
                switch (position.ToLower())
                {
                    case "startswith":
                    default:
                        result = string.Compare(haystack, 0, needle, 0, needle.Length, true) == 0;
                        break;

                    case "endswith":
                        int startHaystack = haystack.Length - needle.Length;
                        result = string.Compare(haystack, startHaystack, needle, 0, needle.Length, true) == 0;
                        break;

                    case "contains":
                        result = haystack.ToLower().Contains(needle.ToLower());
                        break;
                }
            }

            return (result);
        }

    }
}