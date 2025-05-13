using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get() =>
        await _context.Products.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/qrcode")]
    public async Task<IActionResult> GetProductQRCode(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();
    
        var content = $"Product: {product.Name}, SKU: {product.SKU}, Price: {product.Price}";
        var imageBytes = BarcodeGenerator.GenerateQRCode(content);
    
        return File(imageBytes, "image/png");
    }

}
