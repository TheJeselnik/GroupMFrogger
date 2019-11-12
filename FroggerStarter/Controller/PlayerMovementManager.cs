﻿using System.Collections.Generic;
using FroggerStarter.Model.DataObjects;
using FroggerStarter.Model.GameObjects;
using FroggerStarter.Utility;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles checking the player's position and moving the player
    /// </summary>
    public class PlayerMovementManager
    {
        #region Data members

        private readonly Frog player;
        private readonly IList<FrogHome> frogHomes;

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
        ///     Precondition: CanMove AND player.X greater than 0
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            if (!this.playerAtLeftBoundary() && this.CanMove)
            {
                this.player.RotateSprite(GameObject.Direction.Left);
                this.player.MoveLeft();
            }
            else
            {
                SoundEffects.PlayBoundarySound();
            }
        }

        private bool playerAtLeftBoundary()
        {
            return this.player.X <= GameSettings.LeftEdgeOfRoad;
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: CanMove AND player.X + player.Width less than backgroundWidth
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            if (!this.playerAtRightBoundary() && this.CanMove)
            {
                this.player.RotateSprite(GameObject.Direction.Right);
                this.player.MoveRight();
            }
            else
            {
                SoundEffects.PlayBoundarySound();
            }
        }

        private bool playerAtRightBoundary()
        {
            return this.player.X + this.player.Width >= GameSettings.RoadWidth;
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: CanMove AND player.Y less than topShoulderY
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (this.playerCanMoveUp() || this.playerBelowFrogHome())
            {
                this.player.RotateSprite(GameObject.Direction.Up);
                this.player.MoveUp();
            }
            else
            {
                SoundEffects.PlayBoundarySound();
            }
        }

        private bool playerAtTopBoundary()
        {
            return this.player.Y <= GameSettings.TopEdgeOfLanes;
        }

        private bool playerCanMoveUp()
        {
            return this.CanMove;
        }

        private bool playerBelowFrogHome()
        {
            var canMoveIntoFrogHome = false;
            foreach (var currFrogHome in this.frogHomes)
            {
                if (this.player.X.Equals(currFrogHome.X) && this.playerAtTopBoundary() && !currFrogHome.HasFrog &&
                    this.CanMove)
                {
                    canMoveIntoFrogHome = true;
                }
            }

            return canMoveIntoFrogHome;
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: CanMove AND player.Y greater than bottomOffset
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            if (!this.playerAtBottomBoundary() && this.CanMove)
            {
                this.player.RotateSprite(GameObject.Direction.Down);
                this.player.MoveDown();
            }
            else
            {
                SoundEffects.PlayBoundarySound();
            }
        }

        private bool playerAtBottomBoundary()
        {
            return this.player.Y + this.player.Height >= GameSettings.RoadHeight - GameSettings.RoadOffsetHeight;
        }

        #endregion
    }
}