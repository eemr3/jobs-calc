namespace JobsCalc.Communication.DTOs.Requests;

public class FileUploadRequest
{
  public Stream? FileStream { get; set; }
  public string? FileName { get; set; }
}