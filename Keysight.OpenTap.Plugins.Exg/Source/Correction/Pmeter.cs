using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source.Correction
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Keysight.OpenTap.Plugins.Exg;

    [Display("External Power Meter", Group: "Keysight.OpenTap.Plugins.Exg", Description: "Insert description here")]
    public class CommandExpertStep : TestStep
    {
        private MyInst _MyInst;
        [DisplayAttribute("MyInst", "", "Instruments", 1)]
        public MyInst MyInst
        {
            get
            {
                return this._MyInst;
            }
            set
            {
                this._MyInst = value;
            }
        }
        
        private string _Channel = "A";
        
        [DisplayAttribute("Channel", "selects the channel setting on the external power meter for user flatness calibra" +
            "tion\r\n(A, B)", "Input Parameters", 2)]
        public string Channel
        {
            get
            {
                return this._Channel;
            }
            set
            {
                this._Channel = value;
            }
        }
        
        private string _DeviceName = "PM VXI-11";
        
        [DisplayAttribute("DeviceName", "PM VXI-11 Device Name ", "Input Parameters", 2)]
        public string DeviceName
        {
            get
            {
                return this._DeviceName;
            }
            set
            {
                this._DeviceName = value;
            }
        }
        
        private string _IpAddress = "192.168.2.20";
        
        [DisplayAttribute("IpAddress", "", "Input Parameters", 2)]
        public string IpAddress
        {
            get
            {
                return this._IpAddress;
            }
            set
            {
                this._IpAddress = value;
            }
        }
        
        private int _PortNumber = 5025;
        
        [DisplayAttribute("PortNumber", "", "Input Parameters", 2)]
        public int PortNumber
        {
            get
            {
                return this._PortNumber;
            }
            set
            {
                this._PortNumber = value;
            }
        }
        
        private string _CommunicationType = "VXI11";
        
        [DisplayAttribute("CommunicationType", "VX11, USB, SOCKets, SOCKETS", "Input Parameters", 2)]
        public string CommunicationType
        {
            get
            {
                return this._CommunicationType;
            }
            set
            {
                this._CommunicationType = value;
            }
        }
        
        private string _UsbDevice = "USB Device";
        
        [DisplayAttribute("UsbDevice", "selects the USB device to be used for user flatness calibration", "Input Parameters", 2)]
        public string UsbDevice
        {
            get
            {
                return this._UsbDevice;
            }
            set
            {
                this._UsbDevice = value;
            }
        }
        
        private String QueryUsbDevice;
        
        private Int32 QueryPortNumber;
        
        private String QueryIpAddress;
        
        private String QueryDeviceName;
        
        // returns a listing of all connected USB devices.
        private String[] ListDevices;
        
        
        public override void Run()
        {
            MyInst.ScpiCommand(":SOURce:CORRection:PMETer:CHANnel {0}",Channel);
            MyInst.ScpiCommand(":SOURce:CORRection:PMETer:COMMunicate:TYPE {0}",CommunicationType);
            MyInst.ScpiCommand(":SOURce:CORRection:PMETer:COMMunicate:LAN:DEVice {0}",DeviceName);
            QueryDeviceName = MyInst.ScpiQuery<System.String>(Scpi.Format(":SOURce:CORRection:PMETer:COMMunicate:LAN:DEVice?"), true);
            MyInst.ScpiCommand(":SOURce:CORRection:PMETer:COMMunicate:LAN:IP {0}",IpAddress);
            QueryIpAddress = MyInst.ScpiQuery<System.String>(Scpi.Format(":SOURce:CORRection:PMETer:COMMunicate:LAN:IP?"), true);
            MyInst.ScpiCommand(":SOURce:CORRection:PMETer:COMMunicate:LAN:PORT {0}",PortNumber);
            QueryPortNumber = MyInst.ScpiQuery<System.Int32>(Scpi.Format(":SOURce:CORRection:PMETer:COMMunicate:LAN:PORT?"), true);
            MyInst.ScpiCommand(":SOURce:CORRection:PMETer:COMMunicate:USB:DEVice {0}",UsbDevice);
            QueryUsbDevice = MyInst.ScpiQuery<System.String>(Scpi.Format(":SOURce:CORRection:PMETer:COMMunicate:USB:DEVice?"), true);
            ListDevices = MyInst.ScpiQuery<System.String[]>(Scpi.Format(":SOURce:CORRection:PMETer:COMMunicate:USB:LIST?"), true);
        }
    }
}
