﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Xunit;

namespace Howatworks.SubEtha.Bindings.Test
{
    public class BindingQueryTests
    {
        private const string DefaultBindingsFilename = "dahkron.binds";

        [Fact]
        public void KeyBindingListForKeyboard()
        {
            var bindings = DeserializeSampleBindingFile(DefaultBindingsFilename);
            Assert.NotNull(bindings);

            var bindingMapper = new BindingMapper(bindings);
            var foundBindings = bindingMapper.GetBoundButtons("Keyboard");

            Assert.Equal(81, foundBindings.Count);

        }

        [Fact]
        public void KeyBindingListForMouse()
        {
            var bindings = DeserializeSampleBindingFile(DefaultBindingsFilename);
            Assert.NotNull(bindings);

            var bindingMapper = new BindingMapper(bindings);
            var foundBindings = bindingMapper.GetBoundButtons("Mouse");

            Assert.Equal(3, foundBindings.Count);

        }

        [Fact]
        public void KeyBindingListForKeyboardAndMouse()
        {
            var bindings = DeserializeSampleBindingFile(DefaultBindingsFilename);
            Assert.NotNull(bindings);

            var bindingMapper = new BindingMapper(bindings);
            var foundBindings = bindingMapper.GetBoundButtons("Keyboard", "Mouse");

            Assert.Equal(84, foundBindings.Count);
        }

        private static BindingSet DeserializeSampleBindingFile(string path)
        {
            var serializer = new XmlSerializer(typeof(BindingSet), new XmlRootAttribute("Root"));
            using (var file = File.OpenRead(path))
            {
                var binding = (BindingSet)serializer.Deserialize(file);
                return binding;
            }
        }
    }
}