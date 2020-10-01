using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Exg.Source
{
	[Display("Memory", Group: "Keysight.OpenTap.Plugins.Exg.Source", Description: "Insert a description here")]
	public class Memory : TestStep
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

        private string _FromFileName = "fromfilename";

        [DisplayAttribute("FromFileName", "", "Input Parameters", 2)]
        public string FromFileName
        {
            get
            {
                return this._FromFileName;
            }
            set
            {
                this._FromFileName = value;
            }
        }

        private string _ToFileName = "tofilename";

        [DisplayAttribute("ToFileName", "", "Input Parameters", 2)]
        public string ToFileName
        {
            get
            {
                return this._ToFileName;
            }
            set
            {
                this._ToFileName = value;
            }
        }

        private string _DeleteFileName = "deletefilename";

        [DisplayAttribute("DeleteFileName", "", "Input Parameters", 2)]
        public string DeleteFileName
        {
            get
            {
                return this._DeleteFileName;
            }
            set
            {
                this._DeleteFileName = value;
            }
        }

        public override void Run()
        {
            MyInst.ScpiCommand(":MEMory:COPY:NAME {0},{1}", FromFileName, ToFileName);
            MyInst.ScpiCommand(":MEMory:MOVE {0},{1}", FromFileName, ToFileName);
            MyInst.ScpiCommand(":MEMory:DELete:NAME {0}", DeleteFileName);
        }
    }
}
