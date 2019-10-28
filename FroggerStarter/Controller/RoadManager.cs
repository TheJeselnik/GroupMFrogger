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

        private int ticks;
        private const double SpeedIncrease = 0.00005;

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
        public RoadManager()
        {
            placeLanes();
            this.addVehicleToLanes();
            this.getAllVehicles();
        }

        public Vehicle this[int index]
        {
            get => this.AllVehicles[index];
            set => this.AllVehicles.Insert(index, value);
        }

        #endregion

        #region Methods

        private static void placeLanes()
        {
            var currentY = GameSettings.RoadHeight - GameSettings.LaneHeight * 2 + GameSettings.RoadOffsetHeight;
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                currVehicleLane.Y = currentY;
                currentY -= GameSettings.LaneHeight;
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
                resetVehiclePastRightBoundary(currVehicle);
                resetVehicleIfPastLeftBoundary(currVehicle);
                currVehicle.Move();
            }
        }

        /// <summary>
        /// Adds a vehicle to lanes.
        ///     Precondition: currVehicleLane.VehiclesInLane.Count lessThan currVehicleLane.MaxVehicles
        ///     Postcondition: New vehicle queued up in currVehicleLane
        /// </summary>
        public void CheckToAddVehicleToLanes()
        {
            if (this.ticks % 4000 == 0)
            {
                this.addVehicleToLanes();
            }

            this.ticks++;
        }

        public void addVehicleToLanes()
        {
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                if (currVehicleLane.VehiclesInLane.Count < currVehicleLane.MaxVehicles)
                {
                    currVehicleLane.AddVehicle();
                }
            }
        }

        /// <summary>
        /// Resets the vehicles to one.
        ///     Precondition: currVehicleLane.VehiclesInLane.Count greaterThan 1
        ///     Postcondition: Vehicle in currVehicleLane queued for deletion
        /// </summary>
        public void ResetOneVehiclePerLane()
        {
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                
            }
        }

        private static void resetVehicleIfPastLeftBoundary(GameObject vehicle)
        {
            if (vehicle.X + vehicle.Width < GameSettings.LeftEdgeOfRoad)
            {
                vehicle.X = GameSettings.RoadWidth;
            }
        }

        private static void resetVehiclePastRightBoundary(GameObject vehicle)
        {
            if (vehicle.X > GameSettings.RoadWidth)
            {
                vehicle.X = GameSettings.LeftEdgeOfRoad - vehicle.Width;
            }
        }

        /// <summary>
        ///     Increases the speed of all vehicles on the road.
        ///     Precondition: None
        ///     Postcondition: Speed of each vehicle = SpeedX@prev + SpeedIncrease
        /// </summary>
        private void increaseSpeed()
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
            foreach (var currLane in GameSettings.VehicleLanes)
            {
                foreach (var currVehicle in currLane.VehiclesInLane)
                {
                    this.AllVehicles.Add(currVehicle);
                }
            }
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.AllVehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}