using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ModifyBibtex
{
    public class ToolProperty: IToolProperty
    {
        private static readonly string _defaultJsonSettings =
            "{\r\n  \"SourceFile\": \"./\\bibliography.bib\",\r\n  \"FinalFile\": \"./\\btest.bib\",\r\n  \"ReplacingCharactarsDictionary\": {\r\n    \"%\": \"\\\\%\",\r\n    \"_\": \"\\\\_\",\r\n    \"&\": \"\\\\&\"\r\n  }\r\n}";
        public string SourceFile { get; set; }
        public string FinalFile { get; set; }
        public IDictionary<string, string> ReplacingCharactarsDictionary { get ; set; }

        public void Initial()
        {
            string jsonImput;

            FileStream settingStream = new FileStream(path: "./ProgramSettings.json", mode: FileMode.Open);
            using (StreamReader reader = new StreamReader(stream: settingStream))
            {
                jsonImput = reader.ReadToEnd();
            }

            ToolProperty generatedSettings;
            try
            {
                generatedSettings = JsonConvert.DeserializeObject<ToolProperty>(jsonImput);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                generatedSettings = JsonConvert.DeserializeObject<ToolProperty>(_defaultJsonSettings);
                throw;
            }
            FinalFile = generatedSettings.FinalFile;
            SourceFile = generatedSettings.SourceFile;
            ReplacingCharactarsDictionary = generatedSettings.ReplacingCharactarsDictionary;
        }
    }
}
