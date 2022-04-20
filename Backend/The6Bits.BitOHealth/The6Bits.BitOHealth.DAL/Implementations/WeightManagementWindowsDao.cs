using System.IO.Enumeration;
using Microsoft.AspNetCore.Http;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models.WeightManagement;

namespace The6Bits.BitOHealth.DAL.Implementations;

public class WeightManagementWindowsDao : IRepositoryWeightManagementImageDao<IWeightManagerResponse>
{

    private string _filePath;
    public WeightManagementWindowsDao(string path)
    {
        _filePath = path;
    }

    public async Task<IWeightManagerResponse> SaveImage(IFormFile file, string username)
    {
        try
        {
            _filePath = _filePath + username + @"\";
            _filePath = Environment.ExpandEnvironmentVariables(_filePath);


            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }

            _filePath = Path.Combine(_filePath, file.FileName);


            await using (Stream fileStream = new FileStream(_filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }


            return new WeightManagerResponse(_filePath);
        }
        catch(Exception ex)
        {
            return new WeightManagerResponse(ex.Message);
        }


    }


    public Task<IWeightManagerResponse> DeleteImage(string path, string username)
    {
        try
        {
            System.IO.File.Delete(path);
            return Task.FromResult<IWeightManagerResponse>(new WeightManagerResponse("Successfully Deleted File"));

        }
        catch (Exception ex)
        {
            return Task.FromResult<IWeightManagerResponse>(new WeightManagerResponse(ex.Message,true));
        }
    }



}