using EbookCatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EbookCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookCatalogContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BookCatalogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
       
        public async Task<IActionResult>   AddBookWriter( int? id)
        {
            var WriterList = await _context.BookWriters.ToListAsync();
            ViewBag.BookWriter = WriterList;
            if (id !=null && id>0)
            {
                try
                {
                    var WriterDetails= await _context.BookWriters.Where(x=>x.Id==id).ToListAsync();
                    ViewBag.WriterDetails = WriterDetails;
                    return View();

                }
                catch(Exception ex)
                {
                    
                }

            }
          
         
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBookWriter(BookWriter wr)
        {
           
                if (wr.Id >0 && wr.Id !=null)
                {

                var existingWriter = await _context.BookWriters.FindAsync(wr.Id);
                if (existingWriter != null)
                {
                    existingWriter.WriterName = wr.WriterName;
                    _context.Update(existingWriter);
                }
            }
                else
                {
                _context.Add(wr);
               
                  
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("AddBookWriter");
        }

        public async Task<IActionResult> AddPublicattions(int id)
        {
            if(id!=null && id>0)
            {
                var publicationData = await _context.BookPublications.Where(x => x.Id == id).ToListAsync();
                ViewBag.PublicationData = publicationData;

            }
            var Publication = await _context.BookPublications.ToListAsync();
            ViewBag.BookPublication = Publication;

            return View();

        }
        public async Task<IActionResult> DeleteWriter(int id)
        {
            try
            {

                var writerData = await _context.BookWriters.FindAsync(id);


                if (writerData == null)
                {
                    return NotFound();
                }


                _context.BookWriters.Remove(writerData);


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return RedirectToAction("AddPublicattions");
            }


            return RedirectToAction("AddPublicattions");
        }


        [HttpPost]

        public async Task< IActionResult> AddPublicattions(BookPublication pb)
        {
            try
            {
                if (pb != null && pb.Id == 0)
                {
                    _context.Add(pb);
                }
                else
                {
                    var OldPublications = await _context.BookPublications.FindAsync(pb.Id);

                    if(OldPublications !=null )
                    {
                        OldPublications.PublicationName = pb.PublicationName;
                        _context.Update(OldPublications);
                    }
                }
                    _context.SaveChanges();
                    return RedirectToAction("AddPublicattions");
                
            }
           catch (Exception ex)
            {
                return View();
            }
            return View();

        }

        public async Task<IActionResult> DeletePublication(int id)
        {
            try
            {
              
                var publication = await _context.BookPublications.FindAsync(id);

             
                if (publication == null)
                {
                    return NotFound();
                }

            
                _context.BookPublications.Remove(publication);

   
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
             
                return RedirectToAction("AddPublicattions");
            }

           
            return RedirectToAction("AddPublicattions");
        }

        public async Task<IActionResult> Bookcatalog()
        {
            var Writerdata = await _context.BookWriters.ToListAsync();
            var Bookdata = await _context.BookPublications.ToListAsync();
            ViewBag.writers = Writerdata;
            ViewBag.books = Bookdata;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Bookcatalog(BookCatalog _catl)
        {

            if (_catl.formFile != null)
            {
                var uploadsDirectory = Path.Combine("wwwroot/bookImages");
                var fileName = Path.GetFileName(_catl.formFile.FileName); 
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await _catl.formFile.CopyToAsync(stream);
                }

                var trimmedFilePath = Path.Combine("bookImages", fileName)
                               .Replace('\\', '/');

            //    var trimmedFilePath = Path.Combine("bookImages", fileName);


                _catl.Pictureuri= trimmedFilePath;

                
              
            }

            try
            {
    
                    
                if (_catl.Id == 0 && _catl.Id != null)
                {
                    _context.Add(_catl);
                }
                else
                {
                    var bookcatdata = await _context.BookCatalogs.FindAsync(_catl.Id);
                    if (bookcatdata != null)
                    {
                        bookcatdata.Name = _catl.Name;
                        bookcatdata.Description = _catl.Description;
                        bookcatdata.Price = _catl.Price;
                        bookcatdata.BookPublicationId = _catl.BookPublicationId;
                        bookcatdata.BookWriterId = _catl.BookWriterId;

                        _context.Update(bookcatdata);
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
              
            }

            return RedirectToAction("Bookcatalog");
        }

    }
}
