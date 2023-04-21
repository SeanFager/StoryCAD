﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI.ViewManagement;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StoryCAD.Models.Tools;

/// <summary>
/// PreferencesModel contains product and licensing information,
/// and (especially) user preferences. 
/// 
/// The model is maintained from a Shell Preferences() method
/// which is launched from a Command tied to a View button,
/// and by PreferencesViewModel using a ContentDialog as the view.
/// The backing store is a file, StoryCAD.prf, which is 
/// contained with other installation files. If no .prf file is
/// present, StoryCAD.Services.Install.InstallationService's
/// InstallFiles() method will create one.
/// 
/// </summary>
public class PreferencesModel : ObservableRecipient
{
    #region Properties
    public bool Changed { get; set; }

    //User information
    public string Name { get; set; }
    public string Email { get; set; }
    public bool ErrorCollectionConsent { get; set; }
    public bool Newsletter { get; set; }

    /// <summary>
    /// This switch tracks whether this is a new 
    /// installation and if Initialization should be shown.
    /// </summary>
    public bool PreferencesInitialized { get; set; }
    public int LastSelectedTemplate { get; set; } //This is the Last Template Selected by the user.

    // Visual changes
    public SolidColorBrush PrimaryColor { get; set; } //Sets UI Color
    public SolidColorBrush SecondaryColor = new(new UISettings().GetColorValue(UIColorType.Accent)); //Sets Text Color
    public TextWrapping WrapNodeNames { get; set; }

    // Backup Information
    public bool AutoSave
    {
        get => _autoSave; 
        set => SetProperty(ref _autoSave, value);
    }

    public bool _autoSave;
    public int AutoSaveInterval { get; set; }
    public bool BackupOnOpen { get; set; }
    public bool TimedBackup { get; set; }
    public int TimedBackupInterval { get; set; }

    //Directories
    public string ProjectDirectory
    {
        get => _ProjectDirectory;
        set => SetProperty(ref _ProjectDirectory, value);
    }

    private string _ProjectDirectory;

    public string BackupDirectory
    {
        get => _BackupDirectory;
        set=> SetProperty(ref _BackupDirectory, value);
    }
    private string _BackupDirectory;

    // Recent files (set automatically)
    public string LastFile1 { get; set; }
    public string LastFile2 { get; set; }
    public string LastFile3 { get; set; }
    public string LastFile4 { get; set; }
    public string LastFile5 { get; set; }

    //Version Tracking
    public string Version { get; set; }

    // Backend server log status
    public bool RecordPreferencesStatus { get; set; }  // Last preferences change was logged successfully or not
    public bool RecordVersionStatus { get; set; }      // Last version change was logged successfully or notx
    public BrowserType PreferredSearchEngine { get; set; }      // Last version change was logged successfully or not

    public int SearchEngineIndex
    {
        get => (int)PreferredSearchEngine;
        set => PreferredSearchEngine = (BrowserType) value;
    } // Last version change was logged successfully or not
    #endregion

    #region Constructor
    public PreferencesModel()
    {
        LastFile1 = string.Empty;
        LastFile2 = string.Empty;
        LastFile3 = string.Empty;
        LastFile4 = string.Empty;
        LastFile5 = string.Empty;
        AutoSave = true;
        TimedBackup = true;
        PreferredSearchEngine = BrowserType.DuckDuckGo;
        AutoSaveInterval = 15;
        TimedBackupInterval = 5;
        WrapNodeNames = TextWrapping.WrapWholeWords;
    }
    #endregion
}