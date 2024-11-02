using System.ComponentModel.DataAnnotations.Schema;

namespace DOTNETECOMMERCE.Models
{
    public class Product
    {
        public int Id { get; set; }  // Identifiant unique pour le produit
        public string Name { get; set; } = null!; // Nom du produit
        public string? Description { get; set; }  // Description du produit

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }  // Prix du produit
        public string? ImageUrl { get; set; }  // URL de l'image du produit
        public int? Quantity { get; set; }  // Quantité en stock

        // Foreign Key - Relation avec la classe Category
        public int CategoryId { get; set; }  // ID de la catégorie associée
        public Category Category { get; set; } = null!;  // Objet Category pour navigation
        public ICollection<CartProduct> Carts { get; set; }  // Liste des carts dans cette produit


        public override bool Equals(object obj)
        {
            if (obj is Product product)
            {
                return Id == product.Id; // Comparaison par ID
            }
            return false;
        }

    }

}
