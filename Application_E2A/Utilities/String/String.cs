using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Application_E2A
{
    /// <summary>
    /// Partial class that serves as a library of methods
    /// </summary>
    public static partial class Utilities
    {
        #region Method SplitIntoSubstrings
        /// <summary>
        /// Returns didived string into list of substrings basing on maxStringLength input
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxSubstringLength"></param>
        /// <returns></returns>
        public static List<string> SplitIntoSubstrings(string text, int maxSubstringLength)
        {
            //Initialize an empty list of strings
            List<string> substrings = new List<string>();
            if( (text == null)||(text == string.Empty) )
                return new List<string>() { string.Empty };

            //create temporary list of words - input string divided with space char
            List<string> parts = new List<string>(text.Split(' '));

            //take into account two scenarios

            // 1. one word
            #region one word
            if (parts.Count == 1)
            {
                if (parts.First().Length <= maxSubstringLength)
                    return new List<string>() { parts.First() };
                else
                {
                    string splitText = string.Copy(parts.First());
                    while (splitText.Length > maxSubstringLength)
                    {
                        substrings.Add(string.Copy(splitText.Substring(0, maxSubstringLength)));
                        splitText = splitText.Substring(maxSubstringLength);
                    }
                    if (splitText.Length > 0)
                        substrings.Add(splitText);
                    return substrings;
                }
            }
            #endregion

            // 2. more than one word
            #region more than one word
            else
            {
                int currentLineLength = 0;
                int count = 0;
                string line = string.Empty;

                foreach (string word in parts)
                {
                    //if given word is the last one in initial string
                    #region last word in list
                    if (count.Equals(parts.Count - 1))
                    {
                        currentLineLength = currentLineLength + word.Length;
                        if (currentLineLength > maxSubstringLength)
                        {
                            substrings.Add(line.TrimEnd());

                            if (word.Length <= maxSubstringLength)
                                substrings.Add(word);
                            else
                            {
                                string splitText = string.Copy(word);
                                while (splitText.Length > maxSubstringLength)
                                {
                                    substrings.Add(string.Copy(splitText.Substring(0, maxSubstringLength)));
                                    splitText = splitText.Substring(maxSubstringLength);
                                }
                                if (splitText.Length > 0)
                                    substrings.Add(splitText);
                            }
                        }
                        else
                        {
                            substrings.Add(line + word);
                        }
                    }
                    #endregion

                    //if given word is not the last one in initial string
                    #region not last word in list
                    else
                    {
                        currentLineLength = currentLineLength + word.Length;
                        if (currentLineLength > maxSubstringLength)
                        {
                            if (!(word.Length > maxSubstringLength))
                            {
                                substrings.Add(line.TrimEnd());
                                line = word + " ";
                                currentLineLength = word.Length + 1;
                            }
                            else
                            {
                                substrings.Add(line.TrimEnd());

                                string splitText = string.Copy(word);
                                while (splitText.Length > maxSubstringLength)
                                {
                                    substrings.Add(string.Copy(splitText.Substring(0, maxSubstringLength)));
                                    splitText = splitText.Substring(maxSubstringLength);
                                }
                                if (splitText.Length > 0)
                                {
                                    line = splitText + " ";
                                    currentLineLength = splitText.Length + 1;
                                }
                            }
                        }
                        else
                        {
                            line = line + word + " ";
                            currentLineLength = currentLineLength + 1;
                        }
                    }
                    count++;
                }
                #endregion

                return substrings;
            }
            #endregion

        }
        #endregion
    }
}
