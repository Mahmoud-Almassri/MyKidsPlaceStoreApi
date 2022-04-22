using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyKidsPlaceStore.Helpers;
using Service.UnitOfWork;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        
        private FileUploader fileUploaderHelper;
        public CategoryController(IServiceUnitOfWork serviceUnitOfWork, IWebHostEnvironment webHostEnvironmen)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            fileUploaderHelper = new FileUploader(webHostEnvironmen,serviceUnitOfWork);

        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Category.Value.GetAsync(Id));
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
        public IActionResult GetAllItems()
        {
            try
            {
                return Ok(_serviceUnitOfWork.Category.Value.GetAll());
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
        public async Task<IActionResult> GetList([FromBody] BaseSearch baseSearch)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Category.Value.ListAsync(baseSearch));
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
        public async Task<IActionResult> Create([FromForm] string categoryName, [FromForm]  string categoryNameAr,[FromForm] int status,[FromForm]  IFormFile file)
        {

            try
            {
                string fileName = await fileUploaderHelper.PostFile(file);
                Category category = new Category {
                 Id = 0,
                 CategoryName = categoryName,
                 CategoryNameAr = categoryNameAr,
                 Status = status,
                 ImagePath = fileName,
                };

                await _serviceUnitOfWork.Category.Value.AddAsync(category);
                return Ok(category);
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
        public async Task<IActionResult> Update([FromForm] int Id, [FromForm]  string categoryNameAr, [FromForm] string categoryName, [FromForm] int status, [FromForm]  IFormFile file)
        {

            try
            {
               
                Category category = await _serviceUnitOfWork.Category.Value.GetAsync(Id);
                bool isDeleted = fileUploaderHelper.DeleteFile(file.FileName);
                string fileName = await fileUploaderHelper.PostFile(file);

                category.CategoryName = categoryName;
                category.CategoryNameAr = categoryNameAr;
                category.Status = status;
                category.ImagePath = fileName;

                await _serviceUnitOfWork.Category.Value.UpdateAsync(category);
                return Ok(category);
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
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            try
            {
                await _serviceUnitOfWork.Category.Value.RemoveAsync(Id);
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
    }
}