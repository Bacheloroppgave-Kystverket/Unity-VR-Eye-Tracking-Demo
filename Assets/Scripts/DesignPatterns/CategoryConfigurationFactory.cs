using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A factory that makes defualt category configurations for the developers and users.
/// </summary>
[Serializable]
public class CategoryConfigurationFactory
{
    /// <summary>
    /// Makes a default set of category configurations.
    /// </summary>
    /// <returns>all the default category configurations</returns>
    public List<CategoryConfiguration> MakeDefaultCategoryFeedback() {
        List<CategoryConfiguration> categoryConfigurations = new List<CategoryConfiguration>
        {
            MakeCategoryConfiguration(TrackableType.WALL, 0.1f),
            MakeCategoryConfiguration(TrackableType.WINDOW, 0.6f),
            MakeCategoryConfiguration(TrackableType.MIRROR, 0.2f),
            MakeCategoryConfiguration(TrackableType.OTHER, 0.1f)
        };
        return categoryConfigurations;
    }

    /// <summary>
    /// Makes a category configuration.
    /// </summary>
    /// <param name="trackableType">the trackable type</param>
    /// <param name="prosentage">the prosentage</param>
    /// <returns>the category configuration</returns>
    private CategoryConfiguration MakeCategoryConfiguration(TrackableType trackableType, float prosentage) {
        return new CategoryConfiguration(trackableType, prosentage);
    }
}
