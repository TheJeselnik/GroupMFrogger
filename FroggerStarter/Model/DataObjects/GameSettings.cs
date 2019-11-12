using System.Collections.Generic;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Model.DataObjects
{
    /// <summary>
    ///     Contains settings and values needed throughout the game.
    /// </summary>
    public static class GameSettings
    {
        #region Properties

        /// <summary>
        ///     Gets the initial lives.
        /// </summary>
        /// <value>
        ///     The initial lives.
        /// </value>
        public static int InitialLives { get; } = 4;

        /// <summary>
        ///     Gets the initial score.
        /// </summary>
        /// <value>
        ///     The initial score.
        /// </value>
        public static int InitialScore { get; } = 0;

        /// <summary>
        ///     Gets the timer milliseconds.
        /// </summary>
        /// <value>
        ///     The timer milliseconds.
        /// </value>
        public static int TimerMilliseconds { get; } = 15;

        /// <summary>
        ///     Gets the width of the timer.
        /// </summary>
        /// <value>
        ///     The width of the timer.
        /// </value>
        public static int TimerWidth { get; } = 640;

        /// <summary>
        /// Gets the ticks until spawn objects.
        /// </summary>
        /// <value>
        /// The ticks until spawn objects.
        /// </value>
        public static int TicksUntilSpawnObjects { get; } = 200;

        /// <summary>
        ///     Gets the levels in game.
        /// </summary>
        /// <value>
        ///     The levels in game.
        /// </value>
        public static int LevelsInGame { get; } = 3;

        /// <summary>
        ///     Gets the bonus time power up chance.
        /// </summary>
        /// <value>
        ///     The bonus time power up chance.
        /// </value>
        public static double BonusTimePowerUpChance { get; } = 99.9;

        /// <summary>
        ///     Gets the bonus time power up chance ceiling.
        /// </summary>
        /// <value>
        ///     The bonus time power up chance ceiling.
        /// </value>
        public static double BonusTimePowerUpChanceCeiling { get; } = 100.0;

        /// <summary>
        ///     Gets the time limit seconds.
        /// </summary>
        /// <value>
        ///     The time limit seconds.
        /// </value>
        public static double TimeLimitSeconds { get; } = 10.0;

        /// <summary>
        ///     Gets the bonus time reward.
        /// </summary>
        /// <value>
        ///     The bonus time reward.
        /// </value>
        public static double BonusTimeRewardSeconds { get; } = 5.0;

        /// <summary>
        ///     Gets the required minimum spacing of vehicles
        /// </summary>
        /// <value>
        ///     The vehicle spacing.
        /// </value>
        public static double VehicleSpacing { get; } = 25.0;

        /// <summary>
        ///     Gets the water object spacing.
        /// </summary>
        /// <value>
        ///     The water object spacing.
        /// </value>
        public static double WaterObjectSpacing { get; } = 100.0;

        /// <summary>
        ///     Gets the road offset y.
        /// </summary>
        /// <value>
        ///     The road offset y.
        /// </value>
        public static double RoadOffsetHeight { get; } = 5.0;

        /// <summary>
        ///     Gets the height of the water crossing offset.
        /// </summary>
        /// <value>
        ///     The height of the water crossing offset.
        /// </value>
        public static double WaterCrossingOffsetHeight { get; } = 10.0;

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
        ///     Gets the top edge of lanes.
        /// </summary>
        /// <value>
        ///     The top edge of lanes.
        /// </value>
        public static double TopEdgeOfLanes { get; } = 105.0;

        /// <summary>
        ///     Gets the score multiplier.
        /// </summary>
        /// <value>
        ///     The score multiplier.
        /// </value>
        public static double ScoreMultiplier { get; } = 200.0;

        /// <summary>
        ///     Gets or sets the first lane of the first level.
        /// </summary>
        /// <value>
        ///     The first lane.
        /// </value>
        private static Lane FirstLevelFirstLane { get; } =
            new Lane(Vehicle.VehicleType.Car, GameObject.Direction.Left, 3.0, 3);

        /// <summary>
        ///     Gets or sets the second lane of the first level.
        /// </summary>
        /// <value>
        ///     The second lane.
        /// </value>
        private static Lane FirstLevelSecondLane { get; } =
            new Lane(Vehicle.VehicleType.SemiTruck, GameObject.Direction.Right, 3.5, 2);

        /// <summary>
        ///     Gets or sets the third lane of the first level.
        /// </summary>
        /// <value>
        ///     The third lane.
        /// </value>
        private static Lane FirstLevelThirdLane { get; } =
            new Lane(Vehicle.VehicleType.Car, GameObject.Direction.Left, 4.0, 4);

        /// <summary>
        ///     Gets or sets the fourth lane of the first level.
        /// </summary>
        /// <value>
        ///     The fourth lane.
        /// </value>
        private static Lane FirstLevelFourthLane { get; } =
            new Lane(Vehicle.VehicleType.OilSemiTruck, GameObject.Direction.Left, 4.5, 3);

        /// <summary>
        ///     Gets or sets the fifth lane of the first level.
        /// </summary>
        /// <value>
        ///     The fifth lane.
        /// </value>
        private static Lane FirstLevelFifthLane { get; } = new
            Lane(WaterObject.WaterObjectType.Raft, GameObject.Direction.Right, 5.0, 5, true);

        /// <summary>
        ///     Gets the lanes of the first level.
        /// </summary>
        /// <value>
        ///     The first level lanes.
        /// </value>
        public static IList<Lane> FirstLevelLanes { get; } = new List<Lane>
            {FirstLevelFirstLane, FirstLevelSecondLane, FirstLevelThirdLane, FirstLevelFourthLane, FirstLevelFifthLane};

        /// <summary>
        ///     Gets or sets the first lane of the second level.
        /// </summary>
        /// <value>
        ///     The first lane.
        /// </value>
        private static Lane SecondLevelFirstLane { get; } =
            new Lane(Vehicle.VehicleType.Car, GameObject.Direction.Left, 3.0, 3);

        /// <summary>
        ///     Gets or sets the second lane of the second level.
        /// </summary>
        /// <value>
        ///     The second lane.
        /// </value>
        private static Lane SecondLevelSecondLane { get; } =
            new Lane(Vehicle.VehicleType.SemiTruck, GameObject.Direction.Right, 3.5, 2);

        /// <summary>
        ///     Gets or sets the third lane of the second level.
        /// </summary>
        /// <value>
        ///     The third lane.
        /// </value>
        private static Lane SecondLevelThirdLane { get; } =
            new Lane(WaterObject.WaterObjectType.Log, GameObject.Direction.Left, 4.0, 2, true);

        /// <summary>
        ///     Gets or sets the fourth lane of the second level.
        /// </summary>
        /// <value>
        ///     The fourth lane.
        /// </value>
        private static Lane SecondLevelFourthLane { get; } =
            new Lane(Vehicle.VehicleType.OilSemiTruck, GameObject.Direction.Left, 5.0, 3);

        /// <summary>
        ///     Gets or sets the fifth lane of the second level.
        /// </summary>
        /// <value>
        ///     The fifth lane.
        /// </value>
        private static Lane SecondLevelFifthLane { get; } = new
            Lane(Vehicle.VehicleType.Car, GameObject.Direction.Right, 2.0, 5);

        /// <summary>
        ///     Gets the lanes of the second level.
        /// </summary>
        /// <value>
        ///     The second level lanes.
        /// </value>
        public static IList<Lane> SecondLevelLanes { get; } = new List<Lane> {
            SecondLevelFirstLane, SecondLevelSecondLane, SecondLevelThirdLane, SecondLevelFourthLane,
            SecondLevelFifthLane
        };

        /// <summary>
        ///     Gets or sets the first lane of the third level.
        /// </summary>
        /// <value>
        ///     The first lane.
        /// </value>
        private static Lane ThirdLevelFirstLane { get; } =
            new Lane(Vehicle.VehicleType.Car, GameObject.Direction.Left, 4.0, 4);

        /// <summary>
        ///     Gets or sets the second lane of the third level.
        /// </summary>
        /// <value>
        ///     The second lane.
        /// </value>
        private static Lane ThirdLevelSecondLane { get; } =
            new Lane(Vehicle.VehicleType.SemiTruck, GameObject.Direction.Right, 4.5, 3);

        /// <summary>
        ///     Gets or sets the third lane of the third level.
        /// </summary>
        /// <value>
        ///     The third lane.
        /// </value>
        private static Lane ThirdLevelThirdLane { get; } =
            new Lane(Vehicle.VehicleType.Car, GameObject.Direction.Left, 5.0, 5);

        /// <summary>
        ///     Gets or sets the fourth lane of the third level.
        /// </summary>
        /// <value>
        ///     The fourth lane.
        /// </value>
        private static Lane ThirdLevelFourthLane { get; } =
            new Lane(WaterObject.WaterObjectType.Log, GameObject.Direction.Left, 4.5, 2, true);

        /// <summary>
        ///     Gets or sets the fifth lane of the third level.
        /// </summary>
        /// <value>
        ///     The fifth lane.
        /// </value>
        private static Lane ThirdLevelFifthLane { get; } = new
            Lane(WaterObject.WaterObjectType.Raft, GameObject.Direction.Right, 5.0, 5, true);

        /// <summary>
        ///     Gets the lanes of the third level.
        /// </summary>
        /// <value>
        ///     The third level lanes.
        /// </value>
        public static IList<Lane> ThirdLevelLanes { get; } = new List<Lane>
            {ThirdLevelFirstLane, ThirdLevelSecondLane, ThirdLevelThirdLane, ThirdLevelFourthLane, ThirdLevelFifthLane};

        /// <summary>
        ///     Gets the levels.
        /// </summary>
        /// <value>
        ///     The levels.
        /// </value>
        public static IList<IList<Lane>> Levels { get; } = new List<IList<Lane>>
            {FirstLevelLanes, SecondLevelLanes, ThirdLevelLanes};

        #endregion
    }
}