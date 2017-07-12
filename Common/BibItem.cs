using System;
using System.Collections.Generic;
using System.Text;

namespace ModifyBibtex
{
    public class BibItem : IBibItem
    {
        #region Fields
        public string Typ { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Tail { get; set; }
        public string MiddleSection { get; set; }
#endregion

        #region Constructor
        public BibItem()
        {
            
        }
#endregion

        public void SetImportentFields(string oneElementString, IDictionary<string, string> charCouple)
        {
            if (oneElementString == null)
            {
                throw new ArgumentNullException(oneElementString);
            }

            var firstSplit = oneElementString.Split(',');

            if (firstSplit.Length < 3) { return; }

            var index = 0;
            Typ = firstSplit[index++];
            Title = firstSplit[index++];
            int i = SplitFirstStringPartTillFindUrl(firstSplit, index);

            AddRestOfStringToTail(firstSplit, i);

            removeUnderscorePercentSigns();

            URL = RemoveSpecificSigns(URL, charCouple);
        }

        #region private functions
        private int SplitFirstStringPartTillFindUrl(string[] firstSplit, int index)
        {
             // i is needed in both loops, otherwise it writes stuff twice 
            for ( index= 2; index < firstSplit.Length; index++)
            {
                if (firstSplit[index].Contains("url ="))
                {
                    URL = firstSplit[index++];
                    break; // if the url was found we break this loop and we add all entries at the tail
                }
                else
                {
                    MiddleSection += "," + firstSplit[index];
                }
            }

            return index;
        }

        private void AddRestOfStringToTail(string[] firstSplit, int i)
        {
            // add rest of the string to the tail, it is not that much important to modify 
            for (; i < firstSplit.Length; i++)
            {
                Tail += "," + firstSplit[i];
            }
        }

        private void removeUnderscorePercentSigns()
        {
            if (Tail != null)
            {
                Tail = Tail.Replace("%", "\\%"); // sometime are some % in path of file, so it will be replaced as well
                Tail = Tail.Replace("_", "\\_");
            }
        }

        private string RemoveSpecificSigns(string urlString, IDictionary<String,String> allCharaktorsToReplace)
        {
            if (urlString == null)
                return "";

            foreach (var charPair in allCharaktorsToReplace)
            {
                urlString = urlString.Replace(charPair.Key, charPair.Value);
            }
            return urlString;
        }
        #endregion

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
            return returnString.Replace(",,", ","); 
        }
    }
}
