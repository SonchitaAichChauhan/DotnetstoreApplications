using System;

namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IEventService
{
    event EventHandler CloseApplication;

    event EventHandler SetWindowRestore;

    void RunCloseApplication();

    void RunSetWindowRestore();
}