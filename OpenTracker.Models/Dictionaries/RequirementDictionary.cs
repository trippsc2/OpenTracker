using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dictionaries
{
    /// <summary>
    /// This is the dictionary containing requirement data
    /// </summary>
    public class RequirementDictionary : Dictionary<RequirementType, Requirement>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public RequirementDictionary(Game game) : base()
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            foreach (RequirementType type in Enum.GetValues(typeof(RequirementType)))
            {
                Add(type, new Requirement(game, type));
            }
        }

        /// <summary>
        /// Initializes all contained requirements.
        /// </summary>
        public void Initialize()
        {
            foreach (var requirement in Values)
            {
                requirement.Initialize();
            }
        }
    }
}
