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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Vm.GameFramework.Platform;

[SuppressMessage(
    "csharpsquid",
    "S2342:Enumeration types should comply with a naming convention",
    Justification = "Native API names"
)]
internal static partial class NativeImports
{
    private const string LibSdl = "SDL3";

    static NativeImports() =>
        NativeLibrary.SetDllImportResolver(
            typeof(NativeImports).Assembly,
            (name, assembly, path) =>
                NativeLibrary.Load(
                    name switch
                    {
                        LibSdl => name switch
                        {
                            _ when OperatingSystem.IsWindows() => "SDL3.dll",
                            _ when OperatingSystem.IsMacOS() => "libSDL3.0.dylib",
                            _ when OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD() => "libSDL3.so.0",
                            _ => name,
                        },
                        _ => name,
                    },
                    assembly,
                    path
                )
        );
}
