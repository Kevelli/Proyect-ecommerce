using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeoEcommerce.Data;
using Models;

namespace LeoEcommerce.Pages.Admin;

public class Dashboard : PageModel
{
    
    private readonly DomainContext _context;

    public Dashboard(DomainContext context) {
        _context = context;
    }
    
    public IList<Product> Products { get; set; }

    public void OnGet()
    {
        Products = _context.Product.ToList();
    }
}