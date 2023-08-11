using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKW.HKWViewModels.Controls.Interfaces;

namespace HKW.HKWViewModels.Controls;

public partial class RadioItemVM : SelectableItemVM, IRadioItemVM
{
    [ObservableProperty]
    public string? _groupName;
}
