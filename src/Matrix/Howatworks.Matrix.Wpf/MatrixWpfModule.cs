using Autofac;
using Howatworks.Matrix.Core;

namespace Howatworks.Matrix.Wpf
{
    public class MatrixWpfModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((MatrixApp app) => TrayIconViewModel.Create(app));

            builder.RegisterType<AuthenticationDialog>().AsSelf().SingleInstance();
            builder.RegisterType<AuthenticationDialogViewModel>().AsSelf().SingleInstance();

            builder.RegisterType<DialogAuthenticator>().As<IMatrixAuthenticator>().SingleInstance();
        }
    }
}
