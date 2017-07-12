using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace ModifyBibtex
{
    class Program
    {
        private static ToolProperty _currenToolProperties = null;
        static readonly List<IBibItem> _allBibItems = new List<IBibItem>();

        static void Main(string[] args)
        {
            Console.WriteLine(value: "Program has started");
            Console.WriteLine(value: "Program load settings ...");

            _currenToolProperties = new ToolProperty();

            try
            {
                _currenToolProperties.Initial();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
  
            ModifyController readWriteController = new ModifyController();
            var fileInput = readWriteController.ReadFile(_currenToolProperties);

            var listOfEnties = readWriteController.CreateListOfBibItemsFormString(_currenToolProperties, fileInput);

            readWriteController.WriteToFile(_currenToolProperties.FinalFile, listOfEnties);
            Console.WriteLine(value: "Program has finished, file is written! ");
            Console.WriteLine(value: "Press any key to exit ... ");
            Console.ReadLine();
        }

        
    }
}


