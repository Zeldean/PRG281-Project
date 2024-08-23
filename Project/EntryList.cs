using FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project 
{


    internal class EntryList : User
    {
       
        public EntryList(string filepath):base(filepath)
        {
           
            ReadEntries(filepath);
        }

        public static List<(DateTime date, int units, string type)> ReadUserData(string filePath)
        {
            List<(DateTime date, int units, string type)> list = new List<(DateTime, int, string)>();
            string[] lines = File.ReadAllLines(filePath);
            int index = 0;

            for (index = 0; index < lines.Length; index++)
            {
                if (lines[index] == "ENTRIES")
                {
                    break;
                }
            }

            var entries = new ArraySegment<string>(lines, index + 1, lines.Length - (index + 1));

            foreach (string entry in entries)
            {

                string[] parts = entry.Split(',');

                string dateString = parts[0].Trim('(', ')');



                DateTime.TryParse(dateString, out DateTime date);




                int units = int.Parse(parts[1]);
                string type = parts[2];
                list.Add((date, units, type));

            }
            return list;
        }



        public void ReadEntries(string filePath) 
        {
            
            List<String> entries = new List<String>();
            List<String> Fulltxt = new List<String>();

            Fulltxt = File.ReadAllLines(filePath).ToList();

            //string toBeSearched = ",";

            bool data = false;
            foreach (string line in Fulltxt)
            {
                if (data)
                {
                    //string Seper = line.Substring(line.IndexOf(toBeSearched) + toBeSearched.Length, 4);
                    entries.Add(line);
                }
                else if (line == "ENTRIES")
                {
                    data = true;
                }
            }

            foreach (var item in entries)
            {
                Console.WriteLine(item);
            }

        }





        //public List<String> ReadUnits(string filePath)
        //{
        //    List<String> Units = new List<String>();
        //    List<String> Fulltxt = new List<String>();

        //    Fulltxt = File.ReadAllLines(filePath).ToList();

        //    string toBeSearched = ",";

        //    bool data = false;
        //    foreach (string line in Fulltxt)
        //    {
        //        if (data)
        //        {
        //            string Seper = line.Substring(line.IndexOf(toBeSearched) + toBeSearched.Length, 4);
        //            Units.Add(Seper);

        //        }
        //        else if (line == "ENTRIES")
        //        {
        //            data = true;
        //        }
        //    }
        //    return Units;
        //}



    }
}
