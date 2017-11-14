using BLEApp.Models;
using BLEApp.ViewModels;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BLEApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MainPageViewModel();
            viewModel.Navigation = Navigation;
        }

        private async void OnSelectedDevice(object o, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                waiting.IsRunning = true;
                try
                {                    
                    var selectedDevice = e.Item as DeviceModel;
                    DeviceListView.SelectedItem = null;

                    var device = selectedDevice.Device;

                    var adapter = CrossBluetoothLE.Current.Adapter;
                    await adapter.ConnectToDeviceAsync(device);
                    var connectedDevice = adapter.ConnectedDevices.Where(x => x == device).FirstOrDefault();
                    if(connectedDevice != null)
                    {
                        var services = await connectedDevice.GetServicesAsync();
                        await Navigation.PushAsync(new Views.SelectedDevice(selectedDevice));
                    }
                    else
                    {
                        waiting.IsRunning = false;
                        await Application.Current.MainPage.DisplayAlert("Conexion", "No se pudo conectar el dispositivo", "Ok");
                    }
                }
                catch (DeviceConnectionException ex)
                {
                    waiting.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
                catch (Exception ex)
                {
                    waiting.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
            }
        }
    }
}
