﻿using Gamehoax_backend.Areas.Admin.ViewModels.Products;
using Gamehoax_backend.Data;
using Gamehoax_backend.Helpers;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.IO;

namespace Gamehoax_backend.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly IBrandModelService _brandModelService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IDiscountService _discountService;
        private readonly IRatingService _ratingService;
       
        public ProductController(AppDbContext context,
                                 IWebHostEnvironment env,
                                 IProductService productService,
                                 IBrandModelService brandModelService,
                                 ICategoryService categoryService,
                                 ITagService tagService,
                                 IDiscountService discountService,
                                 IRatingService ratingService
                               )
        {
            _context = context;
            _env = env;
            _productService = productService;
            _brandModelService = brandModelService;
            _categoryService = categoryService;
            _tagService = tagService;
            _discountService = discountService;
            _ratingService= ratingService;
        }
        public async Task<IActionResult> Index(int page=1,int take=5)
        {
            List<Product> datas = await _productService.GetPaginateDatasAsync(page,take,null,null,null,null,null,null);
            List<ProductListVM> mappedDatas= GetMappedDatas(datas);
            int pageCount = await GetPageCountAsync(take);
            ViewBag.take = take;
            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }


        private List<ProductListVM> GetMappedDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                var categories = product.ProductCategories.Select(pt => pt.Category).ToList();
                var tags = product.ProductTags.Select(pt => pt.Tag).ToList();
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price.ToString("0.##")+ " £",
                    Image = product.ProductImages.FirstOrDefault(m=>m.IsMain).Image,
                    Weight=product.Weight.ToString("0.##")+ " kg",
                    Model=product.BrandModel.Name,
                    Categories= categories,
                    Tags= tags,
                    Title=product.Title,
                    Popularity=product.Popularity,
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }
        private async Task<SelectList> GetTagsAsync()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            return new SelectList(tags, "Id", "Name");
        }

        private async Task<SelectList> GetBrandModelsAsync()
        {
            List<BrandModel> brandModels = await _brandModelService.GetAllAsync();
            return new SelectList(brandModels, "Id", "Name");
        }
        private async Task<SelectList> GetDiscountsAsync()
        {
            List<Discount> discounts = await _discountService.GetAllAsync();
            return new SelectList(discounts, "Id", "Name");
        }
        private async Task<SelectList> GetRatingCountAsync()
        {
            List<Rating> ratings = await _ratingService.GetAllAsync();
            return new SelectList(ratings, "Id", "RatingCount");
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Product dbProduct=await _productService.GetAllDataById(id);

                if(dbProduct == null) return NotFound();

                ProductDetailVM model = new()
                {
                    Id=dbProduct.Id,
                    Name=dbProduct.Name,
                    Description=dbProduct.Description,
                    Title=dbProduct.Title,
                    Price=dbProduct.Price,
                    Weight=dbProduct.Weight,
                    Feature=dbProduct.Feature,
                    DiscountName=dbProduct.Discount.Name,
                    Model=dbProduct.BrandModel.Name,
                    RatingCount=dbProduct.Rating.RatingCount,
                    Images=dbProduct.ProductImages,
                    ProductCategories=dbProduct.ProductCategories,
                    ProductTags=dbProduct.ProductTags,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;  

                return View();
            }
           
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetAllDataById((int)id);
                if (dbProduct is null) return NotFound();

                foreach (var productImage in dbProduct.ProductImages)
                {
                    string dbPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/product", productImage.Image);
                    FileHelper.DeleteFile(dbPath);
                }

                _context.Products.Remove(dbProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.tags = await GetTagsAsync();
            ViewBag.brandModels = await GetBrandModelsAsync();
            ViewBag.discounts = await GetDiscountsAsync();
            ViewBag.ratingCount = await GetRatingCountAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            try
            {

                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.tags = await GetTagsAsync();
                ViewBag.brandModels = await GetBrandModelsAsync();
                ViewBag.discounts = await GetDiscountsAsync();
                ViewBag.ratingCount = await GetRatingCountAsync();
                if (!ModelState.IsValid) return View(model);

                Product newProduct = new();
                List<ProductImage> productImages = new();
                List<ProductCategory> productCategories = new();
                List<ProductTag> productTags = new();


                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File type must be image");
                        return View(model);
                    }
                    if (!photo.CheckFileSize(600))
                    {
                        ModelState.AddModelError("Photos", "Image size must be max 600kb");
                        return View(model);
                    }
                }

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/product", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);
                }

                newProduct.ProductImages = productImages;
                newProduct.ProductImages.FirstOrDefault().IsMain = true;
                if (newProduct.ProductImages.Count > 1)
                {
                    newProduct.ProductImages.Skip(1).First().IsHover = true;
                }
                else
                {
                    ModelState.AddModelError("Photos", "There must be at least two images to set hover state.");
                    return View(model);
                }

                if (model.CategoryIds.Count > 0)
                {
                    foreach (var item in model.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = item
                        };
                        productCategories.Add(productCategory);
                    }
                    newProduct.ProductCategories = productCategories;
                }
                else
                {
                    ModelState.AddModelError("Categories", "Don`t be empty");
                    return View(model);
                }
                if (model.TagIds.Count > 0)
                {
                    foreach (var item in model.TagIds)
                    {
                        ProductTag productTag = new()
                        {
                            TagId = item
                        };
                        productTags.Add(productTag);
                    }
                    newProduct.ProductTags = productTags;
                }
                else
                {
                    ModelState.AddModelError("Tags", "Don`t be empty");
                    return View(model);
                }



                decimal decimalPrice;
                decimal decimalWeight;
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                Random random = new();

                if(Decimal.TryParse(model.Price, NumberStyles.Any, cultureInfo, out decimalPrice)&& Decimal.TryParse(model.Weight, NumberStyles.Any, cultureInfo, out decimalWeight))
                {
                    newProduct.Name = model.Name;
                    newProduct.SKU = model.SKU + random.Next(100, 1000);
                    newProduct.Description = model.Description;
                    newProduct.Price = decimalPrice;
                    newProduct.RatingId = model.RatingId;
                    newProduct.Feature = model.Feature;
                    newProduct.BrandModelId = model.BrandModelId;
                    newProduct.DiscountId = model.DiscountId;
                    newProduct.Weight = decimalWeight;
                    newProduct.Title = model.Title;
                    newProduct.Description = model.Description;
                    newProduct.Popularity = model.Popularity;

                 
                }

                await _context.ProductImages.AddRangeAsync(productImages);
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View();
            }
           
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetAllDataById((int)id);
                if (dbProduct is null) return NotFound();
              

                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.tags = await GetTagsAsync();
                ViewBag.brandModels = await GetBrandModelsAsync();
                ViewBag.discounts = await GetDiscountsAsync();
                ViewBag.ratingCount = await GetRatingCountAsync();


              

                ProductEditVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Title = dbProduct.Title,
                    Description = dbProduct.Description,
                    Feature = dbProduct.Feature,
                    Weight = dbProduct.Weight.ToString("0.##") ,
                    Price = dbProduct.Price.ToString("0.##") ,
                    SKU=dbProduct.SKU,
                    Images= dbProduct.ProductImages.ToList(),
                    CategoryIds=dbProduct.ProductCategories.Select(m=>m.Category.Id).ToList(),
                    TagIds=dbProduct.ProductTags.Select(m=>m.Tag.Id).ToList(),
                    BrandModelId=dbProduct.BrandModelId,
                    DiscountId=dbProduct.DiscountId,
                    RatingId=dbProduct.RatingId,
                    Popularity=dbProduct.Popularity,
                };

              

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id ,ProductEditVM updatedProduct)
        {
            try
            {

                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.tags = await GetTagsAsync();
                ViewBag.brandModels = await GetBrandModelsAsync();
                ViewBag.discounts = await GetDiscountsAsync();
                ViewBag.ratingCount = await GetRatingCountAsync();

                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetAllDataById((int)id);
                if (dbProduct is null) return NotFound();

                if (!ModelState.IsValid)
                {
                    updatedProduct.Images = dbProduct.ProductImages.ToList();
                    return View(updatedProduct);
                }
                if (updatedProduct.Photos != null)
                {
                    foreach (var photo in updatedProduct.Photos)
                    {
                        if (!photo.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("Photos", "File type must be image");
                            updatedProduct.Images = dbProduct.ProductImages.ToList();
                            return View(updatedProduct);
                        }
                        if (!photo.CheckFileSize(600))
                        {
                            ModelState.AddModelError("Photos", "Image size must be max 600kb");
                            updatedProduct.Images = dbProduct.ProductImages.ToList();
                            return View(updatedProduct);
                        }
                    }

                    foreach (var item in dbProduct.ProductImages)
                    {
                        string dbPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/product", item.Image);
                        FileHelper.DeleteFile(dbPath);
                    }
                    List<ProductImage> productImages = new();

                    foreach (var photo in updatedProduct.Photos)
                    {
                        string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                        string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/product", fileName);

                        await FileHelper.SaveFileAsync(path, photo);

                        ProductImage productImage = new()
                        {
                            Image = fileName
                        };
                        productImages.Add(productImage);
                    }
                    dbProduct.ProductImages = productImages;
                    dbProduct.ProductImages.FirstOrDefault().IsMain = true;
                    if (dbProduct.ProductImages.Count > 1)
                    {
                        dbProduct.ProductImages.Skip(1).FirstOrDefault().IsHover = true;
                    }
                    else
                    {
                        ModelState.AddModelError("Photos", "There must be at least two images to set hover state.");
                        return View(updatedProduct);
                    }
                    //dbProduct.ProductImages.Skip(1).FirstOrDefault().IsHover = true;

                    await _context.ProductImages.AddRangeAsync(productImages);
                }
                else
                {
                    updatedProduct.Images = dbProduct.ProductImages.ToList();
                }


                if (updatedProduct.TagIds.Count > 0)
                {
                    List<ProductTag> productTags = new();

                    foreach (var item in updatedProduct.TagIds)
                    {
                        ProductTag productTag = new()
                        {
                            TagId = item
                        };
                        productTags.Add(productTag);
                    }
                    dbProduct.ProductTags = productTags;
                }

                if (updatedProduct.CategoryIds.Count > 0)
                {
                    List<ProductCategory> productCategories = new();

                    foreach (var item in updatedProduct.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = item
                        };
                        productCategories.Add(productCategory);
                    }
                    dbProduct.ProductCategories = productCategories;
                }

                decimal decimalPrice;
                decimal decimalWeight;
                if (!(decimal.TryParse(updatedProduct.Price, out decimalPrice)&& decimal.TryParse(updatedProduct.Weight, out decimalWeight)))
                {
                    throw new ArgumentException("Invalid price format. Please enter a valid decimal number.");
                }


                dbProduct.Name = updatedProduct.Name;
                dbProduct.Description = updatedProduct.Description;
                dbProduct.Price = decimalPrice;
                dbProduct.Weight = decimalWeight;
                dbProduct.Title = updatedProduct.Title;
                dbProduct.BrandModelId = updatedProduct.BrandModelId;
                dbProduct.Feature = updatedProduct.Feature;
                dbProduct.RatingId = updatedProduct.RatingId;
                dbProduct.SKU = updatedProduct.SKU;
                dbProduct.DiscountId = updatedProduct.DiscountId;
                dbProduct.Popularity= updatedProduct.Popularity;
              
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));




               
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
           
        }



        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            ProductImage image = await _context.ProductImages.FirstOrDefaultAsync(m => m.Id == id);
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "assets/img/product", image.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return Ok();
        }

    }
}
