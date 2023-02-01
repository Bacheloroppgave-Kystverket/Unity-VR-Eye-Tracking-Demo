using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class TrackableObject : MonoBehaviour, Observable<TrackableObserver>
{
    [SerializeField]
    private bool watched = false;

    [SerializeField]
    private bool changeColor = true;

    [SerializeField]
    private int fixations;

    [SerializeField]
    private float fixationDuration;

    [SerializeField]
    private float averageFixationTime;

    [SerializeField]
    private List<TrackableObserver> observers = new List<TrackableObserver>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (watched) {
            fixationDuration += Time.deltaTime;
            UpdateObserversFixationDuration();
        }
    }

    /// <summary>
    /// Gets the fixation duration.
    /// </summary>
    /// <returns>the fixation duration</returns>
    public float GetFixationDuration() {
        return fixationDuration;
    }

    /// <summary>
    /// Calculates the average fixation time and updates the stats.
    /// </summary>
    public void CalculateAverageFixationTime() {
        averageFixationTime = fixationDuration / fixations;
        UpdateObserversAverageFixationDuration();
    }

    public void ToggleIsWatched() {
        if (!watched) {
            fixations += 1;
            UpdateObserversFixations();
        }
        watched = !watched;
    }

    public bool IsWatched() {
        return watched;
    }

    public void FixedUpdate()
    {
        if (changeColor) {
            if (watched)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            }
        }
    }

    /// </inheritdoc>
    public void AddObserver(TrackableObserver observer)
    {
        observers.Add(observer);
    }

    /// </inheritdoc>
    public void RemoveObserver(TrackableObserver observer)
    {
        observers.Remove(observer);
    }

    /// <summary>
    /// Updates the fixations.
    /// </summary>
    private void UpdateObserversFixations()
    {
        observers.ForEach(observer => observer.UpdateFixations(fixations));
    }

    /// <summary>
    /// Updates the fixation durations.
    /// </summary>
    private void UpdateObserversFixationDuration()
    {
        observers.ForEach(observer => observer.UpdateFixationDuration(fixationDuration));
    }

    /// <summary>
    /// Updates the average fixation duration.
    /// </summary>
    private void UpdateObserversAverageFixationDuration()
    {
        observers.ForEach(observer => observer.UpdateAverageFixationDuration(averageFixationTime));
    }
}
