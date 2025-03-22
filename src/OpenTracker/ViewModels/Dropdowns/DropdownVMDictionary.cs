using System;
using System.Collections.Generic;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.ViewModels.Dropdowns
{
    public class DropdownVMDictionary : LazyDictionary<DropdownID, ILargeItemVM>, IDropdownVMDictionary
    {
        private readonly Lazy<IDropdownVMFactory> _factory;
        
        public DropdownVMDictionary(IDropdownVMFactory.Factory factory) :
            base(new Dictionary<DropdownID, ILargeItemVM>())
        {
            _factory = new Lazy<IDropdownVMFactory>(() => factory());
        }

        protected override ILargeItemVM Create(DropdownID key)
        {
            return _factory.Value.GetDropdownVM(key);
        }
    }
}