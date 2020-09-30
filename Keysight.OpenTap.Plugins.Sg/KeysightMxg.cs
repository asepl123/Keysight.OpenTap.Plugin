using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using Keysight.OpenTap.Plugins.Interfaces.Common;
using Keysight.OpenTap.Plugins.Interfaces.MultiModeInstrument;
using Keysight.OpenTap.Plugins.Interfaces.SG;
using OpenTap;


namespace Keysight.OpenTap.Plugins.Sg
{
    [Display("Keysight MXG driver", Group: "OpenTap.Plugins", Description: "Keysight MXG driver")]
    public class SgKeysightMxg : ScpiInstrument, ISg
    {

        protected string ModelName;

        public SgKeysightMxg()
        {
            ModelName = "MXG";
            ArbFileList = new List<string> { "ArbOne" };
            Name = "MXG";
        }

        /// <summary>
        ///     Open procedure for the instrument.
        /// </summary>
        public override void Open()
        {
            base.Open();
            CheckIfArbFileExists(UseArbFileCheck);
        }

        /// <summary>
        ///     Close procedure for the instrument.
        /// </summary>
        public override void Close()
        {
            // TODO:  Shut down the connection to the instrument here.
            base.Close();
        }

        /// <inheritdoc />
        public virtual void SetFrequency(double freqInMHz)
        {
            if (freqInMHz <= 0) throw new ArgumentOutOfRangeException(nameof(freqInMHz));
            ScpiCommand("FREQ:CENT " + freqInMHz.ToString("#.000000", CultureInfo.InvariantCulture) + "MHZ");
        }

        /// <inheritdoc />
        public virtual decimal GetFrequency()
        {
            return ScpiQuery<decimal>("FREQ:CENT?");
        }

        /// <inheritdoc />
        public decimal GetFixedFrequency()
        {
            Log.Warning($"Not supported on Keysight {ModelName}");
            return decimal.MinValue;
        }

        /// <inheritdoc />
        public void SetOutputLevel(double outputLevelInDbm)
        {
            ScpiCommand("POW " + outputLevelInDbm.ToString("#.000", CultureInfo.InvariantCulture) + "DBM");
        }

        /// <inheritdoc />
        public double GetOutputLevel()
        {
            return ScpiQuery<double>("POW?");
        }

        /// <inheritdoc />
        public void SetRfOutputState(EState outputState)
        {
            ScpiCommand("OUTP:STAT " + Scpi.Format("{0}", outputState));
            if (outputState != EState.On) return;
            WaitUnlevelStateToClear();
        }

        /// <inheritdoc />
        public void SetModulationState(EState modulationState)
        {
            ScpiCommand("OUTP:MOD:STAT " + Scpi.Format("{0}", modulationState));
        }

        /// <inheritdoc />
        public void SetWidebandIqModulatorState(EState modulatorState)
        {
            Log.Warning($"Not supported on Keysight {ModelName}");
        }

        /// <inheritdoc />
        public void SetReferenceOscillatorAutoState(EState oscillatorState)
        {
            ScpiCommand("SOUR:ROSC:SOUR:AUTO " + Scpi.Format("{0}", oscillatorState));
        }

        /// <inheritdoc />
        public void SetAlcState(EState state)
        {
            ScpiCommand("SOUR:POW:ALC:STAT " + Scpi.Format("{0}", state));
        }

        /// <inheritdoc />
        public void SetReferenceClock(double refFreqInMhz, EArbRefSource referenceSource)
        {
            // ESG reference fixed 10MHz, ignore input parameter:dRefFreqInMHz
            if (referenceSource != EArbRefSource.RefsourceInt || !(refFreqInMhz > 0))
            {
                if (referenceSource != EArbRefSource.RefsourceExt || !(refFreqInMhz > 0)) return;
                Log.Info("SG:SetReferenceClock. Source: " + referenceSource + ", Freq:" + refFreqInMhz);
                ScpiCommand(
                    "ROSC:FREQ:EXT " + (refFreqInMhz * 1E6).ToString(CultureInfo.InvariantCulture) + ";*WAI");
            }
            else
            {
                Log.Info("SG:SetReferenceClock. Source: " + referenceSource + ", Freq: " + refFreqInMhz);
                ScpiCommand("ROSC:SOUR:AUTO ON;*WAI");
            }
        }

        /// <inheritdoc />
        public uint GetPowerCondition()
        {
            //Check this
            ScpiCommand(":STAT:QUES:POW:ENAB 2");
            return ScpiQuery<uint>(":STAT:QUES:POW:COND?");
        }

        /// <inheritdoc />
        public void SetSelectArbFile(string fileFolderAndName, double awgSampleClockMhz)
        {
            Log.Info("Sg:select arb file");
            if (fileFolderAndName != string.Empty)
            {
                ScpiCommand(":SOUR:RAD:ARB:WAV \"WFM1:" + Path.GetFileName(fileFolderAndName) + "\";*WAI");
                //Arb state on
                ScpiCommand("SOUR:RAD:ARB:STAT ON;*WAI");

                if (awgSampleClockMhz <= 0)
                    throw new ArgumentOutOfRangeException(nameof(awgSampleClockMhz));
                SetSampleClock(awgSampleClockMhz);
            }
            else
            {
                throw new ArgumentException("fileFolderAndName is empty!");
            }
        }

        public void SetTriggerPolarity(ETriggerPolarity polarity)
        {
            Log.Info("Sg:Set trigger polarity");
            if (ETriggerPolarity.PolarityNeg == polarity)
                ScpiCommand("RAD:ARB:TRIG:EXT:SLOP NEG;*WAI");
            else
                ScpiCommand("RAD:ARB:TRIG:EXT:SLOP POS;*WAI");
        }

        public void SetTriggerDelay(double delayMs)
        {
            if (delayMs <= 0)
                throw new ArgumentException("Trying to set trigger delay <= 0 !", "delayMs");
            Log.Info("Sg:Set trigger delay");
            ScpiCommand("RAD:ARB:REF INT;*WAI");
            ScpiCommand("RAD:ARB:TRIG:EXT EPT1;*WAI");
            ScpiCommand("RAD:ARB:TRIG:EXT:DEL:STAT ON;*WAI");
            ScpiCommand("RAD:ARB:TRIG:EXT:DEL " + delayMs.ToString(CultureInfo.InvariantCulture) + "E-003;*WAI");
        }

        public void SetArbTriggerSourceExt()
        {
            Log.Info("Sg:Set Arb Trigger Source Ext CONT");
            ScpiCommand("RAD:ARB:TRIG:SOUR EXT;*WAI");
            ScpiCommand("RAD:ARB:TRIG:TYPE:CONT TRIG;*WAI");
        }

        public void SetArbState(EState arbState)
        {
            Log.Info("Sg:Set Arb State " + arbState);
            ScpiCommand("SOUR:RAD:ARB:STAT " + Scpi.Format("{0}", arbState));
        }

        /// <inheritdoc />
        public EopcStatus Opc()
        {
            var opcStatus = ScpiQuery("*OPC?");
            return opcStatus.Equals("1") ? EopcStatus.Complete : EopcStatus.Incomplete;
        }

        public virtual void SetRuntimeScaling(double scale)
        {
            throw new NotImplementedException();
        }

        private void SetSampleClock(double sampleClockMhz)
        {
            ScpiCommand("RAD:ARB:SCL:RATE " + (sampleClockMhz * 1000000).ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Checks and waits that unlevel state is clear
        /// </summary>
        protected void WaitUnlevelStateToClear()
        {
            //KS SG unlevel = 2
            uint unlevel = 2;
            var counter = 0;
            while (unlevel == 2 && counter < 30)
            {
                Thread.Sleep(500);
                unlevel = GetPowerCondition();
                counter++;
            }
        }

        private void CheckIfArbFileExists(EState arbFileCheckState)
        {
            //Empty previous errors
            QueryErrors();
            const int waitForCopy = 75;
            if (arbFileCheckState != EState.On) return;
            foreach (var arb in ArbFileList)
                if (arb == "ArbOne")
                {
                    Log.Info($"{ModelName}: Empty arbfile list");
                }

                else
                {
                    //ScpiCommand("MMEM:CDIR \"" + InstrumentArbFileFolder + "\";*WAI");
                    var instrumentFiles = ScpiQuery(":MMEM:CAT? \"WFM1:\"");
                    if (instrumentFiles.Contains(arb)) continue;
                    Log.Info($"{ModelName}:Try to move ARB file in volatile memory: " + arb);
                    ScpiCommand(":MEM:COPY:NAME \"NVWFM:" + arb + "\",\"WFM1:" + arb + "\"");

                    //TODO:Maybe need to set visa timeout bigger
                    var copyReady = Opc();
                    var counter = 0;
                    while (copyReady == EopcStatus.Incomplete && counter < waitForCopy)
                    {
                        copyReady = Opc();
                        Thread.Sleep(100);
                        counter++;
                    }

                    var errors = QueryErrors();
                    if (errors.Count <= 0) continue;
                    foreach (var error in errors)
                        //if error is -256,... file not found in SG
                        if (error.Code == -256)
                        {
                            Log.Info($"{ModelName}:Need to transfer file from PC.File:  " + arb);
                            var address = VisaAddress.Remove(0, 8); //"TCPI0::"
                            //192.168.255.xxx
                            var maxLength = 15;
                            //10.1.1.xxx
                            var post = address.IndexOf("::", StringComparison.Ordinal);
                            if (post > 0 && post < maxLength)
                                maxLength = post;

                            address = string.Concat(address.Take(maxLength));
                            var ftpArbFolderAndFileName = InstrumentArbFileFolder + "/" + arb;

                            //transform Arb file to /user/WAVEFORM

                            Log.Info($"{ModelName}:Try to move ARB file in volatile memory: " + arb);
                            ScpiCommand(":MEM:COPY:NAME \"NVWFM:" + arb + "\",\"WFM1:" + arb + "\"");
                            //TODO:Maybe need to set visa timeout bigger
                            counter = 0;
                            while (copyReady == EopcStatus.Incomplete && counter < waitForCopy)
                            {
                                copyReady = Opc();
                                Thread.Sleep(100);
                                counter++;
                            }

                            errors = QueryErrors();
                            if (errors.Count <= 0) continue;
                            string errorMessages = null;
                            foreach (var error2 in errors)
                            {
                                errorMessages += error2.Code;
                                errorMessages += ",";
                                errorMessages += error2.Message;
                                errorMessages += ".";
                            }

                            throw new ApplicationException(
                                "Cant move ARB file to volatile memory after ftp transfer,Error: " +
                                errorMessages);
                        }
                        else
                        {
                            throw new ApplicationException(
                                $"{ModelName} gives error: {error.Code}, {error.Message}");
                        }
                }
        }

        #region Settings

        [Display("Arb file check", "Use arb file check on instrument", null, 1)]
        public EState UseArbFileCheck { get; set; }

        [Display("Ftp username", "Ftp user name for arb file transfer", null, 2)]
        public string FtpUserName { get; set; }

        [Display("Ftp password", "Ftp password for arb file transfer", null, 3)]
        public string FtpPassword { get; set; }

        [Display("Instrument arb folder", "Instrument arb file folder", null, 4)]
        public string InstrumentArbFileFolder { get; set; }

        [Display("Local arb folder", "PC arb file folder", null, 5)]
        public string LocalArbFileFolder { get; set; }

        [Display("Arb files list", "List of used arb files", null, 7)]
        private List<string> ArbFileList { get; set; }

        public List<string> ArbFiles
        {
            get => ArbFileList;
            set
            {
                ArbFileList = value;
                OnPropertyChanged("ArbFileList");
            }
        }

        #endregion Settings

        #region Implementation of IScpi

        public void Clear()
        {
            ScpiCommand("*CLS");
        }

        #endregion

        #region Implementation of IMultiModeInstrument

        public void SetInstrumentMode(EInstrumentMode instrumentMode)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}
