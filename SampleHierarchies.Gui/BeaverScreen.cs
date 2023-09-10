using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class BeaverScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettings _settings;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "BeaverScreen.json";
    ///<summary>
    ///Local variables
    ///</summary>
    bool canBuildDam;

    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public BeaverScreen(IDataService dataService, ISettings settings, ScreenDefinition screenDefinition)
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
                        ListBeavers();
                        break;

                    case GeneralMammalsScreenChoices.Create:
                        Console.Clear();
                        AddBeaver();
                        break;

                    case GeneralMammalsScreenChoices.Delete:
                        Console.Clear();
                        DeleteBeaver();
                        break;

                    case GeneralMammalsScreenChoices.Modify:
                        Console.Clear();
                        EditBeaverMain();
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
    /// List all Beavers
    /// </summary>
    private void ListBeavers()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Beavers is not null &&
            _dataService.Animals.Mammals.Beavers.Count > 0)
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 10);
            int i = 1;
            foreach (Beaver beaver in _dataService.Animals.Mammals.Beavers)
            {
                Console.Write($"Beaver number {i}, ");
                beaver.Display();
                i++;
            }
        }
        else
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 11);
        }
    }

    /// <summary>
    /// Add a beaver.
    /// </summary>
    private void AddBeaver()
    {
        try
        {
            Beaver beaver = AddEditBeaver();
            _dataService?.Animals?.Mammals?.Beavers.Add(beaver);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 12);
        }
        catch
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 13);
        }
    }

    /// <summary>
    /// Deletes a beaver.
    /// </summary>
    private void DeleteBeaver()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Beaver? beaver = (Beaver?)(_dataService?.Animals?.Mammals?.Beavers
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (beaver is not null)
            {
                _dataService?.Animals?.Mammals?.Beavers?.Remove(beaver);
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
    /// Edits an existing beaver after choice made.
    /// </summary>
    private void EditBeaverMain()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 18);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Beaver? beaver = (Beaver?)(_dataService?.Animals?.Mammals?.Beavers
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (beaver is not null)
            {
                Beaver beaverEdited = AddEditBeaver();
                beaver.Copy(beaverEdited);
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 19);
                beaver.Display();
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
    /// Adds/edit specific beaver.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Beaver AddEditBeaver()
    {
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 22);
        string? name = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 23);
        string? ageAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 24);
        string? color = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 25);
        string? favoriteFood = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 26);
        string? tailLenghtAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 27);
        string? isDamBuilder = Console.ReadLine().ToLower();
        if (isDamBuilder == "yes"|| isDamBuilder == "y") 
        {
            canBuildDam=true;
        }
        else 
        { 
            canBuildDam=false; 
        } 
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (color is null)
        {
            throw new ArgumentNullException(nameof(color));
        }
        if (favoriteFood is null)
        {
            throw new ArgumentNullException(nameof(favoriteFood));
        }
        if (tailLenghtAsString is null)
        {
            throw new ArgumentNullException(nameof(tailLenghtAsString));
        }
        int age = Int32.Parse(ageAsString);
        int tailLenght = int.Parse(tailLenghtAsString);
        Beaver beaver = new Beaver(name, age, color, favoriteFood, tailLenght, canBuildDam);

        return beaver;
    }

    #endregion // Private Methods
}
