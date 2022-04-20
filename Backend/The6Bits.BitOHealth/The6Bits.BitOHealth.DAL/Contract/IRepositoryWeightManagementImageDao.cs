using Microsoft.AspNetCore.Http;

namespace The6Bits.BitOHealth.DAL.Contract;

public interface IRepositoryWeightManagementImageDao<T>
{
    public Task<T> SaveImage(IFormFile file, string username);
    public Task<T> DeleteImage(string path, string username);

}