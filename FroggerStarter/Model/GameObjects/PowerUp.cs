namespace FroggerStarter.Model.GameObjects
{
    /// <summary>
    ///     Defines the base for Power Up Game Objects
    /// </summary>
    /// <seealso cref="GameObject" />
    public abstract class PowerUp : GameObject
    {
        #region Data members

        #endregion

        #region Methods

        //TODO not sure what Power Ups will need
        /// <summary>
        ///     Consumes the power up.
        ///     Precondition: none
        ///     Postcondition: PowerUp effect is given to player
        /// </summary>
        public void CollectPowerUp()
        {
        }

        #endregion
    }
}