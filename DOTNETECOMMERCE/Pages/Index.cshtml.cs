using DOTNETECOMMERCE.Data;
using DOTNETECOMMERCE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.Json;

namespace DOTNETECOMMERCE.Pages
{


    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationDbContext _context ;

        public List<Product> listProducts = new List<Product>();

        // constructeur :
        public IndexModel(ILogger<IndexModel> logger)
        {

            _logger = logger;
              _context = new ApplicationDbContext();
         
        }


        // recevoir les produits depuis la BD:
        [HttpGet]
        public void OnGet(String searchBar)
        {

            // recevoir la liste  des produits depuis la BD
            listProducts = _context.Products.Include(p => p.Category) // Charge la catégorie associée
                         .ToList();

            if (!string.IsNullOrEmpty(searchBar))
            {
                // Filtrer les produits par le nom ou la catégorie selon le texte entré
                listProducts = listProducts.Where(p =>     p.Name.Contains(searchBar, StringComparison.OrdinalIgnoreCase) ||
                                                           p.Category.Name.Contains(searchBar, StringComparison.OrdinalIgnoreCase))
                                                            .ToList();
            }

        }


        [HttpPost]
        public IActionResult OnPost(int IDproduct)
        {

            // recevoir le produit depuis BD
            Product product = _context.Products.FirstOrDefault(p => p.Id == IDproduct);
           
            product.Quantity = 1;
            
            // ajout d 'un produit dans la liste de cookies :
            // Lire le cookie existant
                   
            string cartCookie = Request.Cookies["Cart"];
            
            List<Product> cartItems = new List<Product>();

            // Si le cookie existe, désérialiser les données JSON
            if (!string.IsNullOrEmpty(cartCookie))
            {
                cartItems = JsonSerializer.Deserialize<List<Product>>(cartCookie);

                if (!cartItems.Contains(product))
                {
                    cartItems.Add(product);
                    // notification
                    TempData["success"] = "Produit ajouté au panier !";

                }
                else
                {
                    // notify
                    TempData["dejaexiste"] = "Produit deja existe !";
                }
            }
            else
            {
                // 
                cartItems.Add(product);
                TempData["success"] = "Produit ajouté au panier !";
            }

         

            // Sérialiser le panier mis à jour en JSON
            string updatedCart = JsonSerializer.Serialize(cartItems);

                // Ajouter le panier mis à jour dans un cookie avec une durée d'expiration
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };

            Response.Cookies.Append("Cart", updatedCart, options);

            return RedirectToPage("Index");
            
        }


    }

 
}
