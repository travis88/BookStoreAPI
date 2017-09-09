using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AspNetCorePublisherWebAPI.Services;
using AspNetCorePublisherWebAPI.Models;

namespace AspNetCorePublisherWebAPI.Controllers
{
    [Route("api/publishers")]
    public class PublishersController : Controller
    {
        IBookstoreRepository _rep;

        public PublishersController(IBookstoreRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_rep.GetPublishers());
        }

        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id, bool includeBooks = false) 
        {
            var publisher = _rep.GetPublisher(id, includeBooks);

            if (publisher == null) return NotFound();

            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PublisherCreateDTO publisher)
        {
            if (publisher == null) return BadRequest();

            if (publisher.Established < 1534 || publisher.Established > DateTime.Now.Year)
                ModelState.AddModelError("Established", "Первое издательское агенство было основано в 1534 г.");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var publisherToAdd = new PublisherDTO
            {
                Established = publisher.Established,
                Name = publisher.Name
            };

            _rep.AddPublisher(publisherToAdd);
            _rep.Save();

            return CreatedAtAction("GetPublisher", 
                new { id = publisherToAdd.Id }, publisherToAdd);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PublisherUpdateDTO publisher)
        {
            if (publisher == null) return BadRequest();

            if (publisher.Established < 1534 || publisher.Established > DateTime.Now.Year)
                ModelState.AddModelError("Established", "Первое издательское агенство было основано в 1534 г.");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var publisherExists = _rep.PublisherExists(id);
            if (!publisherExists) return NotFound();

            _rep.UpdatePublisher(id, publisher);
            _rep.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, 
            [FromBody]JsonPatchDocument<PublisherUpdateDTO> publisher)
        {
            if (publisher == null) return BadRequest();

            var publisherToUpdate = _rep.GetPublisher(id);
            if (publisherToUpdate == null) return NotFound();

            var publisherPatch = new PublisherUpdateDTO
            {
                Name = publisherToUpdate.Name,
                Established = publisherToUpdate.Established
            };

            publisher.ApplyTo(publisherPatch, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (publisherPatch.Established < 1534 || publisherPatch.Established > DateTime.Now.Year)
                ModelState.AddModelError("Established", "Первое издательское агенство было основано в 1534 г.");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _rep.UpdatePublisher(id, publisherPatch);
            _rep.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var publisherToDelete = _rep.GetPublisher(id);
            if (publisherToDelete == null) return NotFound();

            _rep.DeletePublisher(publisherToDelete);
            _rep.Save();

            return NoContent();
        }
    }
}