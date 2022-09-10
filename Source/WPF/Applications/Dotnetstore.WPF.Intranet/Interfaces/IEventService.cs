using System;

namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IEventService
{
    event EventHandler CloseApplication;

    event EventHandler SetWindowMinimize;

    event EventHandler SetWindowRestore;

    void RunCloseApplication();

    void RunSetWindowMinimize();

    void RunSetWindowRestore();
}