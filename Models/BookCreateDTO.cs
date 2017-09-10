using System.ComponentModel.DataAnnotations;

namespace AspNetCorePublisherWebAPI.Models
{   
    /// <summary>
    /// Модель для добавления новой книги
    /// </summary>
    public class BookCreateDTO
    {
        /// <summary>
        /// Название
        /// </summary>
        /// <returns></returns>
        [Required(ErrorMessage = "Вы должны ввести название книги")]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор издателя
        /// </summary>
        /// <returns></returns>
        public int PublisherId { get; set; }
    }
}