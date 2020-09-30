using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
	[Display("ROscillator", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
	public class ROscillator : TestStep
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

        private string _BandwidthType = "NORMal";

        [DisplayAttribute("BandwidthType", "NORMal, HIGH", "Input Parameters", 2)]
        public string BandwidthType
        {
            get
            {
                return this._BandwidthType;
            }
            set
            {
                this._BandwidthType = value;
            }
        }

        private double _PmDeviation = 0D;

        [Unit("RAD")]
        [DisplayAttribute("PmDeviation", "", "Input Parameters", 2)]
        public double PmDeviation
        {
            get
            {
                return this._PmDeviation;
            }
            set
            {
                this._PmDeviation = value;
            }
        }

        private string _IntExtSource = "INTernal";

        [DisplayAttribute("IntExtSource", "sets the source to generate the phase modulation. ", "Input Parameters", 2)]
        public string IntExtSource
        {
            get
            {
                return this._IntExtSource;
            }
            set
            {
                this._IntExtSource = value;
            }
        }

        private bool _PmState = false;

        [DisplayAttribute("PmState", "enables or disables the phase modulation for the selected path.", "Input Parameters", 2)]
        public bool PmState
        {
            get
            {
                return this._PmState;
            }
            set
            {
                this._PmState = value;
            }
        }


        public override void Run()
        {
            MyInst.ScpiCommand(":SOURce:MODulation:PM:BANDwidth {0}", BandwidthType);
            MyInst.ScpiCommand(":SOURce:MODulation:PM:DEViation {0}", PmDeviation);
            MyInst.ScpiCommand(":SOURce:MODulation:PM:SOURce {0}", IntExtSource);
            MyInst.ScpiCommand(":SOURce:MODulation:PM:STATe {0}", PmState);
        }
    }
}
