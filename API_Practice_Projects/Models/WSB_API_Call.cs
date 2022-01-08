using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_Practice_Projects.Models
{
    public class WSB_API_Call
    {
        private static HttpClient _realClient = null;

        public static HttpClient MyWSBHttp
        {
            get
            {
                if (_realClient == null)
                {
                    _realClient = new HttpClient();
                    _realClient.BaseAddress = new Uri("https://dashboard.nbshare.io/");
                }
                return _realClient;
            }
        }

        public static async Task<WallStreetBets_Object> GrabWSBObject(string ticker)
        {
            var connection = await MyWSBHttp.GetAsync($"/api/v1/apps/reddit");
            List<WallStreetBets_Object> wsbObjects = await connection.Content.ReadAsAsync<List<WallStreetBets_Object>>();

            WallStreetBets_Object myObject = new WallStreetBets_Object();

            for (int i = 0; i < wsbObjects.Count; i++)
            {
                if (wsbObjects[i].ticker.ToLower() == ticker.ToLower())
                {
                    myObject = wsbObjects[i];
                }
            }
            return myObject;            
        }
    }
}
