namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the enum type of USB2SNES Opcode types.
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
