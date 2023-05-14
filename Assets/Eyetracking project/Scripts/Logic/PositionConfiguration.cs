using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the positon and its default "should be" configuration for all the categories.
/// </summary>
[Serializable]
public class PositionConfiguration
{
    [SerializeField, Tooltip("The category configurations of this position configuration")]
    private List<CategoryConfiguration> categoryConfigurations = new CategoryConfigurationFactory().MakeDefaultCategoryFeedback();

    /// <summary>
    /// Gets the category configurations.
    /// </summary>
    /// <returns>list with all of the category configurations</returns>
    public List<CategoryConfiguration> GetCategoryConfigurations() => categoryConfigurations;
}
