using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
	[Display("Initiate", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
	public class Initiate : TestStep
	{
        private MyInst _SCPI_MXG;

        [DisplayAttribute("SCPI_MXG", "", "Instruments", 1)]
        public MyInst SCPI_MXG
        {
            get
            {
                return this._SCPI_MXG;
            }
            set
            {
                this._SCPI_MXG = value;
            }
        }

        private bool _InitiateContinuousStatus = false;

        [DisplayAttribute("InitiateContinuousStatus", "selects either a continuous or single list or step sweep. Execution of this comma" +
            "nd does not affect a sweep in progress.", "Input Parameters", 2)]
        public bool InitiateContinuousStatus
        {
            get
            {
                return this._InitiateContinuousStatus;
            }
            set
            {
                this._InitiateContinuousStatus = value;
            }
        }

        public override void Run()
        {
            SCPI_MXG.ScpiCommand(":INITiate:CONTinuous:ALL {0}", InitiateContinuousStatus);
            // This either sets or sets and starts a single List or Step sweep, depending on the trigger type. The command performs the following:
            //1. arms a single sweep when BUS, EXTernal, or KEY is the trigger source selection
            //2. arms and starts a single sweep when IMMediate is the trigger source selection
            SCPI_MXG.ScpiCommand(":INITiate:IMMediate:ALL");
        }
    }
}
