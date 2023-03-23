using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PositionConfiguration
{
    [SerializeField, Tooltip("The id of the position configuration")]
    private long positionConfigId;

    [SerializeField, Tooltip("The category configurations of this position configuration")]
    private List<CategoryConfiguration> categoryConfigurations = new CategoryConfigurationFactory().MakeDefaultCategoryFeedback();

    /// <summary>
    /// Gets the category configurations.
    /// </summary>
    /// <returns>list with all of the category configurations</returns>
    public List<CategoryConfiguration> GetCategoryConfigurations() => categoryConfigurations;
}
