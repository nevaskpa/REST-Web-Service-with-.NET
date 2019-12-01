using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CafeRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OwnerRecord;

namespace CafeRecords.Pages
{
    public class SearchCafeOwnerModel : PageModel
    {
        public void OnGet()
        {
            SearchCompleted = false;
        }

        [BindProperty]
        public string Search { get; set; }
        public bool SearchCompleted { get; set; }
        //public ICollection<CafeRecord.Cafe> cafeOwners { get; set; }
        public ICollection<CafeRecord.Cafe> cafes { get; set; }

        public void OnPost()
        {
            //List<Owner> cafeOwners = new List<Owner>();
            string CafeJsonString = GetData("https://data.cityofchicago.org/resource/mqmh-p6ud.json");
            cafes = Cafe.FromJson(CafeJsonString);
            ViewData["allCafes"] = cafes;

            //IDictionary<string, Cafe> cafes = new Dictionary<string, Cafe>();
            //ViewData["cafeOwners"] = cafeOwners;
            cafes = cafes.Where(x => x.ZipCode == Search).ToArray();
            SearchCompleted = true;
        }
        public string GetData(string endpoint)
        {
            string downloadedJson;
            using (WebClient webClient = new WebClient())
            {
                downloadedJson = webClient.DownloadString(endpoint);
            }
            return downloadedJson;
        }
    }
}