using System;
using System.IO;
using System.Globalization;
using System.Text;

namespace DotNetAvailableCulturesInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputHelper oh;
            bool ToFile = false;

            if (args.Length >= 2 && (args[0].ToLowerInvariant() == "-o" || args[0].ToLowerInvariant() == "/o"))
            {
                //Write to file
                //For example: -o DotNetAvailableCulturesInfo.txt
                oh = new OutputHelper(
                                new StreamWriter(args[1],   //File path/name
                                        false, //Overwrite if exists
                                        new UTF8Encoding(true) //UTF-8 without BOM
                                    ),
                                    true    //Generate CSV output
                                );
                ToFile = true;
            }
            else
            { 
                oh = new OutputHelper(Console.Out);
                Console.OutputEncoding = System.Text.Encoding.Unicode;
            }


            //Headers
            oh.Write("CULTURE", 12);
            oh.Write("ISO-2L", 7);
            oh.Write("ISO-3L", 7);
            oh.Write("WINDOWSNAME", 12);
            oh.Write("DISPLAYNAME", 50);
            oh.WriteLine("ENGLISHNAME", 50);

            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                oh.Write(ci.Name, 12);
                oh.Write(ci.TwoLetterISOLanguageName, 7);
                oh.Write(ci.ThreeLetterISOLanguageName, 7);
                oh.Write(ci.ThreeLetterWindowsLanguageName, 12);
                oh.Write(ci.DisplayName, 50);
                oh.WriteLine(ci.EnglishName, 50);
            }

            //Additional information thru console
            if (ToFile)
                Console.WriteLine("Available culture information written to: '{0}'", args[1]);
            else
            {
                Console.WriteLine();
                Console.WriteLine("You can write this information to a CSV file with the -o or /o option indicating a file name or path. For example:");
                Console.WriteLine();
                Console.WriteLine("\tDotNetAvailableCulturesInfo -o culturelist.csv");
                Console.WriteLine();
                Console.WriteLine("The resulting file will display all the characters correctly and will have the UTF-8 BOM for Excel to identify the encoding correctly.");
            }
        }

    }

    // Used interchangeably to output information thru different streams
    internal class OutputHelper
    {
        private readonly bool _generateCSV;  //When false, will output comma-separated values
        private readonly TextWriter _strm;

        internal OutputHelper(TextWriter strm, bool generateCSV = false)
        {
            _strm = strm;
            _generateCSV = generateCSV;
        }

        internal void Write(string info, int paddingWidth)
        {
            if (_generateCSV)
                _strm.Write("{0},", Escape(info));
            else
                _strm.Write("{0, -" + paddingWidth + "}", info);
        }

        internal void WriteLine(string info, int paddingWidth)
        {
            if (_generateCSV)
                _strm.WriteLine(Escape(info));
            else
                _strm.WriteLine("{0, -" + paddingWidth + "}", info);
        }

        //Escapes strings with comma
        internal string Escape(string data)
        {
            if (data.IndexOf(',') >= 0)
                return "\"" + data + "\"";
            else
                return data;
        }
    }
}
