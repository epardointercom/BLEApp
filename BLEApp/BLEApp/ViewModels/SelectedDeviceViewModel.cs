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
    public class SelectedDeviceViewModel : BaseViewModel
    {
        #region Commands

        public INavigation Navigation { get; set; }

        #endregion

        #region Properties

        List<DeviceModel> deviceList;
        public List<DeviceModel> DeviceList
        {
            get { return deviceList; }
            set { SetProperty(ref deviceList, value); }
        }

        private string id;
        public string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string temperature;
        public string Temperature
        {
            get { return temperature; }
            set { SetProperty(ref temperature, value); }
        }

        #endregion
    }
}
