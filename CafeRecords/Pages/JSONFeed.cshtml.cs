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
    public class JSONFeedModel : PageModel
    {
        public JsonResult OnGet()
        {
            List<Owner> cafeOwners = new List<Owner>();

            string CafeJsonString = GetData("https://data.cityofchicago.org/resource/mqmh-p6ud.json");
            Cafe[] allCafes = Cafe.FromJson(CafeJsonString);
            
            string OwnerJsonString = GetData("https://data.cityofchicago.org/resource/ezma-pppn.json");
            Owner[] allOwners = Owner.FromJson(OwnerJsonString);
                                  
            IDictionary<string, Cafe> cafes = new Dictionary<string, Cafe>();

            foreach (Cafe cafe in allCafes)
            {
                cafes.Add(cafe.PermitNumber, cafe);
            }

            foreach (Owner owner in allOwners)
            {
                foreach (var cafe in cafes)
                {
                    //Compare Account Number from two JSON and select only ones which have common account number
                    if (cafe.Value.AccountNumber == owner.AccountNumber)
                    {
                        cafeOwners.Add(owner);
                    }
                }
            }

            return new JsonResult(cafeOwners);
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