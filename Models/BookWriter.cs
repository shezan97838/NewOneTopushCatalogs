using System;
using System.Collections.Generic;

namespace EbookCatalog.Models
{
    public partial class BookWriter
    {
        public BookWriter()
        {
            BookCatalogs = new HashSet<BookCatalog>();
        }

        public int Id { get; set; }
        public string? WriterName { get; set; }

        public virtual ICollection<BookCatalog> BookCatalogs { get; set; }
    }
}
