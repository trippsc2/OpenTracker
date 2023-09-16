using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.SequenceBreaks;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="ISequenceBreak"/>
/// objects indexed by <see cref="SequenceBreakType"/>.
/// </summary>
public class SequenceBreakDictionary : LazyDictionary<SequenceBreakType, ISequenceBreak>,
    ISequenceBreakDictionary
{
    private readonly ISequenceBreak.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     A factory for creating new <see cref="ISequenceBreak"/> objects.
    /// </param>
    public SequenceBreakDictionary(ISequenceBreak.Factory factory)
        : base(new Dictionary<SequenceBreakType, ISequenceBreak>())
    {
        _factory = factory;
    }

    public Dictionary<SequenceBreakType, SequenceBreakSaveData> Save()
    {
        return Keys.ToDictionary(type => type, type => this[type].Save());
    }

    public void Load(Dictionary<SequenceBreakType, SequenceBreakSaveData>? saveData)
    {
        if (saveData == null)
        {
            return;
        }

        foreach (var type in saveData.Keys)
        {
            this[type].Load(saveData[type]);
        }
    }

    protected override ISequenceBreak Create(SequenceBreakType key)
    {
        return _factory();
    }
}