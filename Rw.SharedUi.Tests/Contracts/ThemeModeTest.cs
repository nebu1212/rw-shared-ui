using Rw.SharedUi.Contracts;
using Xunit;

namespace Rw.SharedUi.Tests.Contracts;

public class ThemeModeTest
{
    [Fact]
    public void ThemeMode_ShouldHaveExpectedValues()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(Enum.IsDefined(typeof(ThemeMode), ThemeMode.System));
        Assert.True(Enum.IsDefined(typeof(ThemeMode), ThemeMode.Light));
        Assert.True(Enum.IsDefined(typeof(ThemeMode), ThemeMode.Dark));
        
        Assert.Equal(0, (int)ThemeMode.System);
        Assert.Equal(1, (int)ThemeMode.Light);
        Assert.Equal(2, (int)ThemeMode.Dark);
        
    }
}
