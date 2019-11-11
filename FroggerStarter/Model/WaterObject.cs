namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the model for objects in water
    /// </summary>
    public abstract class WaterObject : GameObject
    {
        #region Types and Delegates

        /// <summary>
        ///     Enum types of Water Objects
        /// </summary>
        public enum WaterObjectType
        {
            /// <summary>
            ///     The raft
            /// </summary>
            Raft,

            /// <summary>
            ///     The log
            /// </summary>
            Log
        }

        #endregion

        #region Data members

        /// <summary>
        ///     Determines if the WaterObject can be landed on
        /// </summary>
        public bool CanLandOn;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaterObject" /> class.
        /// </summary>
        /// <param name="canLandOn">if set to <c>true</c> [can land on].</param>
        /// <param name="speed">The speed.</param>
        protected WaterObject(bool canLandOn, double speed)
        {
            this.CanLandOn = canLandOn;
            this.SetSpeed(speed, 0.0);
        }

        #endregion
    }
}