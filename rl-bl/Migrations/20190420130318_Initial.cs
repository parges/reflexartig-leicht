using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kubaapi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Tele = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    AnamneseDate = table.Column<DateTime>(nullable: true),
                    AnamnesePayed = table.Column<bool>(nullable: true),
                    DiagnostikDate = table.Column<DateTime>(nullable: true),
                    DiagnostikPayed = table.Column<bool>(nullable: true),
                    ElternDate = table.Column<DateTime>(nullable: true),
                    ElternPayed = table.Column<bool>(nullable: true),
                    ProblemHierarchy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Anamnesen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    CountOfPositivAnswers = table.Column<int>(nullable: true),
                    PatientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anamnesen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anamnesen_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Payed = table.Column<bool>(nullable: false),
                    Exercises = table.Column<string>(nullable: true),
                    Reasons = table.Column<string>(nullable: true),
                    ObservationsParents = table.Column<string>(nullable: true),
                    ObservationsChild = table.Column<string>(nullable: true),
                    ExerciseAccomplishment = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Testungen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PatientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testungen_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnamneseChapters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AnamneseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnamneseChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnamneseChapters_Anamnesen_AnamneseId",
                        column: x => x.AnamneseId,
                        principalTable: "Anamnesen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProblemHierarchie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InitialValue = table.Column<string>(nullable: true),
                    ChangedValue = table.Column<string>(nullable: true),
                    ReviewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemHierarchie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemHierarchie_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewChapters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    ReviewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewChapters_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestungChapters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    TestungId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestungChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestungChapters_Testungen_TestungId",
                        column: x => x.TestungId,
                        principalTable: "Testungen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnamneseQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    MetaInfo = table.Column<string>(nullable: true),
                    TextPrefix = table.Column<string>(nullable: true),
                    TextValue = table.Column<string>(nullable: true),
                    AnamneseChapterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnamneseQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnamneseQuestions_AnamneseChapters_AnamneseChapterId",
                        column: x => x.AnamneseChapterId,
                        principalTable: "AnamneseChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ReviewChapterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewQuestion_ReviewChapters_ReviewChapterId",
                        column: x => x.ReviewChapterId,
                        principalTable: "ReviewChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestungQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    TestungChapterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestungQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestungQuestions_TestungChapters_TestungChapterId",
                        column: x => x.TestungChapterId,
                        principalTable: "TestungChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "AnamneseDate", "AnamnesePayed", "Avatar", "Birthday", "DiagnostikDate", "DiagnostikPayed", "ElternDate", "ElternPayed", "Firstname", "Lastname", "ProblemHierarchy", "Tele" },
                values: new object[] { 1, null, null, null, null, new DateTime(1988, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Kleiner", "Hase", null, "0177123456" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "AnamneseDate", "AnamnesePayed", "Avatar", "Birthday", "DiagnostikDate", "DiagnostikPayed", "ElternDate", "ElternPayed", "Firstname", "Lastname", "ProblemHierarchy", "Tele" },
                values: new object[] { 2, null, null, null, null, new DateTime(1988, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Stefan", "Parge", null, "0177123457" });

            migrationBuilder.InsertData(
                table: "Anamnesen",
                columns: new[] { "Id", "CountOfPositivAnswers", "Date", "Name", "PatientId" },
                values: new object[] { 1, -1, new DateTime(2019, 4, 20, 15, 3, 18, 379, DateTimeKind.Local), "Anamnese (Fragebogen / Kinder)", 1 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Date", "ExerciseAccomplishment", "Exercises", "Name", "ObservationsChild", "ObservationsParents", "PatientId", "Payed", "Reasons" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium ...", "Liegestütze und dann Kaffee trinken", "Befundgespräch", "Meinung Kind", "Meinung Eltern", 1, true, "Das war dringend notwendig" },
                    { 2, new DateTime(2019, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium ...", "Liegestütze und dann Kaffee trinken", "1. Review", "Meinung Kind", "Meinung Eltern", 1, true, "Das war dringend notwendig" },
                    { 3, new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium ...", "Liegestütze und dann Kaffee trinken", "2. Review", "Meinung Kind", "Meinung Eltern", 1, false, "Das war dringend notwendig" },
                    { 4, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium ...", "Liegestütze und dann Kaffee trinken", "Befundgespräch", "Meinung Kind", "Meinung Eltern", 2, true, "Das war dringend notwendig" },
                    { 5, new DateTime(2019, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium ...", "Liegestütze und dann Kaffee trinken", "1. Review", "Meinung Kind", "Meinung Eltern", 2, false, "Das war dringend notwendig" },
                    { 6, new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium ...", "Liegestütze und dann Kaffee trinken", "2. Review", "Meinung Kind", "Meinung Eltern", 2, false, "Das war dringend notwendig" }
                });

            migrationBuilder.InsertData(
                table: "Testungen",
                columns: new[] { "Id", "Date", "Name", "PatientId" },
                values: new object[] { 1, new DateTime(2019, 4, 20, 15, 3, 18, 368, DateTimeKind.Local), "Erste Testung", 1 });

            migrationBuilder.InsertData(
                table: "AnamneseChapters",
                columns: new[] { "Id", "AnamneseId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "0. Allgemeines" },
                    { 2, 1, "I. Schwangerschaft" },
                    { 3, 1, "II. Geburt" },
                    { 4, 1, "- ENDE 1 Teil(7 - 8 JA Antworten) + Frage 18./ 22.a) / 24. -> Bitte beantworten Sie die folgenden Fragen, bis sie altersgemäß nicht mehr zutreffen - " },
                    { 5, 1, "III. Schulzeit: 6. – 8. Lebensjahr" },
                    { 6, 1, "IV. 8. - 10. Lebensjahr" },
                    { 7, 1, "V. Zusätzliche Angaben" }
                });

            migrationBuilder.InsertData(
                table: "TestungChapters",
                columns: new[] { "Id", "Name", "Score", "TestungId" },
                values: new object[,]
                {
                    { 11, "XI. VISUELLE WAHRNEHMUNGSÜBERPRÜFUNG", null, 1 },
                    { 10, "X. ÜBERPRÜFUNG DER AUGENMUSKELMOTORIK", null, 1 },
                    { 9, "IX. TESTS ZUR SEITIGKEITSÜBERPRÜFUNG", null, 1 },
                    { 8, "VIII. TESTS ZU ABERRANTEN REFLEXEN", null, 1 },
                    { 7, "VII. RÄUMLICHE WAHRNEHMUNGSPROBLEME", null, 1 },
                    { 3, "III. TESTS ZUR ÜBERPRÜFUNG VON KLEINHIRNFUNKTIONEN", null, 1 },
                    { 5, "V. LINKS-RECHTS-DISKRIMINIERUNGSPROBLEME", null, 1 },
                    { 4, "IV. TESTS ZUR DYSDIADOCHOKINESE", null, 1 },
                    { 12, "ZUSÄTZLICHE BEOBACHTUNGEN UND NOTIZEN", null, 1 },
                    { 2, "II. TESTS ZUR MOTORISCHEN ENTWICKLUNG", null, 1 },
                    { 1, "I. TESTS ZUR ÜBERPRÜFUNG DER GROBMOTORISCHEN KOORDINAION UND DES GLEICHGEWICHTS", null, 1 },
                    { 6, "VI. ORIENTIERUNGSPROBLEME", null, 1 },
                    { 13, "ERGEBNISZUSAMMENFASSUNG", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "AnamneseQuestions",
                columns: new[] { "Id", "AnamneseChapterId", "Label", "MetaInfo", "TextPrefix", "TextValue", "Type", "Value" },
                values: new object[,]
                {
                    { 1, 1, "Leidensdruck beim Kind?", null, null, null, "textarea", "" },
                    { 29, 3, "Hatte es  auffällige Schwierigkeiten sich selber anziehen zu lernen?", "Reihenfolge, falsch herum, Schleife (>5 bis 5,6Jahre), Knöpfe, Reißverschluss, „bequem / faul“, Hose fällt schwer anzuziehen", "", null, "radioYesNo", "" },
                    { 30, 3, "Litt bzw. leidet Ihr Kind unter Hautproblemen (trockene Haut, Milchschorf,  Neurodermitis, Ekzeme) oder Asthma(Husten, Reizhusten)?", "", "", null, "radioYesNo", "" },
                    { 31, 3, "Zeigt es irgendwelche allergische Reaktionen? ", "Allergien in der Familie, Heuschnupfen, Auslöser: Milch/Eier/Weizen, ständig laufende Nase, Suchtmittel", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 32, 3, "Gab es irgendwelche auffälligen Reaktionen nach den  Impfungen?", "längeres schlafen, erhöhte Temperatur", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 33, 3, "Lutschte Ihr Kind bis etwa zum 5. Lebensjahr oder länger am Daumen? (Hatte es einen Schnuller (> 2 LJ?), Kuscheltier)", "", "Falls ja, an welchem (links, rechts):", null, "radioYesNo, input", "" },
                    { 34, 3, "Machte oder macht  Ihr Kind auch noch nach dem Alter von 5 Jahren gelegentlich ins Bett?", "Wann war es trocken? Toilettentraining, spinaler Galant, gehäufte Mittelohrentzündungen, Hypoglykämie, nächtlicher O² Mangel, viel Milch Trinker", "", null, "radioYesNo", "" },
                    { 35, 3, "Zusätzliche Angaben zum Vorschulalter (Auffälligkeiten in der Motorik, Sport, externe Betreuung, Trennungsangst, Schreckhaft, Ängstlich, viele Freunde, Einzelgänger, Streit, Reaktionen auf Veränderungen, emotionale Auffälligkeiten, was sagen die Erzieher über das Kind, schneiden, malen, puzzeln, Körperhaltung, Spielverhalten, zündeln, Beziehung zu Tieren, Verletzungen)", "", "", null, "radioYesNo, textarea", "" },
                    { 36, 5, "Leidet Ihr Kind unter Reiseübelkeit?", "> 8Jahre, Auto fahren und lesen, Schiffe, Trampolin, Schaukeln, Höhenangst, Karussell, Fahrstuhl, Klettern", "", null, "radioYesNo", "" },
                    { 37, 5, "Hatte Ihr Kind in den ersten zwei Grundschuljahren Schwierigkeiten, das Lesen zu lernen?", "Tempo, Betonung, Motivation, spezielle Bücher, Comics", "", null, "radioYesNo", "" },
                    { 38, 5, "Hatte es Schwierigkeiten beim Schreiben lernen?", "Sitzhaltung, Stifthaltung, Rechtschreibung, Grammatik, Motorik beim Schreiben, schreiben anstrengend, kann es die Linien halten, drückt es stark auf, sehr kleine Schrift", "", null, "radioYesNo", "" },
                    { 28, 3, "Hatte es während der ersten 18 Lebensmonate irgendwelche Krankheiten, die mit hohem Fieber und / oder Krämpfen verbunden waren ? ", "sehr schnell, sehr hoch gefiebert, Narkose", "Falls ja, bitte Einzelheiten angeben (konnte es nach der Krankheit etwas nicht mehr so gut wie vorher?)", null, "radioYesNo, textarea", "" },
                    { 39, 5, "Falls es zunächst Druckschrift erlernte, hatte es Probleme mit der Schreibschrift?", "", "", null, "radioYesNo", "" },
                    { 41, 5, "Hatte es Schwierigkeiten, Fahrradfahren (ohne Stützräder) zu lernen?", "kann es fahren, seit wann, langsam fahren möglich, wie fährt es", "", null, "radioYesNo", "" },
                    { 42, 5, "Hatte es Schwierigkeiten, Schwimmen zu lernen?", "Verhältnis zum Wasser", "", null, "radioYesNo", "" },
                    { 43, 5, "Konnte es besser unter als über Wasser schwimmen?", "", "", null, "radioYesNo", "" },
                    { 45, 5, "War / ist Ihr Kind ein „Hals- Nasen- und Ohren“ Kind, d.h. litt / leidet es an häufigen Infektionen im Hals -, Nasen - und Ohrenbereich ? ", "Schnupfnase, Mittelohrentzündungen → Hörminderung → Ursache sind dann nicht die Reflexe", "", null, "radioYesNo", "" },
                    { 46, 5, "Hatte bzw. hat Ihr Kind Schwierigkeiten, einen (kleinen) Ball zu fangen oder andere Auge- / Hand - Koordinationsprobleme ? ", "Dyspraxie → kleckern beim Essen und Trinken, Fixierungsprobleme, Raum – Zeit – Gefühl gering, Schreckhaft, Verhältnis zu Bällen, Tollpatschig, zu langsam, zu schnell, greift gar nicht, beim hinfallen abgestützt", "", null, "radioYesNo", "" },
                    { 47, 6, "Hat Ihr Kind Schwierigkeiten still zu sitzen und wird es deswegen ständig vom Lehrer ermahnt ? Bevorzugt es auffällige Sitzpositionen?", "spinaler Galant (Stuhllehne/Gürtel), W – Sitz, Körperhaltung, Beinhaltung (um den Stuhl geschlungen/ hochgezogenes Bein),Sitzdauer, kippeln, liegend schreibend, Geräuscherzeugung", "", null, "radioYesNo", "" },
                    { 48, 6, "Macht Ihr Kind zahlreiche Fehler, wenn es aus einem Buch oder von der Tafel abschreibt?", "", "", null, "radioYesNo", "" },
                    { 49, 6, "Wenn Ihr Kind in Schule einen Aufsatz schreibt, verdreht es dabei gelegentlich Buchstaben oder lässt einzelne Buchstaben oder Wörter aus(auch evtl.Zahlendreher) ? ", "lesen von rechts nach links, häufige Dreher > 8LJ", "", null, "radioYesNo", "" },
                    { 50, 6, "Reagiert Ihr Kind bei plötzlichen, unerwarteten Geräuschen oder Bewegungen auffallend stark?", "Silvester, Staubsauger, Gewitter, Luftballons, Sportarten, Hobby, bewegt es sich gern, Traumverhalten / Albträume, Schlafhaltung", "", null, "radioYesNo", "" },
                    { 51, 7, "Zusätzliche Angaben  (z.B. Ernährungsverhalten: Süßigkeiten, Fleisch, Gemüse, Milch; vorangegangene oder andauernde Behandlungen bzw.Therapien(Therapiemüdigkeit), besondere Familiensituationen, Belastungen für das Kind, Kopfschmerzen, Schlafhaltung, Erschöpfung, Hypersensitivität: Geruch / Sonne / Kuscheln / Material / Geschmack / vestibulär):", "", "", null, "textarea", "" },
                    { 40, 5, "Hatte Schwierigkeiten, die Uhrzeit ablesen zu lernen (nicht Digitaluhr) bzw. sich insgesamt in der Zeit(Wochentage, Monate etc.) zurecht zu finden ? > 8.LJ", "räumliche Wahrnehmung, Morgens/ Mittags/ Abends, Gestern/ Morgen, Jahreszeiten, Orientierungssinn, Orientierung im Raum, Geschwindigkeit beim Anziehen, Aufräumen", "", null, "radioYesNo", "" },
                    { 27, 3, "Hat Ihr Kind spät sprechen gelernt (Zwei- und Dreiwortsätze) (> 2,5 Jahre)?", "eigene Sprache, gesabbert, Schwierigkeiten bei bestimmten Lauten, Gesungene Sprache", "", null, "radioYesNo", "" },
                    { 44, 5, "Hatte Ihr Kind im Verlauf der ersten 8 Lebensjahre (ausgenommen die ersten 18 Lebensmonate) Krankheiten mit sehr hohem Fieber, Bewusstlosigkeit oder Krämpfen ? ", "Vollnarkose, Infektanfälligkeit, Antibiotika, Meningitis, Tumore, Frakturen", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 25, 3, "Hat Ihr Kind, anstatt zunächst auf dem Bauch zu kriechen und dann auf den Händen und Knien zu krabbeln, sich auf andere Weise fortbewegt(z.B.rollend, auf dem Po rutschend, im „Bärengang“ auf Händen und Füßen)?", "(Kriechen = ab 7. - 8. Monat, homolateral die ersten 2 – 4 Wochen; Robben = ab 8. Monat, kontralateral; Krabbeln = ab 8. - 9.Monat, ab 11.Monat flüssig; Sitzen = ab 8.Monat; Stand = ab 8.Monat mit festhalten, ab 10.bis 11.Monat frei)", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 26, 3, "Hat Ihr Kind auffallend spät (> 1,5 Jahre) oder früh (< 12 Monate) laufen gelernt?", "Lauflerngeräte, Babywippe, Maxi- Kosi, Hopser", "", null, "radioYesNo", "" },
                    { 2, 1, "Was wurde/ wird unternommen um den Leidensdruck zu verbessern?", null, null, null, "textarea", "" },
                    { 3, 1, "Wie viele Kinder haben Sie entbunden?", null, null, null, "input", "" },
                    { 4, 1, "Welches Kind bei mehreren?", null, null, null, "input", "" },
                    { 5, 1, "Hatten Sie vorher Fehlgeburten? Warum?", null, null, null, "input", "" },
                    { 7, 2, "Als Sie schwanger waren, hatten Sie irgendwelche medizinischen Probleme?", "hoher Blutdruck, Hyperemesis gravidarum, drohende Fehlgeburt, vorzeitige Wehen, verordnete Bettruhe, Zwischenblutungen, Eisenmangel, Erkrankungen / Infektionen, Alkohol, Drogen, Umweltgifte, Medikamente, Ernährung, Flugreise, Sauna", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo", "" },
                    { 8, 2, "Hatten Sie eine starke Virusinfektion in den ersten 13. Wochen Ihrer Schwangerschaft?", null, null, null, "radioYesNo, textarea", "" },
                    { 9, 2, "Standen Sie während Ihrer Schwangerschaft (besonders im 6. Monat) unter starkem emotionalen Stress?", null, null, null, "radioYesNo", "" },
                    { 10, 2, "Sind während der Schwangerschaft diagnostische Verfahren durchgeführt worden?", "Ultraschall, Sonografie, Röntgen, Fruchtwasseruntersuchung o.ä.", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 11, 2, "Erfolgte vor oder während der Schwangerschaft eine Hormonbehandlung", "z.B. Progesteron Gabe in der 6. Woche", "Falls ja, welche und wann:", null, "radioYesNo, textarea", "" },
                    { 12, 2, "Wurde Ihr Kind früher (vor der 37. SSW) oder später (nach der 42. SSW) als zum errechneten Termin(+/ -2 Tage) geboren?", null, "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 6, 1, "Wurden Sie mit ihrem Kind aufgrund von IVF (künstliche Befruchtung) schwanger?", null, null, null, "radioYesNo", "" },
                    { 14, 3, "War Ihr Kind klein bezogen auf den Geburtszeitpunkt?", "< 2500g oder > 4000 - 4300g", "Geben Sie bitte das Geburtsgewicht die Geburtslänge und den Kopfumfang an.", null, "radioYesNo, textarea", "" },
                    { 13, 3, "War der Geburtsprozess ungewöhnlich oder besonders schwierig?", "Wehen, Geburtsdauer, Medikamente, Kaiserschnitt, Zange, Saugglocke, Sturzgeburt, Steißlage, Fruchtwasser, Kristellern, gerissen/ geschnitten", "Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo, textarea", "" },
                    { 23, 3, "Hat Ihr Kind sich nicht zum richtigen Zeitpunkt (ca. ab 6. Monat) oder nur mit physiotherapeutischer Unterstützung vom Rücken auf den Bauch gedreht?", "vom Bauch auf den Rücken ab 8. Monat postnatal", "", null, "radioYesNo", "" },
                    { 22, 3, "War Ihr Kind ein kleiner „Kopfstoßer“, d.h. stieß es absichtlich mit dem Kopf gegen feste Gegenstände ? Gibt es eine Vorgeschichte von Kopfverletzungen ? Wo waren die Kopfverletzungen?", "(präfrontaler Cortex – kein INPP Kind)", "", null, "radioYesNo", "" },
                    { 24, 3, "Hat ihr Kind nicht ausreichend/ wenig Zeit in BL verbracht bis zum Krabbeln?", "mochte es die BL, Tagesablauf des Babys, Maxi-Kosi, Wippe, Spreizwindel", "", null, "radioYesNo", "" },
                    { 20, 3, "War Ihr Kind zwischen dem 6. und 18. Lebensmonat sehr aktiv und fordernd? Schlief es wenig und schrie es ständig?", "verstopfte Nase, Mundatmer, schnarchendes Baby, Schlafprobleme, mochte nicht liegen → Druck im Mittelohr (unreife Schluckmuster), ADHS", "", null, "radioYesNo", "" },
                    { 21, 3, "Als Ihr Kind alt genug war, in der Karre zu sitzen oder sich im Kinderbett zum Stand hochzuziehen, bewegte es sich dort heftig schaukelnd hin und her, so dass sich Karre oder Bett mitbewegten?", "Wie ließ es sich beruhigen? → heftiges Schaukeln, Autofahren; Stereotype Bewegungen wie im Liegen den Kopf hin und her bewegen; Schreien beim Hinlegen?", "", null, "radioYesNo", "" },
                    { 18, 3, "Dauerte es auffallend lange, bis es seinen Kopf hochhalten konnte? Oder hat es den Kopf  sehr früh überstreckt gehalten?", "> 4 Monat", "", null, "radioYesNo", "" },
                    { 17, 3, "Hatte Ihr Kind in den ersten 13 Lebenswochen Schwierigkeiten beim Saugen an der Brust, beim Trinken aus der Flasche ? Hat es viel gespuckt ? ", "Wie lange gestillt, Dauer einer Mahlzeit, Zeitabstände zwischen den Mahlzeiten, Saugbewegungen", "", null, "radioYesNo", "" },
                    { 16, 3, "Wie waren die APGAR – Werte Ihres Kindes? (siehe Mutterpass ……/……/……) und der pH-Wert:", "", "Zusätzliche Angaben zur Schwangerschaft und Geburt (z.B. Einnahme der Pille ?) ", null, "textarea", "" },
                    { 15, 3, "Gab es irgendwelche Besonderheiten an Ihrem Baby nach der Geburt? Brauchte es Intensivpflege? Kam es dadurch zu einer längeren Trennung? Wochenbettdepression?", "Schädelverformung, viele blaue Flecken, Nabelschnur um den Hals, deutlich blaue Verfärbung, schwere Neugeborenengelbsucht, Lanugo-Behaarung, stark mit Käseschmiere bedeckt, Gelbsucht, Fußdeformitäten, Hüftdysplasie", ":Falls ja, bitte Einzelheiten angeben: ", null, "radioYesNo", "" },
                    { 19, 3, "War Ihr Kind in den ersten 6 Lebensmonaten ein auffallend ruhiges Baby, so ruhig, dass Sie manchmal befürchteten,es sei in seinem Bettchen gestorben?", "", "", null, "radioYesNo", "" }
                });

            migrationBuilder.InsertData(
                table: "TestungQuestions",
                columns: new[] { "Id", "Label", "TestungChapterId", "Type", "Value" },
                values: new object[,]
                {
                    { 97, "Handdominanz - Teleskop", 9, "radioLeftRight", "" },
                    { 96, "Handdominanz - Schreibhand", 9, "radioLeftRight", "" },
                    { 98, "Augendominanz (Entfernung) - Teleskop", 9, "radioLeftRight", "" },
                    { 99, "Augendominanz (Entfernung) - Ring", 9, "radioLeftRight", "" },
                    { 100, "Augendominanz (Nähe) - Lochkarte", 9, "radioLeftRight", "" },
                    { 101, "Augendominanz (Nähe) - Ring", 9, "radioLeftRight", "" },
                    { 102, "Ohrdominanz - Muschel", 9, "radioLeftRight", "" },
                    { 107, "Beeinträchtigte Folgebewegungen (tracking-vertikal)", 10, "radio", "" },
                    { 104, "Ohrdominanz - Rufen (Hinweis auf Hemisphärendominanz)", 9, "radioLeftRight", "" },
                    { 105, "Fixierungsschwierigkeiten", 10, "radio", "" },
                    { 106, "Beeinträchtigte Folgebewegungen (tracking- horizontal)", 10, "radio", "" },
                    { 108, "Verfolgen der Hand mit den Augen (eye-hand-tracking)", 10, "radio", "" },
                    { 109, "Augenzittern (Nystagmus)", 10, "radio", "" },
                    { 95, "Handdominanz - Klatschen in eine Hand", 9, "radioLeftRight", "" },
                    { 110, "Latente Konvergenz - links", 10, "radio", "" },
                    { 103, "Ohrdominanz - Lauschen", 9, "radioLeftRight", "" },
                    { 94, "Handdominanz - Einen Ball fangen", 9, "radioLeftRight", "" },
                    { 82, "Such-Reflex - rechts", 8, "radio", "" },
                    { 92, "Fußdominanz - Auf einen Stuhl steigen", 9, "radioLeftRight", "" },
                    { 111, "Latente Konvergenz - rechts", 10, "radio", "" },
                    { 77, "Segmentäre Rollreaktion- von den Hüften (rechts)", 8, "radio", "" },
                    { 78, "Babinski Reflex - linker Fuß", 8, "radio", "" },
                    { 79, "Babinski Reflex - rechter Fuß", 8, "radio", "" },
                    { 80, "Abdominal Reflex (optional)", 8, "radio", "" },
                    { 81, "Such-Reflex - links", 8, "radio", "" },
                    { 83, "Saug-Reflex", 8, "radio", "" },
                    { 84, "Erwachsener Saug-Reflex", 8, "radio4", "" },
                    { 85, "Palmar-Reflex - linke Hand", 8, "radio", "" },
                    { 86, "Palmar-Reflex - rechte Hand", 8, "radio", "" },
                    { 87, "Plantar-Reflex - linker Fuß", 8, "radio", "" },
                    { 88, "Plantar-Reflex - rechter Fuß", 8, "radio", "" },
                    { 89, "Landau-Reaktion", 8, "radio", "" },
                    { 90, "Fußdominanz - Ball schießen", 9, "radioLeftRight", "" },
                    { 91, "Fußdominanz - Aufstampfen mit einem Fuß", 9, "radioLeftRight", "" },
                    { 93, "Fußdominanz - Auf einem Bein hüpfen", 9, "radioLeftRight", "" },
                    { 112, "Latente Divergenz - links", 10, "radio", "" },
                    { 137, "Abschreiben eines kurzen Textes", 11, "input", "" },
                    { 114, "Konvergenzschwierigkeiten - linkes Auge", 10, "radio", "" },
                    { 134, "Räumliche Probleme - Daniels und Diack Figuren", 11, "radio", "" },
                    { 135, "Räumliche Probleme - Bender Visual Gestalt Figuren", 11, "radio", "" },
                    { 136, "Hinweis auf ‘Stimulusgebundenheit’", 11, "radio", "" },
                    { 76, "Segmentäre Rollreaktion- von den Hüften (links)", 8, "radio", "" },
                    { 138, "Mann-Zeichnen-Test Test (Aston Index) - Entwicklungsalter", 11, "input", "" },
                    { 139, "Mann-Zeichnen-Test Test (Aston Index) - Chronologisches Alter", 11, "input", "" },
                    { 140, "Stiftgriff", 12, "textarea", "" },
                    { 141, "Sitzposition", 12, "textarea", "" },
                    { 142, "Schnelle Ermüdbarkeit", 12, "textarea", "" },
                    { 143, "Kind ist ängstlich und besorgt und mit seinen Ergebnissen nicht zufrieden", 12, "textarea", "" },
                    { 144, "Index der Dysfunktion", 13, "input", "" },
                    { 145, "Grobmotorische Koordination und Gleichgewicht", 13, "textarea", "" },
                    { 146, "Kleinhirnfunktionen", 13, "textarea", "" },
                    { 147, "Dysdiadochokinese", 13, "textarea", "" },
                    { 148, "Aberrante Reflexe", 13, "textarea", "" },
                    { 133, "Räumliche Probleme - Tansley Standard Figuren", 11, "radio", "" },
                    { 113, "Latente Divergenz - rechts", 10, "radio", "" },
                    { 132, "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Bender Visual Gestalt Figuren", 11, "radio", "" },
                    { 130, "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Tansley Standard Figuren", 11, "radio", "" },
                    { 115, "Konvergenzschwierigkeiten - rechtes Auge", 10, "radio", "" },
                    { 116, "Schwierigkeit, die Augen unabhängig voneinander zu schließen - linkes Auge", 10, "radio", "" },
                    { 117, "Schwierigkeit, die Augen unabhängig voneinander zu schließen - rechtes Auge", 10, "radio", "" },
                    { 118, "Beeinträchtigung synchroner Augenbewegungen - linkes Auge", 10, "radio", "" },
                    { 119, "Beeinträchtigung synchroner Augenbewegungen - rechtes Auge", 10, "radio", "" },
                    { 120, "Erweiterte periphere Sicht - linkes Auge", 10, "radio", "" },
                    { 121, "Erweiterte periphere Sicht - rechtes Auge", 10, "radio", "" },
                    { 122, "Akkommodationsfähigkeit", 10, "radio", "" },
                    { 123, "Pupillenreaktion auf Licht (optional) - linkes Auge", 10, "input", "" },
                    { 124, "Pupillenreaktion auf Licht (optional) - rechtes Auge", 10, "input", "" },
                    { 125, "Pupillenreaktion auf Licht (optional) - linkes Auge", 10, "input", "" },
                    { 126, "Pupillenreaktion auf Licht (optional) - rechtes Auge", 10, "input", "" },
                    { 127, "Visuelle Unterscheidungsprobleme - Tansley Standard Figuren", 11, "radio", "" },
                    { 128, "Visuelle Unterscheidungsprobleme - Daniels und Diack Figuren", 11, "radio", "" },
                    { 129, "Visuelle Unterscheidungsprobleme - Bender Visual Gestalt Figuren", 11, "radio", "" },
                    { 131, "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Daniels und Diack Figuren", 11, "radio", "" },
                    { 75, "Segmentäre Rollreaktion- von den Schultern (rechts)", 8, "radio", "" },
                    { 50, "STNR - Füße oder Rumpf", 8, "radio", "" },
                    { 73, "Amphibien Reaktion - rechte Seite (Bauchlage)", 8, "radio", "" },
                    { 20, "Windmühle", 1, "radio", "" },
                    { 21, "Kriechen auf dem Bauch", 2, "radio2", "" },
                    { 22, "Krabbeln auf Händen und Knien", 2, "radio3", "" },
                    { 23, "Ferse auf Schienbein (linke Ferse auf rechtes Schienbein)", 3, "radio", "" },
                    { 24, "Ferse auf Schienbein (rechte Ferse auf linkes Schienbein)", 3, "radio", "" },
                    { 25, "Zeigefinger-Annäherung (Augen offen)", 3, "radio", "" },
                    { 26, "Zeigefinger-Annäherung (Augen geschlossen)", 3, "radio", "" },
                    { 27, "Finger an die Nase (Augen offen)", 3, "radio", "" },
                    { 28, "Finger an die Nase (Augen geschlossen)", 3, "radio", "" },
                    { 29, "Finger (linke Hand)", 4, "radio", "" },
                    { 30, "Finger (rechte Hand)", 4, "radio", "" },
                    { 31, "Hand (links)", 4, "radio", "" },
                    { 32, "Hand (rechts)", 4, "radio", "" },
                    { 33, "Fuß (links)", 4, "radio", "" },
                    { 34, "Fuß (rechts)", 4, "radio", "" },
                    { 19, "Hopserlauf", 1, "radio", "" },
                    { 35, "Links-Rechts-Diskriminierungsprobleme", 5, "radioYesNo", "" },
                    { 18, "Hüpfen auf einem Bein (links oder rechts)", 1, "radio", "" },
                    { 16, "Slalom Gang (rückwärts)", 1, "radio", "" },
                    { 1, "Aufrichten aus Rückenlage in den Stand", 1, "radio", "" },
                    { 2, "Aufrichten aus Bauchlage in den Stand", 1, "radio", "" },
                    { 3, "Romberg Test (Augen geöffnet)", 1, "radio", "" },
                    { 4, "Romberg Test (Augen geschlossen)", 1, "radio", "" },
                    { 5, "Mann Test (Augen geöffnet)", 1, "radio", "" },
                    { 6, "Mann Test (Augen geschlossen)", 1, "radio", "" },
                    { 7, "Einbeinstand", 1, "radio", "" },
                    { 8, "Marschieren und Umdrehen", 1, "radio", "" },
                    { 9, "Zehenspitzengang (vorwärts) 0 1", 1, "radio", "" },
                    { 10, "Zehenspitzengang (rückwärts)", 1, "radio", "" },
                    { 11, "Tandem Gang (vorwärts)", 1, "radio", "" },
                    { 12, "Tandem Gang (rückwärts)", 1, "radio", "" },
                    { 13, "Fog Walk (vorwärts)", 1, "radio", "" },
                    { 14, "Fog Walk (rückwärts)", 1, "radio", "" },
                    { 15, "Slalom Gang (vorwärts)", 1, "radio", "" },
                    { 17, "Fersengang (nur vorwärts)", 1, "radio", "" },
                    { 74, "Segmentäre Rollreaktion- von den Schultern (links)", 8, "radio", "" },
                    { 36, "Orientierungsprobleme", 6, "radioYesNo", "" },
                    { 38, "Standard Test - linker Arm", 8, "radio", "" },
                    { 58, "Moro Reflex / FPR - Standard Test", 8, "radio", "" },
                    { 59, "Moro Reflex / FPR - Aufrecht: TT", 8, "radio", "" },
                    { 60, "Moro Reflex / FPR - Aufrecht: ZT", 8, "radio", "" },
                    { 61, "Moro Reflex / FPR - Aufrecht: FF", 8, "radio", "" },
                    { 62, "Augenkopfstellreaktionen - nach links", 8, "radio", "" },
                    { 63, "Augenkopfstellreaktionen - nach rechts", 8, "radio", "" },
                    { 64, "Augenkopfstellreaktionen - vorwärts", 8, "radio", "" },
                    { 65, "Augenkopfstellreaktionen - rückwärts", 8, "radio", "" },
                    { 66, "Labyrinthkopfstellreaktionen - nach links", 8, "radio", "" },
                    { 67, "Labyrinthkopfstellreaktionen - nach rechts", 8, "radio", "" },
                    { 68, "Labyrinthkopfstellreaktionen - rückwärts", 8, "radio", "" },
                    { 69, "Labyrinthkopfstellreaktionen - vorwärts", 8, "radio", "" },
                    { 70, "Amphibien Reaktion - linke Seite (Rückenlage)", 8, "radio", "" },
                    { 71, "Amphibien Reaktion - rechte Seite (Rückenlage)", 8, "radio", "" },
                    { 72, "Amphibien Reaktion - linke Seite (Bauchlage)", 8, "radio", "" },
                    { 57, "TLR - Aufrechttest – Streckung", 8, "radio", "" },
                    { 37, "Räumliche Wahrnehmungsprobleme", 7, "radioYesNo", "" },
                    { 56, "TLR - Aufrechttest - Beugung", 8, "radio", "" },
                    { 54, "Spinaler Galant-Reflex - rechte Seite", 8, "radio", "" },
                    { 39, "Standard Test - linkes Bein", 8, "radio", "" },
                    { 40, "Standard Test - rechter Arm", 8, "radio", "" },
                    { 41, "Standard Test - rechtes Bein", 8, "radio", "" },
                    { 42, "Ayres Test Nr. 1 - linker Arm", 8, "radio", "" },
                    { 43, "Ayres Test Nr. 1 - rechter Arm", 8, "radio", "" },
                    { 44, "Ayres Test Nr. 2 - linker Arm", 8, "radio", "" },
                    { 45, "Ayres Test Nr. 2 - rechter Arm", 8, "radio", "" },
                    { 46, "Schilder Test - linker Arm", 8, "radio", "" },
                    { 47, "Schilder Test - rechter Arm", 8, "radio", "" },
                    { 48, "TTNR - von rechts nach links", 8, "radio", "" },
                    { 49, "TTNR - von links nach rechts", 8, "radio", "" },
                    { 149, "Okulomotorische Funktionen", 13, "textarea", "" },
                    { 51, "STNR - Arme", 8, "radio", "" },
                    { 52, "STNR - Krabbeltest", 8, "radio", "" },
                    { 53, "Spinaler Galant-Reflex - linke Seite", 8, "radio", "" },
                    { 55, "TLR - Standard Test", 8, "radio", "" },
                    { 150, "Visuelle Wahrnehmungsfunktionen", 13, "textarea", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnamneseChapters_AnamneseId",
                table: "AnamneseChapters",
                column: "AnamneseId");

            migrationBuilder.CreateIndex(
                name: "IX_Anamnesen_PatientId",
                table: "Anamnesen",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AnamneseQuestions_AnamneseChapterId",
                table: "AnamneseQuestions",
                column: "AnamneseChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemHierarchie_ReviewId",
                table: "ProblemHierarchie",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewChapters_ReviewId",
                table: "ReviewChapters",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewQuestion_ReviewChapterId",
                table: "ReviewQuestion",
                column: "ReviewChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PatientId",
                table: "Reviews",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TestungChapters_TestungId",
                table: "TestungChapters",
                column: "TestungId");

            migrationBuilder.CreateIndex(
                name: "IX_Testungen_PatientId",
                table: "Testungen",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TestungQuestions_TestungChapterId",
                table: "TestungQuestions",
                column: "TestungChapterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnamneseQuestions");

            migrationBuilder.DropTable(
                name: "ProblemHierarchie");

            migrationBuilder.DropTable(
                name: "ReviewQuestion");

            migrationBuilder.DropTable(
                name: "TestungQuestions");

            migrationBuilder.DropTable(
                name: "AnamneseChapters");

            migrationBuilder.DropTable(
                name: "ReviewChapters");

            migrationBuilder.DropTable(
                name: "TestungChapters");

            migrationBuilder.DropTable(
                name: "Anamnesen");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Testungen");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
