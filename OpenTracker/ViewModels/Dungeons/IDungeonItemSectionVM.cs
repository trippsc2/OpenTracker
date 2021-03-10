using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.Dungeons
{
    public interface IDungeonItemSectionVM : IDungeonItemVM
    {
        delegate IDungeonItemSectionVM Factory(ISection section);
    }
}