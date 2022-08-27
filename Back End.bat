cd .\Countries\

set ASPNETCORE_URLS=https://localhost:7257;http://localhost:5120 
set ASPNETCORE_ENVIRONMENT=Development 
set ASPNETCORE_HOSTINGSTARTUPASSEMBLIES=Microsoft.AspNetCore.SpaProxy

dotnet run --urls "https://localhost:7257;http://localhost:5120"