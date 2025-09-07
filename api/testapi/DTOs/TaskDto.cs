// DTOs/TaskDto.cs
namespace testapi.DTOs;

public record TaskDto(
    int ProdataID,
    string Register,
    string Diagnostic,
    string Packaging,
    List<TagDto> Tags
);
