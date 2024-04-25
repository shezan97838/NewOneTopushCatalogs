using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EbookCatalog.Models
{
    public partial class BookCatalog
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? PictureFileName { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }
        public string? Pictureuri { get; set; }
        public int? BookPublicationId { get; set; }
        public int? BookWriterId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int? ModifiedByUserId { get; set; }
       
        public virtual BookPublication? BookPublication { get; set; }
        public virtual BookWriter? BookWriter { get; set; }
    }
}
