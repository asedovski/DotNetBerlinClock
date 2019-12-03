using System;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
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

            var berlinClockCalculator = new BerlinClockCalculator();
            berlinClockCalculator.SetTime(hh, mm, ss);
            return berlinClockCalculator.GetStringRepresentation();
        }


    }

    public class BerlinClockCalculator
    {
        private ClockLine[] _berlinClockState = new ClockLine[]
        {
            new ClockLine(01, (hh, mm, ss, lampIndex) => { return ss % 2 == 0 ? ClockLine.LAMP_YELLOW : ClockLine.LAMP_OFF; }),
            new ClockLine(04, (hh, mm, ss, lampIndex) => { return hh / (5 * (lampIndex + 1)) >= 1 ? ClockLine.LAMP_RED : ClockLine.LAMP_OFF; }),
            new ClockLine(04, (hh, mm, ss, lampIndex) => { return (lampIndex + 1) <= hh % 5 ? ClockLine.LAMP_RED : ClockLine.LAMP_OFF; }),
            new ClockLine(11, (hh, mm, ss, lampIndex) => { return mm / (5 * (lampIndex + 1)) >= 1 ? ((lampIndex == (3 - 1) || lampIndex == (6 - 1) || lampIndex == (9 - 1)) ? ClockLine.LAMP_RED : ClockLine.LAMP_YELLOW) : ClockLine.LAMP_OFF;}),
            new ClockLine(04, (hh, mm, ss, lampIndex) => { return (lampIndex + 1) <= mm % 5 ? ClockLine.LAMP_YELLOW : ClockLine.LAMP_OFF; })
        };

        public void SetTime(int hh, int mm, int ss)
        {
            if (hh < 0 || hh > 24)
                throw new ArgumentOutOfRangeException("hh");

            if (mm < 0 || mm > 59)
                throw new ArgumentOutOfRangeException("mm");

            if (ss < 0 || ss > 59)
                throw new ArgumentOutOfRangeException("ss");

            for (int lineIndex = 0; lineIndex < _berlinClockState.Length; lineIndex++)
            {
                for (int lampIndex = 0; lampIndex < _berlinClockState[lineIndex].TotalLampsCount; lampIndex++)
                {
                    _berlinClockState[lineIndex].SetLampMode(lampIndex, _berlinClockState[lineIndex].TimePerPositionToColorRule(hh, mm, ss, lampIndex));
                }
            }
        }

        public string GetStringRepresentation()
        {
            var stringRepresentation = new StringBuilder();

            foreach (var clockLine in _berlinClockState)
            {
                if (stringRepresentation.Length > 0)
                {
                    stringRepresentation.AppendLine();
                }

                stringRepresentation.Append(clockLine.ToString());
            }

            return stringRepresentation.ToString();
        }
    }

    public class ClockLine
    {
        public const string LAMP_OFF = "O";
        public const string LAMP_YELLOW = "Y";
        public const string LAMP_RED = "R";

        private string[] _lamps;

        public int TotalLampsCount { get { return _lamps.Length; } }
        public Func<int, int, int, int, string> TimePerPositionToColorRule { get; private set; }

        public ClockLine(int lampsCount, Func<int, int, int, int, string> timePerPositionToColorRule)
        {
            if (lampsCount < 1)
                throw new ArgumentOutOfRangeException("lampsCount");

            if (timePerPositionToColorRule == null)
                throw new ArgumentNullException("timePerPositionToColorRule");

            _lamps = new string[lampsCount];
            TimePerPositionToColorRule = timePerPositionToColorRule;

            for (int lampIndex = 0; lampIndex < TotalLampsCount; lampIndex++)
            {
                SetLampMode(lampIndex, LAMP_OFF);
            }
        }

        public void SetLampMode(int lampIndex, string lampMode)
        {
            if (lampIndex < 0 || lampIndex > (TotalLampsCount - 1))
                throw new ArgumentOutOfRangeException("lampIndex");

            if (lampMode != LAMP_OFF && lampMode != LAMP_YELLOW && lampMode != LAMP_RED)
                throw new ArgumentOutOfRangeException("lampMode");

            _lamps[lampIndex] = lampMode;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _lamps);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj);
        }
    }
}
