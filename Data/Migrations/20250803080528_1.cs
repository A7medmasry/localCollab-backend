using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tiktok_local_linkup_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordResetRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Token = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetRequests", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    LastName = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Slug = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Password = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Country = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    City = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Address = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Avatar = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Bio = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Gender = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    CollaborationTypes = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Phone = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastEditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EditedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    BusinessInformation_Name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_Location = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_Type = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_Size = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_Website = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_Contact = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_Logo = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_VerificationDocuments = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    BusinessInformation_IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creator_ShowFollowerCountPublicly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Creator_IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusUser_TopRated = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    StatusUser_TopRatedStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusUser_Reliable = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    StatusUser_ReliableStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusUser_FastResponder = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    StatusUser_FastResponderStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusUser_ResponseRate = table.Column<double>(type: "double", nullable: false),
                    StatusUser_ShowUpRate = table.Column<double>(type: "double", nullable: false),
                    StripeCustomerId = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    SubscriptionId = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ValidCredits = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Plan = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    User1Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    User2Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRooms_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRooms_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatorUserModelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Handle = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Subscribers = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Status = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Connect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => new { x.CreatorUserModelId, x.Id });
                    table.ForeignKey(
                        name: "FK_Platforms_Users_CreatorUserModelId",
                        column: x => x.CreatorUserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "ServiceProvider",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserModelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    StartingRate = table.Column<double>(type: "double", nullable: false),
                    Link = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    UploadFile = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider", x => new { x.UserModelId, x.Id });
                    table.ForeignKey(
                        name: "FK_ServiceProvider_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_general_ci"),
                    Description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Category = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Location = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    CompensationType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    CompensationAmount = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    CompensationCurrency = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    compensationProduct = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    FollowerRequirement = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Duration = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Requirements = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Views = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AuthToken = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    AuthTokenCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuthTokenExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    RefreshTokenCreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RefreshTokenExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    UserAgent = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    LastLogin = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AuthTokenRefreshes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChatRoomId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SenderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Message = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    SentAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "ServiceProviderCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServiceProviderUserModelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServiceProviderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviderCategory", x => new { x.ServiceProviderUserModelId, x.ServiceProviderId, x.Id });
                    table.ForeignKey(
                        name: "FK_ServiceProviderCategory_ServiceProvider_ServiceProviderUserM~",
                        columns: x => new { x.ServiceProviderUserModelId, x.ServiceProviderId },
                        principalTable: "ServiceProvider",
                        principalColumns: new[] { "UserModelId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServiceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FromUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ToUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FromUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ToUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CommunicationRating = table.Column<double>(type: "double", nullable: false),
                    DeliveryRating = table.Column<double>(type: "double", nullable: false),
                    QualityRating = table.Column<double>(type: "double", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci"),
                    ProviderComment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci"),
                    ProviderRating = table.Column<double>(type: "double", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_User1Id",
                table: "ChatRooms",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_User2Id",
                table: "ChatRooms",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FromUserId",
                table: "Orders",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ToUserId",
                table: "Orders",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FromUserId",
                table: "Reviews",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ToUserId",
                table: "Reviews",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Slug",
                table: "Services",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserModelId",
                table: "Services",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "PasswordResetRequests");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ServiceProviderCategory");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ServiceProvider");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
