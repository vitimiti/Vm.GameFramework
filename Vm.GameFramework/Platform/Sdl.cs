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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vm.GameFramework.Platform.CustomMarshallers;

namespace Vm.GameFramework.Platform;

[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Native API names")]
internal static unsafe partial class NativeImports
{
    public const uint SDL_INIT_AUDIO = 0x00000010U;
    public const uint SDL_INIT_VIDEO = 0x00000020U;
    public const uint SDL_INIT_JOYSTICK = 0x00000200U;
    public const uint SDL_INIT_HAPTIC = 0x00001000U;
    public const uint SDL_INIT_GAMEPAD = 0x00002000U;
    public const uint SDL_INIT_EVENTS = 0x00004000U;
    public const uint SDL_INIT_SENSOR = 0x00008000U;
    public const uint SDL_INIT_CAMERA = 0x00010000U;

    public const string SDL_PROP_APP_METADATA_NAME_STRING = "SDL.app.metadata.name";
    public const string SDL_PROP_APP_METADATA_VERSION_STRING = "SDL.app.metadata.version";
    public const string SDL_PROP_APP_METADATA_IDENTIFIER_STRING = "SDL.app.metadata.identifier";
    public const string SDL_PROP_APP_METADATA_CREATOR_STRING = "SDL.app.metadata.creator";
    public const string SDL_PROP_APP_METADATA_COPYRIGHT_STRING = "SDL.app.metadata.copyright";
    public const string SDL_PROP_APP_METADATA_URL_STRING = "SDL.app.metadata.url";
    public const string SDL_PROP_APP_METADATA_TYPE_STRING = "SDL.app.metadata.type";

    public enum SDL_EventType : uint
    {
        SDL_EVENT_QUIT = 0x100,
    }

    [StructLayout(LayoutKind.Explicit)]
    [SuppressMessage(
        "Minor Code Smell",
        "S101:Types should be named in PascalCase",
        Justification = "Native API names"
    )]
    public struct SDL_Event
    {
        [FieldOffset(0)]
        public SDL_EventType type;

        [FieldOffset(0)]
        private fixed byte padding[128];
    }

    [LibraryImport(LibSdl, EntryPoint = "SDL_SetMainReady")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    public static partial void SDL_SetMainReady();

    [LibraryImport(
        LibSdl,
        EntryPoint = "SDL_GetError",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    public static partial string SDL_GetError();

    [LibraryImport(LibSdl, EntryPoint = "SDL_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_Init(uint flags);

    [LibraryImport(LibSdl, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    public static partial void SDL_Quit();

    [LibraryImport(LibSdl, EntryPoint = "SDL_SetAppMetadataProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_SetAppMetadataProperty(string name, string? value);

    [LibraryImport(LibSdl, EntryPoint = "SDL_PollEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_PollEvent(out SDL_Event @event);
}
