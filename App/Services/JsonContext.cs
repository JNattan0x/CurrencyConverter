using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    /*
        Json data read implementation for IFileDataContext interface.
        This class read a key/value json schema and parse for generic dictionary
    */
    public class JsonContext : Interfaces.IFileDataContext<Dictionary<string,string>>
    {
        private string fileSourcePath;
        public JsonContext(string fileSourcePath)
        {
            this.fileSourcePath = fileSourcePath;
        }
        public async Task<Dictionary<string, string>> GetContextualData()
        {
            if(string.IsNullOrEmpty(fileSourcePath))
                return new Dictionary<string, string>();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(
                await Utils.IO.ReadFile.RetrieveStringFromFile(fileSourcePath))
                ?? new Dictionary<string, string>();
        }
    }
}