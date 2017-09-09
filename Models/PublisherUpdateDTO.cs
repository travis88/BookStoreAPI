using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebAPI.Models
{
    /// <summary>
    /// Модель для обновления издателя
    /// </summary>
    public class PublisherUpdateDTO
    {
        /// <summary>
        /// Название издателя
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "Вы должны ввести название")]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Год основания издетельства
        /// </summary>
        /// <returns></returns>
        public int Established { get; set; }
    }
}