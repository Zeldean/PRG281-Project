using FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Calculation : User
    {

        //public string reportType { get => reportType; set => reportType = value; }

        public  Calculation(string filepath) : base(filepath)
        {
            FilePath = filepath;
            var List = Entry.ReadUserData(filepath);
            AvrageUsage(List);
        }




        //Must go throuh the Entries list and if it has a type 2 in the line it adds the type 2 line to the previous line
        public double AvrageUsage(List<(DateTime Date, int Units, string Type)> entries)
        {
            double Avrage;
            int totalDifference = 0;
            int previousValue = entries[0].Units; // tart with the first entry 
            int type1BeforeType2 = 0;

            for (int i = 1; i < entries.Count; i++)
            {
                if (entries[i].Type == "Type2")
                {
                    // Store the last Type1 value before  the Type2
                    type1BeforeType2 = previousValue;
                    // Add the Type2 value to the last Type1 value
                    previousValue += entries[i].Units;
                }
                else
                {
                    int difference = previousValue-entries[i].Units;
                    totalDifference += difference;
                    previousValue = entries[i].Units;
                }

                // Reset the sum after the type 2 and 1 has been added
                if (entries[i].Type == "Type1" && type1BeforeType2 > 0)
                {
                    previousValue = entries[i].Units;
                    type1BeforeType2 = 0; 
                }
            }

            Avrage = totalDifference/entries.Count;
            return Avrage;

        }
    }
}
