using Rw.SharedUi.Tests.TestData;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseProfileMenuTests
{
    [Fact]
    public void SetProfileMenu_Null_Throws()
    {
        var ctx = new TestLayoutContext();

        Assert.Throws<ArgumentNullException>(() => ctx.TestSetProfileMenu(null!));
    }

    [Fact]
    public void SetProfileMenu_TakesSnapshot_ModifyingSourceDoesNotAffectContext()
    {
        var ctx = new TestLayoutContext();

        var source = ProfileMenuTestData.Create().ToList();
        ctx.TestSetProfileMenu(source);

        var beforeIds = ctx.ProfileMenuItems.Select(x => x.Id).ToArray();

        source.Add(ProfileMenuTestData.CreateSingle());
        source.Clear();

        var afterIds = ctx.ProfileMenuItems.Select(x => x.Id).ToArray();

        Assert.Equal(beforeIds, afterIds);
    }

    [Fact]
    public void SetProfileMenu_StoresItemsInOrder()
    {
        var ctx = new TestLayoutContext();

        var items = ProfileMenuTestData.Create().ToList();
        ctx.TestSetProfileMenu(items);

        var expectedIds = items.Select(x => x.Id).ToArray();
        var actualIds = ctx.ProfileMenuItems.Select(x => x.Id).ToArray();

        Assert.Equal(expectedIds, actualIds);
    }

    [Fact]
    public void SetProfileMenu_ReplacesListInstance_EachCall()
    {
        var ctx = new TestLayoutContext();

        ctx.TestSetProfileMenu([ProfileMenuTestData.CreateSingle()]);
        var first = ctx.ProfileMenuItems;

        ctx.TestSetProfileMenu([ProfileMenuTestData.CreateSingle()]);
        var second = ctx.ProfileMenuItems;

        Assert.NotSame(first, second);
    }

    [Fact]
    public void SetProfileMenu_RaisesChanged_ExactlyOnce()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetProfileMenu([ProfileMenuTestData.CreateSingle()]);

        Assert.Equal(1, changed);
    }

    [Fact]
    public void SetProfileMenu_AlwaysRaisesChanged_EvenIfSameSequenceIsPassedAgain()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        var source = ProfileMenuTestData.Create().ToList();

        ctx.TestSetProfileMenu(source);
        ctx.TestSetProfileMenu(source);

        Assert.Equal(2, changed);
    }
    
    [Fact]
    public async Task OnProfileMenuItemClickedAsync_DefaultImplementation_IsNoOp_AndDoesNotRaiseChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        var task = ctx.OnProfileMenuItemClickedAsync(null!);

        // should be already completed (no async work in base)
        Assert.True(task.IsCompletedSuccessfully);

        await task;

        Assert.Equal(0, changed);
    }
}