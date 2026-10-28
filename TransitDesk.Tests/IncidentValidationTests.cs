using System.ComponentModel.DataAnnotations;
using TransitDesk.Web.ViewModels;
using Xunit;

public class IncidentValidationTests
{
    [Fact]
    public void Incident_WithShortDescription_FailsValidation()
    {
        var model = new IncidentReportViewModel
        {
            VehicleSideNumber = 2072,
            LineNumber = "6",
            Description = "Za krótki opis",
            OccurredAt = DateTime.Now
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(IncidentReportViewModel.Description)));
    }
}