using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SharedCalendar.Services
{
    public class ApiStorage : IStorage
    {
        public Task<string> Read()
        {
            return SecureStorage.GetAsync("token");
        }

        public void Save(string value)
        {
            SecureStorage.SetAsync("token", value);
        }
    }
}
