using MaxiShop.Application.ApplicationConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _BrandService;
        protected APIResponse _response;

        public BrandController(IBrandService BrandService)
        {
            this._BrandService = BrandService;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var brands = await _BrandService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brands;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("Details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                var brand = await _BrandService.GetByIdAsync(id);
                if (brand is null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.RecordNotFound;
                    return _response;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brand;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateBrandDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return _response;
                }

                var entity = await _BrandService.CreateAsync(dto);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.CreateOperationSuccess;
                _response.Result = entity;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.CreateOperationFailed;
                _response.AddError(CommanMessage.SystemError);
            }

            return Ok(_response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return _response;
                }
                var Brand = await _BrandService.GetByIdAsync(dto.Id);
                if (Brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                    return _response;
                }
                await _BrandService.UpdateAsync(dto);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.UpdateOperationSuccess;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                _response.AddError(CommanMessage.SystemError);
            }

            return Ok(_response);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.DeletedOperationFailed;
                    return _response;
                }
                var Brand = await _BrandService.GetByIdAsync(id);
                if (Brand is null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.DeletedOperationFailed;
                    return _response;
                }
                await _BrandService.DeleteAsync(id);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.DisplayMessage = CommanMessage.DeleteOperationSuccess;
                _response.AddError(ModelState.ToString());
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.DeletedOperationFailed;
                _response.AddError(CommanMessage.SystemError);
            }

            return Ok(_response);
        }
    }
}
