using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Added references
using System.Web;
using System.Data.Entity;

namespace Assignment2.Models
{
    public class EFBookInfoRepository : IBookInfoRepository
    {
        private BookStoreModel db = new BookStoreModel();

        public IQueryable<bookInfo> bookInfo { get { return db.bookInfo; } }

        public void Delete(bookInfo bookInfo)
        {
            db.bookInfo.Remove(bookInfo);
            db.SaveChanges();
        }

        public bookInfo Save(bookInfo bookInfo)
        {
            if(bookInfo.bookID == 0)
            {
                db.bookInfo.Add(bookInfo);
            }
            else
            {
                db.Entry(bookInfo).State = EntityState.Modified;
            }
            db.SaveChanges();

            return bookInfo;
        }
    }
}
