using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCorePublisherWebAPI.Entities
{
    /// <summary>
    /// Модель, описывающая книгу в БД
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <returns></returns>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        /// <returns></returns>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор издателя
        /// </summary>
        /// <returns></returns>
        [ForeignKey("PublisherId")]
        public int PublisherId { get; set; }

        /// <summary>
        /// Издатель
        /// </summary>
        /// <returns></returns>
        public Publisher Publisher { get; set; }
    }
}