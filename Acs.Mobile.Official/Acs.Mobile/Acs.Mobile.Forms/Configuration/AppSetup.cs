
using Autofac;
using Acs.Services.AuthServices;
using Acs.Services.RegistrationServices;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.ViewModels.Base;
using Acs.Mobile.ESig.Controls;

namespace Acs.Mobile.ESig.Configuration
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);

            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            // Register services for DI
            containerBuilder.RegisterType<ESigAuthService>().As<IESigAuthService>();
            containerBuilder.RegisterType<RegistrerDeviceService>().As<IRegistrerDeviceService>();

            // Register View Models
            containerBuilder.RegisterType<LoginViewModel>().SingleInstance().As<ViewModelBase>().As<LoginViewModel>();
            containerBuilder.RegisterType<RegisterationViewModel>().SingleInstance().As<ViewModelBase>().As<RegisterationViewModel>();

            containerBuilder.RegisterType<BarcodeScanViewModel>().SingleInstance().As<ViewModelBase>().As<BarcodeScanViewModel>();
        }
    }
}