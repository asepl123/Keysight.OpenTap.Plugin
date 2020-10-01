using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
	[Display("Display", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
	public class Display : TestStep
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

        private double _Brightness = 0.5D;

        [DisplayAttribute("Brightness", "Range \r\n0.02 to 1", "Input Parameters", 2)]
        public double Brightness
        {
            get
            {
                return this._Brightness;
            }
            set
            {
                this._Brightness = value;
            }
        }

        private double _Contrast = 0.5D;

        [DisplayAttribute("Contrast", " sets the contrast of the LCD display", "Input Parameters", 2)]
        public double Contrast
        {
            get
            {
                return this._Contrast;
            }
            set
            {
                this._Contrast = value;
            }
        }

        private bool _RemoteStatus = false;

        [DisplayAttribute("RemoteStatus", "", "Input Parameters", 2)]
        public bool RemoteStatus
        {
            get
            {
                return this._RemoteStatus;
            }
            set
            {
                this._RemoteStatus = value;
            }
        }

        private Boolean QueryRemoteStatus;


        public override void Run()
        {
            MyInst.ScpiCommand(":DISPlay:BRIGhtness {0}", Brightness);
            MyInst.ScpiCommand(":DISPlay:CONTrast {0}", Contrast);
            MyInst.ScpiCommand(":DISPlay:CAPTure");
            MyInst.ScpiCommand(":DISPlay:REMote {0}", RemoteStatus);
            QueryRemoteStatus = MyInst.ScpiQuery<System.Boolean>(Scpi.Format(":DISPlay:REMote?"), true);
        }
    }
}
