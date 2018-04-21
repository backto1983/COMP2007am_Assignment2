using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public interface IBookInfoRepository
    {
        // Used for Unit Testing with mock book info data
        IQueryable<bookInfo> books { get; }

        bookInfo Save(bookInfo bookInfo);
        void Delete(bookInfo bookInfo);
    }
}
