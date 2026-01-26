using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.DisplaysMapsCompasses;

/// <summary>
///     This class contains the dictionary container for display maps and compasses requirements.
/// </summary>
public class DisplayMapsCompassesRequirementDictionary : LazyDictionary<bool, IRequirement>, IDisplayMapsCompassesRequirementDictionary
{
    private readonly IDisplayMapsCompassesRequirement.Factory _factory;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new display maps and compasses requirements.
    /// </param>
    public DisplayMapsCompassesRequirementDictionary(IDisplayMapsCompassesRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}