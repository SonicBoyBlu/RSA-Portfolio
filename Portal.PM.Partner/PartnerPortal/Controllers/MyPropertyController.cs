using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acme.ViewModels;

namespace Acme.Controllers
{
    public class MyPropertyController : Controller
    {
        // GET: MyProperty
        public ActionResult Index()
        {
            ViewModels.MyProperty model = new MyProperty();
            return View(model);
        }

        [HttpPost]
        public ActionResult FetchMyProperty(ViewModels.MyProperty model)
        {
            // for demo purposes
            //model.Url = string.Format("https://www.acmehouseco.com/vacation-rentals/palm-springs/luxery/id/{0}-{1}", GlobalSettings.Escapia.PMCID, model.PropertyID);
            model.Url = "https://www.acmehouseco.com/vacation-rentals/palm-springs/warm-sands/old-blue-eyes/1596-152031";
            model.ThumbnailImageURL = "/content/images/map.png";
            return PartialView("partials/_myProperty", model);
        }

        /*
        public ActionResult FetchMyProperty(int id) { 
            var photos = new List<string>
            {
                "https://pictures.escapia.com/ACHOCO/120243/6418770188.jpg",
                "https://pictures.escapia.com/ACHOCO/120243/3208490525.jpg",
                "https://pictures.escapia.com/ACHOCO/120243/7432530358.jpg",
                "https://pictures.escapia.com/ACHOCO/120243/7168370835.jpg",
                "https://pictures.escapia.com/ACHOCO/120243/6739110304.jpg"
            };
            var model = new ViewModels.MyProperty()
            {
                UnitName = "PRETTY IN PINK AT INDIAN CANYONS",
                Type = "House",
                Distance = "1.1 miles",
                DogsAllowed = true,
                Highlights = "Midcentury villa on Millionaire's Row in the Indian Canyons. On the golf course. Pool area is walled for privacy. Pool w tanning deck, spa, putting green, firepit & mountain views. Close to Palm Canyon Dr. Beds: K,K,Q,K (can convert to T,T on request) ",
                Description = "Luxury Palm Springs Vacation Home Rental. Beautifully restored midcentury Executive Villa on the golf course w private p... <a href='#'>Read More</a>",
                NumInterested = 81,
                Bathrooms = 3.5,
                Bedrooms = 4,
                Sleeps = 8,
                Price = 910.00,
                Photos = photos,
                PropertyID = id,
                Url = "https://www.acmehouseco.com/vacation-rentals/palm-springs/indian-canyons/pretty-in-pink-at-indian-canyons/1596-120243"
            };
            return PartialView("partials/_myProperty", model);
        }
        */
    }
}