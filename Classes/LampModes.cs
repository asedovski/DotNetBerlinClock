namespace BerlinClock
{
    public class LampModes
    {
        public static readonly ILampState LAMP_OFF = new LampState("O");
        public static readonly ILampState LAMP_RED = new LampState("R");
        public static readonly ILampState LAMP_YELLOW = new LampState("Y");
    }
}
