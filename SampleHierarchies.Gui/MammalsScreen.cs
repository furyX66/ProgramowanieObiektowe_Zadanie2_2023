using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>
    private DogsScreen _dogsScreen;
    private AfricanElephantsScreen _africanElephantsScreen;
    private OrangutanScreen _orangutanScreen;
    private BeaverScreen _beeverScreen;
    private ISettings _settings;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "MammalsScreen.json";

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    /// <param name="africanElephantsScreen">African elphants screen</param>
    /// <param name="orangutanScreen">Orangutan screen</param>
    public MammalsScreen(DogsScreen dogsScreen, AfricanElephantsScreen africanElephantsScreen, OrangutanScreen orangutanScreen, BeaverScreen beeverScreen, ISettings settings, ScreenDefinition screenDefinition)
    {
        _dogsScreen = dogsScreen;
        _africanElephantsScreen = africanElephantsScreen;
        _orangutanScreen = orangutanScreen;
        _beeverScreen = beeverScreen;
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
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 6);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 7);
            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MammalsScreenChoices.Dogs:
                        Console.Clear();
                        _dogsScreen.Show();
                        break;
                    case MammalsScreenChoices.AfricanElephants:
                        Console.Clear();
                        _africanElephantsScreen.Show(); 
                        break;
                    case MammalsScreenChoices.Orangutans:
                        Console.Clear();
                        _orangutanScreen.Show();
                        break;
                    case MammalsScreenChoices.Beavers:
                        Console.Clear();
                        _beeverScreen.Show();
                        break;

                    case MammalsScreenChoices.Exit:
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 8);
                        Thread.Sleep(700);
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 9);
                        break;
                }
            }
            catch
            {
                Console.Clear();
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 9);
            }
        }
    }

    #endregion // Public Methods
}
