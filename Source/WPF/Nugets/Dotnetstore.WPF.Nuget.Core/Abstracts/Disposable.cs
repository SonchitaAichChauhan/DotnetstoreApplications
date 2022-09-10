﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Dotnetstore.WPF.Nuget.Core.Abstracts;

public abstract class Disposable : IDisposable
{
    private Subject<Unit>? _whenDisposedSubject;

    ~Disposable() => Dispose(false);

    [NotMapped]
    public bool IsDisposed { get; private set; }

    [NotMapped]
    public IObservable<Unit> WhenDisposed
    {
        get
        {
            if (IsDisposed)
            {
                return Observable.Return(Unit.Default);
            }

            _whenDisposedSubject ??= new Subject<Unit>();
            return _whenDisposedSubject.AsObservable();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed) return;
        if (disposing)
        {
            DisposeManaged();
        }

        DisposeUnmanaged();

        IsDisposed = true;

        if (_whenDisposedSubject == null) return;
        _whenDisposedSubject.OnNext(Unit.Default);
        _whenDisposedSubject.OnCompleted();
        _whenDisposedSubject.Dispose();
    }

    protected virtual void DisposeManaged()
    {
    }

    protected virtual void DisposeUnmanaged()
    {
    }

    protected void ThrowIfDisposed()
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
}