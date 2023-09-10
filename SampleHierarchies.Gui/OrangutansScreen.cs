using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class OrangutanScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettings _settings;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "OrangutanScreen.json";
    /// <summary>
    /// Local variables.
    /// </summary>
    bool hasArborealLifeStyle;
    bool hasOpposableThumbs;
    bool hasSolitariBehavior;
    bool hasSlowReproductiveRate;

    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public OrangutanScreen(IDataService dataService, ISettings settings, ScreenDefinition screenDefinition)
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
                        ListOrangutans();
                        break;

                    case GeneralMammalsScreenChoices.Create:
                        Console.Clear();
                        AddOrangutan(); break;

                    case GeneralMammalsScreenChoices.Delete:
                        Console.Clear();
                        DeleteOrangutan();
                        break;

                    case GeneralMammalsScreenChoices.Modify:
                        Console.Clear();
                        EditOrangutanMain();
                        break;

                    case GeneralMammalsScreenChoices.Exit:
                        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 8);
                        Thread.Sleep(700);
                        Console.Clear();
                        return;
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
    /// List all Orangutans
    /// </summary>
    private void ListOrangutans()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Orangutans is not null &&
            _dataService.Animals.Mammals.Orangutans.Count > 0)
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 10);
            int i = 1;
            foreach (Orangutan orangutan in _dataService.Animals.Mammals.Orangutans)
            {
                Console.Write($"Orangutans number {i}, ");
                orangutan.Display();
                i++;
            }
        }
        else
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 11);
        }
    }

    /// <summary>
    /// Add an orangutan.
    /// </summary>
    private void AddOrangutan()
    {
        try
        {
            Orangutan orangutan = AddEditOrangutan();
            _dataService?.Animals?.Mammals?.Orangutans.Add(orangutan);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 12);
        }
        catch
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 13);
        }
    }

    /// <summary>
    /// Deletes a orangutan.
    /// </summary>
    private void DeleteOrangutan()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Orangutan? orangutan = (Orangutan?)(_dataService?.Animals?.Mammals?.Orangutans
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (orangutan is not null)
            {
                _dataService?.Animals?.Mammals?.Orangutans?.Remove(orangutan);
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
    /// Edits an existing orangutan after choice made.
    /// </summary>
    private void EditOrangutanMain()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 18);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Orangutan? orangutan = (Orangutan?)(_dataService?.Animals?.Mammals?.Orangutans
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (orangutan is not null)
            {
                Orangutan orangutanEdited = AddEditOrangutan();
                orangutan.Copy(orangutanEdited);
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 19);
                orangutan.Display();
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
    /// Adds/edit specific orangutan.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Orangutan AddEditOrangutan()
    {
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 22);
        string? name = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 23);
        string? ageAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 24);
        string? arborealLifeStyle = Console.ReadLine().ToLower();
        if (arborealLifeStyle == "yes" || arborealLifeStyle == "y")
        {
            hasArborealLifeStyle = true;
        }
        else
        {
            hasArborealLifeStyle = false;
        }
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 25);
        string? opposableThumbs = Console.ReadLine().ToLower();
        if (opposableThumbs == "yes" || opposableThumbs == "y")
        {
            hasOpposableThumbs = true;
        }
        else
        {
            hasOpposableThumbs = false;
        }
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 26);
        string? solitaryBehavior = Console.ReadLine().ToLower();
        if (solitaryBehavior == "yes" || solitaryBehavior == "y")
        {
            hasSolitariBehavior = true;
        }
        else
        {
            hasSolitariBehavior = false;
        }
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 27);
        string? slowReproductiveRate = Console.ReadLine().ToLower();
        if (slowReproductiveRate == "yes" || slowReproductiveRate == "y")
        {
            hasSlowReproductiveRate = true;
        }
        else
        {
            hasSlowReproductiveRate = false;
        }
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 28);
        int highIntelligence = int.Parse(Console.ReadLine());
        
        
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (arborealLifeStyle is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (opposableThumbs is null)
        {
            throw new ArgumentNullException(nameof(opposableThumbs));
        }
        if (solitaryBehavior is null)
        {
            throw new ArgumentNullException(nameof(solitaryBehavior));
        }
        if (slowReproductiveRate is null)
        {
            throw new ArgumentNullException(nameof(slowReproductiveRate));
        }
        int age = Int32.Parse(ageAsString);
        Orangutan orangutan = new Orangutan(name, age, hasArborealLifeStyle, hasOpposableThumbs, hasSolitariBehavior, hasSlowReproductiveRate, highIntelligence);

        return orangutan;
    }

    #endregion // Private Methods
}
