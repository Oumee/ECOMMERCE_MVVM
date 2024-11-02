
  

namespace DOTNETECOMMERCE.Models
{

 
    public class CartProduct
    {

        public int Id { get; set; }  // Identifiant unique pour le produit

        public int? Quantity { get; set; }  // Quantité en stock 

        // Foreign Key - Relation avec la classe Product
        public int ProductId { get; set; }  // ID du produit associée
        public Product Product { get; set; } = null!;  // Objet produit pour navigation 

  

    }
}
