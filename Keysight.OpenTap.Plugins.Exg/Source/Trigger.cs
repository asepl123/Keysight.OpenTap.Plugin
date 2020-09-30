using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
	[Display("Trigger", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
	public class Trigger : TestStep
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

        private string _TriggerPolarity = "POSitive";

        [DisplayAttribute("TriggerPolarity", "POSitive|NEGative", "Input Parameters", 2)]
        public string TriggerPolarity
        {
            get
            {
                return this._TriggerPolarity;
            }
            set
            {
                this._TriggerPolarity = value;
            }
        }

        private string _TimerPeriod = "DEFault";

        [Unit("SEC")]
        [DisplayAttribute("TimerPeriod", "0.5 ms to 1000 seconds", "Input Parameters", 2)]
        public string TimerPeriod
        {
            get
            {
                return this._TimerPeriod;
            }
            set
            {
                this._TimerPeriod = value;
            }
        }

        private string _TriggerSequenceSlope = "POSitive";

        [DisplayAttribute("TriggerSequenceSlope", "sets the polarity of an external signal at the TRIG IN connector that will trigge" +
            "r a list or step sweep.", "Input Parameters", 2)]
        public string TriggerSequenceSlope
        {
            get
            {
                return this._TriggerSequenceSlope;
            }
            set
            {
                this._TriggerSequenceSlope = value;
            }
        }

        private string _TriggerSource = "IMMediate";

        [DisplayAttribute("TriggerSource", @"BUS -  This choice enables GPIB triggering using the *TRG or GET command. 

IMMediate -  This choice enables immediate triggering of the sweep event.

EXTernal -  This choice enables the triggering of a sweep event by an externally applied signal at the TRIG IN connector.

Trigger KEY -  This choice enables triggering through front panel interaction by pressing the Trigger hardkey.

TIMer Trigger -  This choice enables the sweep trigger timer.", "Input Parameters", 2)]
        public string TriggerSource
        {
            get
            {
                return this._TriggerSource;
            }
            set
            {
                this._TriggerSource = value;
            }
        }

        public override void Run()
        {
            MyInst.ScpiCommand(":TRIGger:OUTPut:POLarity {0}", TriggerPolarity);
            MyInst.ScpiCommand(":TRIGger:SEQuence:TIMer {0}", TimerPeriod);
            MyInst.ScpiCommand(":TRIGger:SEQuence:SLOPe {0}", TriggerSequenceSlope);
            MyInst.ScpiCommand(":TRIGger:SEQuence:SOURce {0}", TriggerSource);
        }
    }
}
