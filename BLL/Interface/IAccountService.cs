﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IAccountService
    {
         Task<string> Register(string username, string password);
         Task<string> Login(string username, string password);
    }
}
