﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Howatworks.EliteDangerous.Bindings;
using log4net;
using Microsoft.Extensions.Configuration;
using static PInvoke.User32;

namespace Thumb.Plugin.Controller
{
    public class KeyboardEmulator
    {
        private readonly string _activeWindowTitle;
        private static readonly ILog Log = LogManager.GetLogger(typeof(KeyboardEmulator));

        public KeyboardEmulator(IConfiguration configuration)
        {
            _activeWindowTitle = configuration["ActiveWindowTitle"];
        }

        public void TriggerKeyCombination(Button button)
        {

            Button.ButtonBinding selectedButtonBinding;
            if (button.Primary.Device == "Keyboard")
            {
                selectedButtonBinding = button.Primary;
            }else if (button.Secondary.Device == "Keyboard")
            {
                selectedButtonBinding = button.Secondary;
            }
            else
            {
                Log.Warn($"Neither primary or secondary bindings are for keyboard (found {button.Primary.Device}, {button.Primary.Device}");
                return;
            }

            var modifierNames = selectedButtonBinding.Modifier.Select(x => x.Key).ToList();
            Log.Info($"Pressing {selectedButtonBinding.Key} with {(modifierNames.Any() ? string.Join(", ", modifierNames) : "no")} modifiers");
            var modifiers = MapModifiers(selectedButtonBinding).ToArray();
            var mainKey = MapKey(selectedButtonBinding).ToArray();

            if (DoesWindowHaveFocus(_activeWindowTitle))
            {
                Press(modifiers);
                PressAndRelease(mainKey);
                Release(modifiers);
            }

        }

        private static bool DoesWindowHaveFocus(string windowTitle)
        {
            var hwnd = FindWindow(null, windowTitle);
            if (hwnd == IntPtr.Zero) return false;
            return GetForegroundWindow() == hwnd;
        }

        private IEnumerable<ScanCode> MapKey(Button.ButtonBinding selectedButtonBinding)
        {
            //throw new NotImplementedException();
            Log.Error("NOT IMPLEMENTED");
            return new List<ScanCode>();
        }

        private IEnumerable<VirtualKey> MapModifiers(Button.ButtonBinding selectedButtonBinding)
        {
            //throw new NotImplementedException();
            Log.Error("NOT IMPLEMENTED");
            return new List<VirtualKey>();
        }

        private static void PressAndRelease(params ScanCode[] scanCodes)
        {
            Press(scanCodes);
            Thread.Sleep(100);
            Release(scanCodes);
        }

        private static void PressAndRelease(params VirtualKey[] virtualKeys)
        {
            Press(virtualKeys);
            Thread.Sleep(100);
            Release(virtualKeys);
        }

        private static void Press(params ScanCode[] scanCodes)
        {
            foreach (var scanCode in scanCodes)
            {
                SendKeyAction(true, scanCode, true);
            }
        }

        private static void Release(params ScanCode[] scanCodes)
        {
            foreach (var scanCode in scanCodes)
            {
                SendKeyAction(false, scanCode, true);
            }
        }

        private static void Press(params VirtualKey[] virtualKeys)
        {
            foreach (var virtualKey in virtualKeys)
            {
                SendKeyAction(true, virtualKey);
            }
        }

        private static void Release(params VirtualKey[] virtualKeys)
        {
            foreach (var virtualKey in virtualKeys)
            {
                SendKeyAction(false, virtualKey);
            }
        }

        [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Local")]
        private static uint SendKeyAction(bool keyDown, ScanCode scanCode, bool extended)
        {
            var key = new INPUT
            {
                type = InputType.INPUT_KEYBOARD,
                Inputs = new INPUT.InputUnion()
                {
                    ki = new KEYBDINPUT()
                    {
                        dwFlags = KEYEVENTF.KEYEVENTF_SCANCODE |
                                  (extended ? KEYEVENTF.KEYEVENTF_EXTENDED_KEY : 0) |
                                  (keyDown ? 0 : KEYEVENTF.KEYEVENTF_KEYUP),
                        wScan = scanCode
                    }
                }
            };
            var inputs = new[] { key };
            var response = SendInput(inputs.Length, inputs, Marshal.SizeOf(key));
            return response;
        }

        [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Local")]
        private static uint SendKeyAction(bool keyDown, VirtualKey virtualKey)
        {
            var key = new INPUT
            {
                type = InputType.INPUT_KEYBOARD,
                Inputs = new INPUT.InputUnion()
                {
                    ki = new KEYBDINPUT()
                    {
                        dwFlags = (keyDown ? 0 : KEYEVENTF.KEYEVENTF_KEYUP),
                        wVk = virtualKey
                    }
                }
            };
            var inputs = new[] { key };
            var response = SendInput(inputs.Length, inputs, Marshal.SizeOf(key));
            return response;
        }
    }
}