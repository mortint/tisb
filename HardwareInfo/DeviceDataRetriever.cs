using System.Management;

namespace TISB.HardwareInfo {
    internal class DeviceDataRetriever {
        public string GetHardwareID() {
            var searcher =
                    new ManagementObjectSearcher("Select * From Win32_BaseBoard");

            foreach (var item in searcher.Get())
                return item["SerialNumber"].ToString();

            return "";
        }
    }
}
