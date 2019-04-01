using Mars.Domain;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mars.Logic.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test, TestCaseSource(typeof(TestData), nameof(TestData.TestCases))]
        public void TestLogic(Tuple<int,int> plateauSize, List<RoverTestData> roverTestData)
        {
            SimulationManager simMan = new SimulationManager(plateauSize.Item1, plateauSize.Item2);

            foreach (RoverTestData td in roverTestData)
            {
                int roverId = simMan.LandRover(new RoverNasa("Pathfinder")
                {
                    Position = td.LandingLocation,
                    CardinalDirection = td.LandingOrientation,
                    Speed = 1
                });

                simMan.ProcessRoverCommands(roverId, td.Instructions);
                IRover rover = simMan.GetRover(roverId);
                TestContext.WriteLine($"Rover id: {rover.Id} Rover XY: {rover.Position.X} {rover.Position.Y} Rover Direction: {rover.CardinalDirection.ToString()}");
                Assert.IsTrue(
                    rover.Position.X == td.ExpectedEndLocation.X &&
                    rover.Position.Y == td.ExpectedEndLocation.Y &&
                    rover.CardinalDirection == td.ExpectedEndOrientation
                    );
            }
        }

        public class TestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Tuple<int,int>(5,5),
                        new List<RoverTestData>() {
                            new RoverTestData(){
                                LandingLocation = new CartesianCoordinate(1, 2),
                                LandingOrientation = CardinalDirections.N,
                                Instructions = "LMLMLMLMM",
                                ExpectedEndLocation = new CartesianCoordinate(1, 3),
                                ExpectedEndOrientation = CardinalDirections.N
                            },
                            new RoverTestData(){
                                LandingLocation = new CartesianCoordinate(3, 3),
                                LandingOrientation = CardinalDirections.E,
                                Instructions = "MMRMMRMRRM",
                                ExpectedEndLocation = new CartesianCoordinate(5, 1),
                                ExpectedEndOrientation = CardinalDirections.E
                            }
                        })
                        .SetName("Given_a_Plateau_of_fixed_size_and_two_Rovers_when_instruction_sets_are_processed_then_the_rovers_move_to_expected_coordinates")
                        .SetCategory("CI");
                }
            }
        }

        public class RoverTestData
        {
            public CartesianCoordinate LandingLocation { get; set; }
            public CardinalDirections LandingOrientation { get; set; }
            public string Instructions { get; set; }
            public CartesianCoordinate ExpectedEndLocation { get; set; }
            public CardinalDirections ExpectedEndOrientation { get; set; }
        }
    }
}
