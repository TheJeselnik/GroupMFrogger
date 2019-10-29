using System.Collections.Generic;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Contains settings and values needed throughout the game.
    /// </summary>
    public static class GameSettings
    {
        #region Properties

        /// <summary>
        /// Gets the initial lives.
        /// </summary>
        /// <value>
        /// The initial lives.
        /// </value>
        public static int InitialLives { get; } = 4;

        /// <summary>
        /// Gets the initial score.
        /// </summary>
        /// <value>
        /// The initial score.
        /// </value>
        public static int InitialScore { get; } = 0;

        /// <summary>
        /// Gets the score limit.
        /// </summary>
        /// <value>
        /// The score limit.
        /// </value>
        public static int ScoreLimit { get; } = 5;

        /// <summary>
        /// Gets the required minimum spacing of vehicles
        /// </summary>
        /// <value>
        /// The vehicle spacing.
        /// </value>
        public static double VehicleSpacing { get; } = 15.0;

        /// <summary>
        ///     Gets the road offset y.
        /// </summary>
        /// <value>
        ///     The road offset y.
        /// </value>
        public static double RoadOffsetHeight { get; } = 5.0;

        /// <summary>
        ///     Gets the height of the lane.
        /// </summary>
        /// <value>
        ///     The height of the lane.
        /// </value>
        public static double LaneHeight { get; } = 50.0;

        /// <summary>
        ///     Gets or sets the width of the road.
        /// </summary>
        /// <value>
        ///     The width of the road.
        /// </value>
        public static double RoadWidth { get; set; }

        /// <summary>
        ///     Gets or sets the height of the road.
        /// </summary>
        /// <value>
        ///     The height of the road.
        /// </value>
        public static double RoadHeight { get; set; }

        /// <summary>
        ///     Gets the left edge of road.
        /// </summary>
        /// <value>
        ///     The left edge of road.
        /// </value>
        public static double LeftEdgeOfRoad { get; } = 0.0;

        /// <summary>
        /// Gets the top edge of lanes.
        /// </summary>
        /// <value>
        /// The top edge of lanes.
        /// </value>
        public static double TopEdgeOfLanes { get; } = 105.0;

        /// <summary>
        ///     Gets or sets the first lane.
        /// </summary>
        /// <value>
        ///     The first lane.
        /// </value>
        private static Lane FirstLane { get; } =
            new Lane(Vehicle.VehicleType.Car, Vehicle.Direction.Left, 3.0, 3);

        /// <summary>
        ///     Gets or sets the second lane.
        /// </summary>
        /// <value>
        ///     The second lane.
        /// </value>
        private static Lane SecondLane { get; } =
            new Lane(Vehicle.VehicleType.SemiTruck, Vehicle.Direction.Right, 3.5, 2);

        /// <summary>
        ///     Gets or sets the third lane.
        /// </summary>
        /// <value>
        ///     The third lane.
        /// </value>
        private static Lane ThirdLane { get; } =
            new Lane(Vehicle.VehicleType.Car, Vehicle.Direction.Left, 4.0, 4);

        /// <summary>
        ///     Gets or sets the fourth lane.
        /// </summary>
        /// <value>
        ///     The fourth lane.
        /// </value>
        private static Lane FourthLane { get; } =
            new Lane(Vehicle.VehicleType.SemiTruck, Vehicle.Direction.Left, 4.5, 3);

        /// <summary>
        ///     Gets or sets the fifth lane.
        /// </summary>
        /// <value>
        ///     The fifth lane.
        /// </value>
        private static Lane FifthLane { get; } = new
            Lane(Vehicle.VehicleType.Car, Vehicle.Direction.Right, 5.0, 5);

        /// <summary>
        ///     Gets the vehicle lanes.
        /// </summary>
        /// <value>
        ///     The vehicle lanes.
        /// </value>
        public static IList<Lane> VehicleLanes { get; } = new List<Lane>
            { FirstLane, SecondLane, ThirdLane, FourthLane, FifthLane };

        #endregion
    }
}