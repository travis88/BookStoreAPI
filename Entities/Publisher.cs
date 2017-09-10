using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCorePublisherWebAPI.Entities
{   
    /// <summary>
    /// Модель, описывающая издателя в БД
    /// </summary>
    public class Publisher
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <returns></returns>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        /// <returns></returns>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Год основания
        /// </summary>
        /// <returns></returns>
        public int Established { get; set; }

        /// <summary>
        /// Список книг
        /// </summary>
        /// <returns></returns>
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}