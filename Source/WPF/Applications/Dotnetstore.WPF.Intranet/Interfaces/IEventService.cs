using System;

namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IEventService
{
    event EventHandler CloseApplication;

    void RunCloseApplication();
}