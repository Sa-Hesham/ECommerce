using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.IdentityDto;

public record UserRegisterRequest
{
    public string Username { get; init; } = null!;

    [DataType(DataType.Password)]
    [RegularExpression("(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}")]
    public string Password { get; init; } = null!;
    [EmailAddress]
    public string Email { get; init; } = null!;

    public string DisplayName { get; init; } = null!;   


}
