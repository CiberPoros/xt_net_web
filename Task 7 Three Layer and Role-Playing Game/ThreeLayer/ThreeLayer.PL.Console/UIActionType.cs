using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayer.PL.Console
{
    public enum UIActionType
    {
        NONE = 0,
        ADD_USER = 1,
        GET_ALL_USERS = 2,
        REMOVE_USER_BY_ID = 3,
        ADD_AWARD = 4,
        GET_ALL_AWARDS = 5,
        REMOVE_AWARD_BY_ID = 6,
        ADD_AWARD_TO_USER = 7,
        REMOVE_AWARD_FROM_USER = 8
    }
}
