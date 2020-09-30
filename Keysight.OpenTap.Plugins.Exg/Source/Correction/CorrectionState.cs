using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source.Correction
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Keysight.OpenTap.Plugins.Exg;

    [Display("Flatness Correction Status", Group: "Keysight.OpenTap.Plugins.Exg", Description: "Insert description here")]
    public class CorrectionState : TestStep
    {

        [DisplayAttribute("MyInst", "", "Instruments", 1)]
        public MyInst MyInst;
        
        private bool _FlatnessStatus = false;
        
        [DisplayAttribute("FlatnessStatus", " user-flatness corrections", "Input Parameters", 2)]
        public bool FlatnessStatus
        {
            get
            {
                return this._FlatnessStatus;
            }
            set
            {
                this._FlatnessStatus = value;
            }
        }
        
        private Boolean QueryFlatnessStatus;
        
        
        public override void Run()
        {
            MyInst.ScpiCommand(":SOURce:CORRection:STATe {0}",FlatnessStatus);
            QueryFlatnessStatus = MyInst.ScpiQuery<System.Boolean>(Scpi.Format(":SOURce:CORRection:STATe?"), true);
        }
    }
}
