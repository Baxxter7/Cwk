namespace Cwk.Application.Interfaces;

public interface IPhotoService
{
    Task<string> UploadImageAsync(string imageBase64);
    Task<string> DeletePhotoAsync(string publicId);
}