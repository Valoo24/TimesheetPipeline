﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Persistence.Repositories
{
    public class UserRepository : IReaderRepository<User, Guid>
    {
        public string _csvFilePath
        {
            get
            {
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(AppPath, "UserDataBase.csv");
            }
        }

        public IEnumerable<User> GetAll()
        {
            IList<User> UserList = new List<User>();

            using (StreamReader reader = new StreamReader(_csvFilePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string userLine = reader.ReadLine();
                    string[] userValues = userLine.Split(',');

                    Guid UserId;
                    Guid.TryParse(userValues[0], out UserId);

                    User user = new User
                    { 
                        Id = UserId,
                        FirstName = userValues[1],
                        LastName = userValues[2],
                        MailAdress = userValues[3]
                    };

                    UserList.Add(user);
                }
            }
            return UserList;
        }

        public User GetById(Guid id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }
    }
}