# AMADotNetCore


TrustServerCertificate=True;
dotnet ef dbcontext scaffold "Server=.;Database=AMADotNetCore;User ID=sa;Password=sa@123;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -c AppDbContext -o EFDbContextModel


Nuget Package = npm