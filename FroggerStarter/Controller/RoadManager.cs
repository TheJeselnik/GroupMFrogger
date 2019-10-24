using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages lanes and vehicles
    /// </summary>
    public class RoadManager : IEnumerable<Vehicle>
    {
        #region Data members

        private const int NumberOfLanes = 5;
        private const double LeftBoundary = 0.0;
        private const double SpeedIncrease = 0.005;

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

        //TODO Adjust the way Lanes are defined. Settings needed.
        private void establishLanes()
        {
            this.lanes = new List<Lane>();
            var bottomShoulderLocation = this.backgroundHeight - this.bottomOffset - this.laneHeight;

            var firstLaneLocation = bottomShoulderLocation - this.laneHeight;
            var firstLane = new Lane(2, Vehicle.VehicleType.Car, firstLaneLocation, Vehicle.Direction.Left,
                this.backgroundWidth, this.laneHeight, 1);
            this.lanes.Add(firstLane);

            var secondLaneLocation = firstLaneLocation - this.laneHeight;
            var secondLane = new Lane(3, Vehicle.VehicleType.SemiTruck, secondLaneLocation, Vehicle.Direction.Right,
                this.backgroundWidth, this.laneHeight, 2);
            this.lanes.Add(secondLane);

            var thirdLaneLocation = secondLaneLocation - this.laneHeight;
            var thirdLane = new Lane(3, Vehicle.VehicleType.Car, thirdLaneLocation, Vehicle.Direction.Left,
                this.backgroundWidth, this.laneHeight, 3);
            this.lanes.Add(thirdLane);

            var fourthLaneLocation = thirdLaneLocation - this.laneHeight;
            var fourthLane = new Lane(2, Vehicle.VehicleType.SemiTruck, fourthLaneLocation, Vehicle.Direction.Left,
                this.backgroundWidth, this.laneHeight, 4);
            this.lanes.Add(fourthLane);

            var fifthLaneLocation = fourthLaneLocation - this.laneHeight;
            var fifthLane = new Lane(3, Vehicle.VehicleType.Car, fifthLaneLocation, Vehicle.Direction.Right,
                this.backgroundWidth, this.laneHeight, 5);
            this.lanes.Add(fifthLane);

            this.TopShoulderY = fifthLaneLocation - this.laneHeight;
        }

        //TODO Helper method for above. Need to calculate locations then place all according to settings, like amount of lanes
        private void calculateLaneLocations()
        {
            var bottomShoulderLocation = this.backgroundHeight - this.bottomOffset - this.laneHeight;
            for (var i = 0; i < NumberOfLanes; i++)
            {
                
            }
        }

        /// <summary>
        ///     Moves the vehicles
        ///     Precondition: None
        ///     Postcondition: VehiclesInLane Speed == Speed@prev + Speed
        /// </summary>
        public void MoveVehicles()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                this.resetVehiclePastRightBoundary(currVehicle);
                this.resetVehicleIfPastLeftBoundary(currVehicle);
                currVehicle.Move();
            }
        }

        private void resetVehicleIfPastLeftBoundary(GameObject vehicle)
        {
            if (vehicle.X + vehicle.Width < LeftBoundary)
            {
                vehicle.X = this.backgroundWidth;
            }
        }

        private void resetVehiclePastRightBoundary(GameObject vehicle)
        {
            if (vehicle.X > this.backgroundWidth)
            {
                vehicle.X = LeftBoundary - vehicle.Width;
            }
        }

        /// <summary>
        ///     Increases the speed of all vehicles on the road.
        ///     Precondition: None
        ///     Postcondition: Speed of each vehicle = SpeedX@prev + SpeedIncrease
        /// </summary>
        public void IncreaseSpeed()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                if (currVehicle.SpeedX < 15)
                {
                    currVehicle.SetSpeed(currVehicle.SpeedX + SpeedIncrease, currVehicle.SpeedY);
                }
            }
        }

        /// <summary>
        ///     Resets the speed per each lane.
        ///     Precondition: None
        ///     Postcondition: Speed of each lane == initialSpeed
        /// </summary>
        public void ResetSpeed()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                currVehicle.ResetSpeedX();
            }
        }

        private void getAllVehicles()
        {
            this.AllVehicles = new List<Vehicle>();
            foreach (var currLane in this.lanes)
            {
                foreach (var currVehicle in currLane.VehiclesInLane)
                {
                    this.AllVehicles.Add(currVehicle);
                }
            }
        }

        #endregion

        //TODO incorporate IEnumerable methods with the collection of vehicles
        public IEnumerator<Vehicle> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}