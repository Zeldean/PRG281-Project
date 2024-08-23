using FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Project
{
    internal class Calculation : User
    {

        //public string reportType { get => reportType; set => reportType = value; }

        public Calculation(string filepath, string reportType) : base(filepath)
        {
            var List = EntryList.ReadUserData(filepath);
            TotalUsage(List, reportType);
        }

        public void TotalUsage(List<(DateTime Date, int Units, string Type)> entries, string ReportType)
        {
            if (entries == null || entries.Count == 0)
            {
                Console.WriteLine("No entries available.");
                return;
            }

            switch (ReportType)
            {
                case "Weekly":
                    {
                        // Take the last 7 entries from the list adn display total 
                        var last7Entries = entries.OrderByDescending(entry => entry.Date).Take(7).ToList();
                        double totalUsage = CalculateTotal(last7Entries);
                        Console.WriteLine($"Total usage of Last 7 Entries: {totalUsage:F2}");
                        break;
                    }
                case "Monthly":
                    {
                        // Filter the entries for the current month
                        var currentMonthEntries = entries.Where(entry => entry.Date.Month == DateTime.Now.Month && entry.Date.Year == DateTime.Now.Year).ToList();
                        double totalUsage = CalculateTotal(currentMonthEntries);
                        Console.WriteLine($"Total Monthly usage for {DateTime.Now.ToString("MMMM")}: {totalUsage:F2}");
                        break;
                    }
                case "Yearly":
                    {
                        // Group entries by month and dispolaty total for each month
                        var groupedByMonth = entries.GroupBy(entry => entry.Date.Month);

                        int lastMonth = groupedByMonth.Max(month => month.Key);  //get last month recoorded
                        double totalForPredictuion = CalculateTotal(entries);
                        int ammountOfMonthes = groupedByMonth.Count();
                        double avragePrediction = totalForPredictuion / ammountOfMonthes;

                        foreach (var monthGroup in groupedByMonth)
                        {
                            double MonthlyUsage = CalculateTotal(monthGroup.ToList());

                            string monthName = new DateTime(1, monthGroup.Key, 1).ToString("MMMM");
                            Console.WriteLine($"Total usage for {monthName}: {MonthlyUsage:F2}");
                        }


                        //disply prediction
                        string lastMonthName = new DateTime(1, lastMonth + 1, 1).ToString("MMMM");
                        Console.WriteLine($"Prediction for {lastMonthName}: {avragePrediction}");
                        //predicted month based on the avrages

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid ReportType");
                        break;
                    }
            }
        }
        /*Must go throuh the Entries list and if it has a type 2 in the line it adds the type 2 line to the previous */
        private double CalculateTotal(List<(DateTime Date, int Units, string Type)> entries)
        {
            if (entries == null || entries.Count == 0)
                return 0;

            int totalDifference = 0;
            int previousValue = entries.First().Units;
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
                    int difference = previousValue - entries[i].Units;
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

            return (double)totalDifference;
        }

    }
    
}
