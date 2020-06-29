// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "WebSocketSharp library takes a string for the Uri instead of Uri class.", Scope = "member", Target = "~P:OpenTracker.Models.Interfaces.ISNESConnector.Uri")]
[assembly: SuppressMessage("Design", "CA1010:Generic interface should also be implemented", Justification = "ObservableStack does not need to implement ICollection.", Scope = "type", Target = "~T:OpenTracker.Models.Utils.ObservableStack`1")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Localization will occur at a later date.", Scope = "member", Target = "~M:OpenTracker.Models.SNESConnectors.USB2SNESConnector.AttachDevice(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Localization will occur at a later date.", Scope = "member", Target = "~M:OpenTracker.Models.SNESConnectors.USB2SNESConnector.AttachDeviceIfNeeded(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Localization will occur at a later date.", Scope = "member", Target = "~M:OpenTracker.Models.SNESConnectors.USB2SNESConnector.ConnectIfNeeded(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection is not read only to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveData.BossPlacements")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection is not read only to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveData.Connections")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection is not read only to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveData.ItemCounts")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection is not read only to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveData.LocationSectionCounts")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection is not read only to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveData.LocationSectionMarkings")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection is not read only to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveData.PrizePlacements")]
