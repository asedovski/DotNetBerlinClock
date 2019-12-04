using System;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private readonly IBerlinClockCalculator _berlinClockCalculator;

        public TimeConverter(IBerlinClockCalculator berlinClockCalculator)
        {
            if (berlinClockCalculator == null)
                throw new ArgumentNullException("berlinClockCalculator");

            _berlinClockCalculator = berlinClockCalculator;
        }

        public string convertTime(string aTime)
        {
            if (string.IsNullOrEmpty(aTime))
                throw new ArgumentNullException("aTime");

            var timeParts = aTime.Split(new char[] { ':' });

            if (timeParts.Length != 3)
                throw new ArgumentException("aTime");

            int hh, mm, ss;

            if (!int.TryParse(timeParts[0], out hh))
                throw new ArgumentException("aTime", "Invalid hours value");

            if (!int.TryParse(timeParts[1], out mm))
                throw new ArgumentException("aTime", "Invalid minutes value");

            if (!int.TryParse(timeParts[2], out ss))
                throw new ArgumentException("aTime", "Invalid seconds value");

            _berlinClockCalculator.SetTime(hh, mm, ss);
            return _berlinClockCalculator.GetStringState();
        }
    }
}
