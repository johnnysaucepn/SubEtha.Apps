﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Howatworks.Assistant.Core.ControlSimulators;
using Xunit;
using Howatworks.SubEtha.Bindings;
using Howatworks.SubEtha.Bindings.Monitor;

namespace Howatworks.Assistant.Core.Test
{
    public class KeyboardSimulatorTests
    {
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

            var mapper = new BindingMapper(new BindingSet());
            var controls = new GameControlBridge(config, mapper, new NativeKeyboardSimulator(), new NullMouseSimulator());

            using (var process = Process.Start("notepad.exe", fileName))
            {
                foreach (var keyId in new[]{"Key_A","Key_B","Key_Home"})
                {
                    TriggerKey(controls, keyId);
                }

                process.WaitForExit();
                File.Delete(fileName);
            }
        }

        private static void TriggerKey(GameControlBridge keyboard, string keyId)
        {
            var button = new Button()
            {
                Primary = new Button.ButtonBinding() { Device = "Keyboard", Key = keyId }
            };
            keyboard.ActivateKeyCombination(button);
        }
    }
}
