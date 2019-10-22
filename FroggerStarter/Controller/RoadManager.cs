using System.Collections.Generic;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages lanes and shoulders
    /// </summary>
    public class RoadManager
    {
        #region Data members

        private readonly double laneHeight;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly double bottomOffset;
        private IList<Lane> lanes;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets all vehicles.
        /// </summary>
        /// <value>
        ///     All vehicles.
        /// </value>
        public IList<Vehicle> AllVehicles { get; private set; }

        /// <summary>
        ///     The y coordinate of the top shoulder
        /// </summary>
        public double TopShoulderY { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <param name="laneHeight">Height of the lane.</param>
        /// <param name="bottomOffset">The bottom offset.</param>
        public RoadManager(double backgroundHeight, double backgroundWidth, double laneHeight, double bottomOffset)
        {
            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.laneHeight = laneHeight;
            this.bottomOffset = bottomOffset;
            this.establishLanes();
            this.getAllVehicles();
        }

        #endregion

        #region Methods

        private void establishLanes()
        {
            this.lanes = new List<Lane>();
            var bottomShoulderLocation = this.backgroundHeight - this.bottomOffset - this.laneHeight;

            var firstLaneLocation = bottomShoulderLocation - this.laneHeight;
            var firstLane = new Lane(2, Vehicle.VehicleType.Car, firstLaneLocation, Lane.Direction.Left,
                this.backgroundWidth, this.laneHeight, 1);
            this.lanes.Add(firstLane);

            var secondLaneLocation = firstLaneLocation - this.laneHeight;
            var secondLane = new Lane(3, Vehicle.VehicleType.SemiTruck, secondLaneLocation, Lane.Direction.Right,
                this.backgroundWidth, this.laneHeight, 2);
            this.lanes.Add(secondLane);

            var thirdLaneLocation = secondLaneLocation - this.laneHeight;
            var thirdLane = new Lane(3, Vehicle.VehicleType.Car, thirdLaneLocation, Lane.Direction.Left,
                this.backgroundWidth, this.laneHeight, 3);
            this.lanes.Add(thirdLane);

            var fourthLaneLocation = thirdLaneLocation - this.laneHeight;
            var fourthLane = new Lane(2, Vehicle.VehicleType.SemiTruck, fourthLaneLocation, Lane.Direction.Left,
                this.backgroundWidth, this.laneHeight, 4);
            this.lanes.Add(fourthLane);

            var fifthLaneLocation = fourthLaneLocation - this.laneHeight;
            var fifthLane = new Lane(3, Vehicle.VehicleType.Car, fifthLaneLocation, Lane.Direction.Right,
                this.backgroundWidth, this.laneHeight, 5);
            this.lanes.Add(fifthLane);

            this.TopShoulderY = fifthLaneLocation - this.laneHeight;
        }

        /// <summary>
        ///     Moves the vehicles in each lane.
        ///     Precondition: None
        ///     Postcondition: VehiclesInLane Speed == Speed@prev + Speed
        /// </summary>
        public void MoveVehiclesInEachLane()
        {
            foreach (var currLane in this.lanes)
            {
                currLane.MoveVehicles();
            }
        }

        /// <summary>
        ///     Resets the speed.
        ///     Precondition: None
        ///     Postcondition: Speed of each lane == initialSpeed
        /// </summary>
        public void ResetSpeed()
        {
            foreach (var currLane in this.lanes)
            {
                currLane.ResetSpeed();
            }
        }

        private void getAllVehicles()
        {
            this.AllVehicles = new List<Vehicle>();
            foreach (var currLane in this.lanes)
            {
                foreach (var vehicle in currLane.VehiclesInLane)
                {
                    this.AllVehicles.Add(vehicle);
                }
            }
        }

        #endregion
    }
}