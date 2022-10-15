using AutoMapper;
using Commands.Notes.Create;
using Commands.Notes.Delete;
using Commands.Notes.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Net.Http.Headers;
using Note.API.Services.ViewMapper;
using Note.API.View.Base;
using Note.API.View.Notes;
using Note.API.View.Shared;
using NSwag.Annotations;
using Queries.Notes;

namespace Note.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(Roles ="User, Admin")]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IViewMapperService _viewMapper;
        public NoteController(IMediator mediator, IMapper mapper , IViewMapperService viewMapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _viewMapper = viewMapper;
        }

        [HttpGet]
        public async Task<ActionResult<ViewListModelDto<ViewNoteModelDto>>> GetAll([OpenApiIgnore]ODataQueryOptions<NoteModel> options)
        {

            var query = new FullNoteListQuery() {
                options = options
            };

            var result = await _mediator.Send(query); 

            return _viewMapper.ShowResult<ViewNoteModelDto, NoteModel>(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewNoteModelDto>> Get(ulong id)
        {
            var query = new FindNoteQuery() { Id = id };

            var result = await _mediator.Send(query);

            var viewResult = _mapper.Map<ViewNoteModelDto>(result);

            return _viewMapper.ShowResult<ViewNoteModelDto, NoteModel>(result);
        }

        [HttpPost]
        public async Task<ActionResult<DoneResult>> AddNote(CreateNoteDto note) 
        {
            var query = _mapper.Map<CreateNoteCommand>(note);

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<DoneResult>(data);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<DoneResult>> Patch(ulong id, UpdateNoteDto note)
        {
            
            var query = _mapper.Map<UpdateNoteCommand>(note);

            query.Id = id;

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<DoneResult>(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DoneResult>> Delete(ulong id) 
        {
            var query = new DeleteNoteCommand()
            {
                Id = id,
            };

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<DoneResult>(data);
        }
    }
}
