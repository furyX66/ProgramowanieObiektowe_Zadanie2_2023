using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Runtime;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class AfricanElephantsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettings _settings;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "AfricanElephantsScreen.json";

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public AfricanElephantsScreen(IDataService dataService, ISettings settings, ScreenDefinition screenDefinition)
    {
        _dataService = dataService;
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

                GeneralMammalsScreenChoices choice = (GeneralMammalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case GeneralMammalsScreenChoices.List:
                        Console.Clear();
                        ListElephants();
                        break;

                    case GeneralMammalsScreenChoices.Create:
                        Console.Clear();
                        AddElephant(); 
                        break;

                    case GeneralMammalsScreenChoices.Delete:
                        Console.Clear();
                        DeleteElephant();
                        break;

                    case GeneralMammalsScreenChoices.Modify:
                        Console.Clear();
                        EditElephantMain();
                        break;

                    case GeneralMammalsScreenChoices.Exit:
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

    #region Private Methods

    /// <summary>
    /// List all Elephants.
    /// </summary>
    private void ListElephants()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.AfricanElephants is not null &&
            _dataService.Animals.Mammals.AfricanElephants.Count > 0)
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 10);
            int i = 1;
            foreach (AfricanElephant africanElephant in _dataService.Animals.Mammals.AfricanElephants)
            {
                Console.Write($"African elephants number {i}, ");
                africanElephant.Display();
                i++;
            }
        }
        else
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 11);
        }
    }

    /// <summary>
    /// Add an elephant.
    /// </summary>
    private void AddElephant()
    {
        try
        {
            AfricanElephant elephant = AddEditElephant();
            _dataService?.Animals?.Mammals?.AfricanElephants.Add(elephant);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 12);
        }
        catch
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 13);
        }
    }

    /// <summary>
    /// Deletes a elephant.
    /// </summary>
    private void DeleteElephant()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            AfricanElephant? elephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (elephant is not null)
            {
                _dataService?.Animals?.Mammals?.AfricanElephants?.Remove(elephant);
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 15);
            }
            else
            {
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 16);
            }
        }
        catch
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 17);
        }
    }

    /// <summary>
    /// Edits an existing elephant after choice made.
    /// </summary>
    private void EditElephantMain()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 18);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            AfricanElephant? elephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (elephant is not null)
            {
                AfricanElephant elephantEdited = AddEditElephant();
                elephant.Copy(elephantEdited);
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 19);
                elephant.Display();
            }
            else
            {
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 20);
            }
        }
        catch
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 21);
        }
    }

    /// <summary>
    /// Adds/edit specific elephant.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private AfricanElephant AddEditElephant()
    {
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 22);
        string? name = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 23);
        string? ageAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 24);
        string? heightAsString = (Console.ReadLine());
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 25);
        string? weightAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 26);
        string? tuskLengthAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 27);
        string? longLifespanAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 28);
        string? socialBehavior = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (socialBehavior is null)
        {
            throw new ArgumentNullException(nameof(socialBehavior));
        }
        if (heightAsString is null)
        {
            throw new ArgumentNullException(nameof(heightAsString));
        }
        if (weightAsString is null)
        {
            throw new ArgumentNullException(nameof(weightAsString));
        }
        if (tuskLengthAsString is null)
        {
            throw new ArgumentNullException(nameof(tuskLengthAsString));
        }
        if (longLifespanAsString is null)
        {
            throw new ArgumentNullException(nameof(longLifespanAsString));
        }
        int age = Int32.Parse(ageAsString);
        float height = float.Parse(heightAsString);
        float weight = float.Parse(weightAsString);
        float tuskLength = float.Parse(tuskLengthAsString);
        int longLifespan = Int32.Parse(longLifespanAsString);   

        AfricanElephant elephant = new AfricanElephant(name, age, height, weight, tuskLength, longLifespan, socialBehavior);

        return elephant;
    }

    #endregion // Private Methods
}
