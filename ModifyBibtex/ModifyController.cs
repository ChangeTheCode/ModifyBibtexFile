using Common;
using System.Collections.Generic;
using System.IO;


namespace ModifyBibtex
{
    public class ModifyController
    {
        /// <summary>
        /// Read file retrun whole file content as a string
        /// </summary>
        /// <param name="currentProperty"></param>
        /// <returns>String with file content</returns>
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

        /// <summary>
        /// Create a list of IBibItems from raw data as a string
        /// </summary>
        /// <param name="currentProperty">CurrentToolProperties to get specific signs to replace </param>
        /// <param name="line">Raw string with all Bibitems as a string </param>
        /// <returns>IList<IBibItems> </IBibItems></returns>
        public IList<IBibItem> CreateListOfBibItemsFormString(IToolProperty currentProperty, string line)
        {
            var seperatItems = line.Split('@');
            var allItems = new List<IBibItem>();
            foreach (var singleItem in seperatItems)
            {
                var tempItem = new BibItem();
                tempItem.SetImportentFields(singleItem, currentProperty.ReplacingCharactarsDictionary);
                allItems.Add(tempItem);
            }
            return allItems;
        }

        /// <summary>
        /// Write information to specific file as bib typ 
        /// </summary>
        /// <param name="currentFileProperties">destination path from the file </param>
        /// <param name="completeList">Whole list, with all Bibitem from the original file</param>
        public void WriteToFile(string currentPath, IList<IBibItem> completeList)
        {
            FileStream writeFile =
                            new FileStream(currentPath, mode: FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream: writeFile))
            {
                foreach (var singleBibItem in completeList)
                {
                    writer.WriteLine(value: singleBibItem.ToString());
                }
            }
        }

    }
}