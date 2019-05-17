using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SygmaTerminal
{
    class MainClass
    {
        static public void Main(string[] args)
        {
            string consoleTitle = Console.Title;
            string originPath = consoleTitle.Substring(8);
            int maxWidth = Console.LargestWindowWidth;
            int maxHeight = Console.LargestWindowHeight;
            Console.SetWindowSize(maxWidth - 97, maxHeight - 30);
            Console.Title = "SygmaTerminal";
            Console.ForegroundColor = ConsoleColor.Green;
            Commands command = new Commands();
            Check check = new Check();
            CommandProcessingUnit cpu = new CommandProcessingUnit();
            if (command != null && check != null && cpu != null)
            {
            waiting:
                Console.Write(Environment.CurrentDirectory + ": ");
                string answer = Console.ReadLine(); //Get the command line
                if (answer != "")
                    cpu.Recognition(answer);
                goto waiting;
            }
        }
    }
}