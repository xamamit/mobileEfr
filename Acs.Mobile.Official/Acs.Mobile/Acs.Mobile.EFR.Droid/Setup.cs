
using Autofac;
using Acs.Services.AuthServices;
using Acs.Mobile.EFR.Configuration;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Controls;
using Acs.Services.RegistrationServices;

namespace Acs.Mobile.EFR.Droid
{
    /// <summary>
    /// Main entry point to the Android mobile app. Derives from Acs.Mobile.EFR project.
    /// </summary>
    /// <seealso cref="Acs.Mobile.EFR.Configuration.AppSetup" />
    public class Setup : AppSetup
	{
		/// <summary>Setup dependencies using DI.</summary>
		/// <param name="containerBuilder"></param>
        protected override void RegisterDependencies(ContainerBuilder containerBuilder)
		{
			base.RegisterDependencies(containerBuilder);

            containerBuilder.RegisterType<ESigAuthService>().As<IESigAuthService>();
		    containerBuilder.RegisterType<RegistrerDeviceService>().As<IRegistrerDeviceService>();
        }
    }
}