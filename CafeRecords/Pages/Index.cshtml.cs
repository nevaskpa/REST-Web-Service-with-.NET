using System;
using System.Collections.Generic;

using System.Net;

using CafeRecord;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OwnerRecord;

namespace CafeRecords.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            List<Owner> cafeOwners = new List<Owner>();

            string cafeJsonString = GetData("https://data.cityofchicago.org/resource/mqmh-p6ud.json");
            Cafe[] allCafes = Cafe.FromJson(cafeJsonString);
            

            string ownerJsonString = GetData("https://data.cityofchicago.org/resource/ezma-pppn.json");
            Owner[] allOwners = Owner.FromJson(ownerJsonString);
            

            IDictionary<string, Cafe> cafes = new Dictionary<string, Cafe>();

            foreach (Cafe cafe in allCafes)
            {
                cafes.Add(cafe.PermitNumber, cafe);
            }

            foreach (Owner owner in allOwners)
            {
                foreach (var cafe in cafes)
                {
                    if (cafe.Value.AccountNumber == owner.AccountNumber)
                    {
                        cafeOwners.Add(owner);
                    }
                }
            }
            ViewData["cafeOwners"] = cafeOwners;
        }

        public string GetData(string endpoint)
        {
            string downloadedJson = "";
            using (WebClient webClient = new WebClient())
            {
                downloadedJson = webClient.DownloadString(endpoint); 
            }
            return downloadedJson;
        }

    }
}
