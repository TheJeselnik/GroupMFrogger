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
        /// Gets the number of frog homes.
        /// </summary>
        /// <value>
        /// The number of frog homes.
        /// </value>
        public static int NumberOfFrogHomes { get; } = 5;

        /// <summary>
        ///     Gets the bottom lane offset y.
        /// </summary>
        /// <value>
        ///     The bottom lane offset y.
        /// </value>
        public static double BottomOffsetHeight { get; } = 5;

        /// <summary>
        ///     Gets the height of the lane.
        /// </summary>
        /// <value>
        ///     The height of the lane.
        /// </value>
        public static double LaneHeight { get; } = 50;

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
        /// Gets the left edge of road.
        /// </summary>
        /// <value>
        /// The left edge of road.
        /// </value>
        public static double LeftEdgeOfRoad { get; } = 0.0;

        /// <summary>
        ///     Gets the bottom shoulder.
        /// </summary>
        /// <value>
        ///     The bottom shoulder.
        /// </value>
        public static Lane BottomShoulder { get; private set; }

        // TODO set as a shoulder object?
        /// <summary>
        ///     Gets the top shoulder.
        /// </summary>
        /// <value>
        ///     The top shoulder.
        /// </value>
        public static Lane TopShoulder { get; private set; }

        /// <summary>
        ///     Gets or sets the first lane.
        /// </summary>
        /// <value>
        ///     The first lane.
        /// </value>
        public static Lane FirstLane { get; set; } = 
            new Lane(Vehicle.VehicleType.Car, Vehicle.Direction.Left, 3.0, 3);

        /// <summary>
        ///     Gets or sets the second lane.
        /// </summary>
        /// <value>
        ///     The second lane.
        /// </value>
        public static Lane SecondLane { get; set; } =
            new Lane(Vehicle.VehicleType.SemiTruck, Vehicle.Direction.Right, 3.5, 2);

        /// <summary>
        ///     Gets or sets the third lane.
        /// </summary>
        /// <value>
        ///     The third lane.
        /// </value>
        public static Lane ThirdLane { get; set; } = 
            new Lane(Vehicle.VehicleType.Car, Vehicle.Direction.Left, 4.0, 4);

        /// <summary>
        ///     Gets or sets the fourth lane.
        /// </summary>
        /// <value>
        ///     The fourth lane.
        /// </value>
        public static Lane FourthLane { get; set; } =
            new Lane(Vehicle.VehicleType.SemiTruck, Vehicle.Direction.Left, 4.5, 3);

        /// <summary>
        ///     Gets or sets the fifth lane.
        /// </summary>
        /// <value>
        ///     The fifth lane.
        /// </value>
        public static Lane FifthLane { get; set; } = new 
            Lane(Vehicle.VehicleType.Car, Vehicle.Direction.Right, 5.0, 5);

        /// <summary>
        ///     Gets the vehicle lanes.
        /// </summary>
        /// <value>
        ///     The vehicle lanes.
        /// </value>
        public static IList<Lane> VehicleLanes { get; } = new List<Lane>
            {FirstLane, SecondLane, ThirdLane, FourthLane, FifthLane};

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes the <see cref="GameSettings" /> class.
        /// </summary>
        static GameSettings()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}