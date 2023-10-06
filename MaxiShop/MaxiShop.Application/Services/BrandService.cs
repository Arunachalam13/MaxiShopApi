using AutoMapper;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _BrandRepository;

        private readonly IMapper _mapper;

        public BrandService(IBrandRepository BrandRepository, IMapper mapper)
        {
            this._BrandRepository = BrandRepository;
            this._mapper = mapper;
        }
        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var Brand = _mapper.Map<Brand>(createBrandDto);
            var createdEntity = await _BrandRepository.CreateAsync(Brand);
            var entity = _mapper.Map<BrandDto>(createdEntity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var Brand = await _BrandRepository.GetByIdAsync(x => x.Id == id);
            await _BrandRepository.DeleteAsync(Brand);
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _BrandRepository.GetAllAsync();
            return _mapper.Map<List<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _BrandRepository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);
            await _BrandRepository.UpdateAsync(brand);
        }
    }
}
