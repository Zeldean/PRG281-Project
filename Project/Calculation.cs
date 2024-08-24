using FileStorage;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Project
{
    class Calculation : User
    {
        public Calculation(string filepath, string reportType) : base(filepath)
        {
            // Read user data from the specified file path and return the list from the ReadUserData method
            var AllList = EntryList.ReadUserData(filepath);
            // Calculate and display the total usage based on the specified report type
            TotalUsage(AllList, reportType);
        }

        // Method to calculate and display the total usage based on the report type selected by the user
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
                        Console.Clear();
                        Console.WriteLine("Weekly Report\n==========================================================");
                        Console.WriteLine("Press any key to go back to previous page\n");
                        // Take the last 7 entries from the list and display the total usage for those 7 entries
                        var last7Entries = entries.OrderByDescending(entry => entry.Date).Take(7).ToList();
                        double totalUsage = CalculateTotal(last7Entries);
                        Console.WriteLine($"Total usage of Last 7 Entries: {totalUsage:F2} \n");
                        
                        break;
                    }
                case "Monthly":
                    {
                        Console.Clear();
                        Console.WriteLine("Monthly Report\n==========================================================");
                        Console.WriteLine("Press any key to go back to previous page\n"); 
                        // Filter the entries for the current month and displays the total usage for the month
                        var currentMonthEntries = entries.Where(entry => entry.Date.Month == DateTime.Now.Month && entry.Date.Year == DateTime.Now.Year).ToList();
                        double totalUsage = CalculateTotal(currentMonthEntries);
                        Console.WriteLine($"Total Monthly usage for {DateTime.Now.ToString("MMMM")}: {totalUsage:F2} \n");
                        
                        break;
                    }
                case "Yearly":
                    {
                        Console.Clear();
                        Console.WriteLine("Yearly Report\n==========================================================");
                        Console.WriteLine("Press any key to go back to previous page\n");
                        // Sort the list using IComparable interface in the EntryCompare class.
                        entries.Sort(new EntryCompare());

                        // Calculate and display the prediction for the next month of this year
                        int lastMonth = entries.Max(entry => entry.Date.Month); // Get the last recorded month from the list
                        double totalForPrediction = CalculateTotal(entries);
                        int amountOfMonths = entries.Select(entry => new { entry.Date.Year, entry.Date.Month }).Count();
                        double averagePrediction = totalForPrediction / amountOfMonths;
                        string lastMonthName = new DateTime(DateTime.Now.Year, lastMonth + 1, 1).ToString("MMMM");
                        Console.WriteLine($"Prediction for {lastMonthName} {DateTime.Now.Year}: {averagePrediction:F2}");
                        Console.WriteLine();



                        // Group entries by year
                        var groupedByYear = entries.GroupBy(entry => entry.Date.Year);

                        foreach (var yearGroup in groupedByYear) //Loops through the years to then loop by month to display the data corectly
                        {
                            Console.WriteLine($"{yearGroup.Key}:");

                            // Group entries by month
                            var groupedByMonth = yearGroup.GroupBy(entry => entry.Date.Month);

                            foreach (var monthGroup in groupedByMonth)
                            {
                                double MonthlyUsage = CalculateTotal(monthGroup.ToList());
                                string monthName = new DateTime(yearGroup.Key, monthGroup.Key, 1).ToString("MMMM");
                                Console.WriteLine($"Total usage for {monthName} {yearGroup.Key}: {MonthlyUsage:F2}");
                            }

                            Console.WriteLine();
                        }

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid ReportType"); //Default for wrong options or user input
                        break;
                    }
            }
        }

        // Method to calculate the total usage with consideration to Type2 entries
        private double CalculateTotal(List<(DateTime Date, int Units, string Type)> entries)
        {
            if (entries == null || entries.Count == 0) //Error handeling for if the entries list is empty
                return 0;

            double totalDifference = 0;
            int previousValue = entries.First().Units; //gets the first unit value from the list.
            int type1BeforeType2 = 0;

            for (int i = 1; i < entries.Count; i++)
            {
                if (entries[i].Type == "Type2")
                {
                    // Store the last Type1 value before the Type2
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
                // Reset the sum after the Type2 and Type1 have been added
                if (entries[i].Type == "Type1" && type1BeforeType2 > 0)
                {
                    previousValue = entries[i].Units;
                    type1BeforeType2 = 0;
                }
            }

            return totalDifference;  //returns the total diferece
        }
    }

    // Comparer class to sort entries by date in descending order
    class EntryCompare : IComparer<(DateTime Date, int Units, string Type)>
    {
        public int Compare((DateTime Date, int Units, string Type) First, (DateTime Date, int Units, string Type) Second)
        {
            // Sort dates in descending order to have the latest at the top of the display
            return Second.Date.CompareTo(First.Date);
        }
    }
}
