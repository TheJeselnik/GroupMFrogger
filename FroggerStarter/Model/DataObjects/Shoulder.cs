using System.Collections;
using System.Collections.Generic;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Model.DataObjects
{
    /// <summary>
    ///     Defines a shoulder of the road, which may contain FrogHomes
    /// </summary>
    public class Shoulder : IEnumerable<FrogHome>
    {
        #region Data members

        private readonly double columnWidth;
        private double offsetX;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the frog homes.
        /// </summary>
        /// <value>
        ///     The frog homes.
        /// </value>
        public IList<FrogHome> FrogHomes { get; } = new List<FrogHome>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Shoulder" /> class.
        /// </summary>
        /// <param name="columnWidth">Width of the frog.</param>
        public Shoulder(double columnWidth)
        {
            this.columnWidth = columnWidth;
            this.calculateFrogHomePlacement();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<FrogHome> GetEnumerator()
        {
            return this.FrogHomes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Clears the homes.
        /// </summary>
        public void ClearHomes()
        {
            foreach (var currFrogHome in this.FrogHomes)
            {
                currFrogHome.RemoveFrog();
            }
        }

        private void calculateFrogHomePlacement()
        {
            var widthOfRoadSection = GameSettings.RoadWidth - this.columnWidth * 2;
            var numberOfColumns = widthOfRoadSection / this.columnWidth;

            for (var i = 0; i < numberOfColumns; i++)
            {
                this.offsetX += this.columnWidth;
                if (isOddNumberedLane(i))
                {
                    this.createFrogHome();
                }
            }
        }

        private void createFrogHome()
        {
            var frogHome = new FrogHome();
            this.FrogHomes.Add(frogHome);
            frogHome.X = this.offsetX;
            frogHome.Y = GameSettings.LaneHeight + GameSettings.RoadOffsetHeight;
        }

        private static bool isOddNumberedLane(int laneIndex)
        {
            return laneIndex % 2 == 1;
        }

        #endregion
    }
}