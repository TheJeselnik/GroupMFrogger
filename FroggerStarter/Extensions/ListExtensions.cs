﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Extensions
{
    /// <summary>Extension methods for lists.</summary>
    public static class ListExtensions
    {
        #region Methods

        /// <summary>
        ///     Converts to ObservableCollection.
        ///     Precondition: none
        ///     Postcondition: none
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>A new ObservableCollection from the given collection parameter.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        #endregion
    }
}