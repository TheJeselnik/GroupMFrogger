using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Represents a horizontal lane.
    /// </summary>
    public class Lane
    {
        #region Types and Delegates

        /// <summary>
        ///     Matches the directions of vehicles in a lane
        /// </summary>
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        #endregion

        #region Data members

        private const double LaneLeftBoundary = 0.0;

        private double speed;
        private readonly double initialSpeed;

        private Point location;
        private readonly int numberOfVehicles;
        private readonly Vehicle.VehicleType vehicleType;
        private readonly Direction direction;
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
        public Lane(int numberOfVehicles, Vehicle.VehicleType vehicleType, double yLocation, Direction direction,
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
                var newVehicle = new Vehicle(this.vehicleType);
                this.VehiclesInLane.Add(newVehicle);
                this.rotateVehiclesIfMovingRight(newVehicle);
            }
        }

        private void rotateVehiclesIfMovingRight(GameObject newVehicle)
        {
            if (this.direction != Direction.Right)
            {
                return;
            }

            newVehicle.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
            newVehicle.Sprite.RenderTransform = new ScaleTransform {ScaleX = -1};
        }

        private void setVehiclesSpeed()
        {
            foreach (var vehicle in this.VehiclesInLane)
            {
                vehicle.SetSpeed(this.Speed, 0);
            }
        }

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

        /// <summary>
        ///     Moves the vehicles.
        ///     Precondition: None
        ///     Postcondition: X == X@prev + Speed
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void MoveVehicles()
        {
            foreach (var currVehicle in this.VehiclesInLane)
            {
                switch (this.direction)
                {
                    case Direction.Right:
                        if (currVehicle.X > this.laneWidth)
                        {
                            currVehicle.X = LaneLeftBoundary - currVehicle.Width;
                        }

                        currVehicle.MoveRight();
                        break;
                    case Direction.Left:
                        if (currVehicle.X + currVehicle.Width < LaneLeftBoundary)
                        {
                            currVehicle.X = this.laneWidth;
                        }

                        currVehicle.MoveLeft();
                        break;
                    case Direction.Up:
                        break;
                    case Direction.Down:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            this.increaseSpeed();
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