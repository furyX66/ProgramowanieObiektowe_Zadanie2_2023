using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettings _settings;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "AnimalsScreen.json";

    /// <summary>
    /// Animals screen.
    /// </summary>
    private MammalsScreen _mammalsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        ISettings settings,
        ScreenDefinition screenDefinition)
    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _settings = settings;
        _screenDefinition = screenDefinition;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            _screenDefinition = ScreenDefinitionService.Load(ScreenDefinitionJson);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 0);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 1);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 2);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 3);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 4);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 5);

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                AnimalsScreenChoices choice = (AnimalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case AnimalsScreenChoices.Mammals:
                        Console.Clear();
                        _mammalsScreen.Show();
                        break;

                    case AnimalsScreenChoices.Read:
                        Console.Clear();
                        ReadFromFile();
                        break;

                    case AnimalsScreenChoices.Save:
                        Console.Clear();
                        SaveToFile();
                        break;

                    case AnimalsScreenChoices.Exit:
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 6);
                        Thread.Sleep(700);
                        Console.Clear();
                        return;
                    default:      
                        Thread.Sleep(700);
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 7);
                        Console.Clear();
                        break;
                }
            }
            catch
            {
                Console.Clear();
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 7);        
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// Save to file.
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            Console.Write("Save data to file: ");
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
        }
        catch
        {
            Console.WriteLine("Data saving was not successful.");
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        try
        {
            Console.Write("Read data from file: ");
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Read(fileName);
            Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
        }
        catch
        {
            Console.WriteLine("Data reading from was not successful.");
        }
    }

    #endregion // Private Methods
}
