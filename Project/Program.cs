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
                Notification NewNote = new Notification();

                string entry = NewNote.GeneratingNote(user.FilePath);
                int limit = NewNote.UnitLimit(entry);
                Thread thr1 = new Thread(() => NewNote.GeneratingNote(user.FilePath));
                Thread thr2 = new Thread(() => NewNote.UnitLimit(entry));
                Thread thr3 = new Thread(() => NewNote.Respons(limit));

                thr1.Start();
                thr2.Start();
                thr3.Start();

                // Notification
                // notification note = new notification();
                // notification.Notification(user.FilePath);


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
                    // Logic to display user’s history
                    Console.Clear();
                    Console.WriteLine("History");
                    // Logic to display user’s history
                    Entry Ent = new Entry();
                    Entry Units = new Entry();
 
                    Ent.ReadEntries(user.FilePath);
 
                    List<String> entries = Ent.ReadEntries(user.FilePath);
 
                    foreach (string line in entries)
                    {
                        Console.WriteLine(line);
                    }
                    /*
                                       Units.ReadEntries(user.FilePath);

                                       List<String> iUnits = Ent.ReadEntries(user.FilePath);
                                       foreach (string line in iUnits)
                                       {
                                           Console.WriteLine(line); 
                                       }*/

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
                        dataManagement.AddItem("Back", settings.Display);
                        dataManagement.Display();
                    });
                    settings.AddItem("Notifications", () =>
                    {
                        /*
                        List<string> NoteList = new List<string>();
                        notification note = new notification();
                        notification.Notification(user.FilePath);
                        Console.ReadLine();
                        */
                    });
                    settings.AddItem("Exit Settings", userItem.Display );
                    settings.Display();
                    
                });
                userItem.AddItem("Exit", () => { Environment.Exit(0); });
               
                userItem.Display();
            });
        }

        selectUser.AddItem("End", () => { Environment.Exit(0); });
    }
}
