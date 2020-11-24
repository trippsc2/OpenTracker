// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "ID included in class for debugging purposes.", Scope = "member", Target = "~F:OpenTracker.Models.RequirementNodes.RequirementNode._id")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Generic Singleton class requires inheritability and to be non-static.", Scope = "member", Target = "~P:OpenTracker.Models.Utils.Singleton`1.Instance")]
[assembly: SuppressMessage("Design", "CA1010:Generic interface should also be implemented", Justification = "<Pending>", Scope = "type", Target = "~T:OpenTracker.Models.Utils.ObservableStack`1")]
[assembly: SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Generic Singleton class requires inheritability and to be non-static.", Scope = "type", Target = "~T:OpenTracker.Models.Utils.Singleton`1")]
[assembly: SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "WebSocketSharp requires URI in string format.", Scope = "member", Target = "~P:OpenTracker.Models.AutoTracking.ISNESConnector.Uri")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.LocationSaveData.Markings")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.LocationSaveData.Sections")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.BossPlacements")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Connections")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Items")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Locations")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Collections in save data classes must be writable to allow for deserialization.", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.PrizePlacements")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>", Scope = "member", Target = "~F:OpenTracker.Models.DungeonNodes.DungeonNode._id")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>", Scope = "member", Target = "~P:OpenTracker.Models.SaveLoad.SaveData.Dropdowns")]
