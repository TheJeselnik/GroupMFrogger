using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Extensions
{
    /// <summary>
    ///     Defines the ListExtensions
    /// </summary>
    public static class ListExtensions
    {
        #region Methods

        /// <summary>
        ///     Converts to ObservableCollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>New ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        #endregion
    }
}