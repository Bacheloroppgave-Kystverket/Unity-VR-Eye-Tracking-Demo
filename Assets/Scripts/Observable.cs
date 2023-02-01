using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a observable object.
/// </summary>
/// <typeparam name="T">The type of observer</typeparam>
public interface Observable<T>
{
    /// <summary>
    /// Adds an observer.
    /// </summary>
    /// <param name="observer">the observer to add</param>
    public void AddObserver(T observer);

    /// <summary>
    /// Removes an observer from the object.
    /// </summary>
    /// <param name="observer">the obeserver to add</param>
    public void RemoveObserver(T observer);
}
