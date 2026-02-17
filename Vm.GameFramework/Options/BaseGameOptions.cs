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

namespace Vm.GameFramework.Options;

public sealed class BaseGameOptions
{
    public string AppName { get; set; } = "Vm.GameFramework Game";

    public Version Version { get; set; } = new(1, 0, 0);

    public string Identifier { get; set; } = "game.gameframework.vm";

    public string Creator { get; set; } = "Vm.GameFramework Contributors";

    public string Copyright { get; set; } = "Copyright (C) 2026 The Vm.GameFramework Contributors";

    public Uri Url { get; set; } = new("https://github.com/vitimiti/Vm.GameFramework");

    public string AppType { get; set; } = "game";
}
