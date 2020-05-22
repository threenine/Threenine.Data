/* Copyright (c) threenine.co.uk . All rights reserved.
 
   GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Microsoft.EntityFrameworkCore;

namespace TestDatabase
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TestCategory> TestCategories { get; set; }
        public virtual DbSet<TestProduct> TestProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestCategory>(entity =>
            {
                entity.ToTable("TestCategory");
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasAnnotation("Sqlite:Autoincrement", true)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TestProduct>(entity =>
            {
                entity.ToTable("TestProduct");
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasAnnotation("Sqlite:Autoincrement", true)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.CategoryId).HasName("testCategory_testCategory_id_foreign");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Stock)
                    .HasColumnName("Stock")
                    .HasColumnType("int");

                entity.Property(e => e.InStock)
                    .HasColumnName("inStock")
                    .HasColumnType("bit")
                    .HasDefaultValue(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("testCategory_testCategory_id_foreign");
            });
        }
    }
}