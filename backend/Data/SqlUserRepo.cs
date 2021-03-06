using System;
using System.Linq;
using UrlShortener.Models;
using UrlShortener.Data;
using System.Collections.Generic;

namespace UrlShortener.Data
{
    public class SqlUserRepo : IUserRepo
    {
        private readonly UserContext _context;
        //constructor dependancy injection
        public SqlUserRepo(UserContext context)
        {
         _context=context;

        }

        public void DeleteUsers()
        {
                for(int i=0;i<_context.User.ToList().Count;i++){
                            _context.User.Remove(_context.User.ToList().ElementAt(i));

                }


        }

        public User GetUserByUsername(string username)
        {
            
                return _context.User.FirstOrDefault(p=>p.Username==username);

            // var user=_context.User.FirstOrDefault(p=>p.Username==username);
        
            // if(user!=null){
            //         Console.WriteLine(username);
            //     return null;
            // }
            //         Console.WriteLine("not null");

            // return user;
        }

        public bool IsExists(string username)
        { 


            var user=_context.User.FirstOrDefault(p=>p.Username==username);
            if(user!=null){

                return true;
            }

            return false;

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

    public void CreateUser(User user)
        {

            if(user==null){
            throw new ArgumentNullException(nameof(user));

            }
            _context.Add(user);
        }

       public  User GetUserById(string id)
        {
            return _context.User.FirstOrDefault(p=>p.Id==id);
        }
    }
}