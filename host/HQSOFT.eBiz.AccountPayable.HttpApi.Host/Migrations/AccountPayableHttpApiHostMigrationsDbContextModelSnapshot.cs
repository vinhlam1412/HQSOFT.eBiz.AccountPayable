﻿// <auto-generated />
using System;
using HQSOFT.eBiz.AccountPayable.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace HQSOFT.eBiz.AccountPayable.Migrations
{
    [DbContext(typeof(AccountPayableHttpApiHostMigrationsDbContext))]
    partial class AccountPayableHttpApiHostMigrationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.PostgreSql)
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HQSOFT.eBiz.AccountPayable.VendorClassAttributes.VendorClassAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("ControlType")
                        .HasColumnType("text")
                        .HasColumnName("ControlType");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("text")
                        .HasColumnName("DefaultValue");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("IdAttribute")
                        .HasColumnType("text")
                        .HasColumnName("IdAttribute");

                    b.Property<int>("Idx")
                        .HasColumnType("integer")
                        .HasColumnName("Idx");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsInternal")
                        .HasColumnType("boolean")
                        .HasColumnName("IsInternal");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("IsRequired");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer")
                        .HasColumnName("SortOrder");

                    b.Property<Guid?>("VendorClassId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VendorClassId");

                    b.ToTable("APVendorClassAttributes", (string)null);
                });

            modelBuilder.Entity("HQSOFT.eBiz.AccountPayable.VendorClassCompanies.VendorClassCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasColumnName("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<bool>("Exclude")
                        .HasColumnType("boolean")
                        .HasColumnName("Exclude");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<int>("Idx")
                        .HasColumnType("integer")
                        .HasColumnName("Idx");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<Guid?>("VendorClassId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VendorClassId");

                    b.ToTable("APVendorClassCompanies", (string)null);
                });

            modelBuilder.Entity("HQSOFT.eBiz.AccountPayable.VendorClasses.VendorClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("APAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("APAccount");

                    b.Property<Guid>("APCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("APCostCenter");

                    b.Property<Guid>("CashAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("CashAccount");

                    b.Property<Guid>("CashDiscountAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("CashDiscountAccount");

                    b.Property<Guid>("CashDiscountCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("CashDiscountCostCenter");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<Guid>("Country")
                        .HasColumnType("uuid")
                        .HasColumnName("Country");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<Guid>("Currency")
                        .HasColumnType("uuid")
                        .HasColumnName("Currency");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<Guid>("DiscountAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("DiscountAccount");

                    b.Property<Guid>("DiscountCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("DiscountCostCenter");

                    b.Property<Guid>("ExpeneseAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("ExpeneseAccount");

                    b.Property<Guid>("ExpeneseCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("ExpeneseCostCenter");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<Guid>("POAccrualAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("POAccrualAccount");

                    b.Property<Guid>("POAccrualCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("POAccrualCostCenter");

                    b.Property<Guid>("PaymentMethodCode")
                        .HasColumnType("uuid")
                        .HasColumnName("PaymentMethodCode");

                    b.Property<Guid>("PrepaymentAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("PrepaymentAccount");

                    b.Property<Guid>("PrepaymentCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("PrepaymentCostCenter");

                    b.Property<string>("ReceiptAction")
                        .HasColumnType("text")
                        .HasColumnName("ReceiptAction");

                    b.Property<Guid>("ReclassificationAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("ReclassificationAccount");

                    b.Property<Guid>("ReclassificationCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("ReclassificationCostCenter");

                    b.Property<Guid>("RetainagePayableAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("RetainagePayableAccount");

                    b.Property<Guid>("RetainagePayableCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("RetainagePayableCostCenter");

                    b.Property<Guid>("TermId")
                        .HasColumnType("uuid")
                        .HasColumnName("TermId");

                    b.Property<Guid>("UnrealizedGaintAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("UnrealizedGaintAccount");

                    b.Property<Guid>("UnrealizedGaintCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("UnrealizedGaintCostCenter");

                    b.Property<Guid>("UnrealizedGaintLossAccount")
                        .HasColumnType("uuid")
                        .HasColumnName("UnrealizedGaintLossAccount");

                    b.Property<Guid>("UnrealizedGaintLossCostCenter")
                        .HasColumnType("uuid")
                        .HasColumnName("UnrealizedGaintLossCostCenter");

                    b.Property<string>("VendorClassCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("VendorClassCode");

                    b.HasKey("Id");

                    b.ToTable("APVendorClasses", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicationName")
                        .HasMaxLength(96)
                        .HasColumnType("character varying(96)")
                        .HasColumnName("ApplicationName");

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("BrowserInfo");

                    b.Property<string>("ClientId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("ClientId");

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("ClientIpAddress");

                    b.Property<string>("ClientName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("ClientName");

                    b.Property<string>("Comments")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("Comments");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("CorrelationId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("CorrelationId");

                    b.Property<string>("Exceptions")
                        .HasColumnType("text");

                    b.Property<int>("ExecutionDuration")
                        .HasColumnType("integer")
                        .HasColumnName("ExecutionDuration");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("HttpMethod")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("HttpMethod");

                    b.Property<int?>("HttpStatusCode")
                        .HasColumnType("integer")
                        .HasColumnName("HttpStatusCode");

                    b.Property<Guid?>("ImpersonatorTenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("ImpersonatorTenantId");

                    b.Property<string>("ImpersonatorTenantName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("ImpersonatorTenantName");

                    b.Property<Guid?>("ImpersonatorUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("ImpersonatorUserId");

                    b.Property<string>("ImpersonatorUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("ImpersonatorUserName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.Property<string>("TenantName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("TenantName");

                    b.Property<string>("Url")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("Url");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("UserId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "ExecutionTime");

                    b.HasIndex("TenantId", "UserId", "ExecutionTime");

                    b.ToTable("AbpAuditLogs", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLogAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuditLogId")
                        .HasColumnType("uuid")
                        .HasColumnName("AuditLogId");

                    b.Property<int>("ExecutionDuration")
                        .HasColumnType("integer")
                        .HasColumnName("ExecutionDuration");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("ExecutionTime");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("MethodName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("MethodName");

                    b.Property<string>("Parameters")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("Parameters");

                    b.Property<string>("ServiceName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("ServiceName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.HasIndex("TenantId", "ServiceName", "MethodName", "ExecutionTime");

                    b.ToTable("AbpAuditLogActions", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuditLogId")
                        .HasColumnType("uuid")
                        .HasColumnName("AuditLogId");

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("ChangeTime");

                    b.Property<byte>("ChangeType")
                        .HasColumnType("smallint")
                        .HasColumnName("ChangeType");

                    b.Property<string>("EntityId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("EntityId");

                    b.Property<Guid?>("EntityTenantId")
                        .HasColumnType("uuid");

                    b.Property<string>("EntityTypeFullName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("EntityTypeFullName");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.HasIndex("TenantId", "EntityTypeFullName", "EntityId");

                    b.ToTable("AbpEntityChanges", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityPropertyChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityChangeId")
                        .HasColumnType("uuid");

                    b.Property<string>("NewValue")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("NewValue");

                    b.Property<string>("OriginalValue")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("OriginalValue");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("PropertyName");

                    b.Property<string>("PropertyTypeFullName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("PropertyTypeFullName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("EntityChangeId");

                    b.ToTable("AbpEntityPropertyChanges", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.PermissionManagement.PermissionDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<byte>("MultiTenancySide")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ParentName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Providers")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("StateCheckers")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("GroupName");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpPermissions", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.PermissionManagement.PermissionGrant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Name", "ProviderName", "ProviderKey")
                        .IsUnique();

                    b.ToTable("AbpPermissionGrants", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.PermissionManagement.PermissionGroupDefinitionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AbpPermissionGroups", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.SettingManagement.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("ProviderName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "ProviderName", "ProviderKey")
                        .IsUnique();

                    b.ToTable("AbpSettings", (string)null);
                });

            modelBuilder.Entity("HQSOFT.eBiz.AccountPayable.VendorClassAttributes.VendorClassAttribute", b =>
                {
                    b.HasOne("HQSOFT.eBiz.AccountPayable.VendorClasses.VendorClass", null)
                        .WithMany()
                        .HasForeignKey("VendorClassId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("HQSOFT.eBiz.AccountPayable.VendorClassCompanies.VendorClassCompany", b =>
                {
                    b.HasOne("HQSOFT.eBiz.AccountPayable.VendorClasses.VendorClass", null)
                        .WithMany()
                        .HasForeignKey("VendorClassId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLogAction", b =>
                {
                    b.HasOne("Volo.Abp.AuditLogging.AuditLog", null)
                        .WithMany("Actions")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityChange", b =>
                {
                    b.HasOne("Volo.Abp.AuditLogging.AuditLog", null)
                        .WithMany("EntityChanges")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityPropertyChange", b =>
                {
                    b.HasOne("Volo.Abp.AuditLogging.EntityChange", null)
                        .WithMany("PropertyChanges")
                        .HasForeignKey("EntityChangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLog", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("EntityChanges");
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityChange", b =>
                {
                    b.Navigation("PropertyChanges");
                });
#pragma warning restore 612, 618
        }
    }
}
