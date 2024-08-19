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
                        // Logic to handle "Current Units" entry
                    });
                    entry.AddItem("Units Purchased", () =>
                    {
                        // Logic to handle "Units Purchased" entry
                    });
                    entry.AddItem("Back", () => { DisplayUserSelectionMenu(userFiles, user); });
                    entry.Display();
                });

                userItem.AddItem("View History", () =>
                {
                    // Logic to display user’s history
                });

                userItem.Display();
            });
        }

        selectUser.AddItem("End", () => { Environment.Exit(0); });
    }
public List<Entry> ReadEntries(string filePath)
{
    List<Entry> entries = new List<Entry>();
    
    // Logic to read entries from file
    // For example:
    string[] lines = File.ReadAllLines(filePath);
    bool data =false;
    foreach (string line in lines)
    {
        if (data == true)
        {
            // Logic to read entry
        }
        else if(line == "ENTRIES") 
        {
            data = true;
        }
    }
    
    return entries;
}
}
