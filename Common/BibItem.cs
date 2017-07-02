using System;
using System.Collections.Generic;
using System.Text;

namespace ModifyBibtex
{
    public class BibItem : IBibItem
    {
        public string Typ { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Tail { get; set; }
        public string MiddleSection { get; set; }


        public BibItem(string oneElementString, IDictionary<string,string> charCouple)
        {
            if (oneElementString == null) return;

            var firstSplit = oneElementString.Split(',');

            if(firstSplit.Length < 3 ) {  return; }

            Typ = firstSplit[0];
            Title = firstSplit[1];

            int i; // i is needed in both loops, otherwise it writes stuff twice 
            for (i = 2; i < firstSplit.Length; i++)
            {
                if (firstSplit[i].Contains("url ="))
                {
                    URL = firstSplit[i++];
                    break; // if the url was found we break this loop and we add all entries at the tail
                }
                else
                {
                   MiddleSection += "," + firstSplit[i];
                }
            }

            // add rest of the string to the tail, it is not that much important to modify 
            for (; i < firstSplit.Length; i++)
            {
                Tail += ","+firstSplit[i];
            }

            if (Tail != null)
            {
                Tail = Tail.Replace("%", "\\%"); // sometime are some % in path of file, so it will be replaced as well
                Tail = Tail.Replace("_", "\\_"); // sometime are some % in path of file, so it will be replaced as well
            }

            URL = RemoveAndSigns(URL, charCouple); // change specify signs in the string 

        }

        private string RemoveAndSigns(string urlString, IDictionary<String,String> allCharaktorsToReplace)
        {
            if (urlString == null)
                return "";

            foreach (var charPair in allCharaktorsToReplace)
            {
                urlString = urlString.Replace(charPair.Key, charPair.Value);
            }
            return urlString;
        }

        public override string ToString()
        {
            StringBuilder newString = new StringBuilder();
            string returnString;
            
            if (Typ == null)
                return newString.ToString();

            newString.Append("@").Append(Typ);
            if (Title != null)
                newString.Append(",").Append(Title);
            if (MiddleSection != null)
                newString.Append(",").Append(MiddleSection);
            if (URL != string.Empty)
                newString.Append(",").Append(URL);
            if (Tail != null)
                newString.Append(",").Append(Tail);

            returnString = newString.ToString();
            return returnString.Replace(",,", ","); // replace wrong generated signs
        }
    }
}
