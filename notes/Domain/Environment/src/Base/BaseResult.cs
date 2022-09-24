using Notes.Environment.SystemConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Environment.Base
{
    public abstract class BaseResult <T> where T : struct
    {
        public T? ErrorCode { get; set; }   
        public string? ErrorMessage { get; set; }

        public StateType? State { get; set; }

    }
}
