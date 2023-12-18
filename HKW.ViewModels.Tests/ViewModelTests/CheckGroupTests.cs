using CommunityToolkit.Mvvm.ComponentModel;
using HKW.HKWViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Tests.ViewModelTests;

[TestClass]
public class CheckGroupTests
{
    [TestMethod]
    public void ChangeLeader()
    {
        var group = new CheckGroup();
        var vms = Enumerable.Range(0, 10).Select(i => new CheckGroupViewModel()).ToArray();
        foreach (var vm in vms)
            group.CheckInfos.Add(vm);
        group.LeaderIsChecked = true;
        Assert.IsTrue(group.CheckInfos.All(i => i.IsChecked));
    }

    [TestMethod]
    public void ChangeLeader_CanCheckFalse()
    {
        var group = new CheckGroup();
        var vms = Enumerable.Range(0, 10).Select(i => new CheckGroupViewModel()).ToArray();
        foreach (var vm in vms)
            group.CheckInfos.Add(vm);
        vms[0].CanCheck = false;
        group.LeaderIsChecked = true;
        Assert.IsTrue(vms.Count(m => m.IsChecked) == vms.Length - 1);
        Assert.IsTrue(group.LeaderIsChecked is null);
    }

    [TestMethod]
    public void ChangeSubItem()
    {
        var group = new CheckGroup();
        var vms = Enumerable.Range(0, 10).Select(i => new CheckGroupViewModel()).ToArray();
        foreach (var vm in vms)
            group.CheckInfos.Add(vm);
        vms[0].IsChecked = true;
        Assert.IsTrue(group.LeaderIsChecked is null);
        foreach (var vm in vms)
            vm.IsChecked = true;
        Assert.IsTrue(group.LeaderIsChecked);
    }
}

public partial class CheckGroupViewModel : ObservableObject, ICheckInfo
{
    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private bool _isChecked;

    [ObservableProperty]
    private bool _canCheck = true;
}
