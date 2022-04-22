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
    //[Authorize(Roles = "User, Admin")]
    public class BrandController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public BrandController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Brand.Value.GetAsync(Id));
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
        [Authorize]
        public IActionResult GetAllItems()
        {
            try
            {
                return Ok(_serviceUnitOfWork.Brand.Value.GetAll());
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
        [Authorize]
        public async Task<IActionResult> GetList([FromBody] BaseSearch baseSearch)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Brand.Value.ListAsync(baseSearch));
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
        public IActionResult Create([FromBody] Brand brand)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Brand.Value.AddAsync(brand));
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
        public async Task<IActionResult> Update([FromBody] Brand brand)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Brand.Value.UpdateAsync(brand));
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
                await _serviceUnitOfWork.Brand.Value.RemoveAsync(Id);
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