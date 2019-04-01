using Mars.Domain;

namespace Mars.Logic
{
    public static class RoverExtensions
    {
        public static void TurnLeft(this IRover rover, int angle = 90)
        {
            int bearing = (int)rover.CardinalDirection;
            bearing = bearing - angle;
            if(bearing > 360) { bearing = bearing - 360; }
            if (bearing < 0) { bearing = bearing + 360; }
            rover.CardinalDirection = (CardinalDirections)bearing;
        }

        public static void TurnRight(this IRover rover, int angle = 90)
        {
            int bearing = (int)rover.CardinalDirection;
            bearing = bearing + angle;
            if (bearing > 360) { bearing = bearing - 360; }
            if (bearing < 0) { bearing = bearing + 360; }
            rover.CardinalDirection = (CardinalDirections)bearing;
        }
    }
}
