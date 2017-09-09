namespace AspNetCorePublisherWebAPI.Models
{
    /// <summary>
    /// Модель, описывающая книгу
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор издателя
        /// </summary>
        /// <returns></returns>
        public int PublisherId { get; set; }
    }
}