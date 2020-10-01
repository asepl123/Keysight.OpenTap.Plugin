using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
	[Display("OutputSetting", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
	public class OutputSetting : TestStep
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

        private bool _OutputBlankingState = true;

        [DisplayAttribute("OutputBlankingState", "", "Input Parameters", 2)]
        public bool OutputBlankingState
        {
            get
            {
                return this._OutputBlankingState;
            }
            set
            {
                this._OutputBlankingState = value;
            }
        }

        private bool _OutputBlankingAuto = true;

        [DisplayAttribute("OutputBlankingAuto", "", "Input Parameters", 2)]
        public bool OutputBlankingAuto
        {
            get
            {
                return this._OutputBlankingAuto;
            }
            set
            {
                this._OutputBlankingAuto = value;
            }
        }

        private bool _OutputModulation = true;

        [DisplayAttribute("OutputModulation", "", "Input Parameters", 2)]
        public bool OutputModulation
        {
            get
            {
                return this._OutputModulation;
            }
            set
            {
                this._OutputModulation = value;
            }
        }

        private bool _OutputProtection = true;

        [DisplayAttribute("OutputProtection", "", "Input Parameters", 2)]
        public bool OutputProtection
        {
            get
            {
                return this._OutputProtection;
            }
            set
            {
                this._OutputProtection = value;
            }
        }

        private void ProcessResults()
        {
        }

        public override void Run()
        {
            MyInst.ScpiCommand(":OUTPut:BLANking:AUTO {0}", OutputBlankingAuto);
            MyInst.ScpiCommand(":OUTPut:BLANking:STATe {0}", OutputBlankingState);
            MyInst.ScpiCommand(":OUTPut:MODulation:STATe {0}", OutputModulation);
            MyInst.ScpiCommand(":OUTPut:PROTection:STATe {0}", OutputProtection);
            this.ProcessResults();
        }
    }
}
