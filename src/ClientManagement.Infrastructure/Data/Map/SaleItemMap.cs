using ClientManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Infrastructure.Data.Map
{
    public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItens");

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Id)
                .ValueGeneratedOnAdd();

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(si => si.DataCadastro)
                .IsRequired();

            builder.Property(si => si.Ativo)
                .IsRequired();

            builder.HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(si => si.Product)
                .WithMany(p => p.SaleItens)
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
