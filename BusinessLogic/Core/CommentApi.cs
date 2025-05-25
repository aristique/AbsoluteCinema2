using System;
using ABSOLUTE_CINEMA.Domain.Entities;


namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class CommentApi
    {
        public void Addd(Comment comment)
        {
            using (var db = new WebDbContext())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public void Removee(Guid id)
        {
            using (var db = new WebDbContext())
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
}
