using AutoMapper;
using AspNetCorePublisherWebAPI.Entities;
using AspNetCorePublisherWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCorePublisherWebAPI.Services
{
    /// <summary>
    /// Реализация репозитория для БД
    /// </summary>
    public class BookstoreSqlRepository : IBookstoreRepository
    {
        /// <summary>
        /// Контекст соединения с БД
        /// </summary>
        private SqlDbContext _db;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="db"></param>
        public BookstoreSqlRepository(SqlDbContext db)
        {
            _db = db;
        }
        
        /// <summary>
        /// Добавляем книгу
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(BookDTO book)
        {
            var bookToAdd = Mapper.Map<Book>(book);
            _db.Books.Add(bookToAdd);
        }

        /// <summary>
        /// Добавляем издателя
        /// </summary>
        /// <param name="publisher"></param>
        public void AddPublisher(PublisherDTO publisher)
        {
            var publisherToAdd = Mapper.Map<Publisher>(publisher);
            _db.Publishers.Add(publisherToAdd);
        }

        /// <summary>
        /// Удаляем книгу
        /// </summary>
        /// <param name="book"></param>
        public void DeleteBook(BookDTO book)
        {
            var bookToDelete = _db.Books.FirstOrDefault(b =>
                b.Id.Equals(book.Id) && b.PublisherId.Equals(book.PublisherId));
            
            if (bookToDelete != null) _db.Books.Remove(bookToDelete);
        }

        /// <summary>
        /// Удаляем издателя
        /// </summary>
        /// <param name="publisher"></param>
        public void DeletePublisher(PublisherDTO publisher)
        {
            var publisherToDelete = _db.Publishers.FirstOrDefault(p =>
                p.Id.Equals(publisher.Id));
            
            if (publisherToDelete != null) _db.Publishers.Remove(publisherToDelete);
        }

        /// <summary>
        /// Получаем книгу
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public BookDTO GetBook(int publisherId, int bookId)
        {
            var book = _db.Books.FirstOrDefault(b => 
                b.Id.Equals(bookId) && b.PublisherId.Equals(publisherId));
            
            var bookDTO = Mapper.Map<BookDTO>(book);
            
            return bookDTO;
        }

        /// <summary>
        /// Получаем список книг
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        public IEnumerable<BookDTO> GetBooks(int publisherId)
        {
            var books = _db.Books.Where(b => 
                b.PublisherId.Equals(publisherId));
            
            var bookDTOs = Mapper.Map<IEnumerable<BookDTO>>(books);

            return bookDTOs;
        }

        /// <summary>
        /// Получаем издателя
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="includeBooks"></param>
        /// <returns></returns>
        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = _db.Publishers.FirstOrDefault(p =>
                p.Id.Equals(publisherId));
            
            if (includeBooks && publisher != null)
            {
                _db.Entry(publisher).Collection(c => c.Books).Load();
            }

            var publisherDTO = Mapper.Map<PublisherDTO>(publisher);

            return publisherDTO;
        }

        /// <summary>
        /// Получаем издателей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return Mapper.Map<IEnumerable<PublisherDTO>>(_db.Publishers);
        }

        /// <summary>
        /// Проверяем существование издателя
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        public bool PublisherExists(int publisherId)
        {
            return _db.Publishers.Count(p =>
                p.Id.Equals(publisherId)) == 1;
        }

        /// <summary>
        /// Сохраняем изменения
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        /// <summary>
        /// Обновляем книгу
        /// </summary>
        /// <param name="publisherId"></param>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        public void UpdateBook(int publisherId, int bookId, BookUpdateDTO book)
        {
            var bookToUpdate = _db.Books.FirstOrDefault(b => 
                b.Id.Equals(bookId) && b.PublisherId.Equals(publisherId));
            
            if (bookToUpdate == null) return;

            bookToUpdate.Title = book.Title;
            bookToUpdate.PublisherId = book.PublisherId;
        }

        /// <summary>
        /// Обновляем издателя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publisher"></param>
        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherToUpdate = _db.Publishers.FirstOrDefault(p => 
                p.Id.Equals(id));

            if (publisherToUpdate == null) return;

            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Established = publisher.Established;
        }
    }
}