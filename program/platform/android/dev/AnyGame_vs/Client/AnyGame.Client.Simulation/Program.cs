using AnyGame.Client.Template;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Client.Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Logs.AddConsoleAppender();
            Logs.SetMessageLevel<ConsoleAppender>(LogMessageType.MSG_ERROR);

            Templates.LoadTemplate();
        }
    }
}
