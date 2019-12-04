namespace BerlinClock
{
    public interface IClockLineFactory
    {
        IClockLine GetSecondsLine();
        IClockLine GetTopMinutesLine();
        IClockLine GetBottomMinutesLine();
        IClockLine GetTopHoursLine();
        IClockLine GetBottomHourseLine();
    }
}
