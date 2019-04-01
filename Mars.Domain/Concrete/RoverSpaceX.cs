namespace Mars.Domain
{
    public class RoverSpaceX : RoverBase
    {
        public RoverSpaceX(string name) : base("SxR:" + name, speed: 2)
        {
        }
    }
}
