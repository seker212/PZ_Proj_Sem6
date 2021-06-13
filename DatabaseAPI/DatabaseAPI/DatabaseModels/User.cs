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
    public class User : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "username", "password_hash", "user_type" };

        public User(Guid id, string username, string passwordHash, UserType type)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Type = type;
        }

        public Guid Id { get; }
        public string Username { get; }
        public string PasswordHash { get; set; }
        public UserType Type { get; set; }

        public object[] Data => new object[] { Id, Username, PasswordHash, EnumCaster.UserType.ToStr(Type) };

        public static string[] ColumnNames => _staticColumnNames;
    }
}
