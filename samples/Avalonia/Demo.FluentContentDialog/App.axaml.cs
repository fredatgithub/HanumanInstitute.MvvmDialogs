using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using Microsoft.Extensions.Logging;
using Splat;

namespace Demo.Avalonia.FluentContentDialog;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        var build = Locator.CurrentMutable;
        var loggerFactory = LoggerFactory.Create(builder => builder.AddFilter(logLevel => true).AddDebug());

        build.RegisterLazySingleton(() => (IDialogService)new DialogService(
            new DialogManager(
                viewLocator: new ViewLocator(),
                logger: loggerFactory.CreateLogger<DialogManager>(),
                dialogFactory: new DialogFactory().AddFluent()),
            viewModelFactory: x => Locator.Current.GetService(x)));

        SplatRegistrations.Register<MainWindowViewModel>();
        SplatRegistrations.Register<AskTextBoxViewModel>();
        SplatRegistrations.SetupIOC();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        GC.KeepAlive(typeof(DialogService));
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = MainWindow
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static MainWindowViewModel MainWindow => Locator.Current.GetService<MainWindowViewModel>()!;
    public static AskTextBoxViewModel AskTextBox => Locator.Current.GetService<AskTextBoxViewModel>()!;
    public static IDialogService DialogService => Locator.Current.GetService<IDialogService>()!;
}
