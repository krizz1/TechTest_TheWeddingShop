using System.Collections.Generic;

namespace Mars.Domain
{
    public class Plateau
    {
        public Plateau()
        {
            Rovers = new List<IRover>();
        }

        public int LengthX { get; set; }
        public int LengthY { get; set; }
        public List<IRover> Rovers { get; set; }
    }
}
