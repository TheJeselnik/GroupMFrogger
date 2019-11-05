using System;
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

        /// <summary>
        ///     Gets or sets the <see cref="Vehicle" /> at the specified index.
        /// </summary>
        /// <value>
        ///     The <see cref="Vehicle" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Vehicle this[int index]
        {
            get => this.AllVehicles[index];
            set => this.AllVehicles.Insert(index, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        /// </summary>
        public RoadManager()
        {
            this.placeLanes();
            this.addVehicleToLanes();
            this.getAllVehicles();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.AllVehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Occurs when [vehicle added].
        /// </summary>
        public event EventHandler<Vehicle> VehicleAdded;

        /// <summary>
        ///     Occurs when [vehicle removed].
        /// </summary>
        public event EventHandler<Vehicle> VehicleRemoved;

        private void placeLanes()
        {
            this.AllVehicles = new List<Vehicle>();
            var currentY = GameSettings.RoadHeight - GameSettings.LaneHeight * 2 + GameSettings.RoadOffsetHeight;
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                currVehicleLane.Y = currentY;
                currentY -= GameSettings.LaneHeight;
                currVehicleLane.VehicleAdded += this.addNewVehicle;
                currVehicleLane.VehicleRemoved += this.removeVehicle;
            }
        }

        private void addNewVehicle(object sender, Vehicle vehicle)
        {
            this.AllVehicles.Add(vehicle);
            this.onVehicleAdded(vehicle);
        }

        private void removeVehicle(object sender, Vehicle vehicle)
        {
            this.AllVehicles.Remove(vehicle);
            this.onVehicleRemoved(vehicle);
        }

        /// <summary>
        ///     Moves the vehicles
        ///     Precondition: None
        ///     Postcondition: VehiclesInLane X == X@prev + Speed
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
        ///     Adds a vehicle to lanes.
        ///     Precondition: currVehicleLane.VehiclesInLane.Count lessThan currVehicleLane.MaxVehicles
        ///     Postcondition: New vehicle queued up in currVehicleLane
        /// </summary>
        public void CheckToAddVehicleToLanes()
        {
            if (this.ticks % GameSettings.TicksUntilSpawnCars == 0)
            {
                this.addVehicleToLanes();
                this.ticks = 0;
            }

            this.ticks++;
        }

        private void addVehicleToLanes()
        {
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                if (currVehicleLane.HasRoomForVehicles())
                {
                    currVehicleLane.AddVehicle();
                }
            }
        }

        /// <summary>
        ///     Resets the vehicles to one.
        ///     Precondition: currVehicleLane.VehiclesInLane.Count greaterThan 1
        ///     Postcondition: Vehicle in currVehicleLane queued for deletion
        /// </summary>
        public void ResetOneVehiclePerLane()
        {
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                currVehicleLane.RemoveAddedVehicles();
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

        private void getAllVehicles()
        {
            this.AllVehicles = new List<Vehicle>();
            foreach (var currLane in GameSettings.VehicleLanes)
            {
                var enumerator = currLane.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    this.AllVehicles.Add(enumerator.Current);
                }

                enumerator.Dispose();
            }
        }

        private void onVehicleAdded(Vehicle vehicle)
        {
            this.VehicleAdded?.Invoke(this, vehicle);
        }

        private void onVehicleRemoved(Vehicle vehicle)
        {
            this.VehicleRemoved?.Invoke(this, vehicle);
        }

        #endregion
    }
}