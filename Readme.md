
RATES:
dotnet ef dbcontext scaffold "Host=localhost;Port=5433;Database=rates_db;Username=admin;Password=admin" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models/Rates --context RatesContext --force   --no-onconfiguring

POSITIONS:
dotnet ef dbcontext scaffold "Host=localhost;Port=5434;Database=positions_db;Username=admin;Password=admin" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models/Positions --context PositionsContext --force   --no-onconfiguring
