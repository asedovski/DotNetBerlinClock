using System;
using System.Text;

namespace BerlinClock
{
    public class BerlinClockCalculator : IBerlinClockCalculator
    {
        private IClockLine[] _berlinClockState;

        public BerlinClockCalculator(IClockLineFactory clockLineFactory)
        {
            if (clockLineFactory == null)
                throw new ArgumentNullException("clockLineFactory");

            _berlinClockState = new IClockLine[]
            {
                clockLineFactory.GetSecondsLine(),
                clockLineFactory.GetTopHoursLine(),
                clockLineFactory.GetBottomHourseLine(),
                clockLineFactory.GetTopMinutesLine(),
                clockLineFactory.GetBottomMinutesLine(),
            };
        }

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

        public string GetStringState()
        {
            var stringRepresentation = new StringBuilder();

            foreach (var clockLine in _berlinClockState)
            {
                if (stringRepresentation.Length > 0)
                {
                    stringRepresentation.AppendLine();
                }

                stringRepresentation.Append(clockLine.GetStringState());
            }

            return stringRepresentation.ToString();
        }
    }
}
