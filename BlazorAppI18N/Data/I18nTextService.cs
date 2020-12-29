using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppI18N.Data
{
    public class I18nTextService
    {
        public I18nText Text { get; set; }
        public event EventHandler TextChanged;
        public string SelectedLanguage { get; set; } = "en";
        public readonly List<string> SupportedLanguages = new List<string> {
            "en", "de"
        };

        private readonly IWebHostEnvironment _env;

        public I18nTextService(IWebHostEnvironment env)
        {
            _env = env;
            ChangeLanguage(SelectedLanguage);
        }

        public void ChangeLanguage(string code)
        {
            var path = System.IO.Path.Combine(_env.WebRootPath, "i18n", $"{code}.json");
            if (System.IO.File.Exists(path))
            {
                var json = System.IO.File.ReadAllText(path);
                Text = System.Text.Json.JsonSerializer.Deserialize<I18nText>(json);
                TextChanged?.Invoke(this, null);
            }
        }
    }
}
