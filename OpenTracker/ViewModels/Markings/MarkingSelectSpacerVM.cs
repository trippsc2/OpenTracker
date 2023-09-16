using OpenTracker.Autofac;

namespace OpenTracker.ViewModels.Markings;

/// <summary>
/// This class contains marking select UI spacer control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MarkingSelectSpacerVM : IMarkingSelectItemVMBase
{
    public delegate MarkingSelectSpacerVM Factory();
}