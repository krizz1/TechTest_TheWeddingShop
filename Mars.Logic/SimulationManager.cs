using Mars.Domain;
using System;
using System.Linq;

namespace Mars.Logic
{
    public class SimulationManager
    {
        Plateau _plateau;

        public SimulationManager(int lengthX, int lengthY)
        {
            _plateau = new Plateau()
            {
                LengthX = lengthX,
                LengthY = lengthY
            };
        }

        public int LandRover(IRover rover)
        {
            int newRoverId = 0;

            //If there are existing rovers on the plateau get the next available id
            if(_plateau.Rovers != null && _plateau.Rovers.Count > 0)
            {
                newRoverId = _plateau.Rovers.OrderByDescending(x => x.Id).Select(x => x.Id).First() + 1;
            }

            rover.Id = newRoverId;
            _plateau.Rovers.Add(rover);
            return newRoverId;
        }

        public void MoveRover(int id)
        {
            IRover rover = _plateau.Rovers.Where(x => x.Id == id).SingleOrDefault();
            CartesianCoordinate newPosition = new CartesianCoordinate();

            switch (rover.CardinalDirection)
            {
                case CardinalDirections.N:
                    newPosition.X = rover.Position.X;
                    newPosition.Y = rover.Position.Y + rover.Speed;
                    break;
                case CardinalDirections.E:
                    newPosition.X = rover.Position.X + rover.Speed;
                    newPosition.Y = rover.Position.Y;
                    break;
                case CardinalDirections.S:
                    newPosition.X = rover.Position.X;
                    newPosition.Y = rover.Position.Y - rover.Speed;
                    break;
                case CardinalDirections.W:
                    newPosition.X = rover.Position.X - rover.Speed;
                    newPosition.Y = rover.Position.Y;
                    break;
                default:
                    throw new Exception("Invalid bearing: " + rover.CardinalDirection.ToString());
            }
            
            if (IsValidLocation(newPosition))
            {
                rover.Position = newPosition;
            }
            
        }

        public void TurnRover(int id, bool turnLeft)
        {
            IRover rover = _plateau.Rovers.Where(x => x.Id == id).SingleOrDefault();

            if (turnLeft)
            {
                rover.TurnLeft();
            }
            else
            {
                rover.TurnRight();
            }
        }

        public IRover GetRover(int id)
        {
            return _plateau.Rovers.Where(x => x.Id == id).SingleOrDefault();
        }

        public string ProcessRoverCommands(int roverId, string commands)
        {
            char[] validCommands = new char[3] { 'M', 'L', 'R' };
            string executedCommands = string.Empty;

            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'M':
                        MoveRover(roverId);
                        executedCommands += command;
                        break;
                    case 'L':
                        TurnRover(roverId, true);
                        executedCommands += command;
                        break;
                    case 'R':
                        TurnRover(roverId, false);
                        executedCommands += command;
                        break;
                    default:
                        break;
                }
            }

            return executedCommands;
        }

        private bool IsValidLocation(CartesianCoordinate positionToTest)
        {
            //Check if the position is occupied by another rover
            if(_plateau.Rovers.Any(x => x.Position == positionToTest))
            {
                return false;
            }

            //Check if the position is within the permitted bounds
            if (positionToTest.X < 0 ||
                positionToTest.Y < 0 ||
                positionToTest.X > _plateau.LengthX ||
                positionToTest.Y > _plateau.LengthY)
            {
                return false;
            }

            return true;
        }

        
    }
}
