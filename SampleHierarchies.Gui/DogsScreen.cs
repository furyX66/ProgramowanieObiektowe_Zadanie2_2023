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
public sealed class DogsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettings _settings;
    private ScreenDefinition _screenDefinition;
    public override string? ScreenDefinitionJson { get; set; } = "DogsScreen.json";

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public DogsScreen(IDataService dataService, ISettings settings, ScreenDefinition screenDefinition)
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
                        ListDogs();
                        break;

                    case GeneralMammalsScreenChoices.Create:
                        Console.Clear();
                        AddDog(); 
                        break;

                    case GeneralMammalsScreenChoices.Delete:
                        Console.Clear();
                        DeleteDog();
                        break;

                    case GeneralMammalsScreenChoices.Modify:
                        Console.Clear();
                        EditDogMain();
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
    /// List all dogs.
    /// </summary>
    private void ListDogs()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Dogs is not null &&
            _dataService.Animals.Mammals.Dogs.Count > 0)
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 10);
            int i = 1;
            foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
            {
                Console.Write($"Dog number {i}, ");
                dog.Display();
                i++;
            }
        }
        else
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 11);
        }
    }

    /// <summary>
    /// Add a dog.
    /// </summary>
    private void AddDog()
    {
        try
        {
            Dog dog = AddEditDog();
            _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 12);
        }
        catch
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 13);
        }
    }

    /// <summary>
    /// Deletes a dog.
    /// </summary>
    private void DeleteDog()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 14);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
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
    /// Edits an existing dog after choice made.
    /// </summary>
    private void EditDogMain()
    {
        try
        {
            ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 18);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                Dog dogEdited = AddEditDog();
                dog.Copy(dogEdited);
                ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 19);
                dog.Display();
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
    /// Adds/edit specific dog.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Dog AddEditDog()
    {

        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 22);
        string? name = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 23);
        string? ageAsString = Console.ReadLine();
        ScreenDefinitionService.PrintLine(_screenDefinition.Screens, 24);
        string? breed = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (breed is null)
        {
            throw new ArgumentNullException(nameof(breed));
        }
        int age = Int32.Parse(ageAsString);
        Dog dog = new Dog(name, age, breed);

        return dog;
    }

    #endregion // Private Methods
}
