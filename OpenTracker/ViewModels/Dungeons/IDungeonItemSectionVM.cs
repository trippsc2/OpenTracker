using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.Dungeons
{
    public interface IDungeonItemSectionVM : IDungeonItemVM
    {
        new delegate IDungeonItemSectionVM Factory(ISection section);
    }
}