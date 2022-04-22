using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfWork;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class SubCategoryController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public SubCategoryController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.SubCategory.Value.GetAsync(Id));
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
                return Ok(await _serviceUnitOfWork.SubCategory.Value.ListAsync(baseSearch));
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
                return Ok(_serviceUnitOfWork.SubCategory.Value.GetAll());
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

        [HttpGet("{categoryId}")]
        public IActionResult GetByParentId([FromRoute] int categoryId)
        {
            try
            {
                return Ok(_serviceUnitOfWork.SubCategory.Value.GetByParentId(categoryId));
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
        public async Task<IActionResult> Create([FromBody] SubCategory SubCategory)
        {
            try
            {

                
                return Ok(await _serviceUnitOfWork.SubCategory.Value.AddAsync(SubCategory));
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
        public async Task<IActionResult> Update([FromBody] SubCategory SubCategory)
        {
            try
            {
                
                return Ok(await _serviceUnitOfWork.SubCategory.Value.UpdateAsync(SubCategory));
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
                await _serviceUnitOfWork.SubCategory.Value.RemoveAsync(Id);
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