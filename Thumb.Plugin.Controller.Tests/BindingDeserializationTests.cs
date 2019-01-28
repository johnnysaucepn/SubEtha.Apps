using System.IO;
using System.Xml.Serialization;
using Thumb.Plugin.Controller.Models;
using Xunit;

namespace Thumb.Plugin.Controller.Tests
{
    public class BindingDeserializationTests
    {
        [Fact]
        public void DeserializeRoot()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");
            Assert.NotNull(binding);

            Assert.Equal("Dahkron", binding.PresetName);
            Assert.Equal(3, binding.MajorVersion);
            Assert.Equal("en-US", binding.KeyboardLayout);
        }

        [Fact]
        public void DeserializeAxis()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");
            Assert.NotNull(binding);

            Assert.NotNull(binding.YawAxisRaw);
            Assert.Equal("Joy_ZAxis", binding.YawAxisRaw.Binding.Key);
        }

        [Fact]
        public void DeserializeButton()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");
            Assert.NotNull(binding);

            Assert.NotNull(binding.YawLeftButton);
            Assert.Equal("Key_Numpad_4", binding.YawLeftButton.Primary.Key);

            Assert.Equal("", binding.YawLeftButton.Secondary.Key);
        }

        [Fact]
        public void DeserializeModifiedButton()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");
            Assert.NotNull(binding);

            Assert.NotNull(binding.LeftThrustButton);
            Assert.Equal("Key_Q", binding.LeftThrustButton.Primary.Key);
            Assert.Empty(binding.LeftThrustButton.Primary.Modifier);

            Assert.Equal("Pos_Joy_RXAxis", binding.LeftThrustButton.Secondary.Key);
            Assert.Equal(2, binding.LeftThrustButton.Secondary.Modifier.Count);
            Assert.Equal("Joy_1", binding.LeftThrustButton.Secondary.Modifier[0].Key);
            Assert.Equal("Joy_7", binding.LeftThrustButton.Secondary.Modifier[1].Key);
        }

        [Fact]
        public void DeserializeToggleButton()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");

            Assert.NotNull(binding.YawToRollButton.ToggleOn);
            Assert.False(binding.YawToRollButton.ToggleOn.Value);

            Assert.NotNull(binding.HeadLookToggle.ToggleOn);
            Assert.True(binding.HeadLookToggle.ToggleOn.Value);

            Assert.Null(binding.SetSpeedMinus50.ToggleOn);
        }

        [Fact]
        public void DeserializeFloatOption()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");

            Assert.Equal(0.40000001f, binding.YawToRollSensitivity.Value, 8);
        }

        [Fact]
        public void DeserializeBoolOption()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");

            Assert.True(binding.EnableCameraLockOn.Value);
        }

        [Fact]
        public void DeserializeStringOption()
        {
            var binding = DeserializeSampleBindingFile("samplebindings.xml");

            Assert.Equal("mute_toggle", binding.MuteButtonMode.Value);

        }

        private static Binding DeserializeSampleBindingFile(string path)
        {
            var serializer = new XmlSerializer(typeof(Binding), new XmlRootAttribute("Root"));
            using (var file = File.OpenRead(path))
            {
                var binding = (Binding)serializer.Deserialize(file);
                return binding;
            }
        }

    }
}