using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModifyBibtex
{
    public class ModifyController
    {
        
        /// <summary>
        /// This function read a file of typ bib and return a list of IBibItems 
        /// </summary>
        /// <param name="currentProperty"> Current Tool Properties to be able to localze the file  </param>
        /// <returns> List<IBibItems> which contains all bib entries of the file </IBibItems> </returns>
        public string ReadFile(IToolProperty currentProperty)
        {
            string input = string.Empty;
            FileStream fileStream = new FileStream(currentProperty.SourceFile, mode: FileMode.Open);
            using (StreamReader reader = new StreamReader(stream: fileStream))
            {
               input = reader.ReadToEnd();
            }
            return input;
        }

        public List<IBibItem> CreateBibItemsFormString(IToolProperty currentProperty, string line)
        {
            var seperatItems = line.Split('@');
            var allItems = new List<IBibItem>();
            foreach (var singleItem in seperatItems)
            {
                allItems.Add(item: new BibItem(singleItem, currentProperty.ReplacingCharactarsDictionary));
            }
            return allItems;
        }

        public void WriteToFile(IToolProperty currentFileProperties, List<IBibItem> completlyList)
        {
            FileStream neFile =
                            new FileStream(currentFileProperties.FinalFile, mode: FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream: neFile))
            {
                foreach (var singleBibItem in completlyList)
                {
                    writer.WriteLine(value: singleBibItem.ToString());
                }
            }
        }

        

    }
}