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
	[Display("Fm", Group: "Keysight.OpenTap.Plugins.Exg.Source.Modulation", Description: "Insert a description here")]
	public class Fm : TestStep
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

        private double _Deviation = 0.001D;

        [Unit("HZ")]
        [DisplayAttribute("Deviation", "", "Input Parameters", 2)]
        public double Deviation
        {
            get
            {
                return this._Deviation;
            }
            set
            {
                this._Deviation = value;
            }
        }

        private string _internalExternaletc = "INTernal";

        [DisplayAttribute("internalExternaletc", "", "Input Parameters", 2)]
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

        private bool _FmState = false;

        [DisplayAttribute("FmState", "", "Input Parameters", 2)]
        public bool FmState
        {
            get
            {
                return this._FmState;
            }
            set
            {
                this._FmState = value;
            }
        }

        public override void Run()
        {
            MyInst.ScpiCommand(":SOURce:MODulation:FM:DEViation {0}", Deviation);
            MyInst.ScpiCommand(":SOURce:MODulation:FM:SOURce {0}", internalExternaletc);
            MyInst.ScpiCommand(":SOURce:MODulation:FM:STATe {0}", FmState);
        }
    }
}
