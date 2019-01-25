using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.WebApi.Data
{
    public interface ICourseRepository
    {
        Task<Course> AddAsync(
            Course course);

        Task<Course> AddOrUpdateAsync(
            Course course);

        Task<Course> DeleteByIdAsync(
            Guid id);

        Task<Course> FetchByIdAsync(
            Guid id);

        Task<Course> FetchByUserNameAsync(
            string userName);

        Task<IEnumerable<Course>> FetchListAsync();

        Task<IEnumerable<Course>> FetchListAsync(
            Guid[] ids);

        Task<Course> UpdateByIdAsync(
            Guid id,
            Course course);
    }
}
