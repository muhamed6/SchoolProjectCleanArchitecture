using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Commons
{
    public class LocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string GetLocalized()
        {
            var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLowerInvariant();

            // Fall back if name is null
            if (culture == "ar")
                return !string.IsNullOrWhiteSpace(NameAr) ? NameAr : NameEn;

            return !string.IsNullOrWhiteSpace(NameEn) ? NameEn : NameAr;
        }

    }
}
