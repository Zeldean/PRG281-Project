/*
 * ===========================================
 * Author: Zeldean
 * Project: PRG281 Project
 * Date: August 19, 2024
 * ===========================================
 *   ______      _      _                     
 *  |___  /     | |    | |                    
 *     / /  ___ | |  __| |  ___   __ _  _ __  
 *    / /  / _ \| | / _` | / _ \ / _` || '_ \ 
 *   / /__|  __/| || (_| ||  __/| (_| || | | |
 *  /_____|\___||_| \__,_| \___| \__,_||_| |_|
 *   
 * ===========================================
 */
using Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileStorage
{
    class User 
    {
        // Propirties
        private string? userName;
        private string? password;
        private string? filePath;
        private string? directoryPath;
        

        // Encapsulation
        public string DirectoryPath { get => directoryPath; set => directoryPath = value; }
        public string FilePath { get => filePath; set => filePath = value; }
        public string UserName { get => userName; set => userName = value; }
        public string? Password { get => password; set => password = value; }

        // Constructor
        public User(string directoryPath)
        {
            this.DirectoryPath = directoryPath;
        }
        // Methods
        public void UserStorageExists()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
                Console.WriteLine("Directory created for user files.");
            }
        }
        public void UserExists()
        {
            var files = Directory.GetFiles(DirectoryPath, "*.txt");
            
            if (!files.Any())
            {
                Console.WriteLine("No Users found...");
                Console.WriteLine("Press Enter to Create User");
                Console.ReadKey();
                CreateUser();
            }
        }
        public Dictionary<string, string> UserList()
        {
            var files = Directory.GetFiles(DirectoryPath, "*.txt");
            Dictionary<string, string> userFiles = new Dictionary<string, string>();

            files = Directory.GetFiles(DirectoryPath, "*.txt");
            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                userFiles[fileName] = file;
            }
            return userFiles;
        }       
        public void CreateUser()
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter a User name:");
                UserName = Console.ReadLine();

                Console.WriteLine("Enter Meter code:");
                Password = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(UserName)) // Check that the username is not blank or whitespace
                {
                    break;
                }
                else
                {
                    Console.WriteLine("User name can't be blank. Press any key to try again.");
                    Console.ReadKey(); // Wait for user input before continuing
                }
                if (!string.IsNullOrWhiteSpace(Password)) // Check that the username is not blank or whitespace
                {
                    break;
                }
                else
                {
                    Console.WriteLine("User name can't be blank. Press any key to try again.");
                    Console.ReadKey(); // Wait for user input before continuing
                }
            }

            // Create the file path using the username
            string fileName = UserName + ".txt"; // Add an extension to the filename
            string filePath = Path.Combine(DirectoryPath, fileName);

            // Check if the file already exists
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close(); // Create the file and close it immediately to avoid locking issues
                Console.WriteLine($"User file '{fileName}' created successfully.");
            }
            else
            {
                Console.WriteLine($"A file for '{userName}' already exists. Please choose another name.");
            }
            File.AppendAllText(filePath, Password + Environment.NewLine);
            File.AppendAllText(filePath, "ENTRIES" + Environment.NewLine);
        }
        /// <summary>
        /// Clears all data lines in a file, starting from the line that contains the word "ENTRIES".
        /// </summary>
        public void ClearData()
        {
            string[] lines = File.ReadAllLines(FilePath);
            int index = 0;
            for (index = 0; index < lines.Length; index++)
            {
                if (lines[index] == "ENTRIES")
                {
                    break;
                }
            }
            File.Delete(FilePath);
            File.Create(FilePath).Close();
            var newLines = new ArraySegment<string>(lines, 0, index + 1);
            File.WriteAllLines(FilePath, newLines);
            Console.WriteLine("All data was cleard. Press Enter to continue.");
            Console.ReadKey();
        }
        public static void ClearData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int index = 0;
            for (index = 0; index < lines.Length; index++)
            {
                if (lines[index] == "ENTRIES")
                {
                    break;
                }
            }
            File.Delete(filePath);
            File.Create(filePath).Close();
            var newLines = new ArraySegment<string>(lines, 0, index + 1);
            File.WriteAllLines(filePath, newLines);
        }
        public void ExportDataToCsv(string userFilePath)
        {
            List<(DateTime date, int units, string type)> data = EntryList.ReadUserData(userFilePath);
            // Get the path to the Downloads folder
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // Set the file name and path
            string fileName = "UserData.csv";
            string filePath = Path.Combine(downloadsPath, fileName);

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the header
                    writer.WriteLine("Date,Units,Type");

                    // Write each record
                    foreach (var record in data)
                    {
                        writer.WriteLine($"{record.date:MM/dd/yyyy},{record.units},{record.type}");
                    }
                }
                Console.WriteLine($"File successfully saved to: {filePath}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
    }
        public void ImportDataFromCsv()
        {
            Console.WriteLine("Enter the path to the csv file you want to import.");
            string fullFilePath = Console.ReadLine();
            var importedData = new List<(DateTime date, int units, string type)>();

            try
            {
                // Check if the file exists
                if (!File.Exists(fullFilePath))
                {
                    Console.WriteLine("File not found.");
                    Console.ReadKey();
                }
                else
                {
                    // Read the CSV file line by line
                    string[] lines = File.ReadAllLines(fullFilePath);

                    // Skip the header (assuming the first line is the header)
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split(',');

                        if (parts.Length == 3)
                        {
                            // Parse the data
                            DateTime date = DateTime.Parse(parts[0]);
                            int units = int.Parse(parts[1]);
                            string type = parts[2];

                            // Add the parsed data to the list
                            importedData.Add((date, units, type));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid data format in line {i + 1}: {lines[i]}");
                            Console.ReadKey();
                        }
                    }
                    ClearData(FilePath);
                    foreach (var data in importedData)
                    {
                        Entry.CreateEntry(data.type, data.units, data.date, FilePath);
                        Console.WriteLine($"{data.type} + {data.units} + {data.date}");
                    }
                    Console.WriteLine("Data imported successfully.");
                    Console.ReadKey();
                }            
            }
            catch (Exception ex)
            {   
               Console.WriteLine($"Error importing data: {ex.Message}");
               Console.ReadKey();
            }       
        }
    }

    class Entry
    {
        private DateOnly entryDate;
        private string entryType;
        private int entryUnits;

        public DateOnly EntryDate { get => entryDate; set => entryDate = value; }
        public string EntryType { get => entryType; set => entryType = value; }
        public int EntryUnits { get => entryUnits; set => entryUnits = value; }        
       
        /// <summary>
        /// Reads and parses user data from a file and returns a list containing date, units, and type information.
        /// </summary>
        /// <param name="filePath">The file path to the text file containing the data entries.</param>
        /// <returns>
        /// A list that contains:
        /// - DateTime date: The date of the entry.
        /// - int units: The number of units recorded.
        /// - string type: The type of entry (e.g., "Type1" or "Type2").
        /// </returns>        
        public static void CreateEntry(string type, string fileP) 
        {
            Console.WriteLine("================================================================");

            if (type == "Type1") {
                Console.WriteLine("Please Enter Current number of units");
            }
            else if (type == "Type2") {
                Console.WriteLine("Please Enter number of units Purchesed");
            }   

            int units = Convert.ToInt32(Console.ReadLine());
            DateOnly date = DateOnly.FromDateTime(DateTime.Now); //  gets the current date
            Entry NewEntry = new Entry();
            NewEntry.CurentUnits(date, units, type);
            string EntryText="("+NewEntry.EntryDate.ToString()+"),"+NewEntry.EntryUnits.ToString()+","+NewEntry.EntryType.ToString(); // creates the string that will be used as a entry.
            
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(fileP).ToList(); // reads the texts on the text file.
            lines.Add(EntryText);             //Adds text to a text file.
            File.WriteAllLines(fileP, lines); //Adds text to a text file.
            Console.WriteLine("Latest Entry: "+lines[lines.Count - 1]);
            Console.WriteLine("Second Last Entry: "+lines[lines.Count - 2]);// Displays the last three entries.
            Console.WriteLine("Third Last Entry: "+lines[lines.Count - 3]);
            
            Console.ReadLine();
        }
        public static void CreateEntry(string type, int units , DateTime date, string fileP) 
        {            
            Entry NewEntry = new Entry();
            NewEntry.CurentUnits(DateOnly.FromDateTime(date), units, type);
            string EntryText="("+NewEntry.EntryDate.ToString()+"),"+NewEntry.EntryUnits.ToString()+","+NewEntry.EntryType.ToString(); // creates the string that will be used as a entry.
            
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(fileP).ToList(); // reads the texts on the text file.
            lines.Add(EntryText);             //Adds text to a text file.
            File.WriteAllLines(fileP, lines); //Adds text to a text file.
        }
        public void CurentUnits(DateOnly date,int units, string type)
        {
            EntryUnits = units;
            EntryType = type;
            EntryDate = date;
        }        
    }

    class Notification:INotificationMethods
    {
        public delegate void Note();
        public event Note Alert;
        public int GeneratingNote(string fileP)
        { 
            List<(DateTime date, int units, string type)> lines = EntryList.ReadUserData(fileP); //Generates a list of all the text entries using the EntryList class.
            return lines[lines.Count-1].units; // Gets the last entry in the list.
        }
        
        public void Respons(int unitlimit)  // if Current units is less then 50 it will provide a notification.
        {
            if (unitlimit < 50)
            {
               Console.WriteLine("Notification: DANGER! Current number of units is less then 50 units. Please purchase more units!\n");   
            }
        }
        public void message() // Calls the event that generates the a notification.
        {
            if (Alert != null)
            {
                Alert();
            }
        }
    }
}
