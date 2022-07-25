using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPICRUD_JWT.Data.Interfaces;
using WebAPICRUD_JWT.Dtos;
using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IApiRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IApiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product != null)
                return Ok(product);

            return NotFound("Producto no encontrado"); 
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var product = await _repository.GetProductByNameAsync(name);
            if (product != null)
                return Ok(product);

            return NotFound("Producto no encontrado");
        }

        [HttpPost] 
        public async Task<IActionResult> Post(CreateProductDTO createProductDTO)
        { 
            var newProduct = _mapper.Map<Product>(createProductDTO); 

            _repository.Add(newProduct);
            
            if (await _repository.SaveAll())
                return Ok(newProduct); 

            return BadRequest(); 
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult> Put(int id, UpdateProductDTO updateProductDTO)
        {
            if (id != updateProductDTO.Id)
                return BadRequest("Los Ids no coinciden"); 
            
            var productToUpdate = await _repository.GetProductByIdAsync(updateProductDTO.Id); 

            if (productToUpdate == null)
            {
                return NotFound("Producto no encontrado"); 
            }

            _mapper.Map(updateProductDTO, productToUpdate); 
            
            //_repository.Put(productToUpdate); 
            if (!await _repository.SaveAll())
                return NoContent(); 

            return Ok(productToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await _repository.GetProductByIdAsync(id);

            if (productToDelete == null)
            {
                return NotFound("Producto no encontrado"); 
            }

            _repository.Delete(productToDelete);

            if (!await _repository.SaveAll())
                return BadRequest("No se pudo eliminar el producto");

            return Ok("Producto Borrado");

        }

   
    }
}
