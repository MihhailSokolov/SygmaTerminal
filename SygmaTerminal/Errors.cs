using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygmaTerminal
{
    class Errors
    {
        public void folderDoesntExitst(string folderLocation)
        {
            Console.WriteLine("Error: No such folder {0}", folderLocation);
        }
        public void fileDoesntExist(string fileLocation)
        {
            Console.WriteLine("Error: No such file {0}", fileLocation);
        }
    }
}