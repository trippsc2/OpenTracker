using System;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    ///     This interface contains overworld node data.
    /// </summary>
    public interface IOverworldNode : INode
    {
        /// <summary>
        ///     A 32-bit signed integer representing the number of exits that provide access to this node when entrance
        ///         shuffle is All or higher.
        /// </summary>
        int AllExitsAccessible { get; set; }

        /// <summary>
        ///     A 32-bit signed integer representing the number of exits that provide access to this node when entrance
        ///         shuffle is Dungeon or higher.
        /// </summary>
        int DungeonExitsAccessible { get; set; }

        /// <summary>
        ///     A 32-bit signed integer representing the number of exits that provide access to this node when entrance
        ///         shuffle is Insanity or higher.
        /// </summary>
        int InsanityExitsAccessible { get; set; }

        /// <summary>
        ///     An event that indicates that the accessibility has changed and subscribing methods have been called.
        /// </summary>
        event EventHandler? ChangePropagated;
        
        /// <summary>
        ///     A factory for creating new overworld nodes.
        /// </summary>
        /// <returns>
        ///     A new overworld node.
        /// </returns>
        delegate IOverworldNode Factory();
    }
}