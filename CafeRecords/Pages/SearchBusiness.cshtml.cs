using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessOwnerRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CafeRecords.Pages
{
    public class SearchBusinessModel : PageModel
    {
        public void OnGet()
        {
            SearchCompleted = false;
        }

        [BindProperty]
        public string Search { get; set; }
        public bool SearchCompleted { get; set; }

        public ICollection<BusinessOwner> businessOwners { get; set; }

        public void OnPost()
        {
            string ownerJsonString = GetData("https://licenseowners2019.azurewebsites.net/Privacy");
            businessOwners = BusinessOwner.FromJson(ownerJsonString);
            ViewData["allBusinessOwners"] = businessOwners;

            businessOwners = businessOwners.Where(x => x.DoingBusinessAsName == Search).ToArray();
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