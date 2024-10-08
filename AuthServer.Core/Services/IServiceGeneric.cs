﻿using SharedLibrary.Dtos;
using System.Linq.Expressions;

namespace AuthServer.Core.Services;

// Dto'nun ve Entity'nin tipinin bir class olduğunu declere ediyoruz.
// Entityleri Dto'ya cast ettik
public interface IGenericService<TEntity,TDto> where TEntity : class where TDto : class
{
    Task<Response<TDto>> GetByIdAsync(int id);
    Task<Response<IEnumerable<TDto>>> GetAllAsync();
    Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
    Task <Response<TDto>> AddAsync(TDto entity);

    Task<Response<NoDataDto>> Remove(int id);

    Task<Response<NoDataDto>> Update(TDto entity, int id);

}
