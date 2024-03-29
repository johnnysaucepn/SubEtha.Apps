﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using InputSimulatorStandard.Native;

namespace Howatworks.Assistant.Core.ControlSimulators
{
    public class VirtualKeyMapper
    {
        private readonly VirtualKeyCodeFinder _vkFinder;

        /// <summary>
        /// Sourced from https://github.com/richardbuckle/EDRefCard/blob/1f1d65c3f55eda18d1923ee8175026125fd65e44/bindings/testCases/Help.txt
        /// </summary>
        private readonly IReadOnlyDictionary<string, VirtualKeyCode> _mapping;

        public VirtualKeyMapper(CultureInfo culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;

            _vkFinder = new VirtualKeyCodeFinder(culture);
            _mapping = new Dictionary<string, VirtualKeyCode>
            {
                ["Key_Escape"] = VirtualKeyCode.ESCAPE,
                ["Key_1"] = VirtualKeyCode.VK_1,
                ["Key_2"] = VirtualKeyCode.VK_2,
                ["Key_3"] = VirtualKeyCode.VK_3,
                ["Key_4"] = VirtualKeyCode.VK_4,
                ["Key_5"] = VirtualKeyCode.VK_5,
                ["Key_6"] = VirtualKeyCode.VK_6,
                ["Key_7"] = VirtualKeyCode.VK_7,
                ["Key_8"] = VirtualKeyCode.VK_8,
                ["Key_9"] = VirtualKeyCode.VK_9,
                ["Key_0"] = VirtualKeyCode.VK_0,
                ["Key_Minus"] = _vkFinder.GetKeyOrDefault('-', VirtualKeyCode.OEM_MINUS),
                ["Key_Equals"] = _vkFinder.GetKeyOrDefault('=', VirtualKeyCode.OEM_PLUS), // TODO: unmodified
                ["Key_Backspace"] = VirtualKeyCode.BACK,
                ["Key_Tab"] = VirtualKeyCode.TAB,
                ["Key_Q"] = VirtualKeyCode.VK_Q,
                ["Key_W"] = VirtualKeyCode.VK_W,
                ["Key_E"] = VirtualKeyCode.VK_E,
                ["Key_R"] = VirtualKeyCode.VK_R,
                ["Key_T"] = VirtualKeyCode.VK_T,
                ["Key_Y"] = VirtualKeyCode.VK_Y,
                ["Key_U"] = VirtualKeyCode.VK_U,
                ["Key_I"] = VirtualKeyCode.VK_I,
                ["Key_O"] = VirtualKeyCode.VK_O,
                ["Key_P"] = VirtualKeyCode.VK_P,
                ["Key_LeftBracket"] = _vkFinder.GetKeyOrDefault('{', VirtualKeyCode.OEM_4), // { US
                ["Key_RightBracket"] = _vkFinder.GetKeyOrDefault('}', VirtualKeyCode.OEM_6), // } US
                ["Key_Enter"] = VirtualKeyCode.RETURN,
                ["Key_LeftControl"] = VirtualKeyCode.LCONTROL,
                ["Key_A"] = VirtualKeyCode.VK_A,
                ["Key_S"] = VirtualKeyCode.VK_S,
                ["Key_D"] = VirtualKeyCode.VK_D,
                ["Key_F"] = VirtualKeyCode.VK_F,
                ["Key_G"] = VirtualKeyCode.VK_G,
                ["Key_H"] = VirtualKeyCode.VK_H,
                ["Key_J"] = VirtualKeyCode.VK_J,
                ["Key_K"] = VirtualKeyCode.VK_K,
                ["Key_L"] = VirtualKeyCode.VK_L,
                ["Key_SemiColon"] = _vkFinder.GetKeyOrDefault(';', VirtualKeyCode.OEM_1), // ; US
                ["Key_Apostrophe"] = _vkFinder.GetKeyOrDefault('\'', VirtualKeyCode.OEM_7), // ' US?
                ["Key_Grave"] = _vkFinder.GetKeyOrDefault('`', VirtualKeyCode.OEM_3), // ` US?
                ["Key_LeftShift"] = VirtualKeyCode.LSHIFT,
                ["Key_BackSlash"] = _vkFinder.GetKeyOrDefault('\\', VirtualKeyCode.OEM_5), // \ US? or OEM_102?
                ["Key_Z"] = VirtualKeyCode.VK_Z,
                ["Key_X"] = VirtualKeyCode.VK_X,
                ["Key_C"] = VirtualKeyCode.VK_C,
                ["Key_V"] = VirtualKeyCode.VK_V,
                ["Key_B"] = VirtualKeyCode.VK_B,
                ["Key_N"] = VirtualKeyCode.VK_N,
                ["Key_M"] = VirtualKeyCode.VK_M,
                ["Key_Comma"] = _vkFinder.GetKeyOrDefault(',', VirtualKeyCode.OEM_COMMA),
                ["Key_Period"] = _vkFinder.GetKeyOrDefault('.', VirtualKeyCode.OEM_PERIOD),
                ["Key_Slash"] = VirtualKeyCode.DIVIDE,
                ["Key_RightShift"] = VirtualKeyCode.RSHIFT,
                ["Key_Numpad_Multiply"] = VirtualKeyCode.MULTIPLY,
                ["Key_LeftAlt"] = VirtualKeyCode.MENU,
                ["Key_Space"] = VirtualKeyCode.SPACE,
                ["Key_CapsLock"] = VirtualKeyCode.CAPITAL,
                ["Key_F1"] = VirtualKeyCode.F1,
                ["Key_F2"] = VirtualKeyCode.F2,
                ["Key_F3"] = VirtualKeyCode.F3,
                ["Key_F4"] = VirtualKeyCode.F4,
                ["Key_F5"] = VirtualKeyCode.F5,
                ["Key_F6"] = VirtualKeyCode.F6,
                ["Key_F7"] = VirtualKeyCode.F7,
                ["Key_F8"] = VirtualKeyCode.F8,
                ["Key_F9"] = VirtualKeyCode.F9,
                ["Key_F10"] = VirtualKeyCode.F10,
                ["Key_NumLock"] = VirtualKeyCode.NUMLOCK,
                ["Key_ScrollLock"] = VirtualKeyCode.SCROLL,
                ["Key_Numpad_7"] = VirtualKeyCode.NUMPAD7,
                ["Key_Numpad_8"] = VirtualKeyCode.NUMPAD8,
                ["Key_Numpad_9"] = VirtualKeyCode.NUMPAD9,
                ["Key_Numpad_Subtract"] = VirtualKeyCode.SUBTRACT,
                ["Key_Numpad_4"] = VirtualKeyCode.NUMPAD4,
                ["Key_Numpad_5"] = VirtualKeyCode.NUMPAD5,
                ["Key_Numpad_6"] = VirtualKeyCode.NUMPAD6,
                ["Key_Numpad_Add"] = VirtualKeyCode.ADD,
                ["Key_Numpad_1"] = VirtualKeyCode.NUMPAD1,
                ["Key_Numpad_2"] = VirtualKeyCode.NUMPAD2,
                ["Key_Numpad_3"] = VirtualKeyCode.NUMPAD3,
                ["Key_Numpad_0"] = VirtualKeyCode.NUMPAD0,
                ["Key_Numpad_Decimal"] = VirtualKeyCode.DECIMAL,
                ["Key_OEM_102"] = VirtualKeyCode.OEM_102,
                ["Key_F11"] = VirtualKeyCode.F11,
                ["Key_F12"] = VirtualKeyCode.F12,
                ["Key_F13"] = VirtualKeyCode.F13,
                ["Key_F14"] = VirtualKeyCode.F14,
                ["Key_F15"] = VirtualKeyCode.F15,
                ["Key_Kana"] = VirtualKeyCode.KANA, // TODO: how to test?
                ["Key_ABNT_C1"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_Convert"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_NoConvert"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_Yen"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_ABNT_C2"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_Numpad_Equals"] = VirtualKeyCode.RETURN,
                ["Key_PrevTrack"] = VirtualKeyCode.MEDIA_PREV_TRACK,
                ["Key_AT"] = _vkFinder.GetKeyOrDefault('@', VirtualKeyCode.NONAME), // TODO: is this dupe of unmodified key?
                ["Key_Colon"] = _vkFinder.GetKeyOrDefault(':', VirtualKeyCode.NONAME), // TODO: is this dupe of unmodified key?
                ["Key_Underline"] = _vkFinder.GetKeyOrDefault('_', VirtualKeyCode.NONAME), // TODO: is this dupe of unmodified key?
                ["Key_Kanji"] = VirtualKeyCode.KANJI, // TODO: how to test?
                ["Key_Stop"] = VirtualKeyCode.BROWSER_STOP, // TODO: is this browser?
                ["Key_AX"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_Unlabeled"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_NextTrack"] = VirtualKeyCode.MEDIA_NEXT_TRACK,
                ["Key_Numpad_Enter"] = VirtualKeyCode.RETURN, // TODO: is this numpad only?
                ["Key_RightControl"] = VirtualKeyCode.RCONTROL,
                ["Key_Mute"] = VirtualKeyCode.VOLUME_MUTE,
                ["Key_Calculator"] = VirtualKeyCode.NONAME,
                ["Key_PlayPause"] = VirtualKeyCode.NONAME,
                ["Key_MediaStop"] = VirtualKeyCode.NONAME,
                ["Key_VolumeDown"] = VirtualKeyCode.NONAME,
                ["Key_VolumeUp"] = VirtualKeyCode.NONAME,
                ["Key_WebHome"] = VirtualKeyCode.NONAME,
                ["Key_Numpad_Comma"] = VirtualKeyCode.NONAME, // TODO: how to test?
                ["Key_Numpad_Divide"] = VirtualKeyCode.DIVIDE,
                ["Key_SYSRQ"] = VirtualKeyCode.SNAPSHOT, // SysReq/PrintScreen
                ["Key_RightAlt"] = VirtualKeyCode.NONAME, // TODO: ???
                ["Key_Pause"] = VirtualKeyCode.PAUSE, // TODO: ???
                ["Key_Home"] = VirtualKeyCode.HOME,
                ["Key_UpArrow"] = VirtualKeyCode.UP,
                ["Key_PageUp"] = VirtualKeyCode.PRIOR,
                ["Key_LeftArrow"] = VirtualKeyCode.LEFT,
                ["Key_RightArrow"] = VirtualKeyCode.RIGHT,
                ["Key_End"] = VirtualKeyCode.END,
                ["Key_DownArrow"] = VirtualKeyCode.DOWN,
                ["Key_PageDown"] = VirtualKeyCode.NEXT,
                ["Key_Insert"] = VirtualKeyCode.INSERT,
                ["Key_Delete"] = VirtualKeyCode.DELETE,
                ["Key_LeftWin"] = VirtualKeyCode.LWIN,
                ["Key_RightWin"] = VirtualKeyCode.RWIN,
                ["Key_Apps"] = VirtualKeyCode.APPS,
                ["Key_Power"] = VirtualKeyCode.NONAME, // TODO: hope not!
                ["Key_Sleep"] = VirtualKeyCode.SLEEP,
                ["Key_Wake"] = VirtualKeyCode.NONAME, // TODO: ???
                ["Key_WebSearch"] = VirtualKeyCode.BROWSER_SEARCH,
                ["Key_WebFavourites"] = VirtualKeyCode.BROWSER_FAVORITES,
                ["Key_WebRefresh"] = VirtualKeyCode.BROWSER_REFRESH,
                ["Key_WebStop"] = VirtualKeyCode.BROWSER_STOP,
                ["Key_WebForward"] = VirtualKeyCode.BROWSER_FORWARD,
                ["Key_WebBack"] = VirtualKeyCode.BROWSER_BACK,
                ["Key_MyComputer"] = VirtualKeyCode.NONAME, // TODO: ???
                ["Key_Mail"] = VirtualKeyCode.LAUNCH_MAIL,
                ["Key_MediaSelect"] = VirtualKeyCode.LAUNCH_MEDIA_SELECT,
                ["Key_GreenModifier"] = VirtualKeyCode.NONAME, // TODO: ???
                ["Key_OrangeModifier"] = VirtualKeyCode.NONAME, // TODO: ???
            };
        }

        public VirtualKeyCode? MapKey(string key)
        {
            if (!_mapping.ContainsKey(key)) return null;

            return _mapping[key];
        }

        public List<VirtualKeyCode> MapKeys(params string[] keys)
        {
            return keys.Select(MapKey).Where(x => x.HasValue).Select(x => x.Value).ToList();
        }
    }
}