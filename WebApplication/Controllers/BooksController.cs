using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionFilterExample.Models;

namespace ActionFilterExample.Controllers
{
public class BooksController : Controller
{
[NeedsPersistence]
public ActionResult Index()
{
    var books = DataAccessLayer.GetBooks()
            .Select(x=>new BookModel {Id=x.Id, Name=x.Name,Author= x.Author});
    return View(books);
}
}
}