
using System;
using System.Collections.Generic;
using System.Linq;
using ABSOLUTE_CINEMA.Domain.Entities;

namespace ABSOLUTE_CINEMA.BusinessLogic.Core
{
    public class HistoryApi
    {

        public void Add(ViewingHistory history)
        {
            using (var db = new WebDbContext())
            {
                db.ViewingHistories.Add(history);
                db.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            using (var db = new WebDbContext())
            {
                var entity = db.ViewingHistories.Find(id);
                if (entity != null)
                {
                    db.ViewingHistories.Remove(entity);
                    db.SaveChanges();
                }
            }
        }
        public void RemoveByUser(Guid userId)
        {
            using (var db = new WebDbContext())
            {
                
                var entries = db.ViewingHistories.Where(h => h.UserId == userId).ToList();
                if (entries.Any())
                {
                    db.ViewingHistories.RemoveRange(entries);
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<ViewingHistory> GetByUser(Guid userId)
        {
            using (var db = new WebDbContext())
            {
                return db.ViewingHistories
                         .Where(h => h.UserId == userId)
                         .OrderByDescending(h => h.ViewedAt)
                         .ToList();
            }
        }
    }
}
