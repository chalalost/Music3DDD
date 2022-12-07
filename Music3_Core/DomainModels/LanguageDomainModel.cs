using Music3_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class LanguageDomainModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public List<ProductTranslationDomainModel> ProductTranslations { get; set; }

        public List<CategoryTranslationDomainModel> CategoryTranslations { get; set; }
    }
}
