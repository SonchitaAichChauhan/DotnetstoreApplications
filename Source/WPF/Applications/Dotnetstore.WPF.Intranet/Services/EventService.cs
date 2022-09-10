using Dotnetstore.WPF.Intranet.Interfaces;
using System;

namespace Dotnetstore.WPF.Intranet.Services;

public class EventService : IEventService
{
    public event EventHandler? CloseApplication;

    public event EventHandler? SetWindowMinimize;

    public event EventHandler? SetWindowRestore;

    void IEventService.RunCloseApplication()
    {
        CloseApplication?.Invoke(this, EventArgs.Empty);
    }

    void IEventService.RunSetWindowMinimize()
    {
        SetWindowMinimize?.Invoke(this, EventArgs.Empty);
    }

    void IEventService.RunSetWindowRestore()
    {
        SetWindowRestore?.Invoke(this, EventArgs.Empty);
    }
}