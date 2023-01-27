﻿namespace Demo.ActivateNonModalDialog;

public class CurrentTimeDialogViewModel : ObservableObject
{
    // ReSharper disable once NotAccessedField.Local
    private DispatcherTimer? timer;

    public CurrentTimeDialogViewModel()
    {
        StartClockCommand = new RelayCommand(StartClock);
    }

    public ICommand StartClockCommand { get; }

    public DateTime CurrentTime => DateTime.Now;

    private void StartClock()
    {
        timer = new DispatcherTimer(
            TimeSpan.FromSeconds(1),
            DispatcherPriority.Normal,
            OnTick,
            Dispatcher.CurrentDispatcher);
    }

    private void OnTick(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CurrentTime));
    }
}
