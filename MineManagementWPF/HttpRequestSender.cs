using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MineManagementWPF
{
    public static class HttpRequestSender
    {
        private static HttpClient Client;

        public static async Task<HttpResponseMessage> GET(string url)
        {
            Client = new HttpClient();
            try
            {
                return await Client.GetAsync(url);
            }
            catch (Exception ex)
            {
                if (IsDebug())
                {
                    MessageBox.Show(ex.Message);
                }
                return null;
            }
        }

        public static async Task<HttpResponseMessage> POST(string url,HttpContent httpContent)
        {
            Client = new HttpClient();
            try
            {
                return await Client.PostAsync(url, httpContent);
            }
            catch(Exception ex)
            {
                if (IsDebug())
                {
                    MessageBox.Show(ex.Message);
                }
                throw ex;
            }
        }

        private static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
