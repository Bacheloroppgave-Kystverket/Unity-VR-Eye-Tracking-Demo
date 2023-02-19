using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Visualizes the dictionary so that we can see it in the editor.
/// </summary>
/// <typeparam name="K">the keys</typeparam>
/// <typeparam name="V">the values</typeparam>
[Serializable]
public class HashmapVisualiser<K, V>
{
    [SerializeField, Tooltip("Keys of the data")]
    private List<K> keys;

    [SerializeField, Tooltip("Values of the data")]
    private List<V> values;

    private Dictionary<K, V> dictionary;

    /// <summary>
    /// Makes an instance of the hashmap visualizer
    /// </summary>
    /// <param name="dictionary">the dictionary</param>
    public HashmapVisualiser(Dictionary<K, V> dictionary) {
        this.dictionary = dictionary;
        keys = dictionary.Keys.ToList();
        values = dictionary.Values.ToList();
    }

    /// <summary>
    /// Gets the value that matches the key.
    /// </summary>
    /// <param name="key">the key</param>
    /// <returns>the value that has that key</returns>
    public V GetValue(K key) {
        return dictionary[key];
    }

    public IEnumerator<K> GetKeyIterator() {
        return dictionary.Keys.GetEnumerator();
    }
}
