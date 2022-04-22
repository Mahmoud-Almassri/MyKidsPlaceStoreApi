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
    public class SetController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public SetController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                Item Item = await _serviceUnitOfWork.Item.Value.GetAsync(Id);
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

        //[HttpPost]
        //public IActionResult GetList([FromBody] BaseSearch baseSearch)
        //{
        //    try
        //    {
        //        BaseListResponse<Item> Item = _serviceUnitOfWork.Item.Value.List(baseSearch);
        //        return Ok(Item);
        //    }
        //    catch (ValidationException e)
        //    {
        //        return BadRequest(e);
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Item Item)
        {
            try
            {
               
                return Ok(await _serviceUnitOfWork.Item.Value.AddAsync(Item));
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
                IEnumerable<Set> sets = _serviceUnitOfWork.Set.Value.GetAll();
                return Ok(sets);
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
        public async Task<IActionResult> Update([FromBody] Item Item)
        {
            try
            {
               
                return Ok(await _serviceUnitOfWork.Item.Value.UpdateAsync(Item));
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
                await _serviceUnitOfWork.Item.Value.RemoveAsync(Id);
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