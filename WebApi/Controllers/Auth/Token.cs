﻿using System;

namespace WebApi.Controllers.Auth
{
    public class Token
    {
        public Token(string userName)
        {
            this.UserName = userName;
            this.TokenString = Guid.NewGuid().ToString();
            this.ExpiryDate = DateTime.Now.AddMinutes(300);
        }

        public string TokenString { get; }
        public string UserName { get; }
        public DateTime ExpiryDate { get; }
    }
}
