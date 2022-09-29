using Notes.Environment.Base;
using Notes.Environment.SystemConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Environment.Queries
{
    public class SelectState<TModel> : BaseResult<StatusCode> where TModel : class 
    {
        public IEnumerable<TModel> Data { get; set; }

        public int? Count { get; set; }

        public static SelectState<TModel> Success(IEnumerable<TModel> data, int? count = null) => new SelectState<TModel>() { Data = data, Count = count, State = StateType.finished};

        public static SelectState<TModel> Error(StatusCode errorCode, string errorMessage="") => new SelectState<TModel>() { ErrorCode = errorCode, ErrorMessage = errorMessage, State = StateType.error };
    }
}
