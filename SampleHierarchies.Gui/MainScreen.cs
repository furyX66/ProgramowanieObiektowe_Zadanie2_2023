using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "MainScreen.json";

    /// <summary>
    /// Animals screen.
    /// </summary>
    private AnimalsScreen _animalsScreen;
    private ISettings _settings;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    /// <param name="settings">Settings</param>
    public MainScreen(
        IDataService dataService,
        AnimalsScreen animalsScreen,
        ISettings settings,
        ScreenDefinition screenDefinition)
    {
        _dataService = dataService;
        _animalsScreen = animalsScreen;
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
            Console.ResetColor();
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

                MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MainScreenChoices.Animals:
                        Console.Clear();
                        _animalsScreen.Show();
                        break;
                    case MainScreenChoices.Settings:
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 6);
                        break;

                    case MainScreenChoices.Exit:
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 7);
                        return;
                    default:
                        Console.Clear();
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 8);
                        break;
                }
            }
            catch (FormatException) 
            {
                Console.Clear();
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 8);
            }
        }
    }
}
        #endregion // Public Methods
