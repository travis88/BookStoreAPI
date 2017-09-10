using System.Collections.Generic;
using AspNetCorePublisherWebAPI.Models;

namespace AspNetCorePublisherWebAPI.Services
{
    /// <summary>
    /// Интерфейс для получения моделей DTO (Data Transfer Objects)
    /// </summary>
    public interface IBookstoreRepository
    {
        /// <summary>
        /// Получаем список всех издателей
        /// </summary>
        /// <returns></returns>
        IEnumerable<PublisherDTO> GetPublishers();

        /// <summary>
        /// Получаем издателя *(книги)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeBooks"></param>
        /// <returns></returns>
        PublisherDTO GetPublisher(int publisherId, bool includeBooks = false);

        /// <summary>
        /// Добавление издателя
        /// </summary>
        /// <param name="publisher"></param>
        void AddPublisher(PublisherDTO publisher);

        /// <summary>
        /// Обновление издателя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publisher"></param>
        void UpdatePublisher(int id, PublisherUpdateDTO publisher);

        /// <summary>
        /// Существует ли издатель
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        bool PublisherExists(int publisherId);

        /// <summary>
        /// Удаляем издателя
        /// </summary>
        /// <param name="publisher"></param>
        void DeletePublisher(PublisherDTO publisher);

        /// <summary>
        /// Удаляем книгу
        /// </summary>
        /// <param name="book"></param>
        void DeleteBook(BookDTO book);

        /// <summary>
        /// Список книг по издателю
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        IEnumerable<BookDTO> GetBooks(int publisherId);

        /// <summary>
        /// Книга по издателю и идентификатору
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        BookDTO GetBook(int publisherId, int bookId);

        /// <summary>
        /// Добавление новой книги
        /// </summary>
        /// <param name="book"></param>
        void AddBook(BookDTO book);

        /// <summary>
        /// Обновление книги
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        void UpdateBook(int publisherId, int bookId, BookUpdateDTO book);

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}