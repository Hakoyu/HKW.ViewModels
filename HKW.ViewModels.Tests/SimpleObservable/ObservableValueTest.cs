using HKW.HKWViewModels.SimpleObservable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.ViewModels.Tests.SimpleObservable;

[TestClass]
public class ObservableValueTest
{
    [TestMethod]
    public void ValueChanging()
    {
        var trigger = false;
        var ov = new ObservableValue<int>(114);
        ov.ValueChanging += (int o, int n, ref bool c) =>
        {
            trigger = true;
            Assert.IsTrue(o == 114);
            Assert.IsTrue(n == 514);
            c = false;
        };
        ov.Value = 514;
        Assert.IsTrue(trigger);
        Assert.IsTrue(ov.Value == 514);
    }

    [TestMethod]
    public void ValueChanging_Cancel()
    {
        var trigger = false;
        var ov = new ObservableValue<int>(114);
        ov.ValueChanging += (int o, int n, ref bool c) =>
        {
            trigger = true;
            Assert.IsTrue(o == 114);
            Assert.IsTrue(n == 514);
            // cancel
            c = true;
        };
        ov.Value = 514;
        Assert.IsTrue(trigger);
        Assert.IsTrue(ov.Value == 114);
    }

    [TestMethod]
    public void ValueChanged()
    {
        var trigger = false;
        var ov = new ObservableValue<int>(114);
        ov.ValueChanged += (o, n) =>
        {
            trigger = true;
            Assert.IsTrue(o == 114);
            Assert.IsTrue(n == 514);
        };
        ov.Value = 514;
        Assert.IsTrue(trigger);
        Assert.IsTrue(ov.Value == 514);
    }

    [TestMethod]
    public void PropertyChanging()
    {
        var trigger = false;
        var ov = new ObservableValue<int>(114);
        ov.PropertyChanging += (s, e) =>
        {
            trigger = true;
            Assert.IsTrue(s?.Equals(ov));
            Assert.IsTrue(e.PropertyName == nameof(ov.Value));
        };
        ov.Value = 514;
        Assert.IsTrue(trigger);
        Assert.IsTrue(ov.Value == 514);
    }

    [TestMethod]
    public void PropertyChanged()
    {
        var trigger = false;
        var ov = new ObservableValue<int>(114);
        ov.PropertyChanged += (s, e) =>
        {
            trigger = true;
            Assert.IsTrue(s?.Equals(ov));
            Assert.IsTrue(e.PropertyName == nameof(ov.Value));
        };
        ov.Value = 514;
        Assert.IsTrue(trigger);
        Assert.IsTrue(ov.Value == 514);
    }
}
