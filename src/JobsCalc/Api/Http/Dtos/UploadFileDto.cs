namespace JobsCalc.Api.Http.Dtos;
public class UploadFileGqlDto
{

  [GraphQLNonNullType]
  [GraphQLDescription("Arquivo de imagem para o avatar.")]
  public IFile? File { get; set; }

  public Stream GetStream()
  {
    if (File != null) return File.OpenReadStream();
    throw new ArgumentException("Nenhum arquivo válido foi fornecido.");
  }

  public string GetFileName()
  {
    if (File != null) return File.Name;
    throw new ArgumentException("Nenhum arquivo válido foi fornecido.");
  }
}
public class UploadFileRestDto
{
  public IFormFile? File { get; set; }
}