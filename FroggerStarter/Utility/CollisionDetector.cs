using System.Drawing;
using FroggerStarter.Model.GameObjects;

namespace FroggerStarter.Utility
{
    /// <summary>
    ///     Handles collision checking of Game Objects.
    /// </summary>
    public class CollisionDetector
    {
        #region Methods

        /// <summary>
        ///     Check if passed objects collide
        ///     Precondition: None
        ///     Postcondition: None
        /// </summary>
        /// <param name="firstGameObject">The first game object.</param>
        /// <param name="secondGameObject">The second game object.</param>
        /// <returns>
        ///     True if both gameObjects collide, otherwise false.
        /// </returns>
        public bool IsCollisionBetween(GameObject firstGameObject, GameObject secondGameObject)
        {
            var firstGameObjectRect = constructRectangle(firstGameObject);
            var secondGameObjectRect = constructRectangle(secondGameObject);
            return firstGameObjectRect.IntersectsWith(secondGameObjectRect);
        }

        /// <summary>
        /// Determines whether [is collision between with cushion] [the specified first game object].
        /// Precondition: None
        /// Postcondition: None
        /// </summary>
        /// <param name="firstGameObject">The first game object.</param>
        /// <param name="secondGameObject">The second game object.</param>
        /// <param name="cushionRange">The cushion range.</param>
        /// <returns>
        ///   <c>true</c> if [is collision between with cushion] [the specified first game object]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCollisionBetweenWithCushion(GameObject firstGameObject, GameObject secondGameObject, double cushionRange)
        {
            var firstGameObjectRect = constructRectangleCushioned(firstGameObject, cushionRange);
            var secondGameObjectRect = constructRectangle(secondGameObject);
            var secondGameObjectRightX = secondGameObjectRect.X + secondGameObjectRect.Width;
            var secondGameObjectLeftPoint = new Point(secondGameObjectRect.X, secondGameObjectRect.Y);
            var secondGameObjectRightPoint = new Point(secondGameObjectRightX, secondGameObjectRect.Y);

            return firstGameObjectRect.IntersectsWith(secondGameObjectRect) || firstGameObjectRect.IntersectsWith(secondGameObjectRect);
        }

        private static Rectangle constructRectangle(GameObject gameObject)
        {
            return new Rectangle((int) gameObject.X, (int) gameObject.Y, (int) gameObject.Width,
                (int) gameObject.Height);
        }

        private static Rectangle constructRectangleCushioned(GameObject gameObject, double cushionRange)
        {
            cushionRange /= 2;
            var cushionedX = gameObject.X + cushionRange;
            var cushionedWidth = gameObject.Width - cushionRange;
            return new Rectangle((int)cushionedX, (int)gameObject.Y, (int)cushionedWidth,
                (int)gameObject.Height);
        }

        #endregion
    }
}