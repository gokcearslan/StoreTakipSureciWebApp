using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twotableversion.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionConcurrencyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Takvim",
                columns: table => new
                {
                    primarykey = table.Column<int>(type: "int", nullable: false),
                    AyId = table.Column<int>(type: "int", nullable: false),
                    AyAdı = table.Column<string>(name: "Ay Adı", type: "varchar(max)", nullable: true),
                    Uygulama = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takvim", x => x.primarykey);
                });

            migrationBuilder.CreateTable(
                name: "Uygulamalar",
                columns: table => new
                {
                    SatırId = table.Column<int>(name: "Satır Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(type: "varchar(max)", nullable: true),
                    UygulamaAdı = table.Column<string>(name: "Uygulama Adı", type: "varchar(max)", nullable: true),
                    TakvimId = table.Column<int>(name: "Takvim Id", type: "int", nullable: true),
                    EtkiAlanı = table.Column<string>(name: "Etki Alanı", type: "text", nullable: true),
                    TalepBug = table.Column<string>(name: "Talep/Bug", type: "text", nullable: true),
                    TalepAdı = table.Column<string>(name: "Talep Adı", type: "text", nullable: true),
                    BULGUDURUMU = table.Column<string>(name: "BULGU DURUMU", type: "text", nullable: true),
                    SEGMENT = table.Column<string>(type: "text", nullable: true),
                    KKTYEGÖNDERİLDİMİ = table.Column<string>(name: "KKTYE GÖNDERİLDİ Mİ?", type: "text", nullable: true),
                    KKTONAYIALINDIMI = table.Column<string>(name: "KKT ONAYI ALINDI MI", type: "text", nullable: true),
                    Notlar = table.Column<string>(type: "text", nullable: true),
                    İlgiliAnalist = table.Column<string>(name: "İlgili Analist", type: "text", nullable: true),
                    MERGEDURUMUIOS = table.Column<string>(name: "MERGE DURUMU IOS", type: "text", nullable: true),
                    MERGEDURUMUAND = table.Column<string>(name: "[MERGE DURUMU AND", type: "text", nullable: true),
                    MERGEDURUMUBE = table.Column<string>(name: "[MERGE DURUMU BE", type: "text", nullable: true),
                    İlgiliIOSDeveloper = table.Column<string>(name: "İlgili IOS Developer", type: "text", nullable: true),
                    İlgiliAndroidDeveloper = table.Column<string>(name: "İlgili Android Developer", type: "text", nullable: true),
                    İlgiliBEDeveloper = table.Column<string>(name: "İlgili BE Developer", type: "text", nullable: true),
                    BETaşımaKatmanları = table.Column<string>(name: "BE Taşıma Katmanları", type: "text", nullable: true),
                    GEÇİŞZORUNLULUĞU = table.Column<string>(name: "[GEÇİŞ ZORUNLULUĞU", type: "text", nullable: true),
                    UIAPISENARYOID = table.Column<int>(name: "UI/API SENARYO ID", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uygulamalar", x => x.SatırId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Takvim");

            migrationBuilder.DropTable(
                name: "Uygulamalar");
        }
    }
}
