using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Utilities
{
    public class Validator
    {
        public static void ThrowIfStringIsNullEmptyOrLessThan(string str, int minLenght, string message)
        {
            if (string.IsNullOrEmpty(str) || str.Length < minLenght)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
