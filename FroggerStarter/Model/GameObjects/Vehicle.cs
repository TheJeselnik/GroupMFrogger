﻿namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the vehicle model
    /// </summary>
    public abstract class Vehicle : GameObject
    {
        #region Types and Delegates

        /// <summary>
        ///     Enum Types of vehicles
        /// </summary>
        public enum VehicleType
        {
            /// <summary>
            ///     The car
            /// </summary>
            Car,

            /// <summary>
            ///     The semi truck
            /// </summary>
            SemiTruck,

            /// <summary>
            ///     The oil semi truck
            /// </summary>
            OilSemiTruck
        }

        #endregion

        #region Data members

        private readonly Direction vehicleDirection;

        private readonly double initialSpeedX;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vehicle" /> class.
        /// </summary>
        /// <param name="direction">direction the vehicle is facing</param>
        /// <param name="initialSpeed">initial speed of vehicle</param>
        protected Vehicle(Direction direction, double initialSpeed)
        {
            this.vehicleDirection = direction;
            this.initialSpeedX = initialSpeed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the object the direction it is facing.
        ///     Precondition: None
        ///     Postcondition: Object.X == X@prev + Speed || Object.Y == Y@prev + Speed
        /// </summary>
        public void Move()
        {
            switch (this.vehicleDirection)
            {
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                default:
                    MoveLeft();
                    break;
            }
        }

        /// <summary>
        ///     Resets the speed x.
        ///     Precondition: None
        ///     Postcondition: Vehicle.X == initialSpeedX AND Vehicle.Y == Y@prev
        /// </summary>
        public void ResetSpeedX()
        {
            SetSpeed(this.initialSpeedX, SpeedY);
        }

        #endregion
    }
}