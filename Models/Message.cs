namespace AspNetCorePublisherWebAPI.Models 
{
    /// <summary>
    /// Модель сообщения
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        /// <returns></returns>
        public string Text { get; set; }
    }
}