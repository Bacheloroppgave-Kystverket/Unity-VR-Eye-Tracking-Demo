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
    /// Checks if the number is under zero. Throws exception if the number is less than zero.
    /// </summary>
    /// <param name="number">the number to check.</param>
    /// <param name="error">the error string</param>
    /// <exception cref="IllegalArgumentException">gets thrown when the number is less than zero</exception>
    private void CheckIfNumberIsValid(int number, string error)
    {
        if (number < 0)
        {
            throw new IllegalArgumentException(error + " cannot be below zero");
        }
    }
}
