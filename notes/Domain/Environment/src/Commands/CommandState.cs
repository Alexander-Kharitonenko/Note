using Notes.Environment.Base;
using Notes.Environment.SystemConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Notes.Environment.Commands
{
    public class CommandState : BaseResult<StatusCode>
    {
        public ulong ResurseId { get; set; } 

        public static CommandState Success(ulong id) => new() { State = StateType.finished, ResurseId = id, ErrorCode = null, ErrorMessage = null};
        public static CommandState Error(StatusCode errorCode, string errorMessage ="") => new() { ErrorCode = errorCode, ErrorMessage = errorMessage, State = StateType.error };

    }
}
