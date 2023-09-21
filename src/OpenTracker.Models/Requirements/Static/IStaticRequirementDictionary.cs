using System.Collections.Generic;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements.Static;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="StaticRequirement"/>
/// objects indexed by <see cref="AccessibilityLevel"/>.
/// </summary>
public interface IStaticRequirementDictionary : IDictionary<AccessibilityLevel, IRequirement>
{
}