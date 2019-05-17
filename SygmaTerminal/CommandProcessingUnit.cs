using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygmaTerminal
{
    class CommandProcessingUnit
    {
        private List<string> ComList = new List<string>(); //Making list for all commands
        public List<string> CommandList //Making list accessible for other classes
        {
            get { return ComList; }
        }

        Commands command = new Commands();
        public void Recognition(string commandLine)
        {   //Adding commands
            CommandList.Add("help");  
            //CommandList.Add("howtouse");
            CommandList.Add("makefolder");
            CommandList.Add("go");
            CommandList.Add("delete");
            CommandList.Add("copy");
            CommandList.Add("move");
            //CommandList.Add("print");
            //CommandList.Add("for");
            //CommandList.Add("show");
            //CommandList.Add("read");
            //CommandList.Add("open");
            //CommandList.Add("newcommand");
            //CommandList.Add("savecommand");
            //CommandList.Add("deletecommand");
            //CommandList.Add("newwindow");
            CommandList.Add("list"); //And more commands
            CommandList.Add("start");
            //CommandList.Add("replace");
            //CommandList.Add("rename");
            CommandList.Add("exit");
            //CommandList.Add("listmycommands");
            CommandList.Add("search");
            CommandList.Add("listdrives");
            CommandList.Add("changedrive");
            CommandList.Add("showprocesses");
            CommandList.Add("killprocess");
            CommandList.Add("binaryread");
            CommandList.Add("clear");
            string[] lineWords = commandLine.Split(' ').ToArray(); //Split command line by words and turn it into string array
            string[] arguments = new string[lineWords.Length - 1]; //Array for arguments
            string strCommand = ""; //One word command that should match one of commands from CommandList
            for (int i = 0; i < CommandList.Count(); i++)
            {
                if (CommandList[i] == lineWords[0]) //Checking if first word in line is a command
                {
                    strCommand = lineWords[0];
                    for (int j = 0; j < lineWords.Length - 1; j++)
                    {
                        arguments[j] = lineWords[j+1]; //Everything else (except command) is arguments
                    }
                }
             }
            string fromPath = ""; //path for command methods
            string toPath = ""; //another path for commands
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] == "to") //check if arguments contain word 'to'
                {                         //because it connects fromPath and toPath
                    if (i == 0)
                    {
                        toPath = arguments[i + 1];
                    }
                    else
                    {
                        fromPath = arguments[i - 1];
                        toPath = arguments[i + 1];
                    }
                }
            }
            switch (strCommand) //find out what was the command
            {
                case "copy":
                    {
                        command.copy(fromPath, toPath);
                        break;
                    }
                case "move":
                    {
                        command.move(fromPath, toPath);
                        break;
                    }
                case "makefolder":
                    {
                        command.makeFolder(arguments[0]);
                        break;
                    }
                case "delete":
                    {
                        command.delete(arguments[0]);
                        break;
                    }
                case "exit":
                    {
                        command.exit();
                        break;
                    }
                case "go":
                    {
                        command.go(arguments[arguments.Length - 1]);
                        break;
                    }
                case "list":
                    {
                        command.list();
                        break;
                    }
                case "start":
                    {
                        command.start(arguments[0]);
                        break;
                    }
                case "search":
                    {
                        command.search(arguments[0]);
                        break;
                    }
                case "listdrives":
                    {
                        command.listDrives();
                        break;
                    }
                case "changedrive":
                    {
                        command.changeDrive(arguments[0]);
                        break;
                    }
                case "showprocesses":
                    {
                        command.showProcesses();
                        break;
                    }
                case "killprocess":
                    {
                        command.killProcess(arguments[0]);
                        break;
                    }
                case "binaryread":
                    {
                        command.binaryRead(arguments[0]);
                        break;
                    }
                case "clear":
                    {
                        command.clearConsole();
                        break;
                    }
                case "help":
                    {
                        for (int i = 0; i < CommandList.Count; i++)
                            Console.WriteLine(CommandList[i]);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unknown command: {0}", lineWords[0]);
                        break;
                    }
                    
            }
        }
    }
}