using Microsoft.AspNetCore.Mvc;
using AspNetCorePublisherWebAPI.Services;
using AspNetCorePublisherWebAPI.Models;
using AspNetCorePublisherWebAPI.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCorePublisherWebAPI.Controllers
{
    [Route("api/genpublishers")]
    public class GenPublishersController : Controller
    {
        IGenericEFRepository _rep;

        public GenPublishersController(IGenericEFRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _rep.Get<Publisher>();

            var DTOs = Mapper.Map<IEnumerable<PublisherDTO>>(items);
            
            return Ok(DTOs);
        }

        [HttpGet("{publisherId}/books")]
        public IActionResult Get(int publisherId)
        {
            var items = _rep.Get<Book>()
                .Where(b => b.PublisherId.Equals(publisherId));
            
            var DTOs = Mapper.Map<IEnumerable<BookDTO>>(items);
            
            return Ok(DTOs);
        }

        [HttpGet("{id}", Name = "GetGenericPublisher")]
        public IActionResult Get(int id, bool includeRelatedEntities = false)
        {
            var item = _rep.Get<Publisher>(id, includeRelatedEntities);

            if (item == null) return NotFound();

            var DTO = Mapper.Map<PublisherDTO>(item);
            
            return Ok(DTO);
        }

        [HttpGet("{publisherId}/books/{id}", Name = "GetGenericBook")]
        public IActionResult Get(int publisherId, int id, bool includeRelatedEntities = false)
        {
            var item = _rep.Get<Book>(id, includeRelatedEntities);

            if (item == null || !item.PublisherId.Equals(publisherId))  
            {
                return NotFound();
            }

            var DTO = Mapper.Map<BookDTO>(item);

            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PublisherDTO DTO)
        {
            if (DTO == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var itemToCreate = Mapper.Map<Publisher>(DTO);

            _rep.Add(itemToCreate);

            if (!_rep.Save()) return StatusCode(500, 
                "A problem occured while handling your request.");
            
            var createdDTO = Mapper.Map<PublisherDTO>(itemToCreate);

            return CreatedAtRoute("GetGenericPublisher", new { id = createdDTO.Id }, createdDTO);
        }

        [HttpPost("{publisherId}/books")]
        public IActionResult Post(int publisherId, [FromBody]BookDTO DTO)
        {
            if (DTO == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var itemToCreate = Mapper.Map<Book>(DTO);
            
            itemToCreate.PublisherId = publisherId;

            _rep.Add(itemToCreate);

            if (!_rep.Save()) return StatusCode(500, 
                "A problem occured while handling your request.");

            var createdDTO = Mapper.Map<BookDTO>(itemToCreate);

            return CreatedAtRoute("GetGenericBook", 
                new { id = createdDTO.Id }, createdDTO);
        }
    }
}