﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Howatworks.EliteDangerous.Bindings;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Thumb.Plugin.Controller.Test
{
    public class KeyboardEmulatorTests
    {
        public KeyboardEmulatorTests()
        {

        }

        [Trait("Category","Disabled")]
        [Fact(Skip="Requires External Application")]
        public void TriggerKeyCombination_AllKeyCodes_GeneratedFileConsistent()
        {
            var fileName = Path.GetTempFileName();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ActiveWindowTitle"] = $"{Path.GetFileName(fileName)} - Notepad"
                })
                .Build();

            var keyboard = new KeyboardEmulator(config);
            var table = new KeyMappingTable();



            using (var process = Process.Start("notepad.exe", fileName))
            {
                foreach (var keyId in new[]{"Key_A","Key_B","Key_Home"})
                {
                    TriggerKey(keyboard, keyId);
                }

                process.WaitForExit();
                File.Delete(fileName);
            }
        }

        private static void TriggerKey(KeyboardEmulator keyboard, string keyId)
        {
            var button = new Button()
            {
                Primary = new Button.ButtonBinding() { Device = "Keyboard", Key = keyId }
            };
            keyboard.TriggerKeyCombination(button);
        }
    }
}