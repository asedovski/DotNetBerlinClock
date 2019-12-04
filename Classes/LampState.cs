namespace BerlinClock
{
    public class LampState : ILampState
    {
        private string _state;

        public LampState(string state)
        {
            _state = state;
        }

        public string GetStringState()
        {
            return _state;
        }
    }
}
