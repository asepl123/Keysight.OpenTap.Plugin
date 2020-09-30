// Author: MyName
// Copyright:   Copyright 2020 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source.Modulation
{
	[Display("Dm", Group: "Keysight.OpenTap.Plugins.Exg.Source.Modulation", Description: "Insert a description here")]
	public class Dm : TestStep
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

        private double _AmDepth = 10D;

        [Unit("PCT")]
        [DisplayAttribute("AmDepth", "", "Input Parameters", 2)]
        public double AmDepth
        {
            get
            {
                return this._AmDepth;
            }
            set
            {
                this._AmDepth = value;
            }
        }

        private string _AmMode = "DEEP";

        [DisplayAttribute("AmMode", "sets the amplitude modulation mode\r\nDEEP, NORMal", "Input Parameters", 2)]
        public string AmMode
        {
            get
            {
                return this._AmMode;
            }
            set
            {
                this._AmMode = value;
            }
        }

        private string _internalExternaletc = "INTernal";

        [DisplayAttribute("internalExternaletc", "sets the source to generate the amplitude modulation. \r\nINT, EXT", "Input Parameters", 2)]
        public string internalExternaletc
        {
            get
            {
                return this._internalExternaletc;
            }
            set
            {
                this._internalExternaletc = value;
            }
        }

        private bool _AmState = false;

        [DisplayAttribute("AmState", "enables or disables the amplitude modulation for the selected path.", "Input Parameters", 2)]
        public bool AmState
        {
            get
            {
                return this._AmState;
            }
            set
            {
                this._AmState = value;
            }
        }

        public override void Run()
        {
            MyInst.ScpiCommand(":SOURce:MODulation:AM:STATe {0}", AmState);
            MyInst.ScpiCommand(":SOURce:MODulation:AM:DEPTh:LINear {0}", AmDepth);
            MyInst.ScpiCommand(":SOURce:MODulation:AM:MODE {0}", AmMode);
            MyInst.ScpiCommand(":SOURce:MODulation:AM:SOURce {0}", internalExternaletc);
        }
    }
}
