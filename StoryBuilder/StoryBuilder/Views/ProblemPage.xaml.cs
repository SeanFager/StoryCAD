﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using StoryBuilder.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StoryBuilder.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProblemPage : BindablePage
    {
        public ProblemViewModel ProblemVm; 
        public ShellViewModel ShellVm => Ioc.Default.GetService<ShellViewModel>();


        public ProblemPage()
        {
            ProblemVm = Ioc.Default.GetService<ProblemViewModel>();
            this.InitializeComponent();
            this.DataContext = ProblemVm;
        }

        //private void Conflict_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        //{
        //    FlyoutShowOptions myOption = new FlyoutShowOptions();
        //    myOption.ShowMode = FlyoutShowMode.Transient;
        //    ConflictCommandBarFlyout.ShowAt(NavigationTree, myOption);
        //}
    }
}
