namespace App
{
    public class DependencyInjection
    {
        
        public static void DependencyRepository(IServiceCollection services)
        {
            Register(services);
        }
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<App.Interfaces.IFileDataContext<Dictionary<string,string>>>(i => new App.Services.JsonContext("currency.json"));
            services.AddScoped<App.Interfaces.IFileDataContext<string>>(i => new App.Services.XmlContext.SingleDataAsString(
                "api_key.xml",
                "apikey",
                new System.Xml.Linq.XElement("apikey"),
                (x => (string)x.Attribute("name")=="convertCurrency"),
                (o => (string)o.Attribute("name") == "convertCurrency")
            ));
        }
    }
}