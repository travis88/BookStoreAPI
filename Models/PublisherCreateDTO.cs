using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebAPI.Models
{
    /// <summary>
    /// Модель для создания нового издателя
    /// </summary>
    public class PublisherCreateDTO
    {
        /// <summary>
        /// Название издателя
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "Вы должны ввести название")]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Год основания
        /// </summary>
        /// <returns></returns>
        public int Established { get; set; }
    }
}