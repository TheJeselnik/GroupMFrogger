using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Represents a horizontal lane.
    /// </summary>
    public class Lane
    {
        #region Data members

        private double speed;
        private readonly double initialSpeed;

        private readonly Vehicle.Direction direction;
        private Point location;
        private readonly int numberOfVehicles;
        private readonly Vehicle.VehicleType vehicleType;
        private readonly double laneWidth;
        private readonly double laneHeight;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the vehicles in lane.
        /// </summary>
        /// <value>
        ///     The vehicles in lane.
        /// </value>
        public IList<Vehicle> VehiclesInLane { get; }

        public double Speed
        {
            get => this.speed;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.speed = value;
                this.setVehiclesSpeed();
            }
        }

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
        public Lane(int numberOfVehicles, Vehicle.VehicleType vehicleType, double yLocation, Vehicle.Direction direction,
            double laneWidth, double laneHeight, double speed)
        {
            this.numberOfVehicles = numberOfVehicles;
            this.vehicleType = vehicleType;
            this.Y = yLocation;
            this.direction = direction;
            this.laneWidth = laneWidth;
            this.laneHeight = laneHeight;
            this.VehiclesInLane = new List<Vehicle>();
            this.addVehiclesToLane();
            this.placeVehicles();

            this.Speed = speed;
            this.initialSpeed = speed;
        }

        #endregion

        #region Methods

        private void addVehiclesToLane()
        {
            for (var i = 0; i < this.numberOfVehicles; i++)
            {
                var newVehicle = new Vehicle(this.vehicleType, this.direction);
                this.VehiclesInLane.Add(newVehicle);
            }
        }

        private void setVehiclesSpeed()
        {
            foreach (var vehicle in this.VehiclesInLane)
            {
                vehicle.SetSpeed(this.Speed, 0);
            }
        }

        //TODO Refactor method, might need to adjust spacing per adding new vehicles. Especially trucks
        private void placeVehicles()
        {
            var emptyHorizontalLaneSpace = this.laneWidth - this.measureVehiclesWidth();
            var gapBetweenVehicles = emptyHorizontalLaneSpace / this.VehiclesInLane.Count;
            var vehicleOffset = 0.0;
            var firstCarPlaced = false;

            foreach (var currVehicle in this.VehiclesInLane)
            {
                if (!firstCarPlaced)
                {
                    currVehicle.X = gapBetweenVehicles / 2;
                    vehicleOffset += currVehicle.X;
                    firstCarPlaced = true;
                }
                else
                {
                    currVehicle.X = vehicleOffset;
                }

                var emptyVerticalLaneSpace = this.laneHeight - currVehicle.Height;
                var laneVerticalOffset = emptyVerticalLaneSpace / 2;
                currVehicle.Y = this.Y + laneVerticalOffset;
                vehicleOffset += gapBetweenVehicles + currVehicle.Width;
            }
        }

        private double measureVehiclesWidth()
        {
            var vehiclesWidth = 0.0;
            foreach (var currVehicle in this.VehiclesInLane)
            {
                vehiclesWidth += currVehicle.Width;
            }

            return vehiclesWidth;
        }

        private void increaseSpeed()
        {
            if (this.Speed < 15)
            {
                this.Speed += 0.005;
            }
        }

        /// <summary>
        ///     Resets the speed.
        ///     Precondition: None
        ///     Postcondition: Speed == initialSpeed
        /// </summary>
        public void ResetSpeed()
        {
            this.Speed = this.initialSpeed;
        }

        #endregion
    }
}