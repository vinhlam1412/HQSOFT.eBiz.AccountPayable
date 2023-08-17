using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HQSOFT.eBiz.AccountPayable.Migrations
{
    /// <inheritdoc />
    public partial class AddTableVendorClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "APVendorClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorClassCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentMethodCode = table.Column<Guid>(type: "uuid", nullable: false),
                    CashAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    TermId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiptAction = table.Column<string>(type: "text", nullable: true),
                    APAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    APCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpeneseAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpeneseCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    CashDiscountAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    CashDiscountCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    PrepaymentAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    PrepaymentCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    ReclassificationAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    ReclassificationCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    POAccrualAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    POAccrualCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    UnrealizedGaintAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    UnrealizedGaintCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    UnrealizedGaintLossAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    UnrealizedGaintLossCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    RetainagePayableAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    RetainagePayableCostCenter = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APVendorClasses", x => x.Id);
                });
       
            migrationBuilder.CreateTable(
                name: "APVendorClassAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IdAttribute = table.Column<Guid>(type: "uuid", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsInternal = table.Column<bool>(type: "boolean", nullable: false),
                    ControlType = table.Column<string>(type: "text", nullable: true),
                    DefaultValue = table.Column<string>(type: "text", nullable: true),
                    Idx = table.Column<int>(type: "integer", nullable: false),
                    VendorClassId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APVendorClassAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APVendorClassAttributes_APVendorClasses_VendorClassId",
                        column: x => x.VendorClassId,
                        principalTable: "APVendorClasses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "APVendorClassCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Exclude = table.Column<bool>(type: "boolean", nullable: false),
                    Idx = table.Column<int>(type: "integer", nullable: false),
                    VendorClassId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APVendorClassCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_APVendorClassCompanies_APVendorClasses_VendorClassId",
                        column: x => x.VendorClassId,
                        principalTable: "APVendorClasses",
                        principalColumn: "Id");
                });
          

            migrationBuilder.CreateIndex(
                name: "IX_APVendorClassAttributes_VendorClassId",
                table: "APVendorClassAttributes",
                column: "VendorClassId");

            migrationBuilder.CreateIndex(
                name: "IX_APVendorClassCompanies_VendorClassId",
                table: "APVendorClassCompanies",
                column: "VendorClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DropTable(
                name: "APVendorClassAttributes");

            migrationBuilder.DropTable(
                name: "APVendorClassCompanies");
      
            migrationBuilder.DropTable(
                name: "APVendorClasses");

        }
    }
}
