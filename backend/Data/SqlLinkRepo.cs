using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UrlShortener.Dtos;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class SqlLinkRepo : ILinkRepo
    {
        private readonly LinkContext _context;
        public SqlLinkRepo(LinkContext context)
        {
            _context = context;

        }
        public void CreateLink(Link link)
        {

            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));

            }
            _context.Link.Add(link);
        }



        public void DeleteLink(Link link)
        {
            if(link==null){
                throw new ArgumentException(nameof(link));

            }
            Console.WriteLine(link.Id);
            _context.Link.Remove(link);

        }

        public IEnumerable<Link> GetAllLinks(string userId)
        {

return _context.Link.Where(x=>x.UserId==userId).ToList();

        }

        public Link GetLinkById(string id)
        {
            return _context.Link.FirstOrDefault(p => p.Id == id);
        }

        public Link GetLinkByUrlCode(string urlCode)
        {
            return _context.Link.FirstOrDefault(p => p.UrlCode == urlCode);
        }

        public void HangfireDeleteLink(string id)
        {
            Link link=GetLinkById(id);
            if(link==null){
                throw new ArgumentException(nameof(link));

            }
            _context.Link.Remove(link);
            SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);

        }

    }
}