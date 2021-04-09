using System.Collections.Generic;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.SequenceBreak
{
    /// <summary>
    ///     This class contains the dictionary container for sequence break requirements.
    /// </summary>
    public class SequenceBreakRequirementDictionary : LazyDictionary<SequenceBreakType, IRequirement>,
        ISequenceBreakRequirementDictionary
    {
        private readonly ISequenceBreakDictionary _sequenceBreaks;

        private readonly ISequenceBreakRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="sequenceBreaks">
        ///     The sequence break dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating sequence break requirements.
        /// </param>
        public SequenceBreakRequirementDictionary(
            ISequenceBreakDictionary sequenceBreaks, ISequenceBreakRequirement.Factory factory)
            : base(new Dictionary<SequenceBreakType, IRequirement>())
        {
            _sequenceBreaks = sequenceBreaks;
            _factory = factory;
        }

        protected override IRequirement Create(SequenceBreakType key)
        {
            return _factory(_sequenceBreaks[key]);
        }
    }
}