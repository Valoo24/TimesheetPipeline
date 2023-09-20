using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public string _csvFilePath
        {
            get
            {
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(AppPath, "UserDataBase.csv");
            }
        }

        public Guid Add(User entity)
        {
            using (StreamWriter writer = new StreamWriter(_csvFilePath, true, Encoding.UTF8))
            {
                writer.WriteLine($"{entity.Id},{entity.FirstName},{entity.LastName},{entity.MailAdress}");
            }

            return entity.Id;
        }

        public Guid Update(User entity)
        {
            IList<User> userList = GetAll().ToList();

            int index = userList.IndexOf(userList.FirstOrDefault(u => u.Id == entity.Id));

            userList.Insert(index, entity);

            userList.RemoveAt(index + 1);

            InitializeCSV();

            foreach (var user in userList)
            {
                Add(user);
            }

            return entity.Id;
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

        public void InitializeCSV()
        {
            using (StreamWriter writer = new StreamWriter(_csvFilePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Id,Name,Surname,Mail Adress");
            }
        }

        public Guid Delete(Guid id)
        {
            IList<User> userList = GetAll().ToList();

            userList.Remove(userList.FirstOrDefault(u => u.Id == id));

            InitializeCSV();

            foreach (var user in userList)
            {
                Add(user);
            }

            return id;
        }
    }
}