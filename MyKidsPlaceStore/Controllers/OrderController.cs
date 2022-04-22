using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public OrderController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int Id)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Order.Value.GetWithRelated(Id));
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
        [HttpGet("{SubCategoryId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetSubCategoryUsers(int SubCategoryId)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Order.Value.GetSubCategoryUsers(SubCategoryId));
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
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllItems()
        {
            try
            {
                return Ok(_serviceUnitOfWork.Order.Value.GetAll());
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
        public IActionResult FixAllOrdersAmount()
        {
            try
            {
                return Ok(_serviceUnitOfWork.Order.Value.FixAllOrdersAmount());
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
        [Authorize(Roles = "Admin")]
        public IActionResult GetList([FromBody] OrderSearch orderSearch)
        {
            try
            {
                return Ok(_serviceUnitOfWork.Order.Value.ListOrders(orderSearch));
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
        public async Task<IActionResult> GetLoggedInUserOrder()
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Order.Value.GetLoggedInUserOrdersAsync());
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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] Orders order)
        {
            try
            {
                return Ok(await _serviceUnitOfWork.Order.Value.AddOrder(order));
            }
            catch (ValidationException e)
            {

                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

       
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] Orders order)
        {
            try
            {
                await _serviceUnitOfWork.Order.Value.UpdateAsync(order);
                return Ok(order);
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
                await _serviceUnitOfWork.Order.Value.RemoveAsync(Id);
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