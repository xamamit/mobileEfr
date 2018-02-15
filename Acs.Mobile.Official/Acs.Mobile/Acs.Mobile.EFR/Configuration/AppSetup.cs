
using Autofac;

using Acs.Services.AuthServices;
using Acs.Services.RegistrationServices;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.ViewModels.Base;
using Acs.Mobile.EFR.Controls;

namespace Acs.Mobile.EFR.Configuration
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
            //
            // Service Registrations
            //
            containerBuilder.RegisterType<Acs.Services.AuthServices.ESigAuthService>()
                .As<Acs.Services.AuthServices.IESigAuthService>();

            containerBuilder.RegisterType<Acs.Services.RegistrationServices.RegistrerDeviceService>()
                .As<Acs.Services.RegistrationServices.IRegistrerDeviceService>();

            // 
            // ViewModel Registrations
            //
            containerBuilder.RegisterType<LoginViewModel>().SingleInstance().As<ViewModelBase>().As<LoginViewModel>();
            containerBuilder.RegisterType<BarcodeScanViewModel>().SingleInstance().As<ViewModelBase>().As<BarcodeScanViewModel>();
            containerBuilder.RegisterType<RegisterationViewModel>().SingleInstance().As<ViewModelBase>().As<RegisterationViewModel>();
            containerBuilder.RegisterType<AboutViewModel>().SingleInstance().As<ViewModelBase>().As<AboutViewModel>();
        }
    }
}