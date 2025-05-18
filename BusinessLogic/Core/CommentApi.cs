using System;
using ABSOLUTE_CINEMA.Domain.DTO;
using ABSOLUTE_CINEMA.Domain.Entities;
namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class CommentApi
    {
        public void Add(WebDbContext db, Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }
        public void Remove(WebDbContext db, Guid id)
        {
            var entity = db.Comments.Find(id);
            if (entity != null)
            {
                db.Comments.Remove(entity);
                db.SaveChanges();
            }
        }
    }
}
