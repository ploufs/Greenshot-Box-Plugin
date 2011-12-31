﻿/*
Copyright (c) 2011 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/
using System;
using System.Text;

namespace GreenshotBoxPlugin
{
   public static class ValueTypeExtensions
    {
        #region ToBase64String

        /// <summary>
        /// Converts a byte array into a base 64 string
        /// </summary>
        /// <param name="Input">Input array</param>
        /// <returns>The equivalent byte array in a base 64 string</returns>
        public static string ToBase64String(this byte[] Input)
        {

            if (Input == null)
                return string.Empty;
            else
            return Convert.ToBase64String(Input);
        }

        #endregion

        #region ToEncodedString

        /// <summary>
        /// Converts a byte array to a string
        /// </summary>
        /// <param name="Input">input array</param>
        /// <param name="EncodingUsing">The type of encoding the string is using (defaults to UTF8)</param>
        /// <param name="Count">Number of bytes starting at the index to convert (use -1 for the entire array starting at the index)</param>
        /// <returns>string of the byte array</returns>
        public static string ToEncodedString(this byte[] Input, Encoding EncodingUsing = null, int Index = 0, int Count = -1)
        {
            if (Input==null)
                return "";
            if (Count == -1)
                Count = Input.Length - Index;
            if (EncodingUsing == null)
                EncodingUsing = new UTF8Encoding();
            return EncodingUsing.GetString(Input, Index, Count);
        }

        #endregion
    }
}
