namespace The6Bits.BitOHealth.DAL.Contract;

public interface IRepositoryWeightManagementImageDao
{
    public string SaveImage(byte[] file, string filetype);

    public byte[] ServeImage(string filepath);
}