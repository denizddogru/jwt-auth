﻿using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;

namespace AuthServer.Core.Services;
public interface ITokenService
{
    TokenDto CreateToken(AppUser appUser);
    ClientTokenDto CreateTokenByClient(Client client);
}
