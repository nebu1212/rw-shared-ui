using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class DataThemeModeTests
{
    [Fact]
    public void ThemeMode_HasExpectedNumericValues()
    {
        Assert.Equal(0, (int)ThemeMode.System);
        Assert.Equal(1, (int)ThemeMode.Light);
        Assert.Equal(2, (int)ThemeMode.Dark);
    }
}