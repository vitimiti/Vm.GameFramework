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

using System.Diagnostics;

namespace Vm.GameFramework;

public sealed class GameTime
{
    private Stopwatch? _stopwatch;
    private TimeSpan _lastTimestamp;

    internal GameTime() { }

    public TimeSpan DeltaTime { get; private set; }

    public TimeSpan TotalTime { get; private set; }

    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;

    public float TotalSeconds => (float)TotalTime.TotalSeconds;

    internal void Initialize()
    {
        _stopwatch = Stopwatch.StartNew();
        _lastTimestamp = _stopwatch.Elapsed;
        DeltaTime = TimeSpan.Zero;
        TotalTime = TimeSpan.Zero;
    }

    internal void Update()
    {
        if (_stopwatch is null)
        {
            throw new InvalidOperationException($"{nameof(GameTime)} is not initialized.");
        }

        TimeSpan now = _stopwatch.Elapsed;
        DeltaTime = now > _lastTimestamp ? now - _lastTimestamp : TimeSpan.Zero;
        _lastTimestamp = now;

        TotalTime += DeltaTime;
    }
}
