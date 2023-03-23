﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using StoryBuilder.Controls;
using StoryBuilder.Models;
using StoryBuilder.Services.Logging;
using StoryBuilder.Services.Messages;
using StoryBuilder.Services.Navigation;

namespace StoryBuilder.ViewModels;

public class ProblemViewModel : ObservableRecipient, INavigable
{
    #region Fields

    private readonly LogService _logger;
    private bool _changeable;
    private bool _changed;

    #endregion

    #region Properties

    // StoryElement data

    private Guid _uuid;
    public Guid Uuid
    {
        get => _uuid;
        set => SetProperty(ref _uuid, value);
    }

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (_changeable && _name != value) // Name changed?
            {
                _logger.Log(LogLevel.Info, $"Requesting Name change from {_name} to {value}");
                NameChangeMessage _msg = new(_name, value);
                Messenger.Send(new NameChangedMessage(_msg));
            }
            SetProperty(ref _name, value);
        }
    }

    // Problem problem data
    private string _problemType;
    public string ProblemType
    {
        get => _problemType;
        set => SetProperty(ref _problemType, value);
    }

    private string _problemCategory;
    public string ProblemCategory
    {
        get => _problemCategory;
        set => SetProperty(ref _problemCategory, value);
    }

    private string _subject;
    public string Subject
    {
        get => _subject;
        set => SetProperty(ref _subject, value);
    }

    private string _problemSource;
    public string ProblemSource
    {
        get => _problemSource;
        set => SetProperty(ref _problemSource, value);
    }

    private string _conflictType;
    public string ConflictType
    {
        get => _conflictType;
        set => SetProperty(ref _conflictType, value);
    }
    private string _storyQuestion;
    public string StoryQuestion
    {
        get => _storyQuestion;
        set => SetProperty(ref _storyQuestion, value);
    }

    // Problem protagonist data

    private string _protagonist;  // The Guid of a Character StoryElement
    public string Protagonist
    {
        get => _protagonist;
        set => SetProperty(ref _protagonist, value);
    }

    private string _protGoal;
    public string ProtGoal
    {
        get => _protGoal;
        set => SetProperty(ref _protGoal, value);
    }

    private string _protMotive;
    public string ProtMotive
    {
        get => _protMotive;
        set => SetProperty(ref _protMotive, value);
    }

    private string _protConflict;
    public string ProtConflict
    {
        get => _protConflict;
        set => SetProperty(ref _protConflict, value);
    }

    // Problem antagonist data

    private string _antagonist;  // The Guid of a Character StoryElement
    public string Antagonist
    {
        get => _antagonist;
        set => SetProperty(ref _antagonist, value);
    }

    private string _antagGoal;
    public string AntagGoal
    {
        get => _antagGoal;
        set => SetProperty(ref _antagGoal, value);
    }

    private string _antagMotive;
    public string AntagMotive
    {
        get => _antagMotive;
        set => SetProperty(ref _antagMotive, value);
    }

    private string _antagConflict;
    public string AntagConflict
    {
        get => _antagConflict;
        set => SetProperty(ref _antagConflict, value);
    }

    // Problem resolution data

    private string _outcome;
    public string Outcome
    {
        get => _outcome;
        set => SetProperty(ref _outcome, value);
    }

    private string _method;
    public string Method
    {
        get => _method;
        set => SetProperty(ref _method, value);
    }

    private string _theme;
    public string Theme
    {
        get => _theme;
        set => SetProperty(ref _theme, value);
    }

    private string _premise;
    public string Premise
    {
        get => _premise;
        set => SetProperty(ref _premise, value);
    }

    // Problem notes data

    private string _notes;
    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    // The ProblemModel is passed when ProblemPage is navigated to
    private ProblemModel _model;
    public ProblemModel Model
    {
        get => _model;
        set => _model = value;
    }

    public RelayCommand ConflictCommand { get; }

    #endregion

    #region Methods

    public void Activate(object parameter)
    {
        Model = (ProblemModel)parameter;
        LoadModel();
    }

    /// <summary>
    /// Saves this VM back to the story.
    /// </summary>
    /// <param name="parameter"></param>
    public void Deactivate(object parameter)
    {
        SaveModel();
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (_changeable)
        {
            _changed = true;
            ShellViewModel.ShowChange();
        }
    }

    private void LoadModel()
    {
        _changeable = false;
        _changed = false;

        Uuid = Model.Uuid;
        Name = Model.Name;
        ProblemType = Model.ProblemType;
        ConflictType = Model.ConflictType;
        ProblemCategory = Model.ProblemCategory;
        Subject = Model.Subject;
        StoryQuestion = Model.StoryQuestion;
        ProblemSource = Model.ProblemSource;
        // Character instances like Protagonist and Antagonist are 
        // read and written as the CharacterModel's StoryElement Guid 
        // string. A binding converter, StringToStoryElementConverter,
        // provides the UI the corresponding StoryElement itself.f
        Protagonist = Model.Protagonist ?? string.Empty;
        ProtGoal = Model.ProtGoal;
        ProtMotive = Model.ProtMotive;
        ProtConflict = Model.ProtConflict;
        Antagonist = Model.Antagonist ?? string.Empty;
        AntagGoal = Model.AntagGoal;
        AntagMotive = Model.AntagMotive;
        AntagConflict = Model.AntagConflict;
        Outcome = Model.Outcome;
        Method = Model.Method;
        Theme = Model.Theme;
        Premise = Model.Premise;
        Notes = Model.Notes;

        _changeable = true;
    }

    internal void SaveModel()
    {
        if (_changed)
        {
            // Story.Uuid is read-only and cannot be assigned
            Model.Name = Name;
            Model.ProblemType = ProblemType;
            Model.ConflictType = ConflictType;
            Model.ProblemCategory = ProblemCategory;
            Model.Subject = Subject;
            Model.ProblemSource = ProblemSource;
            Model.Protagonist = Protagonist ?? string.Empty;
            Model.ProtGoal = ProtGoal;
            Model.ProtMotive = ProtMotive;
            Model.ProtConflict = ProtConflict;
            Model.Antagonist = Antagonist ?? string.Empty;
            Model.AntagGoal = AntagGoal;
            Model.AntagMotive = AntagMotive;
            Model.AntagConflict = AntagConflict;
            Model.Outcome = Outcome;
            Model.Method = Method;
            Model.Theme = Theme;

            // Write RTF files
            Model.StoryQuestion = StoryQuestion;
            Model.Premise = Premise;
            Model.Notes = Notes;

            //_logger.Log(LogLevel.Info, string.Format("Requesting IsDirty change to true"));
            //Messenger.Send(new IsChangedMessage(Changed));
        }
    }

    /// <summary>
    /// Opens conflict builder
    /// </summary>
    public async void ConflictTool()
    {
        _logger.Log(LogLevel.Info, "Displaying Conflict Finder tool dialog");

        //Creates and shows content
        ContentDialog _conflictDialog = new()
        {
            Title = "Conflict builder",
            XamlRoot = GlobalData.XamlRoot,
            PrimaryButtonText = "Copy to Protagonist",
            SecondaryButtonText = "Copy to Antagonist",
            CloseButtonText = "Close"
        };
        Conflict _selectedConflict = new();
        _conflictDialog.Content = _selectedConflict;
        ContentDialogResult _result = await _conflictDialog.ShowAsync();

        if (_selectedConflict.ExampleText == null) {_selectedConflict.ExampleText = "";}
        switch (_result)
        {
            // Copy to Protagonist conflict
            case ContentDialogResult.Primary:
                ProtConflict = _selectedConflict.ExampleText;
                _logger.Log(LogLevel.Info, "Conflict Finder finished (copied to protagonist)");
                break;
            // Copy to Antagonist conflict
            case ContentDialogResult.Secondary:
                AntagConflict = _selectedConflict.ExampleText;
                _logger.Log(LogLevel.Info, "Conflict Finder finished (copied to antagonist)");
                break;
            default:
                _logger.Log(LogLevel.Info, "Conflict Finder canceled");
                break;
        }
    }

    #endregion

    #region Control initialization sources

    // ListControls sources
    public ObservableCollection<string> ProblemTypeList;
    public ObservableCollection<string> ConflictTypeList;
    public ObservableCollection<string> ProblemCategoryList;
    public ObservableCollection<string> SubjectList;
    public ObservableCollection<string> ProblemSourceList;
    public ObservableCollection<string> GoalList;
    public ObservableCollection<string> MotiveList;
    public ObservableCollection<string> ConflictList;
    public ObservableCollection<string> OutcomeList;
    public ObservableCollection<string> MethodList;
    public ObservableCollection<string> ThemeList;
    #endregion;

    #region Constructors

    public ProblemViewModel()
    {
        _logger = Ioc.Default.GetService<LogService>();

        ProblemType = string.Empty;
        ConflictType = string.Empty;
        Subject = string.Empty;
        ProblemSource = string.Empty;
        StoryQuestion = string.Empty;
        Protagonist = null;
        ProtGoal = string.Empty;
        ProtMotive = string.Empty;
        ProtConflict = string.Empty;
        Antagonist = string.Empty;
        AntagGoal = string.Empty;
        AntagMotive = string.Empty;
        AntagConflict = string.Empty;
        Outcome = string.Empty;
        Method = string.Empty;
        Theme = string.Empty;
        Premise = string.Empty;
        Notes = string.Empty;
        try
        {
            Dictionary<string, ObservableCollection<string>> _lists = GlobalData.ListControlSource;
            ProblemTypeList = _lists["ProblemType"];
            ConflictTypeList = _lists["ConflictType"];
            ProblemCategoryList = _lists["ProblemCategory"];
            SubjectList = _lists["ProblemSubject"];
            ProblemSourceList = _lists["ProblemSource"];
            GoalList = _lists["Goal"];
            MotiveList = _lists["Motive"];
            ConflictList = _lists["Conflict"];
            OutcomeList = _lists["Outcome"];
            MethodList = _lists["Method"];
            ThemeList = _lists["Theme"];
        }
        catch (Exception e)
        {
            _logger.LogException(LogLevel.Fatal, e, "Error loading lists in Problem view model");
            ShowError();
            throw new MissingManifestResourceException();
        }

        ConflictCommand = new RelayCommand(ConflictTool, () => true);

        PropertyChanged += OnPropertyChanged;
    }
    #endregion

    async void ShowError()
    {
        await new ContentDialog()
        {
            XamlRoot = GlobalData.XamlRoot,
            Title = "Error loading resources",
            Content = "An error has occurred, please reinstall or update StoryBuilder to continue.",
            CloseButtonText = "Close"
        }.ShowAsync();
    }
}