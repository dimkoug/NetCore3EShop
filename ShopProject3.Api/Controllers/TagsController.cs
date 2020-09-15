using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShopProject3.Domain.Helpers;
using ShopProject3.Domain.Interfaces;
using ShopProject3.Domain.Models;
using ShopProject3.Domain.ResourceParameters;

namespace ShopProject3.Api.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;
        private readonly IMapper _mapper;

        public TagsController(ITagsService tagsService, IMapper mapper)
        {
            _tagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet(Name = "GetTags")]
        [HttpHead]
        public async Task<IActionResult> onGet([FromQuery] TagsResourceParameters parameters)
        {
            var data = await _tagsService.GetAll(parameters);
            var previousLink = data.HasPrevious ? CreateTagsResourceUri(parameters, ResourceUriType.PreviousPage) : null;
            var nextLink = data.HasNext ? CreateTagsResourceUri(parameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = data.TotalCount,
                pageSize = data.PageSize,
                currentPage = data.CurrentPage,
                totalPages = data.TotalPages,
                previousLink,
                nextLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(data);
        }

        [HttpGet("{Id:int}", Name = "GetTag")]
        public async Task<IActionResult> onGet(int Id)
        {


            var data = await _tagsService.Get(Id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> onPost(Tags tag)
        {

            Tags created = await _tagsService.Add(tag);
            return CreatedAtRoute("GetTag", new { Id = created.Id }, created);
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> onPut(int Id, Tags tag)
        {

            var data = await _tagsService.Get(Id);
            if (data == null)
            {
                return NotFound();
            }

            Tags updated = await _tagsService.Update(tag);
            return CreatedAtRoute("GetTag", new { Id = updated.Id }, updated);
        }

        [HttpPatch("{Id:int}")]
        public async Task<IActionResult> onPatch(int Id, JsonPatchDocument<Tags> tag)
        {

            var data = await _tagsService.Get(Id);
            if (data == null)
            {
                return NotFound();
            }

            tag.ApplyTo(data);
            Tags updated = await _tagsService.Update(data);
            return CreatedAtRoute("GetTag", new { Id = updated.Id }, updated);
        }


        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> onDelete(int Id, Tags tag)
        {

            await _tagsService.Delete(Id);
            return NoContent();
        }

        private string CreateTagsResourceUri(TagsResourceParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetTags",
                        new
                        {
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize


                        }
                    );
                case ResourceUriType.NextPage:
                    return Url.Link("GetTags",
                        new
                        {
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize


                        }
                    );
                default:
                    return Url.Link("GetTags",
                        new
                        {
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize


                        }
                    );

            }
        }
    }
}
