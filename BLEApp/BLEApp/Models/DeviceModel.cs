using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEApp.Models
{
    public class DeviceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Temperature { get; set; }
        public Plugin.BLE.Abstractions.Contracts.IDevice Device { get; set; }
    }
}
