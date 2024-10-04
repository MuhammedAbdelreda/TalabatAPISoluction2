using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat;
using TalabatAPI.Controllers;
using TalabatAPI.DTOs;

namespace TalabatAPI
{
    //[Authorize]
    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper; //for mapping

        public ProductController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductType> typeRepo ,IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpGet] //endpoint
        #region Default Calling for GetProducts
        /*        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() //ActionResult for API,IActionResult for MVC
        {
            var products = await _productRepo.GetAllAsync();
            return Ok(products);
        }*/
        #endregion

        #region Calling GetProducts using SpecificationPattern
        /*        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() //ActionResult for API,IActionResult for MVC
        {
            var spec = new ProductWithTypeAndBrandSpecification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }*/
        #endregion

        #region Calling GetProducts using DTO
        /*        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts() //ActionResult for API,IActionResult for MVC
        {
            var products = await _productRepo.GetAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }*/
        #endregion

        #region Calling GetProducts using SpecificationPattern using DTO
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParam ProductSpec) //ActionResult for API,IActionResult for MVC
        {
            var spec = new ProductWithTypeAndBrandSpecification(ProductSpec);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }
        #endregion

        [HttpGet("{id}")] //endpoint
        #region Default Calling GetProductById
        /*        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductById(int id)
        {
            var products = await _productRepo.GetByIdAsync(id);
            return Ok(products);
        }*/
        #endregion

        #region Calling GetProductById using DTO
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var products = await _productRepo.GetByIdAsync(id);
            return Ok(_mapper.Map<Product, ProductToReturnDto>(products));
        } 
        #endregion

        [HttpGet("brands")] //path:(baseUrl/api/productController/brands) endpoint
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")] //path:(baseUrl/api/productController/types) endpoint
        public new async Task<ActionResult<IReadOnlyList<ProductType>>> GetType()
        {
            var types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }


    }
}
