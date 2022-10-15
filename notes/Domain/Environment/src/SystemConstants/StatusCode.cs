using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Environment.SystemConstants
{
    public enum StatusCode
    {
        // 600-699
        DuplicationResul = 600,
        InvalidData = 601,
        NotAuthorized = 602,
        NotRegistered = 603,
        NotrefreshToken = 604,

        //700-799
        NoteNotFound = 700,
        NoteNotCreated = 701,
        NoteNotUpdated = 702,
        NoteNotDeleted = 703,

        //800-899
        UserNotFound = 800,
        UserNotCreated = 801,
        UserNotUpdated = 802,
        UserNotDeleted = 803
    }
}
