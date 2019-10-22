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

        #endregion

        #region Data members

        private readonly VehicleType vehicleType;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vehicle" /> class.
        /// </summary>
        /// <param name="vehicleType">the type of vehicle</param>
        public Vehicle(VehicleType vehicleType)
        {
            this.vehicleType = vehicleType;
            this.assignVehicleSprite();
        }

        #endregion

        #region Methods

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

        #endregion
    }
}