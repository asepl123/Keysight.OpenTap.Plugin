
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
    [Display("Sweep", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
    public class Sweep : TestStep
    {
        #region Settings
        // ToDo: Add property here for each parameter the end user should be able to change

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

        private string _Direction = "UP";

        [DisplayAttribute("Direction", "sets the direction of a list or step sweep. ", "Input Parameters", 2)]
        public string Direction
        {
            get
            {
                return this._Direction;
            }
            set
            {
                this._Direction = value;
            }
        }

        private double[] _ListFrequency = new double[] {
                25000D,
                30000D};

        [DisplayAttribute("ListFrequency", "sets the direction of a list or step sweep. ", "Input Parameters", 2)]
        public double[] ListFrequency
        {
            get
            {
                return this._ListFrequency;
            }
            set
            {
                this._ListFrequency = value;
            }
        }

        private double[] _ListPower = new double[] {
                0D};

        [DisplayAttribute("ListPower", "", "Input Parameters", 2)]
        public double[] ListPower
        {
            get
            {
                return this._ListPower;
            }
            set
            {
                this._ListPower = value;
            }
        }

        private string _SweepType = "LIST";

        [DisplayAttribute("SweepType", "", "Input Parameters", 2)]
        public string SweepType
        {
            get
            {
                return this._SweepType;
            }
            set
            {
                this._SweepType = value;
            }
        }

        private Int32 QueryCurrentSPoint;

        private String QueryDirection;

        private Double[] QueryListDirection;

        #endregion

        public override void Run()
        {
            QueryCurrentSPoint = MyInst.ScpiQuery<System.Int32>(Scpi.Format(":SOURce:SWEEp:POINt?"), true);
            MyInst.ScpiCommand(":SOURce:LIST:DIRection {0}", Direction);
            QueryDirection = MyInst.ScpiQuery<System.String>(Scpi.Format(":SOURce:LIST:DIRection?"), true);
            MyInst.ScpiCommand(":SOURce:SWEEp:FREQuency {0}", ListFrequency);
            QueryListDirection = MyInst.ScpiQuery<System.Double[]>(Scpi.Format(":SOURce:LIST:FREQuency?"), true);
            MyInst.ScpiCommand(":SOURce:SWEEp:POWer {0}", ListPower);
            MyInst.ScpiCommand(":SOURce:SWEEp:TYPE {0}", SweepType);
        }
    }
}
