
namespace FroggerStarter.Model
{
    /// <summary>
    /// Defines the base for Power Up Game Objects
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public abstract class PowerUp : GameObject
    {

        private bool canBeAddedToGame;

        //TODO not sure what Power Ups will need
        /// <summary>
        /// Consumes the power up.
        /// Precondition: none
        /// Postcondition: PowerUp effect is given to player
        /// </summary>
        public void CollectPowerUp()
        {

        }
    }
}
