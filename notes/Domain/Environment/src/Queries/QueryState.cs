using Notes.Environment.Base;
using Notes.Environment.SystemConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Environment.Queries
{
    public class QueryState<TResult> : BaseResult<StatusCode> where TResult : class
    {
        public StateType State { get; set; }
        public TResult? Data { get; set; }

        public static QueryState<TResult> Success(TResult Data) => new() { Data = Data, State = StateType.finished, ErrorCode = null, ErrorMessage = null };

        public static QueryState<TResult> Error(StatusCode errorCode, string errorMessage ="") => new() { ErrorCode = errorCode, ErrorMessage = errorMessage, State = StateType.error };
    }
}
