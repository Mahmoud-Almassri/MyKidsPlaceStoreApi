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
    [Authorize(Roles = "SuperAdmin")]
    public class SaleController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public SaleController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Sale.Value.GetAsync(Id));
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
        public async Task<IActionResult> ToggleStatus(int Id)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Sale.Value.ToggleStatusAsync(Id));
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
                return Ok(_serviceUnitOfWork.Sale.Value.GetAll());
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
        public async Task<IActionResult> GetList([FromBody] BaseSearch baseSearch)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Sale.Value.ListAsync(baseSearch));
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
        public async Task<IActionResult> Create([FromBody] Sale Sale)
        {
            try
            {

                
                return Ok(await _serviceUnitOfWork.Sale.Value.AddAsync(Sale));
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
        public async Task<IActionResult> PostSingleSale([FromBody] SignleSaleDTO Sale)
        {
            try
            {
                
                return Ok(await _serviceUnitOfWork.Sale.Value.PostSignleSaleAsync(Sale));
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
        public async Task<IActionResult> Update([FromBody] Sale Sale)
        {
            try
            {
                await _serviceUnitOfWork.Sale.Value.UpdateAsync(Sale);
                return Ok(Sale);
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
               await _serviceUnitOfWork.Sale.Value.RemoveAsync(Id);
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