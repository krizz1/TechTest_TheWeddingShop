namespace Mars.Domain
{
    public class RoverBase : IRover
    {
        public RoverBase(string name, CardinalDirections cardinalDirection = CardinalDirections.N, int speed = 1)
        {
            Name = name;
            CardinalDirection = cardinalDirection;
            Speed = speed;
            Position = new CartesianCoordinate();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public CartesianCoordinate Position { get; set; }
        public CardinalDirections CardinalDirection { get; set; }
        public int Speed { get; set; }
    }
}
