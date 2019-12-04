using System;
using System.Linq;

namespace BerlinClock
{
    public class ClockLine : IClockLine
    {
        private ILampState[] _lamps;

        public int TotalLampsCount { get { return _lamps.Length; } }
        public Func<int, int, int, int, ILampState> TimePerPositionToColorRule { get; private set; }

        public ClockLine(int lampsCount, Func<int, int, int, int, ILampState> timePerPositionToColorRule)
        {
            if (lampsCount < 1)
                throw new ArgumentOutOfRangeException("lampsCount");

            if (timePerPositionToColorRule == null)
                throw new ArgumentNullException("timePerPositionToColorRule");

            _lamps = new ILampState[lampsCount];
            TimePerPositionToColorRule = timePerPositionToColorRule;

            for (int lampIndex = 0; lampIndex < TotalLampsCount; lampIndex++)
            {
                SetLampMode(lampIndex, LampModes.LAMP_OFF);
            }
        }

        public void SetLampMode(int lampIndex, ILampState lampMode)
        {
            if (lampIndex < 0 || lampIndex > (TotalLampsCount - 1))
                throw new ArgumentOutOfRangeException("lampIndex");

            if (lampMode != LampModes.LAMP_OFF && lampMode != LampModes.LAMP_YELLOW && lampMode != LampModes.LAMP_RED)
                throw new ArgumentOutOfRangeException("lampMode");

            _lamps[lampIndex] = lampMode;
        }

        public string GetStringState()
        {
            return string.Join(string.Empty, _lamps.Select(r => r.GetStringState()));
        }
    }
}
