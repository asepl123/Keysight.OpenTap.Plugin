using System.Collections.Generic;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Sg
{
    public enum EState
    {
        [Scpi("OFF")] Off,
        [Scpi("ON")] On
    }

    public enum ERefSource
    {
        RefsourceInt,
        RefsourceExt,
        RefsourceAuto
    }

    public enum EopcStatus
    {
        Incomplete = 0,
        Complete = 1
    }

    public enum ETriggerPolarity
    {
        PolarityNeg,
        PolarityPos
    }

    public interface ISg : IInstrument
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
        ///     Resets SG.
        /// </summary>
        void Reset();

        /// <summary>
        ///     Sets frequency to SG.
        /// </summary>
        void SetFrequency(double freqInMHz);

        /// <summary>
        ///     Gets SG fixed frequency. Unit is MHz.
        /// </summary>
        decimal GetFixedFrequency();

        /// <summary>
        ///     Gets SG frequency. Unit is MHz.
        /// </summary>
        // TODO: Check return value. double not recommended, use decimal instead.
        decimal GetFrequency();

        /// <summary>
        ///     Sets RF output level on SG.
        /// </summary>
        void SetOutputLevel(double outputLevelInDbm);

        /// <summary>
        ///     Queries RF output level from SG.
        /// </summary>
        double GetOutputLevel();

        /// <summary>
        ///     Sets RF output state on SG.
        /// </summary>
        void SetRfOutputState(EState outputState);

        /// <summary>
        ///     Set modulation state.
        /// </summary>
        void SetModulationState(EState modulationState);

        /// <summary>
        ///     This command enables or disables the automatic leveling control (ALC) circuit
        /// </summary>
        /// <param name="state">Output state</param>
        void SetAlcState(EState state);

        /// <summary>
        ///     Set wideband I/Q modulator state.
        /// </summary>
        void SetWidebandIqModulatorState(EState modulatorState);

        /// <summary>
        ///     This command enables or disables the ability of the signal generator to automatically
        ///     select between the internal and an external reference oscillator
        /// </summary>
        /// <param name="oscillatorState">
        ///     ON (1): This choice enables the signal generator to detect
        ///     when a valid reference signal is present at the 10 MHz IN connector and automatically
        ///     switches from internal to external frequency reference.
        ///     OFF (0) :This choice selects the internal reference oscillator and disables the switching
        ///     capability between the internal and an external frequency reference.
        /// </param>
        void SetReferenceOscillatorAutoState(EState oscillatorState);

        /// <summary>
        ///   Selects arb file and run
        /// </summary>
        /// <param name="fileFolderAndName">folder and file name</param>
        /// <param name="awgSampleClockMhz">Sample clock frequency</param>
        void SetSelectArbFile(string fileFolderAndName, double awgSampleClockMhz);

        /// <summary>
        ///     Set reference Clock
        /// </summary>
        /// <param name="refFreqInMhz">Frequency in Mhz</param>
        /// <param name="referenceSource">External or Internal</param>
        void SetReferenceClock(double refFreqInMhz, EArbRefSource referenceSource);

        /// <summary>
        ///     Queries Power condition (e.g. unlevel state) on SG.
        /// </summary>
        uint GetPowerCondition();

        /// <summary>
        ///     Set trigger polarity, rise or fall.
        /// </summary>
        void SetTriggerPolarity(ETriggerPolarity polarity);

        /// <summary>
        ///     Set trigger delay (sec/chip).
        /// </summary>
        void SetTriggerDelay(double delayMs);

        /// <summary>
        ///     Set ARB State.
        /// </summary>
        void SetArbState(EState arbState);

        /// <summary>
        ///     Set ARB trigger source to external.
        /// </summary>
        void SetArbTriggerSourceExt();

        /// <summary>
        ///     Checks from instrument if previous operation is completed or not.
        /// </summary>
        /// <returns>Operation complete status</returns>
        EopcStatus Opc();

        /// <summary>
        /// Sets runtime scale
        /// </summary>
        /// <param name="scale">1-100%</param>
        void SetRuntimeScaling(double scale);
    }

}
