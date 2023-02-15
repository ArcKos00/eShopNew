using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Host.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "abnormal_type_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "anomaly_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "artefact_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "characteristic_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "frequency_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "location_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "AbnormalType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbnormalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Radiation = table.Column<int>(type: "integer", nullable: false),
                    Restoration = table.Column<int>(type: "integer", nullable: false),
                    RestorationHealth = table.Column<int>(type: "integer", nullable: false),
                    WoundHealing = table.Column<int>(type: "integer", nullable: false),
                    MaximumWeight = table.Column<int>(type: "integer", nullable: false),
                    ProtectionDogs = table.Column<int>(type: "integer", nullable: false),
                    ThermalProtection = table.Column<int>(type: "integer", nullable: false),
                    ChemicalProtection = table.Column<int>(type: "integer", nullable: false),
                    ElectricalProtection = table.Column<int>(type: "integer", nullable: false),
                    Saturation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Place = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Meets = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Anomaly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AbnormalTypeId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    FrequencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anomaly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anomaly_AbnormalType_AbnormalTypeId",
                        column: x => x.AbnormalTypeId,
                        principalTable: "AbnormalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anomaly_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anomaly_Meets_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Meets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artefact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    Nature = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    AnomalyId = table.Column<int>(type: "integer", nullable: false),
                    AbnormalTypeId = table.Column<int>(type: "integer", nullable: false),
                    FrequencyId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artefact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artefact_AbnormalType_AbnormalTypeId",
                        column: x => x.AbnormalTypeId,
                        principalTable: "AbnormalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artefact_Anomaly_AnomalyId",
                        column: x => x.AnomalyId,
                        principalTable: "Anomaly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artefact_Characteristic_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artefact_Meets_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Meets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anomaly_AbnormalTypeId",
                table: "Anomaly",
                column: "AbnormalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Anomaly_FrequencyId",
                table: "Anomaly",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Anomaly_LocationId",
                table: "Anomaly",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Artefact_AbnormalTypeId",
                table: "Artefact",
                column: "AbnormalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Artefact_AnomalyId",
                table: "Artefact",
                column: "AnomalyId");

            migrationBuilder.CreateIndex(
                name: "IX_Artefact_CharacteristicId",
                table: "Artefact",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_Artefact_FrequencyId",
                table: "Artefact",
                column: "FrequencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artefact");

            migrationBuilder.DropTable(
                name: "Anomaly");

            migrationBuilder.DropTable(
                name: "Characteristic");

            migrationBuilder.DropTable(
                name: "AbnormalType");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Meets");

            migrationBuilder.DropSequence(
                name: "abnormal_type_hilo");

            migrationBuilder.DropSequence(
                name: "anomaly_hilo");

            migrationBuilder.DropSequence(
                name: "artefact_hilo");

            migrationBuilder.DropSequence(
                name: "characteristic_hilo");

            migrationBuilder.DropSequence(
                name: "frequency_hilo");

            migrationBuilder.DropSequence(
                name: "location_hilo");
        }
    }
}
