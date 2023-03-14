using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
namespace WebApplication4.Pages
{
    public class CountryModel
    {
        public List<SelectListItem> CountryList { get; set; } 
        public int[] CountryId { get; set; }

    }
}
