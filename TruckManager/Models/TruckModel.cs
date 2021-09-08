using System.ComponentModel.DataAnnotations;

namespace TruckManager.Models
{
    public enum TruckModel
    {
        [Display(Name = "Frontal high")]
        FrontalHigh = 1,

        [Display(Name = "Frontal medium")]
        FrontalMedium = 2
    }
}