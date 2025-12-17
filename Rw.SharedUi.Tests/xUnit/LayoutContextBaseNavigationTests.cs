using Rw.SharedUi.Contracts;
using Rw.SharedUi.Tests.TestData;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseNavigationTests
{
    [Fact]
    public void SetNavigation_Null_Throws()
    {
        var ctx = new TestLayoutContext();

        Assert.Throws<ArgumentNullException>(() => ctx.TestSetNavigation(null!));
    }

    [Fact]
    public void SetNavigation_TakesSnapshot_ModifyingSourceDoesNotAffectContext()
    {
        var ctx = new TestLayoutContext();

        var source = NavbarTestData.Create().ToList();
        ctx.TestSetNavigation(source);

        var beforeIds = ctx.NavbarItems.Select(x => x.Id).ToArray();

        // mutate original list
        source.Add(NavbarTestData.CreateSingle());
        source.Clear();

        var afterIds = ctx.NavbarItems.Select(x => x.Id).ToArray();

        Assert.Equal(beforeIds, afterIds);
    }

    [Fact]
    public void SetNavigation_StoresItemsInOrder()
    {
        var ctx = new TestLayoutContext();

        var items = NavbarTestData.Create().ToList();
        ctx.TestSetNavigation(items);

        var expectedIds = items.Select(x => x.Id).ToArray();
        var actualIds = ctx.NavbarItems.Select(x => x.Id).ToArray();

        Assert.Equal(expectedIds, actualIds);
    }

    [Fact]
    public void SetNavigation_ReplacesListInstance_EachCall()
    {
        var ctx = new TestLayoutContext();

        ctx.TestSetNavigation([NavbarTestData.CreateSingle()]);
        var first = ctx.NavbarItems;

        ctx.TestSetNavigation([NavbarTestData.CreateSingle()]);
        var second = ctx.NavbarItems;

        Assert.NotSame(first, second);
    }

    [Fact]
    public void SetNavigation_RaisesChanged_ExactlyOnce()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetNavigation([NavbarTestData.CreateSingle()]);

        Assert.Equal(1, changed);
    }

    [Fact]
    public void SetNavigation_AlwaysRaisesChanged_EvenIfSameSequenceIsPassedAgain()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        var source = NavbarTestData.Create().ToList();

        ctx.TestSetNavigation(source);
        ctx.TestSetNavigation(source);

        Assert.Equal(2, changed);
    }
    
    [Fact]
    public void SetNavigation_ExposesReadOnlySnapshot()
    {
        var ctx = new TestLayoutContext();

        var source = NavbarTestData.Create().ToList();
        ctx.TestSetNavigation(source);

        // Must not expose the same mutable list instance
        Assert.NotSame(source, ctx.NavbarItems);

        // Basic sanity: still read-only API
        Assert.IsAssignableFrom<IReadOnlyList<NavbarItem>>(ctx.NavbarItems);
        Assert.False(ctx.NavbarItems is List<NavbarItem>);
    }
}