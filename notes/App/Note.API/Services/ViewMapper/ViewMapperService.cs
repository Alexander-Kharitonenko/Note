using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Note.API.View.Base;
using Note.API.View.Notes;
using Note.API.View.Shared;
using Notes.Environment.Base;
using Notes.Environment.Commands;
using Notes.Environment.Queries;
using Notes.Environment.SystemConstants;
using Queries.Notes;
using System.Linq;

namespace Note.API.Services.ViewMapper
{
    public class ViewMapperService : IViewMapperService
    {
        private readonly IMapper _mapper;
        public ViewMapperService(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public ActionResult ShowError(BaseResult<StatusCode> result)
        {
            return new BadRequestObjectResult(new { errorCode = result.ErrorCode.ToString(), errorMessage = result.ErrorMessage , status = result.State.ToString() });
        }

        public ActionResult<TResult> ShowResult<TResult>(CommandState result) where TResult : class
        {
            if (result.State != StateType.error)
            {
                var viewData = new DoneResult() { resursId = result.ResurseId, touching = DateTime.UtcNow };

               return new OkObjectResult(viewData);
            }

            return ShowError(result);
        }

        public ActionResult<TResult> ShowResult<TResult, TModel>(QueryState<TModel> result) 
            where TModel : class
            where TResult : class
        {
            if (result.State != StateType.error)
            {
                var viewResult = _mapper.Map<TModel, TResult>(result.Data);

                if (viewResult != null) 
                {
                    return new OkObjectResult(viewResult);
                }
            }

            return ShowError(result);

        }

        public ActionResult<ViewListModelDto<TResult>> ShowResult<TResult, TModel>(SelectState<TModel> result) 
            where TResult : class
            where TModel : class
        {
            if(result.State != StateType.error) 
            {
                var resource = result.Data.Select(el => _mapper.Map<TModel, TResult>(el)).AsQueryable();
                var viewResult = new ViewListModelDto<TResult>() { data = resource , Count = result.Count };

                return new OkObjectResult(viewResult);
            }

            return ShowError(result);
        }
    }
}
