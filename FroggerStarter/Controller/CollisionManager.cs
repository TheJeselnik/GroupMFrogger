using System.Drawing;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Handles collision checking of Game Objects.
    /// </summary>
    public class CollisionManager
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
        public bool ObjectsCollide(GameObject firstGameObject, GameObject secondGameObject)
        {
            var firstGameObjectRect = constructRectangle(firstGameObject);
            var secondGameObjectRect = constructRectangle(secondGameObject);
            return firstGameObjectRect.IntersectsWith(secondGameObjectRect);
        }

        private static Rectangle constructRectangle(GameObject gameObject)
        {
            return new Rectangle((int) gameObject.X, (int) gameObject.Y, (int) gameObject.Width,
                (int) gameObject.Height);
        }

        #endregion
    }
}