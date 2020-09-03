using System;
using System.IO;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

namespace DotNetAvailableCulturesInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputHelper oh;
            if (args.Length > 0 && args[0] == "")
                oh = new OutputHelper(Console.Out);
            else
                oh = new OutputHelper(Console.Out);

            oh.WriteLine("CULTURE,ISO,ISO,WIN,DISPLAYNAME,ENGLISHNAME");
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                oh.Write("{0, -7}", ci.Name);
                oh.Write(" {0, -3}", ci.TwoLetterISOLanguageName);
                oh.Write(" {0, -3}", ci.ThreeLetterISOLanguageName);
                oh.Write(" {0, 3}", ci.ThreeLetterWindowsLanguageName);
                oh.Write(" {0, -40}", ci.DisplayName);
                oh.WriteLine("{0, -40}", ci.EnglishName);
            }
        }

    }

    // Used interchangeably to output information thru different streams
    internal class OutputHelper
    {
        private readonly TextWriter _strm;

        internal OutputHelper(TextWriter strm)
        {
            _strm = strm;
        }

        internal void Write(string info, params object[] arg)
        {
            _strm.Write(info, arg);
        }

        internal void WriteLine(string info, params object[] arg)
        {
            _strm.WriteLine(info, arg);
        }
    }
}
