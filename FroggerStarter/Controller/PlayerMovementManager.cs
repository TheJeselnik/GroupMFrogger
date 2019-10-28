using System.Collections.Generic;
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
        private IList<FrogHome> frogHomes;

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
        /// Initializes a new instance of the <see cref="PlayerMovementManager" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="frogHomes">The frog homes.</param>
        public PlayerMovementManager(Frog player, IList<FrogHome> frogHomes)
        {
            this.player = player;
            this.frogHomes = frogHomes;
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
            return this.player.X <= GameSettings.LeftEdgeOfRoad;
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
            return this.player.X + this.player.Width >= GameSettings.RoadWidth;
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: CanMove && player.Y less than topShoulderY
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (this.playerCanMoveUp() || this.playerBelowFrogHome())
            {
                this.player.MoveUp();
            }
        }

        private bool playerAtTopBoundary()
        {
            return this.player.Y <= GameSettings.TopEdgeOfLanes;
        }

        private bool playerCanMoveUp()
        {
            return !this.playerAtTopBoundary() && this.CanMove;
        }

        private bool playerBelowFrogHome()
        {
            var canMoveIntoFrogHome = false;
            foreach (var currFrogHome in this.frogHomes)
            {
                if (this.player.X.Equals(currFrogHome.X) && this.playerAtTopBoundary() && !currFrogHome.HasFrog)
                {
                    canMoveIntoFrogHome = true;
                }
            }

            return canMoveIntoFrogHome;
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
            return this.player.Y + this.player.Height >= GameSettings.RoadWidth - GameSettings.RoadOffsetHeight;
        }

        #endregion
    }
}