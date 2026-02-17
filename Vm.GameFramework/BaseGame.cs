// A cross-platform, single code base framework to develop games.
// Copyright (C) 2026  The Vm.GameFramework Contributors
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Vm.GameFramework.CustomEventArgs;

namespace Vm.GameFramework;

public class BaseGame : IDisposable
{
    public event EventHandler<EventArgs>? Initialized;
    public event EventHandler<GameUpdatedEventArgs>? Updated;
    public event EventHandler<GameDrawnEventArgs>? Drawn;
    public event EventHandler<GameExitedEventArgs>? Exited;

    private readonly GameTime _gameTime = new();

    private bool _shouldQuit; // TODO: make this change
    private bool _disposed;

    ~BaseGame() => Dispose(disposing: false);

    public void Run()
    {
        InitializeCore();
        _gameTime.Initialize();
        while (!_shouldQuit)
        {
            _gameTime.Update();
            UpdateCore(_gameTime);
            DrawCore(_gameTime);
        }

        Exited?.Invoke(this, new GameExitedEventArgs(_gameTime.TotalTime));
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void InitializeCore()
    {
        Initialized?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateCore(GameTime gameTime)
    {
        Updated?.Invoke(this, new GameUpdatedEventArgs(gameTime));
    }

    private void DrawCore(GameTime gameTime)
    {
        Drawn?.Invoke(this, new GameDrawnEventArgs(gameTime));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        ReleaseUnmanagedResources();
        if (disposing)
        {
            // TODO release managed resources here
        }

        _disposed = true;
    }

    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
    }
}
