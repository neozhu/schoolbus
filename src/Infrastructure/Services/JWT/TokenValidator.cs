﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Common.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Blazor.Infrastructure.Services.JWT;
/// <summary>
/// Use this class to validate refresh tokens
/// </summary>
public class TokenValidator : ITokenValidator
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly TokenValidationParameters _validationParameters;

    public TokenValidator(JwtSecurityTokenHandler tokenHandler, IOptions<SimpleJwtOptions> options)
    {
        _tokenHandler = tokenHandler;
        _validationParameters = options.Value.AccessValidationParameters!;

    }

    /// <summary>
    /// Validate a refresh token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<TokenValidationResult> ValidateTokenAsync(string token)
    {

        // if not exists: return invalid validation result.
        if (token is null)
        {
            return new TokenValidationResult
            {
                IsValid = false,
                Exception = new RefreshTokenNotFoundException("Refresh token might be invalid or expired"),
            };
        }

        // if jwt is invalid: delete token from db? and return validation result.
        var result = await _tokenHandler.ValidateTokenAsync(token, _validationParameters);
        if (!result.IsValid)
        {
            // delete token from db
            // ...
            return result;
        }

        return result;
    }
}
