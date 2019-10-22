using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles checking the player's position and moving the player
    /// </summary>
    public class PlayerMovementManager
    {
        #region Data members

        private readonly Frog player;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly double topShoulderY;
        private readonly double bottomOffset;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can move.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can move; otherwise, <c>false</c>.
        /// </value>
        public bool CanMove { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerMovementManager" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <param name="topShoulderY">The top shoulder y.</param>
        /// <param name="bottomOffset">The bottom offset.</param>
        public PlayerMovementManager(Frog player, double backgroundHeight, double backgroundWidth, double topShoulderY,
            double bottomOffset)
        {
            this.player = player;
            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.topShoulderY = topShoulderY;
            this.bottomOffset = bottomOffset;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: CanMove && player.X greater than 0
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            if (!this.playerAtLeftBoundary() && this.CanMove)
            {
                this.player.MoveLeft();
            }
        }

        private bool playerAtLeftBoundary()
        {
            return this.player.X <= 0;
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: CanMove && player.X + player.Width less than backgroundWidth
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            if (!this.playerAtRightBoundary() && this.CanMove)
            {
                this.player.MoveRight();
            }
        }

        private bool playerAtRightBoundary()
        {
            return this.player.X + this.player.Width >= this.backgroundWidth;
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: CanMove && player.Y less than topShoulderY
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (!this.playerAtTopBoundary() && this.CanMove)
            {
                this.player.MoveUp();
            }
        }

        private bool playerAtTopBoundary()
        {
            return this.player.Y <= this.topShoulderY;
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: CanMove && player.Y greater than bottomOffset
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (!this.playerAtBottomBoundary() && this.CanMove)
            {
                this.player.MoveDown();
            }
        }

        private bool playerAtBottomBoundary()
        {
            return this.player.Y + this.player.Height >= this.backgroundHeight - this.bottomOffset;
        }

        #endregion
    }
}