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
using Vm.GameFramework.Options;
using static Vm.GameFramework.Platform.NativeImports;

namespace Vm.GameFramework;

public class BaseGame : IDisposable
{
    public event EventHandler<EventArgs>? Initialized;
    public event EventHandler<GameUpdatedEventArgs>? Updated;
    public event EventHandler<GameDrawnEventArgs>? Drawn;
    public event EventHandler<GameExitedEventArgs>? Exited;

    private readonly BaseGameOptions _options = new();
    private readonly GameTime _gameTime = new();

    private bool _shouldQuit;
    private bool _disposed;

    public BaseGame(Action<BaseGameOptions>? options = null) => options?.Invoke(_options);

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
        SDL_SetMainReady();
        if (
            !SDL_Init(
                SDL_INIT_AUDIO
                    | SDL_INIT_VIDEO
                    | SDL_INIT_JOYSTICK
                    | SDL_INIT_HAPTIC
                    | SDL_INIT_GAMEPAD
                    | SDL_INIT_EVENTS
                    | SDL_INIT_SENSOR
                    | SDL_INIT_CAMERA
            )
        )
        {
            throw new InvalidOperationException($"Failed to initialize SDL: {SDL_GetError()}.");
        }

        try
        {
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_NAME_STRING, _options.AppName);
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_VERSION_STRING, _options.Version.ToString());
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_IDENTIFIER_STRING, _options.Identifier);
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_CREATOR_STRING, _options.Creator);
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_COPYRIGHT_STRING, _options.Copyright);
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_URL_STRING, _options.Url.ToString());
            SetAppMetadataProperty(SDL_PROP_APP_METADATA_TYPE_STRING, _options.AppType);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Failed to set SDL app metadata.", ex);
        }

        Initialized?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateCore(GameTime gameTime)
    {
        while (SDL_PollEvent(out SDL_Event ev))
        {
            if (ev.type == SDL_EventType.SDL_EVENT_QUIT)
            {
                _shouldQuit = true;
            }
        }

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

    private static void ReleaseUnmanagedResources() => SDL_Quit();

    private static void SetAppMetadataProperty(string name, string? value)
    {
        if (!SDL_SetAppMetadataProperty(name, value))
        {
            throw new InvalidOperationException($"Failed to set SDL app metadata property '{name}': {SDL_GetError()}.");
        }
    }
}
