using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.HKWViewModels.Tests.ViewModelTests;

[TestClass]
public class ViewModelBaseTests
{
    [TestMethod]
    public void PropertyChanging()
    {
        var newValue = 1;
        var trigger = false;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            Assert.IsTrue(s?.Equals(vm));
            Assert.IsTrue(e.PropertyName == nameof(ExampleViewModel.Value1));
            trigger = true;
        };
        vm.Value1 = newValue;
        Assert.IsTrue(vm.Value1 == newValue);
        Assert.IsTrue(trigger);
    }

    [TestMethod]
    public void PropertyChanged()
    {
        var newValue = 1;
        var trigger = false;
        var vm = new ExampleViewModel();
        vm.PropertyChanged += (s, e) =>
        {
            Assert.IsTrue(s?.Equals(vm));
            Assert.IsTrue(e.PropertyName == nameof(ExampleViewModel.Value1));
            trigger = true;
        };
        vm.Value1 = newValue;
        Assert.IsTrue(vm.Value1 == newValue);
        Assert.IsTrue(trigger);
    }

    [TestMethod]
    public void ValueChuanged()
    {
        var newValue = 1;
        var trigger = false;
        var vm = new ExampleViewModel();
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            Assert.IsTrue(e.PropertyName == nameof(ExampleViewModel.Value1));
            Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
            Assert.IsTrue(e.NewValue?.Equals(newValue));
            trigger = true;
        };
        vm.Value1 = newValue;
        Assert.IsTrue(vm.Value1 == newValue);
        Assert.IsTrue(trigger);
    }

    [TestMethod]
    public void ValueChuanged_Cancel()
    {
        var newValue = 1;
        var triggerPropertyChangingCount = 0;
        var triggerPropertyChangedCount = 0;
        var triggerValueChangedCount = 0;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            triggerPropertyChangingCount++;
        };
        vm.PropertyChanged += (s, e) =>
        {
            triggerPropertyChangedCount++;
        };
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            Assert.IsTrue(e.PropertyName == nameof(ExampleViewModel.Value1));
            Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
            Assert.IsTrue(e.NewValue?.Equals(newValue));
            triggerValueChangedCount++;
            e.Cancel = true;
        };
        vm.Value1 = newValue;
        Assert.IsTrue(triggerValueChangedCount == 1);
        Assert.IsTrue(triggerPropertyChangingCount == 2);
        Assert.IsTrue(triggerPropertyChangedCount == 2);
        Assert.IsTrue(vm.Value1 == ExampleViewModel.Value1Default);
    }

    [TestMethod]
    public void ValueChuanged_ChangeProperty()
    {
        var newValue1 = 1;
        var newValue2 = 2;
        var triggerPropertyChangingCount = 0;
        var triggerPropertyChangedCount = 0;
        var triggerValueChangedCount = 0;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            triggerPropertyChangingCount++;
        };
        vm.PropertyChanged += (s, e) =>
        {
            triggerPropertyChangedCount++;
        };
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            Assert.IsTrue(e.PropertyName == nameof(ExampleViewModel.Value1));
            Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
            Assert.IsTrue(e.NewValue?.Equals(newValue1));
            vm.Value1 = newValue2;
            triggerValueChangedCount++;
        };
        vm.Value1 = newValue1;
        Assert.IsTrue(triggerValueChangedCount == 1);
        Assert.IsTrue(triggerPropertyChangingCount == 2);
        Assert.IsTrue(triggerPropertyChangedCount == 2);
        Assert.IsTrue(vm.Value1 == newValue2);
    }

    [TestMethod]
    public void ValueChuanged_ChangeTwoProperty()
    {
        var newValue1 = 1;
        var newValue2 = 2;
        var triggerPropertyChangingCount = 0;
        var triggerPropertyChangedCount = 0;
        var triggerValueChangedCount = 0;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            triggerPropertyChangingCount++;
        };
        vm.PropertyChanged += (s, e) =>
        {
            triggerPropertyChangedCount++;
        };
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            if (e.PropertyName == nameof(ExampleViewModel.Value1))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                vm.Value1 = newValue2;
                vm.Value2 = newValue1;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value2))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value2Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
            }
            else
                Assert.Fail();
            triggerValueChangedCount++;
        };
        vm.Value1 = newValue1;
        Assert.IsTrue(triggerValueChangedCount == 2);
        Assert.IsTrue(triggerPropertyChangingCount == 3);
        Assert.IsTrue(triggerPropertyChangedCount == 3);
        Assert.IsTrue(vm.Value1 == newValue2);
        Assert.IsTrue(vm.Value2 == newValue1);
    }

    [TestMethod]
    public void ValueChuanged_ChangeTwoProperty_Value2Cancel()
    {
        var newValue1 = 1;
        var newValue2 = 2;
        var triggerPropertyChangingCount = 0;
        var triggerPropertyChangedCount = 0;
        var triggerValueChangedCount = 0;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            triggerPropertyChangingCount++;
        };
        vm.PropertyChanged += (s, e) =>
        {
            triggerPropertyChangedCount++;
        };
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            if (e.PropertyName == nameof(ExampleViewModel.Value1))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                vm.Value1 = newValue2;
                vm.Value2 = newValue1;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value2))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value2Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                e.Cancel = true;
            }
            else
                Assert.Fail();
            triggerValueChangedCount++;
        };
        vm.Value1 = newValue1;
        Assert.IsTrue(triggerValueChangedCount == 2);
        Assert.IsTrue(triggerPropertyChangingCount == 4);
        Assert.IsTrue(triggerPropertyChangedCount == 4);
        Assert.IsTrue(vm.Value1 == newValue2);
        Assert.IsTrue(vm.Value2 == ExampleViewModel.Value2Default);
    }

    [TestMethod]
    public void ValueChuanged_ChangeFourProperty()
    {
        var newValue1 = 1;
        var newValue2 = 2;
        var triggerPropertyChangingCount = 0;
        var triggerPropertyChangedCount = 0;
        var triggerValueChangedCount = 0;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            triggerPropertyChangingCount++;
        };
        vm.PropertyChanged += (s, e) =>
        {
            triggerPropertyChangedCount++;
        };
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            if (e.PropertyName == nameof(ExampleViewModel.Value1))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                vm.Value2 = newValue2;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value2))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value2Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue2));
                vm.Value3 = newValue1;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value3))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value3Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                vm.Value4 = newValue2;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value4))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value4Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue2));
            }
            else
                Assert.Fail();
            triggerValueChangedCount++;
        };
        vm.Value1 = newValue1;
        Assert.IsTrue(triggerValueChangedCount == 4);
        Assert.IsTrue(triggerPropertyChangingCount == 4);
        Assert.IsTrue(triggerPropertyChangedCount == 4);
        Assert.IsTrue(vm.Value1 == newValue1);
        Assert.IsTrue(vm.Value2 == newValue2);
        Assert.IsTrue(vm.Value3 == newValue1);
        Assert.IsTrue(vm.Value4 == newValue2);
    }

    [TestMethod]
    public void ValueChuanged_ChangeFourProperty_Value4Cancel()
    {
        var newValue1 = 1;
        var newValue2 = 2;
        var triggerPropertyChangingCount = 0;
        var triggerPropertyChangedCount = 0;
        var triggerValueChangedCount = 0;
        var vm = new ExampleViewModel();
        vm.PropertyChanging += (s, e) =>
        {
            triggerPropertyChangingCount++;
        };
        vm.PropertyChanged += (s, e) =>
        {
            triggerPropertyChangedCount++;
        };
        vm.ValueChanged += (s, e) =>
        {
            Assert.IsTrue(s.Equals(vm));
            if (e.PropertyName == nameof(ExampleViewModel.Value1))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value1Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                vm.Value2 = newValue2;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value2))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value2Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue2));
                vm.Value3 = newValue1;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value3))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value3Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue1));
                vm.Value4 = newValue2;
            }
            else if (e.PropertyName == nameof(ExampleViewModel.Value4))
            {
                Assert.IsTrue(e.OldValue?.Equals(ExampleViewModel.Value4Default));
                Assert.IsTrue(e.NewValue?.Equals(newValue2));
                e.Cancel = true;
            }
            else
                Assert.Fail();
            triggerValueChangedCount++;
        };
        vm.Value1 = newValue1;
        Assert.IsTrue(triggerValueChangedCount == 4);
        Assert.IsTrue(triggerPropertyChangingCount == 5);
        Assert.IsTrue(triggerPropertyChangedCount == 5);
        Assert.IsTrue(vm.Value1 == newValue1);
        Assert.IsTrue(vm.Value2 == newValue2);
        Assert.IsTrue(vm.Value3 == newValue1);
        Assert.IsTrue(vm.Value4 == ExampleViewModel.Value4Default);
    }
}
