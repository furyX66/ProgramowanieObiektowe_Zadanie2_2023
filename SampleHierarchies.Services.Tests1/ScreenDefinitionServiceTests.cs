using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Services;
using System;
using System.IO;

namespace SampleHierarchies.Tests
{
    [TestClass]
    public class ScreenDefinitionServiceTests
    {
        [TestMethod]
        public void Load_ValidJsonFile_ReturnsScreenDefinition()
        {
            // Arrange
            var lines = new List<ScreenLineEntry>
        {
            new ScreenLineEntry
            {
                BackgroundColor = ConsoleColor.DarkBlue,
                ForegroundColor = ConsoleColor.White,
                Text = "Test line"
            }
        };
            int lineNumber = 0;
            string expectedLine = "Test line";

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ScreenDefinitionService.PrintLine(lines, lineNumber);
                string printedLine = sw.ToString().Trim();

                // Assert
                Assert.AreEqual(expectedLine, printedLine);
            }
        }
        [TestMethod]
        public void Load_NonExistentJsonFile_ThrowsFileNotFoundExceptionAndExits()
        {
            // Arrange
            string jsonFileName = "nonexistent.json";

            // Act & Assert
            try
            {
                ScreenDefinition result = ScreenDefinitionService.Load(jsonFileName);
                Assert.Fail("Expected exception was not thrown");
            }
            catch (FileNotFoundException ex)
            {
                Assert.AreEqual("JSON file not found.", ex.Message);
            }
        }
        [TestMethod]
        public void Save_WritesExpectedJsonToFile()
        {
            // Arrange
            string jsonFileName = "output.json";
            var screenDefinition = new ScreenDefinition
            {
                Screens = new List<ScreenLineEntry>
            {
                new ScreenLineEntry
                {
                    BackgroundColor = ConsoleColor.DarkBlue,
                    ForegroundColor = ConsoleColor.White,
                    Text = "Line 1"
                },
                new ScreenLineEntry
                {
                    BackgroundColor = ConsoleColor.Black,
                    ForegroundColor = ConsoleColor.Green,
                    Text = "Line 2"
                }
            }
            };

            // Act
            ScreenDefinitionService.Save(screenDefinition, jsonFileName);

            // Assert
            string savedJson = File.ReadAllText(jsonFileName);
            var deserializedScreenDefinition = JsonConvert.DeserializeObject<ScreenDefinition>(savedJson);

            Assert.IsNotNull(deserializedScreenDefinition);
            Assert.AreEqual(screenDefinition.Screens.Count, deserializedScreenDefinition.Screens.Count);

            for (int i = 0; i < screenDefinition.Screens.Count; i++)
            {
                Assert.AreEqual(screenDefinition.Screens[i].BackgroundColor, deserializedScreenDefinition.Screens[i].BackgroundColor);
                Assert.AreEqual(screenDefinition.Screens[i].ForegroundColor, deserializedScreenDefinition.Screens[i].ForegroundColor);
                Assert.AreEqual(screenDefinition.Screens[i].Text, deserializedScreenDefinition.Screens[i].Text);
            }

            File.Delete(jsonFileName);
        }
    }
}

