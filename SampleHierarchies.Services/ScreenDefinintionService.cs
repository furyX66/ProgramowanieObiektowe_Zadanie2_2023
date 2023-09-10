using Newtonsoft.Json;
using SampleHierarchies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Services
{
    public static class ScreenDefinitionService
    {
        public static ScreenDefinition Load(string jsonFileName)
        {
            if (File.Exists(jsonFileName))
            {
                string? json = File.ReadAllText(jsonFileName);
                return JsonConvert.DeserializeObject<ScreenDefinition>(json);
            }                               
            else
            {
                throw new FileNotFoundException("JSON file not found.", jsonFileName);
            }
        }

        public static void Save(ScreenDefinition screenDefinition, string jsonFileName)
        {
            string json = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
            File.WriteAllText(jsonFileName, json);
        }
        public static void PrintLine(List<ScreenLineEntry> lines, int lineNumber) 
        {
            Console.ForegroundColor = lines[lineNumber].ForegroundColor;
            Console.BackgroundColor = lines[lineNumber].BackgroundColor;
            Console.WriteLine(lines[lineNumber].Text);
            Console.ResetColor();
        }
    }

}

