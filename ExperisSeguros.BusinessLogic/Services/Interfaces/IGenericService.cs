using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExperisSeguros.BusinessLogic.Services.Interfaces
{
    public interface IGenericService<TCreateDto, TReadDto, TUpdateDto>
        where TCreateDto : class
        where TReadDto : class
        where TUpdateDto : class
    {
        Task<List<TReadDto>> GetAllAsync();
        Task<TReadDto> GetByIdAsync(int id);
        Task CreateAsync(TCreateDto createDto);
        Task UpdateAsync(int id, TUpdateDto updateDto);
        Task DeleteAsync(int id);
    }
}
