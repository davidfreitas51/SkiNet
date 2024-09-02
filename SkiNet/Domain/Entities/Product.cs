using SkiNet.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Product : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
    public decimal Price { get; set; }

    [Url]
    public string PictureUrl { get; set; }

    [Required]
    [ForeignKey("ProductTypeId")]
    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }

    [Required]
    [ForeignKey("ProductBrandId")]
    public ProductBrand ProductBrand { get; set; }
    public int ProductBrandId { get; set; } 
}
