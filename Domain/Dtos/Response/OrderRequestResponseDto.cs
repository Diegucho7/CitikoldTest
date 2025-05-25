namespace RetailCitikold.Domain.Dtos.Response;


public record OrderRequestResponseDto(bool IsSuccess = true, string Error = "");
