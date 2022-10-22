using Brazil.Api.Integration.Models;
using Brazil.Api.Integration.Models.CompanyService;

namespace Brazil.Api.Integration.Converter
{
    public static class CompanyConverter
    {
        public static CompanyResponse Success(this Company company)
            => new()
            {
                Success = true,
                Company = company
            };

        public static CompanyResponse CompanyUnsuccessfully(this MessageError messageError)
            => new()
            {
                Success = false,
                Message = messageError.Message
            };
    }
}
