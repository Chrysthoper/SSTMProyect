﻿using DataEntityFramework.Models;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DataEntityFramework.Business
{
    public class Authenticate
    {
        public static User Authentication(string email)
        {
            using (var context = new DBContext())
            {
                return context.Users.FirstOrDefault(e => e.email == email);
            }
        }

        public static Boolean TokenExist(string email)
        {
            using (var context = new DBContext())
            {
                return context.Tokens.Any(e => e.email == email && !e.used);
            }
        }

        public static Boolean TokenExist(Guid token)
        {
            using (var context = new DBContext())
            {
                return context.Tokens.Any(e => e.token == token && !e.used);
            }
        }

        public static Token CreateToken(User user)
        {
            using (var context = new DBContext())
            {
                Token t = new Token();
                t.token = Guid.NewGuid();
                t.email = user.email;
                t.name = user.name;
                t.phone = user.phone;
                t.used = false;
                context.Tokens.Add(t);
                context.SaveChanges();
                return context.Tokens.FirstOrDefault(e => e.email == user.email);
            }
        }

        public static User CreateUser(Guid token)
        {
            using (var context = new DBContext())
            {
                var Token = context.Tokens.FirstOrDefault(x => x.token == token);
                Token.used = true;
                User user = new User();
                user.email = Token.email;
                user.name = Token.name;
                user.phone = Token.phone;
                context.Users.Add(user);
                context.SaveChanges();
                return context.Users.FirstOrDefault(e => e.email == user.email);
            }
        }

    }
}