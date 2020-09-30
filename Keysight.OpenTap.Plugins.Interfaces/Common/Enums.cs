using OpenTap;

namespace Keysight.OpenTap.Plugins.Interfaces.Common
{
    public enum EState
    {
        NotSet,
        [Scpi("OFF")] Off,
        [Scpi("ON")] On
    }

    public enum EErrorOccurred
    {
        No,
        Yes
    }

    public enum EopcStatus
    {
        Incomplete = 0,
        Complete = 1
    }

    class Enums
    {
    }

}
