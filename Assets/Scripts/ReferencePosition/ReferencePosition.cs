using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReferencePosition
{
    [SerializeField, Tooltip("The id of the location")]
    private long locationId;

    [SerializeField, Tooltip("The id of the location")]
    private string locationName;

    [SerializeField, Tooltip("The feedback configuration of this position")]
    private PositionConfiguration positionConfiguration;
    
    /// <summary>
    /// Gets the location name.
    /// </summary>
    /// <returns>the location name</returns>
    public string GetLocationName() => locationName;

    /// <summary>
    /// Gets all the category configurations.
    /// </summary>
    /// <returns>a list with all the category configurations</returns>
    public List<CategoryConfiguration> GetCategoryConfigurationsForPosition() => positionConfiguration.GetCategoryConfigurations();

    /// <summary>
    /// Sets the location id to a new value.
    /// </summary>
    /// <param name="locationID">the location id</param>
    public void SetLocationId(long locationID) {
        CheckIfNumberIsAboveZero(locationID, "location id");
        this.locationId = locationID;
    }

    /// <summary>
    /// Sets the position configuration.
    /// </summary>
    /// <param name="positionConfiguration">the position configuration</param>
    public void SetPositionConfiguration(PositionConfiguration positionConfiguration) {
        CheckIfObjectIsNull(positionConfiguration, "position configuration");
        this.positionConfiguration = positionConfiguration;
    }

    /// <summary>
    /// Gets the position configuration.
    /// </summary>
    /// <returns>the position configuration</returns>
    public PositionConfiguration GetPositionConfiguration() => positionConfiguration;

    /// <summary>
    /// Gets the location id.
    /// </summary>
    /// <returns>the location id</returns>
    public long GetLocationId() {
        return locationId;
    }

    /// <summary>
    /// Checks if a number is above zero.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error prefix.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the number is negative.</exception>
    private void CheckIfNumberIsAboveZero(float number, string error) {
        if (number < 0) {
            throw new IllegalArgumentException("The " + error + " must be above zero.");
        }
    }

    /// <summary>
    /// Checks if the object is null or not. Throws an exception if the object is null.
    /// </summary>
    /// <param name="objecToCheck">the object to check</param>
    /// <param name="error">the error to be in the string.</param>
    /// <exception cref="IllegalArgumentException">gets thrown if the object to check is null.</exception>
    private void CheckIfObjectIsNull(object objecToCheck, string error) {
        if (objecToCheck == null) {
            throw new IllegalArgumentException("The " + error + " cannot be null.");
        }
    }
}
