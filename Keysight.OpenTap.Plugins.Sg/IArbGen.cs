using System.Collections.Generic;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Sg
{

    /// <summary>
    ///     Arb generator reference sources:
    ///     RefsourceInt = source internal.
    ///     RefsourceExt = source external.
    ///     RefsourceAxi = source AXI backplane.
    /// </summary>
    public enum EArbRefSource
    {
        [Scpi("INT")] RefsourceInt,
        [Scpi("EXT")] RefsourceExt,
        [Scpi("AXI")] RefsourceAxi
    }


    /// <summary>
    ///     Arb generator file formats
    /// </summary>
    public enum FileFormat
    {
        [Scpi("TXT")] Txt,
        [Scpi("TXT14")] Txt14,
        [Scpi("BIN")] Bin,
        [Scpi("QBIN")] IqBin,
        [Scpi("BIN6030")] Bin6030,
        [Scpi("BIN5110")] Bin5110,
        [Scpi("LIC")] Lic,
        [Scpi("MAT89600")] Mat89600,
        [Scpi("DSA90000")] Dsa90000,
        [Scpi("CSV")] Csv
    }


    /// <summary>
    ///     Arb generator data types
    /// </summary>
    public enum DataType
    {
        [Scpi("IONL")] Ionly,
        [Scpi("QONL")] Qonly,
        [Scpi("BOTH")] Both
    }

    /// <summary>
    ///     Arb file padding
    ///     ALENgth: Automatic LENgth : segment_id may or may not exist.
    ///     After execution segment_id has exactly the length of the pattern in
    ///     file or a multiple of this length to fulfill granularity and minimum segment length requirements.
    ///     Formula: length = granularity * max(5 , # samples in file). This behavior is default padding is omitted.
    ///     FILL:segment_id must exist. If pattern in file is larger than the defined segment length,
    ///     just ignore excessive samples. If pattern in file is smaller than defined segment length,
    ///     fill remaining samples withinit_value.init_value; defaults to 0 if it is not specified.
    /// </summary>
    public enum Padding
    {
        [Scpi("FILL")] Fill,
        [Scpi("ALEN")] Alenght
    }

    /// <summary>
    ///     Arb generator channels
    /// </summary>
    public enum Channel
    {
        [Scpi("1")] One,
        [Scpi("2")] Two
    }


    internal interface IArbGen : IInstrument
    {
        /// <summary>
        ///     Returns all the errors on the instrument error stack. Clears the list in
        ///     the same call.
        /// </summary>
        /// <param name="suppressLogMessages">if true the errors will not be logged</param>
        /// <param name="maxErrors">
        ///     The max number of errors to retrieve. Useful if instrument generates errors faster than they
        ///     can be read.
        /// </param>
        /// <returns></returns>
        List<ScpiInstrument.ScpiError> QueryErrors(bool suppressLogMessages = false, int maxErrors = 1000);

        /// <summary>
        ///     Import segment data from a file.
        ///     Different file formats are supported.
        ///     An already existing segment can be filled,
        ///     or a new segment can be created.
        ///     This can be used to import real waveform data as well complex I/Q data.
        /// </summary>
        /// <param name="channel">Channel one or two</param>
        /// <param name="seqmentid">Used segment id</param>
        /// <param name="fileName">file name including folder</param>
        /// <param name="fileFormat">Used file format: TXT|TXT14|BIN|IQBIN|BIN6030|BIN5110|LICensed|MAT89600|DSA90000|CSV</param>
        /// <param name="dataType">Used data type: IONLy|QONLy|BOTH</param>
        /// <param name="markerFlaq">marker_flag:ON|OFF|1|0 </param>
        /// <param name="padding">Padding:ALENgth|FILL</param>
        void SetIqImport(Channel channel, int seqmentid, string fileName, FileFormat fileFormat, DataType dataType,
            EState markerFlaq, Padding padding);

        /// <summary>
        ///     Set Reference Clock.
        /// </summary>
        void SetReferenceClock(double refFreqInMhz, EArbRefSource referenceSource);

        /// <summary>
        ///     Resets AWG.
        /// </summary>
        void Reset();

        /// <summary>
        ///     Sets the sample clock frequency
        /// </summary>
        /// <param name="sampleClockMhz">Sample clock frequency in Mhz</param>
        void SetSampleClock(double sampleClockMhz);

        /// <summary>
        ///     Stops signal generation on channel
        /// </summary>
        /// <param name="channel">channel one or two</param>
        void SetSignalGenerationOff(Channel channel);

        /// <summary>
        ///     Sets continous and trigger state
        /// </summary>
        /// <param name="channel">Channel one or twor</param>
        /// <param name="continousMode">continous mode, on or off</param>
        /// <param name="gateMode">
        ///     gate mode,0/OFF – Gate mode is off,
        ///     1/ON – Gate mode is on. If continuous mode is off, the trigger mode is “gated”.
        /// </param>
        void SetContinousAndTriggerState(Channel channel, EState continousMode, EState gateMode);

        /// <summary>
        ///     Start signal generation on a channel. If channels are coupled, both channels are started.
        /// </summary>
        /// <param name="channel">
        ///     Channel one or two</param>
        void SetSignalGenerationOn(Channel channel);

        /// <summary>
        ///     Switch (normal) output on or off.
        /// </summary>
        /// <param name="channel">
        ///     Channel one or two</param>
        /// <param name="outputState">Output state</param>
        void SetOutputState(Channel channel, EState outputState);
    }
}
