using System;

namespace BerlinClock
{
    public interface IClockLine : IStringStateObject
    {
        int TotalLampsCount { get; }
        Func<int, int, int, int, ILampState> TimePerPositionToColorRule { get; }

        void SetLampMode(int lampIndex, ILampState lampMode);
    }
}
