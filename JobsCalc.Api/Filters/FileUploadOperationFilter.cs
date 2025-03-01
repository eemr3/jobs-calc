using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class FileUploadOperationFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    // Verifica se o método HTTP é POST ou PUT
    if (context.ApiDescription.HttpMethod == "POST" || context.ApiDescription.HttpMethod == "PUT")
    {
      // Configura o RequestBody para suportar multipart/form-data
      if (operation.RequestBody == null)
      {
        operation.RequestBody = new OpenApiRequestBody
        {
          Content = new Dictionary<string, OpenApiMediaType>
          {
            ["multipart/form-data"] = new OpenApiMediaType
            {
              Schema = new OpenApiSchema
              {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
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
  }
}