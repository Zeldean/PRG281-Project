﻿/*
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CurentUnits(DateOnly date,int units, string type)
        {
            EntryUnits = units;
            EntryType = type;
            EntryDate = date;
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
}
