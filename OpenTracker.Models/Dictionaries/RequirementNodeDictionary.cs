using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dictionaries
{
    /// <summary>
    /// This is the dictionary containing all requirement node data
    /// </summary>
    public class RequirementNodeDictionary : Dictionary<RequirementNodeID, RequirementNode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public RequirementNodeDictionary(Game game) : base()
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            foreach (RequirementNodeID iD in Enum.GetValues(typeof(RequirementNodeID)))
            {
                if (iD < RequirementNodeID.HCSanctuary)
                {
                    Add(iD, new RequirementNode(game, iD));
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Initializes all contained requirement nodes
        /// </summary>
        public void Initialize()
        {
            foreach (var node in Values)
            {
                node.Initialize();
            }
        }
    }
}
