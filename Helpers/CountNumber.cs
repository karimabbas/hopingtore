using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShopingStore.Data;

namespace ShopingStore.Helpers
{
    public class CountNumber
    {
        private readonly MyDBContext _myDBContext;
        private readonly UserManager<IdentityUser> _userManager;

        public CountNumber(MyDBContext myDBContext, UserManager<IdentityUser> userManager)
        {
            _myDBContext = myDBContext;
            _userManager = userManager;
        }

        public int CountOfWish()
        {

            return _myDBContext.UserWishLists.Count();
        }
    }
}