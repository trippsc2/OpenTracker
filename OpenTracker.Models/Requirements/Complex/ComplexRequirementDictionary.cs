using System;
using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Complex
{
    /// <summary>
    ///     This class contains the dictionary container for complex requirements.
    /// </summary>
    public class ComplexRequirementDictionary : LazyDictionary<ComplexRequirementType, IRequirement>,
        IComplexRequirementDictionary
    {
        private readonly Lazy<IComplexRequirementFactory> _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     The factory for creating complex requirements.
        /// </param>
        public ComplexRequirementDictionary(IComplexRequirementFactory.Factory factory)
            : base(new Dictionary<ComplexRequirementType, IRequirement>())
        {
            _factory = new Lazy<IComplexRequirementFactory>(() => factory());
        }

        protected override IRequirement Create(ComplexRequirementType key)
        {
            return _factory.Value.GetComplexRequirement(key);
        }
    }
}