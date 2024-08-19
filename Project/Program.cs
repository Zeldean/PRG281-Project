using System.Reflection.Metadata.Ecma335;
using FileStorage;
using Navigation;

class Program
{
static void Main()
    {
        string directoryPath = "UserFiles";
        Dictionary<string, string> userFiles = new Dictionary<string, string>();
        User user = new User(directoryPath);

        user.UserStorageExists(); // Check if user storage exists; if not, creates it.
        user.UserExists(); // Check if user files exist; if not, prompt user to create one.
        userFiles = user.UserList();

        DisplayUserSelectionMenu(userFiles, user);
    }

static void DisplayUserSelectionMenu(Dictionary<string, string> userFiles, User user)
    {
        Menu selectUser = new Menu("Select User");
        UserListDisplay(userFiles, selectUser, user);
        selectUser.Display();
    }

static void UserListDisplay(Dictionary<string, string> userFiles, Menu selectUser, User user) // File name without extension, File path
    {
        selectUser.AddItem("Add User", () =>
        {
            user.CreateUser();
            userFiles = user.UserList(); // Update the dictionary after a new user is created.
            DisplayUserSelectionMenu(userFiles, user); // Rebuild the menu with the updated user list.
        });

        foreach (var userFile in userFiles)
        {
            selectUser.AddItem(userFile.Key, () =>
            {
                user.UserName = userFile.Key;
                user.FilePath = userFile.Value;
                Menu userItem = new Menu(userFile.Key);
                userItem.AddItem("New Entry", () =>
                {
                    Menu entry = new Menu("Entries");
                    entry.AddItem("Current Units", () =>
                    {
                        Console.WriteLine("================================================================");

                        Console.WriteLine("Please Enter Current number of units"); //gets the current number of units from user.
                        int units = Convert.ToInt32(Console.ReadLine());

                        DateOnly date = DateOnly.FromDateTime(DateTime.Now); //  gets the current date

                        const string type1 = "Type 1";  // sets entry to type1

                        Entry NewEntry = new Entry();
                        NewEntry.CurentUnits(date,units, type1);

                        string EntryText="("+NewEntry.EntryDate.ToString()+"),"+NewEntry.EntryUnits.ToString()+","+NewEntry.EntryType.ToString(); // creates the string that will be used as a entry.

                        string fileP = user.FilePath; // Gets file path

                        List<string> lines = new List<string>();
                        lines = File.ReadAllLines(fileP).ToList(); // reads the texts on the text file.
                        lines.Add(EntryText);             //Adds text to a text file.
                        File.WriteAllLines(fileP, lines); //Adds text to a text file.

                        foreach (String line in lines)
                        {
                            Console.WriteLine(line); // loops through all of the text lines on the text file and displays it on the console.
                        }
                       
                        Console.ReadLine();

                    });
                    entry.AddItem("Units Purchased", () =>
                    {
                        Console.WriteLine("================================================================");

                        Console.WriteLine("Please Enter the number of units that you have purchase:"); //gets the current number of units from user.
                        int unitsPurchased = Convert.ToInt32(Console.ReadLine());

                        DateOnly date = DateOnly.FromDateTime(DateTime.Now); //  gets the current date

                        const string type2 = "Type 2";  // sets entry to type1

                        Entry NewEntry = new Entry();
                        NewEntry.CurentUnits(date, unitsPurchased, type2);
                        
                        string EntryText = "("+ NewEntry.EntryDate.ToString()+"),"+NewEntry.EntryUnits.ToString() + "," + NewEntry.EntryType.ToString();
                        Console.WriteLine("================================================================");
                        string fileP = user.FilePath; // Gets file path

                        List<string> lines = new List<string>();
                        lines = File.ReadAllLines(fileP).ToList(); // reads the texts on the text file.
                        lines.Add(EntryText);   //Adds text to a text file.
                        File.WriteAllLines(fileP, lines); //Adds text to a text file.
                        Console.Clear();
                        foreach (String line in lines)
                        {
                            Console.WriteLine(line); // loops through all of the text lines on the text file and displays it on the console.
                        }


                        Console.ReadLine();
                    });
                    entry.AddItem("Back", userItem.Display);
                    entry.Display();
                });

                userItem.AddItem("View History", () =>
                {
                    // Logic to display user’s history
                    Console.Clear();
                    Console.WriteLine("History");
                    // Logic to display user’s history
                    FileStorage.Entry Ent = new Entry();
                    FileStorage.Entry Units = new Entry();
 
                    Ent.ReadEntries(user.FilePath);
 
                    List<String> entries = Ent.ReadEntries(user.FilePath);
 
                    foreach (string line in entries)
                    {
                        Console.WriteLine(line);
                    }
 
                    Units.ReadEntries(user.FilePath);
 
                    List<String> iUnits = Ent.ReadEntries(user.FilePath);
                    foreach (string line in iUnits)
                    {
                        Console.WriteLine(line); 
                    }
                    
                    Console.ReadKey();
                    userItem.Display();
                });
                userItem.AddItem("Genarate Reports", () =>
                {
                    Menu reports = new Menu("Reports");
                    reports.AddItem("Weekly", () => 
                    {
                        // Logic to genarate a Weekly report
                    });
                    reports.AddItem("Mounthly", () => 
                    {
                        // Logic to genarate a Mounthly report
                    });
                    reports.AddItem("Yearly", () => 
                    {
                        // Logic to genarate a Yearly report
                    });
                    reports.AddItem("Back", userItem.Display);
                    reports.Display();
                });
                userItem.AddItem("Settings", () =>
                {
                    Menu settings = new Menu("Settings");
                    settings.AddItem("Data Management", () =>
                    {
                        Menu dataManagement = new Menu("Data Management");
                        dataManagement.AddItem("Clear Data", () =>
                        {
                            user.ClearData();
                        });
                        dataManagement.AddItem("Import", () =>
                        {
                            //user.ImportData();
                        });
                        dataManagement.AddItem("Export", () =>
                        {
                            //user.ExportData();
                        });
                        dataManagement.Display();
                    });
                    settings.AddItem("Notifications", () =>
                    {
                        // Logic for changing notification settings
                    });
                    settings.AddItem("Exit Settings", userItem.Display );
                    settings.Display();
                });
                userItem.Display();
            });
        }

        selectUser.AddItem("End", () => { Environment.Exit(0); });
    }
}
