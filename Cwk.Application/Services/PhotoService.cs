using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Cwk.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cwk.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IConfiguration configuration)
    {
        var account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]);
        
        _cloudinary = new Cloudinary(account);
    }
    
    public async Task<string> UploadImageAsync(string imageBase64)
    {
        if (string.IsNullOrWhiteSpace(imageBase64))
            throw new ArgumentException("La imagen base64 es inválida o está vacía.", nameof(imageBase64)); 
        
        var base64Data = imageBase64[(imageBase64.IndexOf(',') + 1)..];
        var imageBytes = Convert.FromBase64String(base64Data);

        using var stream = new MemoryStream(imageBytes);
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription("image.jpg", stream),
            AssetFolder = "josue"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            throw new InvalidOperationException("Error al cargar la imagen: " + uploadResult.Error.Message);
        }

        var urlFoto = uploadResult.SecureUrl.ToString();
        return urlFoto;
    }
    
    public async Task<string> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result == "ok" ? result.Result : null!;
    }
}