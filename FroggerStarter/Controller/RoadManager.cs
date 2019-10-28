﻿using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages lanes and vehicles
    /// </summary>
    public class RoadManager : IEnumerable<Vehicle>
    {
        #region Data members

        private const double SpeedIncrease = 0.00005;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets all vehicles.
        /// </summary>
        /// <value>
        ///     All vehicles.
        /// </value>
        public IList<Vehicle> AllVehicles { get; private set; }

        /// <summary>
        ///     The y coordinate of the top shoulder
        /// </summary>
        public double TopShoulderY { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        /// </summary>
        public RoadManager()
        {
            placeLanes();
            this.getAllVehicles();
        }

        #endregion

        #region Methods

        private static void placeLanes()
        {
            var currentY = GameSettings.RoadHeight - GameSettings.LaneHeight * 2 + GameSettings.BottomOffsetHeight;
            foreach (var currVehicleLane in GameSettings.VehicleLanes)
            {
                currVehicleLane.Y = currentY;
                currentY -= GameSettings.LaneHeight;
                currVehicleLane.PlaceVehicle();
            }
        }

        /// <summary>
        ///     Moves the vehicles
        ///     Precondition: None
        ///     Postcondition: VehiclesInLane Speed == Speed@prev + Speed
        /// </summary>
        public void MoveVehicles()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                resetVehiclePastRightBoundary(currVehicle);
                resetVehicleIfPastLeftBoundary(currVehicle);
                currVehicle.Move();
            }
        }

        private static void resetVehicleIfPastLeftBoundary(GameObject vehicle)
        {
            if (vehicle.X + vehicle.Width < GameSettings.LeftEdgeOfRoad)
            {
                vehicle.X = GameSettings.RoadWidth;
            }
        }

        private static void resetVehiclePastRightBoundary(GameObject vehicle)
        {
            if (vehicle.X > GameSettings.RoadWidth)
            {
                vehicle.X = GameSettings.LeftEdgeOfRoad - vehicle.Width;
            }
        }

        /// <summary>
        ///     Increases the speed of all vehicles on the road.
        ///     Precondition: None
        ///     Postcondition: Speed of each vehicle = SpeedX@prev + SpeedIncrease
        /// </summary>
        private void increaseSpeed()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                if (currVehicle.SpeedX < 15)
                {
                    currVehicle.SetSpeed(currVehicle.SpeedX + SpeedIncrease, currVehicle.SpeedY);
                }
            }
        }

        /// <summary>
        ///     Resets the speed per each lane.
        ///     Precondition: None
        ///     Postcondition: Speed of each lane == initialSpeed
        /// </summary>
        public void ResetSpeed()
        {
            foreach (var currVehicle in this.AllVehicles)
            {
                currVehicle.ResetSpeedX();
            }
        }

        private void getAllVehicles()
        {
            this.AllVehicles = new List<Vehicle>();
            foreach (var currLane in GameSettings.VehicleLanes)
            {
                foreach (var currVehicle in currLane.VehiclesInLane)
                {
                    this.AllVehicles.Add(currVehicle);
                }
            }
        }

        #endregion

        //TODO incorporate IEnumerable methods with the collection of vehicles
        public IEnumerator<Vehicle> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}