using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeoEcommerce.Data;
using Models;

namespace LeoEcommerce.Pages.Views;

public class IndexPartial : PageModel
{
 private readonly DomainContext _context;
    public IndexPartial(DomainContext context)
    {
        _context = context;
    }

    public IList<Product> Products { get; set; }

    public void OnGet()
    {
        Products = _context.Product.ToList();
    }
}