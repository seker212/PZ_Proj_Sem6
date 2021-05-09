﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Helpers;

namespace DatabaseAPI.DatabaseModels
{
    public enum UserType
    {
        Admin,
        Manager
    }
    public class User
    {
        public Guid Id { get; }
        public string Username { get; }
        public string PasswordHash { get; set; }
        public UserType Type { get; set; }
    }
}