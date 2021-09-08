using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using TruckManager.Infrastructure;

namespace TruckManager.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Truck : IEntity
    {
        public Guid Id { get; set; }

        [Required, Display(Name = "Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, Display(Name = "Model")]
        public TruckModel Model { get; set; }

        [Required, Display(Name = "Manufacturing year")]
        public int ManufacturingYear { get; set; }

        [Required, Display(Name = "Model year")]
        public int ModelYear { get; set; }
    }
}