namespace OpenTracker.Models.Nodes
{
    /// <summary>
    /// This interface contains requirement node data.
    /// </summary>
    public interface IOverworldNode : INode
    {
        int ExitsAccessible { get; set; }
        int DungeonExitsAccessible { get; set; }
        int InsanityExitsAccessible { get; set; }
        
        delegate IOverworldNode Factory();
    }
}