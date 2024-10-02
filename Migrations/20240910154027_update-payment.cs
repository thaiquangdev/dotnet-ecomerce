using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_mvc.Migrations
{
    /// <inheritdoc />
    public partial class updatepayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Carts_CartId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Users_UserId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetail_Carts_CartId",
                table: "ShippingDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetail_Users_UserId",
                table: "ShippingDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingDetail",
                table: "ShippingDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "ShippingDetail",
                newName: "ShippingDetails");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingDetail_UserId",
                table: "ShippingDetails",
                newName: "IX_ShippingDetails_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingDetail_CartId",
                table: "ShippingDetails",
                newName: "IX_ShippingDetails_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_UserId",
                table: "Payments",
                newName: "IX_Payments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_CartId",
                table: "Payments",
                newName: "IX_Payments_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingDetails",
                table: "ShippingDetails",
                column: "ShippingDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Carts_CartId",
                table: "Payments",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_Carts_CartId",
                table: "ShippingDetails",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_Users_UserId",
                table: "ShippingDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Carts_CartId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_Carts_CartId",
                table: "ShippingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_Users_UserId",
                table: "ShippingDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingDetails",
                table: "ShippingDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "ShippingDetails",
                newName: "ShippingDetail");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingDetails_UserId",
                table: "ShippingDetail",
                newName: "IX_ShippingDetail_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingDetails_CartId",
                table: "ShippingDetail",
                newName: "IX_ShippingDetail_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_UserId",
                table: "Payment",
                newName: "IX_Payment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CartId",
                table: "Payment",
                newName: "IX_Payment_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingDetail",
                table: "ShippingDetail",
                column: "ShippingDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Carts_CartId",
                table: "Payment",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Users_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetail_Carts_CartId",
                table: "ShippingDetail",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetail_Users_UserId",
                table: "ShippingDetail",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
