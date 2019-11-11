﻿using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the log water object
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.WaterObject" />
    public class Log : WaterObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Log" /> class.
        /// </summary>
        /// <param name="canLandOn">if set to <c>true</c> [can land on].</param>
        /// <param name="speed">The speed.</param>
        public Log(bool canLandOn, double speed) : base(canLandOn, speed)
        {
            Sprite = new LogSprite();
        }

        #endregion
    }
}