namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This enum type contains USB2SNES opcode data.
    /// </summary>
    public enum OpcodeType
    {
        DeviceList,
        Attach,
        AppVersion,
        Name,
        Info,
        Boot,
        Menu,
        Reset,
        Stream,
        Fence,
        GetAddress,
        PutAddress,
        PutIPS,
        GetFile,
        PutFile,
        List,
        Remove,
        Rename,
        MakeDir,
    }
}
