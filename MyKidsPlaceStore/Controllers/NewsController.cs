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
using Serilog;
using Service.UnitOfWork;

namespace MyKidsPlaceStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        
        public NewsController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "User,SuperAdmin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _serviceUnitOfWork.News.Value.GetAsync());
            }
            catch (ValidationException e)
            {
                //Log.Error("News Response", e);
                return BadRequest(e);
            }
            catch (Exception e)
            {
                //Log.Error("News Response", e);
                throw e;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                //Log.Information("News Response", News);
                return Ok(await _serviceUnitOfWork.News.Value.GetNews());
            }
            catch (ValidationException e)
            {
                //Log.Error("News Response", e);
                return BadRequest(e);
            }
            catch (Exception e)
            {
                //Log.Error("News Response", e);
                throw e;
            }
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Update([FromBody] News news)
        {

            try
            {
                return Ok(_serviceUnitOfWork.News.Value.Update(news));
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