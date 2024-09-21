namespace Calculator.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData("1", "1")]
    [InlineData("-1", "-1")]
    public void Test1(string expr, string want)
    {
        Assert.Equal(want, Program.Do(expr));
    }
}