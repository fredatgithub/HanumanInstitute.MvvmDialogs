﻿using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;

namespace Demo.Wpf.NonModalCustomDialog;

public class MainWindowViewModel : ObservableObject
{
    private readonly IDialogService dialogService;

    public MainWindowViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;

        ImplicitShowCommand = new RelayCommand(ImplicitShow);
        ExplicitShowCommand = new RelayCommand(ExplicitShow);
    }

    public ICommand ImplicitShowCommand { get; }

    public ICommand ExplicitShowCommand { get; }

    private void ImplicitShow()
    {
        Show(viewModel => dialogService.Show(this, viewModel));
    }

    private void ExplicitShow()
    {
        Show(viewModel => dialogService.Show<CurrentTimeCustomDialog>(this, viewModel));
    }

    private void Show(Action<CurrentTimeCustomDialogViewModel> show)
    {
        var dialogViewModel = dialogService.CreateViewModel<CurrentTimeCustomDialogViewModel>();
        show(dialogViewModel);
    }
}
