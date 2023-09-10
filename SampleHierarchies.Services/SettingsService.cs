﻿using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System.Text.Json;

namespace SampleHierarchies.Services;

/// <summary>
/// Settings service.
/// </summary>
public class SettingsService : ISettingsService
{
    #region ISettings Implementation

    /// <inheritdoc/>
    public ISettings? Read(string jsonPath)
    {
        ISettings? result = null;

        return result;
    }

    /// <inheritdoc/>
    public void Write(ISettings settings, string jsonPath)
    {
        
    }

    #endregion // ISettings Implementation
}