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

    private bool visualizeKeysAndValues;

    /// <summary>
    /// Makes an instance of the hashmap visualizer
    /// </summary>
    /// <param name="visualizeKeysAndValues">true if the hashmap should show in editor. False otherwise</param>
    public HashmapVisualiser(bool visualizeKeysAndValues) {
        dictionary = new Dictionary<K, V>();
        this.visualizeKeysAndValues = visualizeKeysAndValues;
        if (visualizeKeysAndValues) {
            keys = new List<K>();
            values = new List<V>();
        }
    }

    /// <summary>
    /// Makes an instance of the hashmap visualizer
    /// </summary>
    /// <param name="dictionary">the dictionary</param>
    /// <param name="visualizeKeysAndValues">true if the keys and values should appear in editor. False otherwise</param>
    public HashmapVisualiser(Dictionary<K, V> dictionary, bool visualizeKeysAndValues) {
        this.dictionary = dictionary;
        if (visualizeKeysAndValues) {
            keys = dictionary.Keys.ToList();
            values = dictionary.Values.ToList();
        }
        this.visualizeKeysAndValues = visualizeKeysAndValues;
    }

    /// <summary>
    /// Adds a new key and value to the map.
    /// </summary>
    /// <param name="key">the new key</param>
    /// <param name="value">the new value</param>
    public void Add(K key, V value) {
        if (!CheckForKey(key)) { 
            dictionary.Add(key, value);
            if (visualizeKeysAndValues) {
                keys.Add(key);
                values.Add(value);
            }
        }
    }

    /// <summary>
    /// Checks if the map contains the key.
    /// </summary>
    /// <param name="key">the key to check for</param>
    /// <returns>true if the key is in the map. False otherwise.</returns>
    public bool CheckForKey(K key) {
        return dictionary.ContainsKey(key);
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
