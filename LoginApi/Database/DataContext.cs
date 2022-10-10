using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoginApi.Models;

namespace LoginApi.Database;

public class DataContext: IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){ }

}
