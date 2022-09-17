using Autofac;
using Howatworks.Assistant.Core;

namespace Howatworks.Assistant.Wpf
{
    public class AssistantWpfModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((AssistantApp app) => TrayIconViewModel.Create(app));
        }
    }
}
