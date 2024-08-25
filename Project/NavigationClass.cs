/*
 * ===========================================
 * Author: Zeldean
 * Project: PRG281 Project
 * Date: August 15, 2024
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
using System;
using System.Collections.Generic;

namespace Navigation
{
    /// <summary>
    /// Represents an item in the menu, containing a display option and an action to execute.
    /// </summary>
    public class MenuItem
    {
        public string Option { get; } // The text displayed for this menu item.
        public Action Action { get; } // The action to be executed when this menu item is selected.

        /// <summary>
        /// Constructor to create a menu item with the specified option text and associated action.
        /// </summary>
        /// <param name="option">The option text displayed to the user.</param>
        /// <param name="action">The action to execute when the item is selected.</param>
        public MenuItem(string option, Action action)
        {
            Option = option;
            Action = action;
        }
    }

    /// <summary>
    /// Represents a menu that can contain multiple menu items and sub-menus.
    /// </summary>
    public class Menu
    {
        private string title; // The title of the menu.
        private List<MenuItem> items = new List<MenuItem>(); // A list to store all menu items.

        /// <summary>
        /// Constructor to create a menu with the specified title.
        /// </summary>
        /// <param name="menuTitle">The title of the menu.</param>
        public Menu(string menuTitle)
        {
            title = menuTitle;
        }

        /// <summary>
        /// Adds an item to the menu that performs a specific action.
        /// </summary>
        /// <param name="option">The option text displayed to the user.</param>
        /// <param name="action">The action to execute when the item is selected.</param>
        public void AddItem(string option, Action action)
        {
            items.Add(new MenuItem(option, action));
        }

        /// <summary>
        /// Adds a sub-menu as an item in the current menu, allowing for nested navigation.
        /// </summary>
        /// <param name="option">The option text displayed to the user.</param>
        /// <param name="subMenu">The sub-menu to be displayed when the item is selected.</param>
        public void AddItem(string option, Menu subMenu)
        {
            items.Add(new MenuItem(option, subMenu.Display)); // Link the sub-menu's display method as the action.
        }

        /// <summary>
        /// Displays the menu and handles user input to select menu items.
        /// The menu continues to loop until an action is performed that breaks out of it.
        /// </summary>
        public void Display()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine(new string('-', title.Length));

                // Display each menu item
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {items[i].Option}");
                }

                  ///Built in exception here:
              try
              {
                  // Handle user input and execute the corresponding action
                  if (int.TryParse(Console.ReadLine(), out int choice))
                  {
                      // Check if the choice is out of range
                      if (choice < 1 || choice > items.Count)
                      {
                          throw new ArgumentOutOfRangeException(nameof(choice), "Selection is out of the valid range.");
                      }

                      // Execute the selected item's action.
                      items[choice - 1].Action();
                  }
                  else
                  {
                      Console.WriteLine("Invalid input, please enter a number.");
                  }
                  }
                 catch (ArgumentOutOfRangeException ex)
                 {
                    Console.WriteLine($"Error: {ex.Message}");
                 }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                  }

                  Console.ReadKey(); // Pause to show the message before refreshing the menu.
            }
        }
    }
}
