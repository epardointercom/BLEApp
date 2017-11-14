using BLEApp.Models;
using BLEApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLEApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedDevice : ContentPage
    {
        SelectedDeviceViewModel viewModel;

        public SelectedDevice(DeviceModel device)
        {
            InitializeComponent();

            viewModel = new SelectedDeviceViewModel()
            {
                Id = device.Device.Id.ToString(),
                Name = device.Name,
                Description = device.Device.NativeDevice.ToString(),
                Temperature = "No found"
            };
            BindingContext = viewModel;
            viewModel.Navigation = Navigation;
        }
    }
}