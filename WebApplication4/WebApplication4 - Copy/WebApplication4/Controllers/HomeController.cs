using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Pages;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(CountryModel country)
        {
            country.CountryList = CountryDate();
            if(country.CountryList != null)
            {
                List<SelectListItem> selectedItems = country.CountryList.Where(p => country.CountryId.Contains(int.Parse(p.Value))).ToList();
                ViewBag.Message = "Selected Countries:";
                foreach (var selectedItem in selectedItems)
                {
                    selectedItem.Selected = true;
                    ViewBag.Message += "\\n" + selectedItem.Text;
                }
            }
            
            
            return View(country);
        }
        public List<SelectListItem> CountryDate()
        {
            List<SelectListItem> countryItems = new List<SelectListItem>();
            //objcountry.Add(new Country { CountryName = "America" });
            // objcountry.Add(new Country { CountryName = "India" });
            //objcountry.Add(new Country { CountryName = "Sri Lanka" });
            //objcountry.Add(new Country { CountryName = "China" });
            //objcountry.Add(new Country { CountryName = "Pakistan" });
            countryItems.Add(new SelectListItem
            {
                Text = "America", //sdr["continentName"].ToString(),
                Value = "America"//sdr["continentId"].ToString()
            });
            countryItems.Add(new SelectListItem
            {
                Text = "Europe", //sdr["continentName"].ToString(),
                Value = "Europe"//sdr["continentId"].ToString()
            });
            return countryItems;
        }
    }
}
