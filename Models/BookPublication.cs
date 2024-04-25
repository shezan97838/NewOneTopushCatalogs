using System;
using System.Collections.Generic;

namespace EbookCatalog.Models
{
    public partial class BookPublication
    {
        public BookPublication()
        {
            BookCatalogs = new HashSet<BookCatalog>();
        }

        public int Id { get; set; }
        public string? PublicationName { get; set; }

        public virtual ICollection<BookCatalog> BookCatalogs { get; set; }
    }
}
