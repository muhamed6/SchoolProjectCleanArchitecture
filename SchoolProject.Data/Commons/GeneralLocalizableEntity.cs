using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Commons
{
    public class GeneralLocalizableEntity
    {
        public string Localize(string textAr,string textEn)
        {
            var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLowerInvariant();

            if (culture == "ar")
                return !string.IsNullOrWhiteSpace(textAr) ? textAr : textEn;

            return !string.IsNullOrWhiteSpace(textEn) ? textEn : textAr;
        }
    }
}
