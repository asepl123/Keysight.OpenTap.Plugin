using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Interfaces.MultiModeInstrument
{
    public enum EInstrumentMode
    {
        NotSet,
        [Scpi("SA")] ModeSpa,
        [Scpi("WCDMA")] ModeWcdma,
        [Scpi("EDGEGSM")] ModeGsmedge,
        [Scpi("LTE")] ModeLte,
        [Scpi("LTETDD")] ModeLteTdd,
        [Scpi("SG")] ModeSg,
        [Scpi("CONFIG")] ModeConfig,
        [Scpi("NA")] ModeNa,
        [Scpi("BASIC")] ModeIqAnalyzer,
        [Scpi("MSRA")] ModeMsra,
        [Scpi("RTM")] ModeRtm,
        [Scpi("NR5G")] ModeNr5g,
        [Scpi("CCDF")] ModeCcdf,
        [Scpi("VMA")] ModeVma,
        [Scpi("")] ModeNone
    }

    public interface IMultiModeInstrument
    {
        /// <summary>
        ///     Set Instrument mode.
        /// </summary>
        /// <param name="instrumentMode">Set instrument mode</param>
        void SetInstrumentMode(EInstrumentMode instrumentMode);
    }

}
