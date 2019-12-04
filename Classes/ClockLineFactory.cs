namespace BerlinClock
{
    public class ClockLineFactory : IClockLineFactory
    {
        public IClockLine GetSecondsLine()
        {
            return new ClockLine(01, (hh, mm, ss, lampIndex) => { return ss % 2 == 0 ? LampModes.LAMP_YELLOW : LampModes.LAMP_OFF; });
        }

        public IClockLine GetTopMinutesLine()
        {
            return new ClockLine(11, (hh, mm, ss, lampIndex) => { return mm / (5 * (lampIndex + 1)) >= 1 ? ((lampIndex == (3 - 1) || lampIndex == (6 - 1) || lampIndex == (9 - 1)) ? LampModes.LAMP_RED : LampModes.LAMP_YELLOW) : LampModes.LAMP_OFF; });
        }

        public IClockLine GetBottomMinutesLine()
        {
            return new ClockLine(04, (hh, mm, ss, lampIndex) => { return (lampIndex + 1) <= mm % 5 ? LampModes.LAMP_YELLOW : LampModes.LAMP_OFF; });
        }

        public IClockLine GetTopHoursLine()
        {
            return new ClockLine(04, (hh, mm, ss, lampIndex) => { return hh / (5 * (lampIndex + 1)) >= 1 ? LampModes.LAMP_RED : LampModes.LAMP_OFF; });
        }

        public IClockLine GetBottomHourseLine()
        {
            return new ClockLine(04, (hh, mm, ss, lampIndex) => { return (lampIndex + 1) <= hh % 5 ? LampModes.LAMP_RED : LampModes.LAMP_OFF; });
        }
    }
}
