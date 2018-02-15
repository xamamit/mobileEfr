using System;
using System.Collections.Generic;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class AbouttView : ContentPage
    {
        public AbouttView()
        {
            InitializeComponent();

            lblDeviceId.Text = CrossDevice.Hardware.DeviceId;
            lblDevicePlatform.Text = CrossDevice.Hardware.OperatingSystem;
            lblDeviceModel.Text = CrossDevice.Hardware.Model;
            lblDeviceVersion.Text = CrossDevice.Hardware.OperatingSystemVersion;

        }
    }
}