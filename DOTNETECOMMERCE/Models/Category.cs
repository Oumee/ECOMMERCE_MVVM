namespace DOTNETECOMMERCE.Models
{
    public class Category
    {

        public int Id { get; set; }  // Identifiant unique pour la catégorie
        public string Name { get; set; } = null!; // Nom de la catégorie
        public string? Description { get; set; }  // Description de la catégorie
        public string? ImageUrl { get; set; }  // ilage
        public ICollection<Product> Products { get; set; }  // Liste des produits dans cette catégorie
   
    }
}
