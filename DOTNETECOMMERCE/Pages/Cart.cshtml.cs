using DOTNETECOMMERCE.Data;
 using DOTNETECOMMERCE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace DOTNETECOMMERCE.Pages
{


    public class CartModel : PageModel
    {
        
        public List<Product> listProducts = new List<Product>();

        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public void OnGet()
        {
            string cartCookie = Request.Cookies["Cart"];
           
            if (!string.IsNullOrEmpty(cartCookie))
            {
              
                listProducts = JsonSerializer.Deserialize<List<Product>>(cartCookie);
            
            }
        }


        [HttpPost]
        public IActionResult OnPostAddQuantity(int id)
        {
 
            string cartCookie = Request.Cookies["Cart"];

            List<Product> cart = JsonSerializer.Deserialize<List<Product>>(cartCookie);
             

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                {

                    cart[i].Quantity += 1;
 
                    break; // Sortir de la boucle après avoir retiré l'élément
                }
            }

            // Sérialiser le panier mis à jour en JSON
            string updatedCart = JsonSerializer.Serialize(cart);

           // return RedirectToPage(updatedCart);

            // Ajouter le panier mis à jour dans un cookie avec une durée d'expiration
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };
           
            Response.Cookies.Append("Cart", updatedCart, options);
            
 
             return RedirectToPage("Cart");
         }
        [HttpPost]
        public IActionResult OnPostRemoveQuantity(int id)
        {

            string cartCookie = Request.Cookies["Cart"];

            List<Product> cart = JsonSerializer.Deserialize<List<Product>>(cartCookie);


            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                {

                    cart[i].Quantity -= 1;

                    break; // Sortir de la boucle après avoir retiré l'élément
                }
            }

            // Sérialiser le panier mis à jour en JSON
            string updatedCart = JsonSerializer.Serialize(cart);

            // return RedirectToPage(updatedCart);

            // Ajouter le panier mis à jour dans un cookie avec une durée d'expiration
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append("Cart", updatedCart, options);


            return RedirectToPage("Cart");
        }

        public IActionResult OnPostUpdate(int id, int quantity)
        { 
            
                // suppression
                string cartCookie = Request.Cookies["Cart"];

                List<Product> cart = JsonSerializer.Deserialize<List<Product>>(cartCookie);

                // Récupérer le produit correspondant à l'IDproduct à partir de la base de données
                Product product = _context.Products.FirstOrDefault(p => p.Id == id);

                // Ajouter le produit au panier
                if (product != null)
                {

                    // listProducts.Remove(product);
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (cart[i].Id == id)
                        {
                            cart.RemoveAt(i);
                            product.Quantity = quantity;
                            cart.Add(product);
                            break; // Sortir de la boucle après avoir retiré l'élément
                        }
                    }

                    // Sérialiser le panier mis à jour en JSON
                    string updatedCart = JsonSerializer.Serialize(cart);

                    // Ajouter le panier mis à jour dans un cookie avec une durée d'expiration
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    };
                    Response.Cookies.Append("Cart", updatedCart, options);
                }

                return RedirectToPage("Cart");
 
        }


        [HttpPost]
        public IActionResult OnPostDelete(int id)
        {
            // suppression
            string cartCookie = Request.Cookies["Cart"];
 
            List<Product> cart = JsonSerializer.Deserialize<List<Product>>(cartCookie);
            
            // Récupérer le produit correspondant à l'IDproduct à partir de la base de données
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);

            // Ajouter le produit au panier
            if(product != null)
            {

                // listProducts.Remove(product);
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Id == id)
                    {
                        cart.RemoveAt(i);
                        break; // Sortir de la boucle après avoir retiré l'élément
                    }
                }

                // Sérialiser le panier mis à jour en JSON
                string updatedCart = JsonSerializer.Serialize(cart);

                // Ajouter le panier mis à jour dans un cookie avec une durée d'expiration
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("Cart", updatedCart, options);
            }

            return RedirectToPage("Cart");


        } 

    }

}

