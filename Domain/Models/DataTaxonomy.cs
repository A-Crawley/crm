using Microsoft.Extensions.Compliance.Classification;

namespace Domain.Models;

public static class DataTaxonomy
{
    public static string TaxonomyName => typeof(DataTaxonomy).FullName!;

    public static DataClassification SensitiveData => new(TaxonomyName, nameof(SensitiveData));
    public static DataClassification PersonalData => new(TaxonomyName, nameof(PersonalData));
}