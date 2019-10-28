﻿using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Represents a horizontal lane.
    /// </summary>
    public class Lane
    {

        public delegate void VehicleAddedHandler(Vehicle vehicle);

        #region Data members

        private Point location;

        public readonly int MaxVehicles;
        private readonly double initialSpeed;
        private readonly Vehicle.Direction direction;
        private readonly Vehicle.VehicleType vehicleType;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the vehicles in lane.
        /// </summary>
        /// <value>
        ///     The vehicles in lane.
        /// </value>
        public IList<Vehicle> VehiclesInLane { get; }

        /// <summary>
        ///     Gets the new vehicle in lane.
        /// </summary>
        /// <value>
        ///     The new vehicle in lane.
        /// </value>
        public Vehicle NewVehicle { get; set; }

        /// <summary>
        ///     Gets or sets the y location of the lane.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public double Y
        {
            get => this.location.Y;
            set => this.location.Y = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane" /> class.
        /// </summary>
        public Lane(Vehicle.VehicleType vehicleType, Vehicle.Direction direction, double speed, int maxVehicles)
        {
            this.vehicleType = vehicleType;
            this.direction = direction;
            this.initialSpeed = speed;
            this.MaxVehicles = maxVehicles;
            this.VehiclesInLane = new List<Vehicle>();
        }

        #endregion

        public event VehicleAddedHandler VehicleAdded;

        // TODO queue up a lane to add a vehicle, to remove vehicles??
        /// <summary>
        /// Adds the vehicle.
        ///     Precondition: VehiclesInLane.Count lessThan MaxVehicles
        ///     Postcondition: New vehicle queued up
        /// </summary>
        public void AddVehicle()
        {
            if (this.vehiclesClearOfLeftEdge() && this.vehiclesClearOfRightEdge())
            {
                this.placeVehicle();
            }
        }

        /// <summary>
        /// Removes the vehicle.
        ///     Precondition: VehiclesInLane.Count greaterThan 1
        ///     Postcondition: Vehicle queued for deletion
        /// </summary>
        public void RemoveVehicle()
        {

        }

        private void placeVehicle()
        {
            var newVehicle = new Vehicle(this.vehicleType, this.direction, this.initialSpeed);
            var vehicleYOffset = calculateVehicleYOffset(newVehicle);
            switch (this.direction)
            {
                case Vehicle.Direction.Up:
                    break;
                case Vehicle.Direction.Down:
                    break;
                case Vehicle.Direction.Left:
                    newVehicle.X = GameSettings.RoadWidth;
                    newVehicle.Y = this.Y - vehicleYOffset;
                    break;
                case Vehicle.Direction.Right:
                    newVehicle.X = GameSettings.LeftEdgeOfRoad - newVehicle.Width;
                    newVehicle.Y = this.Y - vehicleYOffset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.NewVehicle = newVehicle;
            this.VehiclesInLane.Add(newVehicle);
            this.onVehicleAdded();
        }

        private static double calculateVehicleYOffset(GameObject newVehicle)
        {
            var emptyVerticalSpace = GameSettings.LaneHeight - newVehicle.Height;
            return emptyVerticalSpace / 2;
        }

        private bool vehiclesClearOfLeftEdge()
        {
            foreach (var currVehicle in this.VehiclesInLane)
            {
                if (currVehicle.X - 25.0 < GameSettings.LeftEdgeOfRoad)
                {
                    return false;
                }
            }

            return true;
        }

        private bool vehiclesClearOfRightEdge()
        {
            foreach (var currVehicle in this.VehiclesInLane)
            {
                if (currVehicle.X + currVehicle.Width + 25.0 > GameSettings.RoadWidth)
                {
                    return false;
                }
            }

            return true;
        }

        private void onVehicleAdded()
        {
            this.VehicleAdded?.Invoke(this.NewVehicle);
        }
    }
}