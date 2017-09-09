using System.Collections.Generic;

namespace AspNetCorePublisherWebAPI.Models
{
    /// <summary>
    /// Модель, описывающая издателя
    /// </summary>
    public class PublisherDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// Имя издателя
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// Год создания
        /// </summary>
        /// <returns></returns>
        public int Established { get; set; }

        /// <summary>
        /// Кол-во книг
        /// </summary>
        /// <returns></returns>
        public int BookCount { get { return Books.Count; } }
        
        /// <summary>
        /// Список книг
        /// </summary>
        /// <returns></returns>
        public ICollection<BookDTO> Books { get; set; } =
            new List<BookDTO>();
    }
}