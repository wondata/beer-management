using BeerManagement.Application.Entities;
using BeerManagement.Application.Interfaces;
using BeerManagement.Application.Models;
using BeerManagement.WebApi.Models.RequestModel;
using BeerManagement.WebApi.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeerManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;
        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet("GetBeers")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BeerEntity>>>> GetBeers([FromQuery] string? searchParam = null)
        {
            try
            {
                var beers = await _beerService.GetAllBeers(searchParam);
                return Ok(new ApiResponse<IEnumerable<BeerEntity>>(beers, false, "Getting Beers data is successful."));
            }
            catch (Exception ex)
            {
                //Exception data can be logged here(i.e DB, file, Seq, etc)
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse<IEnumerable<BeerEntity>>(true, message));
            }
            
        }

        [HttpPost("AddBeer")]
        public async Task<ActionResult<ApiResponse<bool>>> AddBeer(AddBeerRequestModel rm)
        {
            try
            {
                await _beerService.AddBeer(rm.MapToEntity());

                return Ok(new ApiResponse<bool>(true, false, "Beer data added successfully."));
            }
            catch (Exception ex)
            {
                //Exception data can be logged here(i.e DB, file, Seq, etc)
                var message = ex.InnerException == null? ex.Message : ex.InnerException.Message;
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse<bool>(true, message));

            }

        }

        [HttpPost("UpdateBeerRating")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBeerRating(UpdateRatingRequestModel rm)
        {
            try
            {
                await _beerService.UpdateRating(rm.Id);

                return Ok(new ApiResponse<bool>(true, false, "Beer rating updated successfully."));
            }
            catch (Exception ex)
            {
                //Exception data can be logged here(i.e DB, file, Seq, etc)
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse<bool>(true, message));

            }

        }
    }
}
