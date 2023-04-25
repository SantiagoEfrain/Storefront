using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF//.Metadata
{

    public class CategoryMetadata
    {
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "500 Char Max")]
        [DataType(DataType.MultilineText)]
        public string? CategoryDesc { get; set; }


    }
    public class MobileSuitMetadata
    {
   
        public int ModelID { get; set; }

        
        [Required(ErrorMessage = "Required Field")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "500 Char Max")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int UniverseID { get; set; }


        [Range(0, (double)decimal.MaxValue)]
        [Display(Name = "Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int ScaleID { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int StockStatusID { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int StockAmount { get; set; }

        [StringLength(75)]
        [Display(Name = "Image")]
        public string? MSImage { get; set; }

    }
    public class ModelScaleMetadata
    {

        public int ScaleID { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public int ScaleSize { get; set; }

    }
    public class StockStatusMetadata
    {
        public int StockStatusID { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string? StockStatusName { get; set; }
    }
    public class UniverseMetadata
    {
        [Required(ErrorMessage = "Required Field")]
        public int UniverseID { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string? UniverseName { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [StringLength(500, ErrorMessage = "500 Char Max")]
        [DataType(DataType.MultilineText)]
        public string? UniverseDesc { get; set; }
    }

    










}
