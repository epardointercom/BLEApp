using BLEApp.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BLEApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            FindDevicesCommand = new Command(FindDevices);
            Message = "Presione Buscar para visualizar los dispositivos disponibles";
        }

        #region Commands

        public INavigation Navigation { get; set; }
        public ICommand FindDevicesCommand { get; set; }

        #endregion

        #region Properties

        List<DeviceModel> deviceList;
        public List<DeviceModel> DeviceList
        {
            get { return deviceList; }
            set { SetProperty(ref deviceList, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion

        private async void FindDevices()
        {
            IsBusy = true;
            try
            {
                var ble = CrossBluetoothLE.Current;
                var adapter = CrossBluetoothLE.Current.Adapter;
                var state = ble.State;

                if (state == Plugin.BLE.Abstractions.Contracts.BluetoothState.On)
                {
                    var _deviceList = new List<DeviceModel>();

                    adapter.DeviceDiscovered += (s, a) => _deviceList.Add(
                        new DeviceModel()
                        {
                            Name = string.IsNullOrEmpty(a.Device.Name) ? "Sin Nombre" : a.Device.Name,
                            Device = a.Device
                        });
                    await adapter.StartScanningForDevicesAsync();
                    if (_deviceList.Count == 0)
                    {
                        Message = "No se encontraron dispositivos";
                        IsBusy = false;
                    }
                    else
                    {
                        DeviceList = _deviceList;
                        Message = string.Empty;
                        IsBusy = false;
                    }
                }
                else
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Dispositivo", "El Bluetooth del equipo no está encendido", "Ok");
                }
            }
            catch (DeviceConnectionException ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}
