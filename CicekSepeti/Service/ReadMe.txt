1. Install Package Microsoft.EntityFrameworkCore.SqlServer
2. Install EntityFrameworkCore CLI Global dotnet tool install --global dotnet-ef
3. Install Package Microsoft.EntityFrameworkCore.Design
4. Create Migrations dotnet ef migrations add InitialCreate
5. Create Database dotnet ef database update


6. Many to Many Example

	 modelBuilder.Entity<UserRoles>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<UserRoles>()
                .HasOne(f => f.User)
                .WithMany(f => f.UserRoles)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(f => f.Role)
                .WithMany(f => f.UserRoles)
                .HasForeignKey(f => f.RoleId);