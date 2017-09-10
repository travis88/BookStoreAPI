using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebAPI.Models
{
    /// <summary>
    /// Модель для обновления книги
    /// </summary>
    public class BookUpdateDTO
    {
        /// <summary>
        /// Название
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "Вы должны ввести название")]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор издателя
        /// </summary>
        /// <returns></returns>
        public int PublisherId { get; set; }
    }
}