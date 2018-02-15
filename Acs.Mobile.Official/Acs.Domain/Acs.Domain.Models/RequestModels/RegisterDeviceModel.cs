
using System;

namespace Acs.Domain.Models.RequestModels
{
    /// <summary>
    /// Provides the property fields for the <see cref="ResponseModels.RegisterDeviceModel"/>
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.RequestModels.BaseModel" />
    public class RegisterDeviceModel : BaseModel
    {
        /// <summary>Initializes a new instance of the <see cref="RegisterDeviceModel"/> class.</summary>
        public RegisterDeviceModel() { }

        /// <summary>Initializes a new instance of the <see cref="RegisterDeviceModel"/> class.</summary>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="os">The os on which the device software runs.</param>
        /// <param name="manufacturer">The manufacturer of the device.</param>
        /// <param name="model">The model of the device.</param>
        public RegisterDeviceModel(string deviceName, string os, string manufacturer, string model)
        {
            DeviceName = deviceName;
            OS = os;
            Manufacturer = manufacturer;
            Model = model;
        }

        /// <summary>Gets or sets the name of the device.</summary>
        /// <value>The name of the device.</value>
        public string DeviceName { get; set; }

        /// <summary>Gets or sets the OS of the device.</summary>
        /// <value>The os.</value>
        public string OS { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        public string Manufacturer { get; set; }

        /// <summary>Gets or sets the model.</summary>
        /// <value>The model.</value>
        public string Model { get; set; }
    }
}