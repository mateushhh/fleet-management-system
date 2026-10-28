using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TransitDesk.Web.Routing.Constraints;
using Xunit;

public class FleetSideNumberConstraintTests
{
    [Theory]
    [InlineData("1000", true)]
    [InlineData("2072", true)]
    [InlineData("9999", true)]
    [InlineData("999", false)]
    [InlineData("10000", false)]
    [InlineData("abc", false)]
    [InlineData(null, false)]
    public void Constraint_ValidatesCorrectly(string? input, bool expectedResult)
    {
        var constraint = new FleetSideNumberConstraint();
        var values = new RouteValueDictionary { { "sideNumber", input } };

        var result = constraint.Match(null, null, "sideNumber", values, RouteDirection.IncomingRequest);

        Assert.Equal(expectedResult, result);
    }
}