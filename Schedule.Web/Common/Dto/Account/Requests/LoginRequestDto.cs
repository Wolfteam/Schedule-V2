﻿namespace Schedule.Web.Common.Dto.Account.Requests
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
