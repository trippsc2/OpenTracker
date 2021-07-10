using System;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    /// This interface contains overworld node data.
    /// </summary>
    public interface IOverworldNode : INode
    {
        /// <summary>
        /// A <see cref="int"/> representing the number of accessible exits providing access to this node when entrance
        /// shuffle is Dungeon or higher.
        /// </summary>
        int DungeonExitsAccessible { get; set; }

        /// <summary>
        /// A <see cref="int"/> representing the number of accessible exits providing access to this node when entrance
        /// shuffle is All or higher.
        /// </summary>
        int AllExitsAccessible { get; set; }

        /// <summary>
        /// A <see cref="int"/> representing the number of accessible exits providing access to this node when entrance
        /// shuffle is Insanity.
        /// </summary>
        int InsanityExitsAccessible { get; set; }

        /// <summary>
        /// An event that indicates that the <see cref="Accessibility"/> property has changed and all subscribing
        /// methods have been executed.
        /// </summary>
        event EventHandler? ChangePropagated;
        
        /// <summary>
        /// A factory for creating new <see cref="IOverworldNode"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="IOverworldNode"/> object.
        /// </returns>
        delegate IOverworldNode Factory();
    }
}