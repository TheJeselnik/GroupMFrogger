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

        private Point location;

        private readonly int maxVehicles;
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
            this.maxVehicles = maxVehicles;
            this.VehiclesInLane = new List<Vehicle>();
        }

        #endregion

        public void PlaceVehicle()
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
            this.VehiclesInLane.Add(newVehicle);
        }

        private static double calculateVehicleYOffset(GameObject newVehicle)
        {
            var emptyVerticalSpace = GameSettings.LaneHeight - newVehicle.Height;
            return emptyVerticalSpace / 2;
        }
    }
}