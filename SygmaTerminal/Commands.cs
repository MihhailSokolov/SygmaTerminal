using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SygmaTerminal
{
    class Commands
    {
        Check check = new Check(); //Reference on checking class
        public void copy(string fromLocation, string toLocation)
        {
            if (check.ifItIsFile(fromLocation) == true) //Determine if path contain folder or file
            {                                           //If it is a file then just copy
                FileInfo fileinfo = new FileInfo(fromLocation);
                if (check != null && fileinfo != null)
                {
                    if (check.ifFileExists(fromLocation)) //Check if file for copying exists
                    {
                        if (check.ifFolderExists(toLocation)) //Check if destination folder exists
                        {
                            File.Copy(fromLocation, toLocation + @"\" + fileinfo.Name); //When checked that everything is OK, will copy the file or folder
                            Console.WriteLine("Done!");
                        }
                    }
                }
            }
            else //If it is a folder then copy this folder
            {
                DirectoryInfo dirInfo = new DirectoryInfo(fromLocation);
                if (check != null && dirInfo != null)
                {
                    if (check.ifFolderExists(fromLocation)) //Check if folder for copying exists
                    {
                        if (check.ifFolderExists(toLocation)) //Check if folder where to copy exists
                        {
                            Directory.CreateDirectory(toLocation + "\\" + dirInfo.Name); //Firstly, create new folder with the same name
                            FileInfo[] fileList = dirInfo.GetFiles();                   //Get list of files in origin folder
                            for (int i = 0; i < fileList.Length; i++)
                            {
                                File.Copy(fromLocation + "\\" + fileList[i].Name, toLocation + "\\" + dirInfo.Name + "\\" + fileList[i].Name); //Copy all files to the new folder
                            }
                            Console.WriteLine("Done!");
                        }
                    }
                }
            }
        }

        public void move(string fromLocation, string toLocation)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fromLocation);
            FileInfo fileinfo = new FileInfo(fromLocation);
            if (check.ifItIsFile(fromLocation))
            {
                if (check != null && fileinfo != null)
                {
                    if (check.ifFileExists(fromLocation)) //Check if file for moveing exists
                    {
                        if (check.ifFolderExists(toLocation)) //Check if destination folder exists
                        {
                            File.Move(fromLocation, toLocation + @"\" + fileinfo.Name); //When checked that everything is OK, will move the file or folder
                            Console.WriteLine("Done!");
                        }
                    }
                }
            }
            else
            {
                if (check != null && fileinfo != null)
                {
                    //if (check.ifFileExists(fromLocation)) //Check if file for moving exists
                   // {
                        //if (check.ifFolderExists(toLocation)) //Check if destination folder exists
                      //  {
                            Directory.Move(fromLocation, toLocation);
                            //FileInfo[] fileList = dirInfo.GetFiles();
                            //for (int i = 0; i < fileList.Length; i++)
                            //{
                            //    if (check.ifItIsFile(fromLocation + fileList[i]) == false)
                            //    {
                            //        moveFolder(fromLocation + "\\" + fileList[i], toLocation);
                            //    }
                            //    else
                            //    {
                            //        File.Move(fromLocation, toLocation + @"\" + fileinfo.Name);
                            //    }
                            //}
                            Console.WriteLine("Done!");
                      //  }
                  //  }
                }
            }
        }
        public void moveFolder(string fromLocation, string toLocation)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(fromLocation);
            if (check != null && dirInfo != null)
            {
                if (check.ifFolderExists(fromLocation)) //Check if folder for moving exists
                {
                    if (check.ifFolderExists(toLocation)) //Check if folder where to move exists
                    {
                        Directory.CreateDirectory(toLocation + "\\" + dirInfo.Name); //Firstly, create new folder with the same name
                        FileInfo[] fileList = dirInfo.GetFiles();                   //Get list of files in origin folder
                        for (int i = 0; i < fileList.Length; i++)
                        {
                            File.Move(fromLocation + "\\" + fileList[i].Name, toLocation + "\\" + dirInfo.Name + "\\" + fileList[i].Name); //Move all files to the new folder
                        }
                        Directory.Delete(fromLocation); //After all, delete old folder
                    }
                }
            }
        }
        public void makeFolder(string folderPath)
        {
            Directory.CreateDirectory(folderPath); //Make a new folder
            Console.WriteLine("Done!");
        }
        public void delete(string path)
        {
            if (check.ifItIsFile(path)) //Check if it is a file
            {
                if (check.ifFileExists(path)) //Check if it exists
                {
                    File.Delete(path); //When everything is OK, delete file
                    Console.WriteLine("Done!");
                }
                else
                {
                    Console.WriteLine("File {0} does not exist.", path);
                }
            }
            else //if it is a folder
            {
                if (check.ifFolderExists(path)) //Check if folder exists
                {
                    Directory.Delete(path, true); //delete folder
                    Console.WriteLine("Done!");
                }
                else
                {
                    Console.WriteLine("Folder {0} does not exist.", path); //if folder doesn't exist
                }
            }
        }
        public void exit()
        {
            System.Environment.Exit(1);
        }
        public void go(string where)
        {
            try
            {
                if (where == "back") // if command was 'go back'
                {
                    string[] folders = Environment.CurrentDirectory.Split('\\').ToArray(); //split whole path into seperate folders
                    int len = folders.Length;
                    string newPath = "";
                    for (int i = 0; i < len; i++)
                    {
                        if (i < len - 1)
                        {
                            newPath = newPath + folders[i] + "\\"; //Rewrite old path, but don't include last folder
                        }
                    }
                    Environment.CurrentDirectory = newPath;
                }
                else if (where == "home") // if command was 'go home'
                {
                    string[] folders = Environment.CurrentDirectory.Split('\\').ToArray();//split whole path into seperate folders
                    Environment.CurrentDirectory = folders[0] + "\\"; //go to the first folder in path

                }
                else // if command was 'go ' and path
                {
                    Environment.CurrentDirectory = Environment.CurrentDirectory + @"\" + where; // just go to the given path
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // if caught exception, write it to console
            }
            
        }
        public void list() //Lists all files and directories in directory
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            if (dirInfo != null)
            {
                DirectoryInfo[] dirList = dirInfo.GetDirectories();
                FileInfo[] fileList = dirInfo.GetFiles();
                for (int i = 0; i < dirList.Length; i++)
                {
                    Console.WriteLine(dirList[i]); //write all directories in current directory
                }
                for (int i = 0; i < fileList.Length; i++)
                {
                    Console.WriteLine(fileList[i]); //write all files in current directory
                }
            }
        }
        public void start(string appName) //starts or opens files
        {
            if (check.ifItIsFile(appName)) //check if given file is actually file
            {
                try
                {
                    Process.Start(appName); //start or open file
                    Console.WriteLine("{0} was started.", appName); // give feedback
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);// if caught exception, write it to console
                }
            }
        }
        public void search(string searchString) //searches for file in given directory and its subdirectories
        {
            try
            {
                List<string> files = dirSearch(Environment.CurrentDirectory); //get list of paths of all files in current directory and its subdirectories
                int c = files.Count; //amount of all files in list
                string[] fileNames = new string[c];
                for (int i = 0; i < c; i++)
                {
                    string[] pathPieces = files[i].Split('\\').ToArray();
                    fileNames[i] = pathPieces[pathPieces.Length - 1]; //making list of file names out of a list of file paths
                }
                string[] matches = new string[c];
                int j = 0;
                for (int i = 0; i < c; i++)//go through all file names
                {
                    if (fileNames[i] == searchString) //if found match with given file name
                    {
                        matches[j] = files[i]; //add it to list of matches
                        j++;
                    }
                }
                Console.WriteLine("Found {0} match(es)!", j);
                for (int i = 0; i < j; i++)
                {
                    Console.WriteLine("  {0}", matches[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);// if caught exception, write it to console
            }
        }
        public List<string> dirSearch(string sDir)
        {
            bool searched = false;
            List<string> fileList = new List<string>();//list of all file paths in given directory and its subdirectories
            try
            {
                if (!searched)
                {
                    foreach (string f in Directory.GetFiles(sDir)) 
                    {
                        fileList.Add(f);//add to list file paths in given directory
                    }
                    searched = true;
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        fileList.Add(f);//add to list file paths in subdirectories
                    }
                    dirSearch(d);//search in next subdirectory
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);// if caught exception, write it to console
            }
            return fileList; //return list of all file paths in given directory and its subdirectories
        }
        public void listDrives()
        {
            foreach (string d in Environment.GetLogicalDrives())
            {
                Console.WriteLine(d); //list all drives
            }
        }
        public void changeDrive(string driveName)
        {
            bool driveExist = false;
            foreach (string d in Environment.GetLogicalDrives())
            {
                if (driveName == d)
                {
                    driveExist = true; //check if given drive name exists
                }
            }
            if (driveExist) //if it does then...
            {
                Environment.CurrentDirectory = driveName; //change current directory to other drive
            }
        }
        public void showProcesses()
        {
            Process[] processes = Process.GetProcesses(); //Making list of all current processes
            Console.WriteLine("Current processes:");
            foreach (var currentProcess in processes)
            {
                Console.WriteLine("  {0}", currentProcess.ProcessName); //Printing process' name
            }
        }
        public void killProcess(string processName)
        {
            bool processExists = false;
            int count = 0;
            int c = 0;
            Process[] processes = Process.GetProcesses();
            foreach (var currentProcess in processes)
            {
                if (currentProcess.ProcessName == processName) { processExists = true; c = count; } //Check if process exitst
                count++;
            }
            if (processExists)
            {
                processes[c].Kill(); //Kill the process
            }
        }
        public void binaryRead(string fileName)
        {
            if (check.ifItIsFile(fileName))
            {
                string filePath = Environment.CurrentDirectory + "\\" + fileName;
                byte[] fileBytes = File.ReadAllBytes(filePath);
                StringBuilder strBuilder = new StringBuilder();
                foreach (byte b in fileBytes)
                {
                    strBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }
                string binStr = strBuilder.ToString();
                Console.WriteLine(binStr);
            }
        }
        public void clearConsole()
        {
            Console.Clear();
        }
    }
}