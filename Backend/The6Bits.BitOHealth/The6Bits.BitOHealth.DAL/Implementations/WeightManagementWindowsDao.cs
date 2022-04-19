using System.IO.Enumeration;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations;

public class WeightManagementWindowsDao : IRepositoryWeightManagementImageDao
{
    private string filepath;
    public WeightManagementWindowsDao()
    {
        filepath = AppDomain.CurrentDomain.BaseDirectory;
    }

    public string SaveImage(byte[] file, string filetype)
    {
        
        File.WriteAllBytes("Foo"+filetype, file);
        
        
        return filepath;
        throw new NotImplementedException();
    }

    public byte[] ServeImage(string filepath)
    {

            byte[] fileData = System.IO.File.ReadAllBytes(filepath);
            return fileData;
        

    }
}