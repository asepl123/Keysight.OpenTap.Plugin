using OpenTap;


namespace Keysight.OpenTap.Plugins.Exg.Source.Correction
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Keysight.OpenTap.Plugins.Exg;


    [Display("Flatness Correction", Group: "Keysight.OpenTap.Plugins.Exg", Description: "Insert description here")]
    public class Flateness : TestStep
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
        
        private int _FreqPoint = 51;
        
        [DisplayAttribute("FreqPoint", " returns the frequency value of the <point> queried. \r\n(1 to 3,201)", "Input Parameters", 2)]
        public int FreqPoint
        {
            get
            {
                return this._FreqPoint;
            }
            set
            {
                this._FreqPoint = value;
            }
        }
        
        private string _FlatnessCorrectionFilename = "MyFile";
        
        [DisplayAttribute("FlatnessCorrectionFilename", " loads a user-flatness correction file", "Input Parameters", 2)]
        public string FlatnessCorrectionFilename
        {
            get
            {
                return this._FlatnessCorrectionFilename;
            }
            set
            {
                this._FlatnessCorrectionFilename = value;
            }
        }
        
        private double _CorrFreq = 5D;
        
        [Unit("Hz")]
        [DisplayAttribute("CorrFreq", "", "Input Parameters", 2)]
        public double CorrFreq
        {
            get
            {
                return this._CorrFreq;
            }
            set
            {
                this._CorrFreq = value;
            }
        }
        
        private double _CorrAmplitude = 0.1D;
        
        [Unit("dB")]
        [DisplayAttribute("CorrAmplitude", "", "Input Parameters", 2)]
        public double CorrAmplitude
        {
            get
            {
                return this._CorrAmplitude;
            }
            set
            {
                this._CorrAmplitude = value;
            }
        }
        
        private string _SaveCorrectionFile = "myfile";
        
        [DisplayAttribute("SaveCorrectionFile", "stores the current user-flatness correction data to a file named ", "Input Parameters", 2)]
        public string SaveCorrectionFile
        {
            get
            {
                return this._SaveCorrectionFile;
            }
            set
            {
                this._SaveCorrectionFile = value;
            }
        }
        
        private int _CorrStepPoint = 501;
        
        [DisplayAttribute("CorrStepPoint", " define the number of points in the user flatness calibration step array\r\n2 to 3," +
            "201", "Input Parameters", 2)]
        public int CorrStepPoint
        {
            get
            {
                return this._CorrStepPoint;
            }
            set
            {
                this._CorrStepPoint = value;
            }
        }
        
        private string _CorrStepStartFreq = "MINimum";
        
        [Unit("HZ")]
        [DisplayAttribute("CorrStepStartFreq", "sets the start frequency for the user flatness calibration step array", "Input Parameters", 2)]
        public string CorrStepStartFreq
        {
            get
            {
                return this._CorrStepStartFreq;
            }
            set
            {
                this._CorrStepStartFreq = value;
            }
        }
        
        private string _CorrStepStopFreq = "MAXimum";
        
        [Unit("HZ")]
        [DisplayAttribute("CorrStepStopFreq", "sets the stop frequency for the user flatness calibration step array", "Input Parameters", 2)]
        public string CorrStepStopFreq
        {
            get
            {
                return this._CorrStepStopFreq;
            }
            set
            {
                this._CorrStepStopFreq = value;
            }
        }
        
        private Double QueriedFreq;
        
        // returns the number of points in the user-flatness correction file.
        private Int32 QueriedPointInFile;
        
        public override void Run()
        {
            QueriedFreq = MyInst.ScpiQuery<System.Double>(Scpi.Format(":SOURce:CORRection:FLATness:FREQuency? {0}",FreqPoint), true);
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:LOAD {0}",FlatnessCorrectionFilename);
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:PAIR {0},{1}",CorrFreq,CorrAmplitude);
            QueriedPointInFile = MyInst.ScpiQuery<System.Int32>(Scpi.Format(":SOURce:CORRection:FLATness:POINts?"), true);
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:PRESet");
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:STORe {0}",SaveCorrectionFile);
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:STEP:POINts {0}",CorrStepPoint);
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:STEP:STARt {0}",CorrStepStartFreq);
            MyInst.ScpiCommand(":SOURce:CORRection:FLATness:STEP:STOP {0}",CorrStepStopFreq);
        }
    }
}
