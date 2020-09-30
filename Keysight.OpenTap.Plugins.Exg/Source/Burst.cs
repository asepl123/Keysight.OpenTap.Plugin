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

namespace Keysight.OpenTap.Plugins.Exg.Source
{
    [Display("Burst", Group: "Keysight.OpenTap.Plugins.Exg", Description: "Insert a description here")]
    public class Burst : TestStep
    {
        #region Settings

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

        private bool _BurstState = false;

        [DisplayAttribute("BurstState", "", "Input Parameters", 2)]
        public bool BurstState
        {
            get
            {
                return this._BurstState;
            }
            set
            {
                this._BurstState = value;
            }
        }
        #endregion

        public override void Run()
        {
            MyInst.ScpiCommand(":SOURce:BURSt:STATe {0}", BurstState);
        }
    }
}
