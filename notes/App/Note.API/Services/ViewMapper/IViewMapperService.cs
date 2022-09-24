using Microsoft.AspNetCore.Mvc;
using Note.API.View.Base;
using Note.API.View.Shared;
using Notes.Environment.Base;
using Notes.Environment.Commands;
using Notes.Environment.Queries;
using Notes.Environment.SystemConstants;
using Queries.Notes;

namespace Note.API.Services.ViewMapper
{
    public interface IViewMapperService
    {
        public ActionResult ShowResult(CommandState result);

        public ActionResult<TResult> ShowResult<TResult, TModel>(QueryState<TModel> result) 
            where TResult : class
            where TModel : class;

        public ActionResult<ViewListModelDto<TResult>> ShowResult<TResult, TModel>(SelectState<TModel> result)
            where TResult : class
            where TModel : class;

        public ActionResult ShowError(BaseResult<StatusCode> result);
    }
}
