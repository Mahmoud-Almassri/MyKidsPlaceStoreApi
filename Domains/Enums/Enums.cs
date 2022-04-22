using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Enums
{

    public enum GlobalStatusEnum {
        Active = 1,
        InActive = 2,
        InCart = 3,
        NotifyIfAvailable = 4,
        InMyOrders = 5,
        InSale = 6,
        Delivered=7,
        Empty = 8
    }
    
    public enum GenderEnum
    {
        Male = 1, 
        Female = 2, 
        All = 3
    }
    public enum ErrorMessages
    {
        LoginPasswordWrong = 1,
        MobileNumberIsUsed = 2,
        ErrorCreatingAccount = 3,
        ErrorUpdatingAccount = 4,
        UserNotFound = 5,
        ItemCountNotEnough = 6,
        UserCartNotFound = 7,
        CartIsEmpty = 8,
        ItemNotAvailable = 9,
        ItemAlreadyExist = 10
    }

}
