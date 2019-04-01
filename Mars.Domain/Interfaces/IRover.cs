namespace Mars.Domain
{
    public interface IRover
    {
        int Id { get; set; }
        string Name { get; set; }
        CartesianCoordinate Position { get; set; }
        //int Bearing { get; set; }
        CardinalDirections CardinalDirection { get; set; }
        int Speed { get; set; }
    }
}
