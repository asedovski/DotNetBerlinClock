namespace BerlinClock
{
    public interface IBerlinClockCalculator : IStringStateObject
    {
        void SetTime(int hh, int mm, int ss);
    }
}
