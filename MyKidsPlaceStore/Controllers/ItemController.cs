using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domains.DTO;
using Domains.Enums;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyKidsPlaceStore.Helpers;
using Service.UnitOfWork;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private FileUploader fileUploaderHelper;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IServiceUnitOfWork serviceUnitOfWork, IWebHostEnvironment webHostEnvironmen, ILogger<ItemController> logger)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            fileUploaderHelper = new FileUploader(webHostEnvironmen, serviceUnitOfWork);
            _logger = logger;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetByIdAsync(Id));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetItemByIdWithRelated(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetItemByIdWithRelatedAsync(Id));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("{SetId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetItemsBySetId(int SetId)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetItemsBySetIdAsync(SetId));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public IActionResult CompressAllImages()
        {
            try
            {
                return Ok(fileUploaderHelper.CompressAllImages());
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("{ItemId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddItemImages(int ItemId, [FromForm] IFormFileCollection files)
        {
            try
            {
                foreach (IFormFile file in files.ToList())
                {
                    string fileName = await fileUploaderHelper.PostFile(file);
                    ItemImages image = new ItemImages
                    {
                        Id = 0,
                        ImagePath = fileName,
                        ItemId = ItemId,
                        Status = (int)GlobalStatusEnum.Active,
                        IsDefault=files.First().FileName == file.FileName?true:false
                    };
                    await _serviceUnitOfWork.ItemImages.Value.AddAsync(image);
                }

                return Ok("Product Images Added");
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetList([FromBody] ItemSearch itemSearch)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetItemsListAsync(itemSearch));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        } 
        [HttpPost]
        public async Task<IActionResult> GetFilteredItems([FromBody] ItemSearch itemSearch)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetFilteredItemsAsync(itemSearch));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpGet("{from}/{to}")]
        public IActionResult ToggleAllProducts(int from, int to)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Item.Value.ToggleAllItems(from, to));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpPost("{Id}/{Gender}")]
        public async Task<IActionResult> GetItemsBySubCategoryId(int Id, int Gender, [FromBody] ItemSearch search)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetItemsBySubCategoryIdAsync(Id, Gender, search));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("{Id}/{Gender}")]
        public IActionResult GetItemsListBySubCategoryId(int Id, int Gender)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Item.Value.GetItemsListBySubCategoryIdAsync(Id, Gender));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAsync([FromBody] Item Item)
        {
            try
            {
                await _serviceUnitOfWork.Item.Value.AddAsync(Item);
                return Ok(Item);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllItems()
        {
            try
            {
                return Ok(_serviceUnitOfWork.Item.Value.GetAll());
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{subCategoryId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllItemsWithImages(int subCategoryId)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Item.Value.GetItemsWithImagesAsync(subCategoryId));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update([FromBody] Item item)
        {
            try
            {

                await _serviceUnitOfWork.Item.Value.UpdateAsync(item);
                return Ok(item);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        } 
        [HttpGet("{ItemId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteSetItem(int ItemId)
        {
            try
            {

                await _serviceUnitOfWork.Item.Value.DeleteSetItemAsync(ItemId);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                _serviceUnitOfWork.Item.Value.Delete(Id);
                return Ok(true);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet("{ImageId}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteItemImage(int ImageId)
        {
            try
            {
                ItemImages itemImage = await _serviceUnitOfWork.ItemImages.Value.GetAsync(ImageId);
                fileUploaderHelper.DeleteFile(itemImage.ImagePath);
                bool imageRemoved = await _serviceUnitOfWork.ItemImages.Value.RemoveAsync(ImageId);
                return Ok(imageRemoved);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {

                throw e;
            }
        }



    }
}