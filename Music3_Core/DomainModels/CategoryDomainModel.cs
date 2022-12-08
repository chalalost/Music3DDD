using Music3_Core.Entities;
using Music3_Core.Entities.BaseEntity;
using Music3_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Core.DomainModels
{
    public class CategoryDomainModel : BaseEntity
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; }
        public Status Status { set; get; }

        public List<ProductInCategoryDomainModel> ProductInCategories { get; set; }

        public List<CategoryTranslationDomainModel> CategoryTranslations { get; set; }
    }
}
