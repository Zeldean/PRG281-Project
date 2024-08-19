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
                Menu userItem = new Menu(userFile.Key);
                userItem.AddItem("New Entry", () =>
                {
                    Menu entry = new Menu("Entries");
                    entry.AddItem("Current Units", () =>
                    {
                        Entry newEntry = new Entry(1);
                        newEntry.AddEntry();
                        // Logic to handle "Current Units" entry
                    });
                    entry.AddItem("Units Purchased", () =>
                    {
                        Entry newEntry = new Entry(2);
                        newEntry.AddEntry();
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
}
