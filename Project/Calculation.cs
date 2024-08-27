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



        public double UseageEstimation(int endRange, int span, List<(DateTime, int, string, int)> useageList)
        {
            //double tempUsage = useageList[endRange].Item4 - useageList[0].Item4;
            double tempUsage = useageList[endRange].Item4 - useageList[useageList.Count() - 1].Item4 ;
            double days = (useageList[endRange].Item1 - useageList[useageList.Count() - 1].Item1).Days;
            double spanUseage = Math.Round(tempUsage / (days / span));
            return spanUseage;
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Weekly Report:\n==========================================================\n");
                       
                        // Take the last 7 entries from the list and display the total usage for those 7 entries
                        
                        List<(DateTime, int, string, int)> useageList = CalculateTotal(entries.Where(entry => entry.Date.Month == DateTime.Now.Month && entry.Date.Year == DateTime.Now.Year).ToList());



                        //foreach (var entry in useageList)
                        //{
                        //    Console.WriteLine($"{entry.Item1}, {entry.Item2}, {entry.Item3}, {entry.Item4}");
                        //}

                        int count = 0;
                        int stopIndex;
                        while (true)
                        {   
                            count++;
                            if ((useageList[useageList.Count() - 1].Item1 - useageList[useageList.Count() - 1 - count].Item1).Days >= 7)
                            {
                                stopIndex = useageList.Count() - 1 - count;
                                break;
                            }
                        }
                        
                        double weeklyUseage = UseageEstimation(stopIndex, 7, useageList);

                        Console.WriteLine($"USage for 7 days: {weeklyUseage:F2} \n");
                        Console.WriteLine("\n==========================================================\nPress any key to go back to the previous page\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                case "Monthly":
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Monthly Report:\n==========================================================");
                        
                        // Filter the entries for the current month and displays the total usage for the month
                        var currentMonthEntries = entries.Where(entry => entry.Date.Month == DateTime.Now.Month && entry.Date.Year == DateTime.Now.Year).ToList();
                       
                        List<(DateTime, int, string, int)> currentMonthEntriesUseage = CalculateTotal(entries.Where(entry => entry.Date.Month == DateTime.Now.Month && entry.Date.Year == DateTime.Now.Year).ToList());
                        int daysInMounth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                        //foreach (var entry in currentMonthEntriesUseage)
                        //{
                        //    Console.WriteLine($"{entry.Item1}, {entry.Item2}, {entry.Item3}, {entry.Item4}");
                        //}

                        double mounthlyUseage = UseageEstimation(currentMonthEntriesUseage[0].Item4, daysInMounth, currentMonthEntriesUseage);

                        Console.WriteLine($"Total Monthly usage for {DateTime.Now.ToString("MMMM")}: {mounthlyUseage:F2} \n");
                        Console.WriteLine("\n==========================================================\nPress any key to go back to the previous page\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                case "Yearly":
                    {

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Yearly Report:\n==========================================================\n");
                        

                        // Sort the list using the EntryCompare class to ensure acending order order by date.
                        entries.Sort(new EntryCompare());

                        // Filter the entries for the current year
                        List<(DateTime, int, string, int)> currentYearEntries = CalculateTotal(entries.Where(entry => entry.Date.Year == DateTime.Now.Year).ToList());


                        //foreach (var entry in currentYearEntries)
                        //{
                        //    Console.WriteLine($"{entry.Item1}, {entry.Item2}, {entry.Item3}, {entry.Item4}");

                        //}


                        var groupedByMonth = currentYearEntries.GroupBy(entry => entry.Item1.Month).ToList();
                        double Sum = 0; 
                        foreach (var monthGroup in groupedByMonth.OrderBy(g => g.Key))
                        {
                            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, monthGroup.Key);

                            // Convert the monthGroup to a list
                            List<(DateTime, int, string, int)> monthGroupList = monthGroup.ToList();

                            double mounthlyUsageForYear = UseageEstimation(0, daysInMonth, monthGroupList);
                            Sum += mounthlyUsageForYear;

                            string monthName = new DateTime(DateTime.Now.Year, monthGroup.Key, 1).ToString("MMMM yyyy");
                            Console.WriteLine($"{monthName}: {mounthlyUsageForYear:F2}");
                        }

                        DateTime now = DateTime.Now;

                        // Determine the next month
                        DateTime nextMonth = now.AddMonths(1);
                        int nextMonthNumber = nextMonth.Month;
                        int nextYear = nextMonth.Year;


                        
                        string nextMonthName = new DateTime(nextYear, nextMonthNumber, 1).ToString("MMMM yyyy");
                        
                        int numberOfMonths = groupedByMonth.Count();

                        double avrageforPrediction = Math.Round(Sum/numberOfMonths, 2);

                        Console.WriteLine($"\nPrediction of units needed for {nextMonthName}: {avrageforPrediction}");
                        Console.WriteLine("\n==========================================================\nPress any key to go back to the previous page\n");
                        Console.ForegroundColor = ConsoleColor.White;
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
        private List<(DateTime, int, string, int)> CalculateTotal(List<(DateTime Date, int Units, string Type)> entries)
        {
            if (entries == null || entries.Count == 0) //Error handeling for if the entries list is empty
                return null;

            double totalDifference = 0;

            Tuple<int, int> previousValueSet = new Tuple<int, int>(0, 0);
            int index = 0;
            while (true)
            {
                if (entries[index].Type != "Type2")
                {
                    previousValueSet = new Tuple<int, int>(entries[index].Units, index);
                    break;
                }
                index++;
            }

            int type1BeforeType2 = 0;
            List<(DateTime, int, string, int)> usageList = new List<(DateTime, int, string, int)>();

            int preVal = previousValueSet.Item1, totalUsage = 0;
            for (int pos = previousValueSet.Item2; pos < entries.Count; pos++)
            {
                if (entries[pos].Type == "Type1")
                {
                    int curentUseage = preVal - entries[pos].Units;
                    totalUsage += curentUseage;
                    preVal = entries[pos].Units;
                }
                else if (entries[pos].Type == "Type2")
                {
                    preVal += entries[pos].Units;
                }
                else
                {
                    continue;
                }
                usageList.Add((entries[pos].Date, entries[pos].Units, entries[pos].Type, totalUsage));
            }

            return usageList;
        }

    }

    // Comparer class to sort entries by date in descending order
    class EntryCompare : IComparer<(DateTime Date, int Units, string Type)>
    {
        public int Compare((DateTime Date, int Units, string Type) First, (DateTime Date, int Units, string Type) Second)
        {
            // Sort dates in descending order to have the latest at the top of the display
            return First.Date.CompareTo(Second.Date);
        }
    }
}
