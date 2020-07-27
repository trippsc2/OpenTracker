// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Singleton generic requires static member.", Scope = "member", Target = "~P:OpenTracker.Models.Utils.Singleton`1.Instance")]
[assembly: SuppressMessage("Design", "CA1010:Generic interface should also be implemented", Justification = "Interface is unnecessary for this class.", Scope = "type", Target = "~T:OpenTracker.Models.Utils.ObservableStack`1")]
[assembly: SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Singleton generic requires non-static and inheritable.", Scope = "type", Target = "~T:OpenTracker.Models.Utils.Singleton`1")]
[assembly: SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "WebSocketSharp accepts URI as a string.", Scope = "member", Target = "~P:OpenTracker.Models.AutoTracking.ISNESConnector.Uri")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "AutoTracker localization will be done in the future.", Scope = "member", Target = "~M:OpenTracker.Models.AutoTracking.USB2SNESConnector.AttachDevice(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "AutoTracker localization will be done in the future.", Scope = "member", Target = "~M:OpenTracker.Models.AutoTracking.USB2SNESConnector.AttachDeviceIfNeeded(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "AutoTracker localization will be done in the future.", Scope = "member", Target = "~M:OpenTracker.Models.AutoTracking.USB2SNESConnector.ConnectIfNeeded(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.LocationSaveData.Markings")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.LocationSaveData.Sections")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.BossPlacements")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Connections")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Items")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Locations")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collection must be deserialized and requires a public setter.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.PrizePlacements")]
