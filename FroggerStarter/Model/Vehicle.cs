using System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the vehicle model
    /// </summary>
    public class Vehicle : GameObject
    {
        #region Types and Delegates

        /// <summary>
        ///     Enum Types of vehicles
        /// </summary>
        public enum VehicleType
        {
            Car,
            SemiTruck
        }

        /// <summary>
        ///     Enum Types of four directions a vehicle can be facing and move
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

        private readonly VehicleType vehicleType;
        private readonly Direction direction;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vehicle" /> class.
        /// </summary>
        /// <param name="vehicleType">the type of vehicle</param>
        public Vehicle(VehicleType vehicleType, Direction direction)
        {
            this.vehicleType = vehicleType;
            this.direction = direction;
            this.assignVehicleSprite();
            this.rotateSprite();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Moves the vehicle the direction it is facing.
        ///     Precondition: None
        ///     Postcondition: Vehicle.X == X@prev + Speed || Vehicle.Y == Y@prev + Speed
        /// </summary>
        public void Move()
        {
            switch (this.direction)
            {
                case Direction.Left:
                    this.MoveLeft();
                    break;
                case Direction.Right:
                    this.MoveRight();
                    break;
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                default:
                    this.MoveLeft();
                    break;
            }
        }

        private void assignVehicleSprite()
        {
            switch (this.vehicleType)
            {
                case VehicleType.Car:
                    Sprite = new CarSprite();
                    break;
                case VehicleType.SemiTruck:
                    Sprite = new SemiTruckSprite();
                    break;
                default:
                    Sprite = new CarSprite();
                    break;
            }
        }
        private void rotateSprite()
        {
            switch (this.direction)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    this.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.Sprite.RenderTransform = new ScaleTransform { ScaleX = -1 };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}