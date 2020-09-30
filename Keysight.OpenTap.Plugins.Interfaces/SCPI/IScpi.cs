using System;
using System.Collections.Generic;
using OpenTap;

namespace Keysight.OpenTap.Plugins.Interfaces.SCPI
{
    public interface IScpi
    {
        void WaitForOperationComplete(int timeoutMs = 2000);
        void Reset();
        List<ScpiInstrument.ScpiError> QueryErrors(bool suppressLogMessages = false, int maxErrors = 1000);
        string IdnString { get; }
        void Clear();
    }

}
