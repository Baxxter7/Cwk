namespace Cwk.Application.Interfaces;

public interface IPhotoService
{
    Task<string> UploadPhotoAsync(string imageBase64);
    Task<string> DeletePhotoAsync(string publicId);
}