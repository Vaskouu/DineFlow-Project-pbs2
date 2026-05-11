using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DineFlow.Application.DTOs.Establishments

{

    /// <summary>
    /// DTO for creating a new establishment.
    /// </summary>
    public class CreateEstablishmentDto
    {
        /// <summary>
        /// Establishment name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}