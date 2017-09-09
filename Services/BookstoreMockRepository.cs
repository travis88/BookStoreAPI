using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCorePublisherWebAPI.Models;
using AspNetCorePublisherWebAPI.Data;

namespace AspNetCorePublisherWebAPI.Services
{
    /// <summary>
    /// Реализация репозитория для данных прописанных в коде
    /// </summary>
    public class BookstoreMockRepository : IBookstoreRepository
    {
        /// <summary>
        /// Список издателей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PublisherDTO> GetPublishers()
        {
            return MockData.Current.Publishers;
        }

        /// <summary>
        /// Отдельный издатель
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeBooks"></param>
        /// <returns></returns>
        public PublisherDTO GetPublisher(int publisherId, bool includeBooks = false)
        {
            var publisher = MockData.Current.Publishers
                .FirstOrDefault(p => p.Id.Equals(publisherId));
            
            if (includeBooks && publisher != null)
            {
                publisher.Books = MockData.Current.Books
                    .Where(b => b.PublisherId.Equals(publisherId)).ToList();
            }

            return publisher;
        }

        /// <summary>
        /// Новый издатель
        /// </summary>
        /// <param name="publisher"></param>
        public void AddPublisher(PublisherDTO publisher) 
        {
            var id = GetPublishers().Max(m => m.Id) + 1;
            publisher.Id = id;
            MockData.Current.Publishers.Add(publisher);
        }

        /// <summary>
        /// Обновление издателя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publisher"></param>
        public void UpdatePublisher(int id, PublisherUpdateDTO publisher)
        {
            var publisherToUpdate = GetPublisher(id);
            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Established = publisher.Established;
        }

        /// <summary>
        /// Существует ли издатель
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns></returns>
        public bool PublisherExists(int publisherId)
        {
            return MockData.Current.Publishers
                .Count(p => p.Id.Equals(publisherId)).Equals(1);
        }

        /// <summary>
        /// Удаляем издателя
        /// </summary>
        /// <param name="publisher"></param>
        public void DeletePublisher(PublisherDTO publisher) 
        {
            foreach (var book in publisher.Books)
                DeleteBook(book);
            
            MockData.Current.Publishers.Remove(publisher);
        }

        /// <summary>
        /// Удаляем книгу
        /// </summary>
        /// <param name="book"></param>
        public void DeleteBook(BookDTO book)
        {
            MockData.Current.Books.Remove(book);
        }

        /// <summary>
        /// Сохраняем изменения
        /// </summary>
        /// <returns></returns>
        public bool Save() 
        {
            return true;
        }
    }
}