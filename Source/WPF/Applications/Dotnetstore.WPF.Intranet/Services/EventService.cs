using Dotnetstore.WPF.Intranet.Interfaces;
using System;

namespace Dotnetstore.WPF.Intranet.Services;

public class EventService : IEventService
{
    public event EventHandler? CloseApplication;

    void IEventService.RunCloseApplication()
    {
        CloseApplication?.Invoke(this, EventArgs.Empty);
    }
}