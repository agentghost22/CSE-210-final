using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        SmartLight light1 = new SmartLight { Name = "Living Room Light" };
        SmartLight light2 = new SmartLight { Name = "Bedroom Light" };
        SmartHeater heater = new SmartHeater { Name = "Living Room Heater" };
        SmartTV tv = new SmartTV { Name = "Living Room TV" };

        Room livingRoom = new Room { Name = "Living Room" };
        livingRoom.AddDevice(light1);
        livingRoom.AddDevice(light2);
        livingRoom.AddDevice(heater);
        livingRoom.AddDevice(tv);

        livingRoom.TurnOnAllDevices();

        Console.WriteLine("All items in the room:");
        foreach (var item in livingRoom.ReportAllItems())
        {
            Console.WriteLine(item);
        }
    }

    public abstract class SmartDevice
    {
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public DateTime StartTime { get; set; }

        public void TurnOn()
        {
            IsOn = true;
            StartTime = DateTime.Now;
        }

        public void TurnOff()
        {
            IsOn = false;
        }

        public string GetStatus()
        {
            return $"{Name} is {(IsOn ? "on" : "off")}";
        }

        public TimeSpan GetElapsedTime()
        {
            return IsOn ? DateTime.Now - StartTime : TimeSpan.Zero;
        }

        public abstract string DeviceType();
    }

    public class SmartLight : SmartDevice
    {
        public override string DeviceType()
        {
            return "Smart Light";
        }
    }

    public class SmartHeater : SmartDevice
    {
        public override string DeviceType()
        {
            return "Smart Heater";
        }
    }

    public class SmartTV : SmartDevice
    {
        public override string DeviceType()
        {
            return "Smart TV";
        }
    }

    public class Room
    {
        public string Name { get; set; }
        public List<SmartDevice> Devices { get; set; } = new List<SmartDevice>();

        public void AddDevice(SmartDevice device)
        {
            Devices.Add(device);
        }

        public void TurnOnAllDevices()
        {
            foreach (var device in Devices)
            {
                device.TurnOn();
            }
        }

        public void TurnOffAllDevices() //this one works
        {
            foreach (var device in Devices)
            {
                device.TurnOff();
            }
        }

        public List<string> ReportAllItems() // might have to redo this, has some complications
        {
            var report = new List<string>();
            foreach (var device in Devices)
            {
                report.Add(device.GetStatus());
            }
            return report;
        }

        public List<string> ReportAllItemsOn()
        {
            var report = new List<string>();
            foreach (var device in Devices)
            {
                if (device.IsOn)
                {
                    report.Add(device.GetStatus());
                }
            }
            return report;
        }

        public string ReportItemLongestOn()
        {
            TimeSpan longestOnTime = TimeSpan.Zero;
            SmartDevice longestOnDevice = null;

            foreach (var device in Devices)
            {
                if (device.GetElapsedTime() > longestOnTime)
                {
                    longestOnTime = device.GetElapsedTime();
                    longestOnDevice = device;
                }
            }

            return longestOnDevice != null ? longestOnDevice.GetStatus() : "No devices in the room have been on.";
        }
    }
}
