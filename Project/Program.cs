using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using FileStorage;
using Navigation;
using Project;

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

static void UserListDisplay(Dictionary<string, string> userFiles, Menu selectUser, User user) // Key: File name without extension; Value: File path
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
                Console.ReadKey();
                
                Notification NewNote = new Notification();

                Thread thr1 = new Thread(() => NewNote.GeneratingNote(user.FilePath)); // a Threads that gets the units of the last entry.
                NewNote.Alert += Message; // Appends the notification message to the event.
                Thread thr2 = new Thread(() => NewNote.message()); // Thread 2 Triggers the event.
                thr1.Start();
                thr1.Join();
                thr2.Start();
                
                Menu userItem = new Menu(userFile.Key);
                userItem.AddItem("New Entry", () =>
                {
                    Menu entry = new Menu("Entries");
                    entry.AddItem("Current Units", () =>
                    {
                        Entry.CreateEntry("Type1", user.FilePath);
                    });
                    entry.AddItem("Units Purchased", () =>
                    {
                        Entry.CreateEntry("Type2", user.FilePath);
                    });
                    entry.AddItem("Back", userItem.Display);
                    entry.Display();
                });
                userItem.AddItem("View History", () =>
                {
                    Console.Clear();
                   
                    Console.WriteLine("All History:");
                    EntryList entryList = new EntryList(user.FilePath);
                    Console.ReadKey();
                    userItem.Display();
                });
                userItem.AddItem("Genarate Reports", () =>
                {
                    Menu reports = new Menu("Reports");
                    reports.AddItem("Weekly", () =>
                    {
                        // Logic to genarate a Weekly report
                        Calculation calculation = new Calculation(user.FilePath, "Weekly");
                        Console.ReadLine();
                    });
                    reports.AddItem("Monthly", () =>
                    {
                        // Logic to genarate a Mounthly report
                        Calculation calculation = new Calculation(user.FilePath, "Monthly");
                        Console.ReadLine();
                    });
                    reports.AddItem("Yearly", () =>
                    {
                        // Logic to genarate a Yearly report
                        Calculation calculation = new Calculation(user.FilePath, "Yearly");
                        Console.ReadLine();
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
                            user.ImportDataFromCsv();
                        });
                        dataManagement.AddItem("Export", () =>
                        {
                            user.ExportDataToCsv(user.FilePath);
                        });
                        dataManagement.AddItem("Back", settings.Display);
                        dataManagement.Display();
                    });
                    settings.AddItem("Notifications", () =>
                    {
                        List<(DateTime date, int units, string type)> list = EntryList.ReadUserData(user.FilePath);
                        foreach (var item in list)
                        {
                            if (item.units < 50)
                            {
                                Console.WriteLine(item);
                                NewNote.message();
                            }
                        }
                        Console.ReadLine();
                    });
                    settings.AddItem("Exit Settings", userItem.Display );
                    settings.Display();
                    
                });
                userItem.AddItem("Exit", () => { Environment.Exit(0); });
               
                userItem.Display();
                void Message() 
                {
                    NewNote.Respons(NewNote.GeneratingNote(user.FilePath));
                }
            });
        }

        selectUser.AddItem("End", () => { Environment.Exit(0); });
    }
   
}
