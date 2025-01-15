using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JobsCalc.Api.Http.Filters;
public class FileUploadOperationFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    operation.RequestBody = new OpenApiRequestBody
    {
      Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties =
                        {
                            ["file"] = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        }
                    }
                }
            }
    };
  }
}
