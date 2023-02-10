﻿using System.Threading.Tasks;
using System.Windows.Input;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using ReactiveUI;

namespace Demo.Avalonia.FluentMessageBoxTaskDialog;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    public ICommand ShowMessageBoxWithMessageCommand { get; }
    public ICommand ShowMessageBoxWithCaptionCommand { get; }
    public ICommand ShowMessageBoxWithButtonCommand { get; }
    public ICommand ShowMessageBoxWithIconCommand { get; }
    public ICommand ShowMessageBoxWithDefaultResultCommand { get; }

    public MainWindowViewModel(IDialogService dialogService)
    {
        this._dialogService = dialogService;

        ShowMessageBoxWithMessageCommand = ReactiveCommand.CreateFromTask(ShowMessageBoxWithMessage);
        ShowMessageBoxWithCaptionCommand = ReactiveCommand.CreateFromTask(ShowMessageBoxWithCaption);
        ShowMessageBoxWithButtonCommand = ReactiveCommand.CreateFromTask(ShowMessageBoxWithButton);
        ShowMessageBoxWithIconCommand = ReactiveCommand.CreateFromTask(ShowMessageBoxWithIcon);
        ShowMessageBoxWithDefaultResultCommand = ReactiveCommand.CreateFromTask(ShowMessageBoxWithDefaultResult);
    }

    private string _confirmation = string.Empty;
    public string Confirmation
    {
        get => _confirmation;
        private set => this.RaiseAndSetIfChanged(ref _confirmation, value, nameof(Confirmation));
    }

    private async Task ShowMessageBoxWithMessage()
    {
        var result = await _dialogService.ShowMessageBoxAsync(
            this,
            "This is the text.");

        UpdateResult(result);
    }

    private async Task ShowMessageBoxWithCaption()
    {
        var result = await _dialogService.ShowMessageBoxAsync(
            this,
            "This is the text.",
            "This Is The Caption");

        UpdateResult(result);
    }

    private async Task ShowMessageBoxWithButton()
    {
        var result = await _dialogService.ShowMessageBoxAsync(
            this,
            "This is the text.",
            "This Is The Caption",
            MessageBoxButton.OkCancel);

        UpdateResult(result);
    }

    private async Task ShowMessageBoxWithIcon()
    {
        var result = await _dialogService.ShowMessageBoxAsync(
            this,
            "This is the text.",
            "This Is The Caption",
            MessageBoxButton.OkCancel,
            MessageBoxImage.Information);

        UpdateResult(result);
    }

    private async Task ShowMessageBoxWithDefaultResult()
    {
        var result = await _dialogService.ShowMessageBoxAsync(
            this,
            "This is the text.",
            "This Is The Caption",
            MessageBoxButton.OkCancel,
            MessageBoxImage.Information,
            true);

        UpdateResult(result);
    }

    private void UpdateResult(bool? result) =>
        Confirmation = result == true ? "We got confirmation to continue!" : string.Empty;
}
