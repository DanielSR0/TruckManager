namespace TruckManager.Services
{
    public static class ValidationMessages
    {
        public const string TruckNameMustBeUnique = "The truck's name must be unique";
        public const string TruckNameIsReadonly = "The truck's name cannot be changed.";
        public const string ManufacturingYearMustBeCurrent = "Manufacturing year must be the current year.";
        public const string ModelYearMustBeSameOrSubsequentOfManufacturing = "Model year must be the same or the subsequent year of manufacturing.";
    }
}