using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using rl_contract.Models;
using rl_contract.Models.Review;

namespace rl_bl.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewChapter> ReviewChapters { get; set; }
        public DbSet<ReviewQuestion> ReviewQuestion { get; set; }

        public DbSet<Testung> Testungen { get; set; }
        public DbSet<TestungChapter> TestungChapters { get; set; }
        public DbSet<TestungQuestion> TestungQuestions { get; set; }

        public DbSet<Anamnese> Anamnesen { get; set; }
        public DbSet<AnamneseChapter> AnamneseChapters { get; set; }
        public DbSet<AnamneseQuestion> AnamneseQuestions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Reviews)
                .WithOne()
                .HasForeignKey(s => s.PatientId);

            addPatients(modelBuilder);

        }

        private void addPatients(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(p =>
            {
                p.HasData(new
                {
                    Id = 1,
                    Firstname = "Kleiner",
                    Lastname = "Hase",
                    Birthday = new DateTime(1988, 8, 10),
                    Tele = "0177123456"
                    
                });
                p.HasData(new
                {
                    Id = 2,
                    Firstname = "Stefan",
                    Lastname = "Parge",
                    Birthday = new DateTime(1988, 8, 11),
                    Tele = "0177123457"
                });
            });

            modelBuilder.Entity<Review>(r =>
            {
                r.HasData(new
                    {
                        Id = 1,
                        Date = new DateTime(2019, 03, 04),
                        Name = "Befundgespräch",
                        Payed = true,
                        Exercises = "Liegestütze und dann Kaffee trinken",
                        Reasons = "Das war dringend notwendig",
                        PatientId = 1,
                        ObservationsParents = "Meinung Eltern",
                        ObservationsChild = "Meinung Kind",
                        ExerciseAccomplishment = "Medium ..."
                },
                    new
                    {
                        Id = 2,
                        Date = new DateTime(2019, 03, 03),
                        Name = "1. Review",
                        Payed = true,
                        Exercises = "Liegestütze und dann Kaffee trinken",
                        Reasons = "Das war dringend notwendig",
                        PatientId = 1,
                        ObservationsParents = "Meinung Eltern",
                        ObservationsChild = "Meinung Kind",
                        ExerciseAccomplishment = "Medium ..."
                    },
                    new
                    {
                        Id = 3,
                        Date = new DateTime(2019, 03, 02),
                        Name = "2. Review",
                        Payed = false,
                        Exercises = "Liegestütze und dann Kaffee trinken",
                        Reasons = "Das war dringend notwendig",
                        PatientId = 1,
                        ObservationsParents = "Meinung Eltern",
                        ObservationsChild = "Meinung Kind",
                        ExerciseAccomplishment = "Medium ..."
                    }
                );

                r.HasData(new
                    {
                        Id = 4,
                        Date = new DateTime(2019, 03, 04),
                        Name = "Befundgespräch",
                        Payed = true,
                        Exercises = "Liegestütze und dann Kaffee trinken",
                        Reasons = "Das war dringend notwendig",
                        PatientId = 2,
                        ObservationsParents = "Meinung Eltern",
                        ObservationsChild = "Meinung Kind",
                        ExerciseAccomplishment = "Medium ..."
                },
                    new
                    {
                        Id = 5,
                        Date = new DateTime(2019, 03, 03),
                        Name = "1. Review",
                        Payed = false,
                        Exercises = "Liegestütze und dann Kaffee trinken",
                        Reasons = "Das war dringend notwendig",
                        PatientId = 2,
                        ObservationsParents = "Meinung Eltern",
                        ObservationsChild = "Meinung Kind",
                        ExerciseAccomplishment = "Medium ..."
                    },
                    new
                    {
                        Id = 6,
                        Date = new DateTime(2019, 03, 02),
                        Name = "2. Review",
                        Payed = false,
                        Exercises = "Liegestütze und dann Kaffee trinken",
                        Reasons = "Das war dringend notwendig",
                        PatientId = 2,
                        ObservationsParents = "Meinung Eltern",
                        ObservationsChild = "Meinung Kind",
                        ExerciseAccomplishment = "Mediums ..."
                    }
                );
            });

            /*modelBuilder.Entity<ProblemHierarchie>(h =>
            {
                h.HasData(new
                    {
                        Id = 1,
                        initialValue = "Von schlecht",
                        changedValue = "zu gut",
                        ReviewId = 53
                    },
                    new
                    {
                        Id = 2,
                        initialValue = "Von schlecht2",
                        changedValue = "zu gut2",
                        ReviewId = 53
                    },
                    new
                    {
                        Id = 3,
                        initialValue = "Von schlecht3",
                        changedValue = "zu gut3",
                        ReviewId = 53
                    }
                );
            });*/

            modelBuilder.Entity<Testung>(r =>
            {
                r.HasData(new
                    {
                        Id = 1,
                        Date = DateTime.Now,
                        Name = "Erste Testung",
                        PatientId = 1
                    }
                );
            });

            modelBuilder.Entity<TestungChapter>(r =>
            {
                r.HasData(
                    new { Id = 1, Name = "I. TESTS ZUR ÜBERPRÜFUNG DER GROBMOTORISCHEN KOORDINATION UND GLEICHGEWICHT", ShortName= "GROBMOTORISCHE KOORDINATION UND GLEICHGEWICHT", TestungId = 1},
                    new { Id = 2, Name = "II. TESTS ZUR MOTORISCHEN ENTWICKLUNG", TestungId = 1 },
                    new { Id = 3, Name = "III. TESTS ZUR ÜBERPRÜFUNG VON KLEINHIRNFUNKTIONEN", ShortName = "KLEINHIRNFUNKTIONEN", TestungId = 1 },
                    new { Id = 4, Name = "IV. TESTS ZUR DYSDIADOCHOKINESE", ShortName = "DYSDIADOCHOKINESE", TestungId = 1 },
                    new { Id = 5, Name = "V. LINKS-RECHTS-DISKRIMINIERUNGSPROBLEME", TestungId = 1 },
                    new { Id = 6, Name = "VI. ORIENTIERUNGSPROBLEME", TestungId = 1 },
                    new { Id = 7, Name = "VII. RÄUMLICHE WAHRNEHMUNGSPROBLEME", TestungId = 1 },
                    new { Id = 8, Name = "VIII. TESTS ZU ABERRANTEN REFLEXEN", ShortName = "ABERRANTEN REFLEXEN", TestungId = 1 },
                    new { Id = 9, Name = "IX. TESTS ZUR SEITIGKEITSÜBERPRÜFUNG", TestungId = 1 },
                    new { Id = 10, Name = "X. ÜBERPRÜFUNG DER AUGENMUSKELMOTORIK", ShortName = "AUGENMUSKELMOTORIK", TestungId = 1 },
                    new { Id = 11, Name = "XI. VISUELLE WAHRNEHMUNGSÜBERPRÜFUNG", ShortName = "visuelle Wahrnehmungsprüfung", TestungId = 1 },
                    new { Id = 12, Name = "ZUSÄTZLICHE BEOBACHTUNGEN UND NOTIZEN", TestungId = 1 },
                    new { Id = 13, Name = "ERGEBNISZUSAMMENFASSUNG", TestungId = 1 }
                );
            });

            modelBuilder.Entity<TestungQuestion>(r =>
            {
                r.HasData(
                    new { Id = 1, Label = "Aufrichten aus Rückenlage in den Stand", Type = "radio", Value = "", TestungChapterId = 1  },
                    new { Id = 2, Label = "Aufrichten aus Bauchlage in den Stand", Type = "radio",  Value = "", TestungChapterId = 1 },
                    new { Id = 3, Label = "Romberg Test (Augen geöffnet)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 4, Label = "Romberg Test (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 5, Label = "Mann Test (Augen geöffnet)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 6, Label = "Mann Test (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 7, Label = "Einbeinstand", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 8, Label = "Marschieren und Umdrehen", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 9, Label = "Zehenspitzengang (vorwärts) 0 1", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 10, Label = "Zehenspitzengang (rückwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 11, Label = "Tandem Gang (vorwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 12, Label = "Tandem Gang (rückwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 13, Label = "Fog Walk (vorwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 14, Label = "Fog Walk (rückwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 15, Label = "Slalom Gang (vorwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 16, Label = "Slalom Gang (rückwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 17, Label = "Fersengang (nur vorwärts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 18, Label = "Hüpfen auf einem Bein (links oder rechts)", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 19, Label = "Hopserlauf", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 20, Label = "Windmühle", Type = "radio", Value = "", TestungChapterId = 1 },
                    new { Id = 21, Label = "Kriechen auf dem Bauch", Type = "radio2", Value = "", TestungChapterId = 2 },
                    new { Id = 22, Label = "Krabbeln auf Händen und Knien", Type = "radio3", Value = "", TestungChapterId = 2 },
                    new { Id = 23, Label = "Ferse auf Schienbein (linke Ferse auf rechtes Schienbein)", Type = "radio", Value = "", TestungChapterId = 3 },
                    new { Id = 24, Label = "Ferse auf Schienbein (rechte Ferse auf linkes Schienbein)", Type = "radio", Value = "", TestungChapterId = 3 },
                    new { Id = 25, Label = "Zeigefinger-Annäherung (Augen offen)", Type = "radio", Value = "", TestungChapterId = 3 },
                    new { Id = 26, Label = "Zeigefinger-Annäherung (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = 3 },
                    new { Id = 27, Label = "Finger an die Nase (Augen offen)", Type = "radio", Value = "", TestungChapterId = 3 },
                    new { Id = 28, Label = "Finger an die Nase (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = 3 },
                    new { Id = 29, Label = "Finger (linke Hand)", Type = "radio", Value = "", TestungChapterId = 4 },
                    new { Id = 30, Label = "Finger (rechte Hand)", Type = "radio", Value = "", TestungChapterId = 4 },
                    new { Id = 31, Label = "Hand (links)", Type = "radio", Value = "", TestungChapterId = 4 },
                    new { Id = 32, Label = "Hand (rechts)", Type = "radio", Value = "", TestungChapterId = 4 },
                    new { Id = 33, Label = "Fuß (links)", Type = "radio", Value = "", TestungChapterId = 4 },
                    new { Id = 34, Label = "Fuß (rechts)", Type = "radio", Value = "", TestungChapterId = 4 },
                    new { Id = 35, Label = "Links-Rechts-Diskriminierungsprobleme", Type = "radioYesNo", Value = "", TestungChapterId = 5 },
                    new { Id = 36, Label = "Orientierungsprobleme", Type = "radioYesNo", Value = "", TestungChapterId = 6 },
                    new { Id = 37, Label = "Räumliche Wahrnehmungsprobleme", Type = "radioYesNo", Value = "", TestungChapterId = 7 },
                    new { Id = 38, Label = "Standard Test - linker Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 39, Label = "Standard Test - linkes Bein", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 40, Label = "Standard Test - rechter Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 41, Label = "Standard Test - rechtes Bein", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 42, Label = "Ayres Test Nr. 1 - linker Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 43, Label = "Ayres Test Nr. 1 - rechter Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 44, Label = "Ayres Test Nr. 2 - linker Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 45, Label = "Ayres Test Nr. 2 - rechter Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 46, Label = "Schilder Test - linker Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 47, Label = "Schilder Test - rechter Arm", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 48, Label = "TTNR - von rechts nach links", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 49, Label = "TTNR - von links nach rechts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 50, Label = "STNR - Füße oder Rumpf", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 51, Label = "STNR - Arme", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 52, Label = "STNR - Krabbeltest", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 53, Label = "Spinaler Galant-Reflex - linke Seite", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 54, Label = "Spinaler Galant-Reflex - rechte Seite", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 55, Label = "TLR - Standard Test", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 56, Label = "TLR - Aufrechttest - Beugung", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 57, Label = "TLR - Aufrechttest – Streckung", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 58, Label = "Moro Reflex / FPR - Standard Test", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 59, Label = "Moro Reflex / FPR - Aufrecht: TT", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 60, Label = "Moro Reflex / FPR - Aufrecht: ZT", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 61, Label = "Moro Reflex / FPR - Aufrecht: FF", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 62, Label = "Augenkopfstellreaktionen - nach links", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 63, Label = "Augenkopfstellreaktionen - nach rechts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 64, Label = "Augenkopfstellreaktionen - vorwärts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 65, Label = "Augenkopfstellreaktionen - rückwärts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 66, Label = "Labyrinthkopfstellreaktionen - nach links", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 67, Label = "Labyrinthkopfstellreaktionen - nach rechts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 68, Label = "Labyrinthkopfstellreaktionen - rückwärts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 69, Label = "Labyrinthkopfstellreaktionen - vorwärts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 70, Label = "Amphibien Reaktion - linke Seite (Rückenlage)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 71, Label = "Amphibien Reaktion - rechte Seite (Rückenlage)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 72, Label = "Amphibien Reaktion - linke Seite (Bauchlage)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 73, Label = "Amphibien Reaktion - rechte Seite (Bauchlage)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 74, Label = "Segmentäre Rollreaktion- von den Schultern (links)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 75, Label = "Segmentäre Rollreaktion- von den Schultern (rechts)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 76, Label = "Segmentäre Rollreaktion- von den Hüften (links)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 77, Label = "Segmentäre Rollreaktion- von den Hüften (rechts)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 78, Label = "Babinski Reflex - linker Fuß", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 79, Label = "Babinski Reflex - rechter Fuß", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 80, Label = "Abdominal Reflex (optional)", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 81, Label = "Such-Reflex - links", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 82, Label = "Such-Reflex - rechts", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 83, Label = "Saug-Reflex", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 84, Label = "Erwachsener Saug-Reflex", Type = "radio4", Value = "", TestungChapterId = 8 },
                    new { Id = 85, Label = "Palmar-Reflex - linke Hand", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 86, Label = "Palmar-Reflex - rechte Hand", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 87, Label = "Plantar-Reflex - linker Fuß", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 88, Label = "Plantar-Reflex - rechter Fuß", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 89, Label = "Landau-Reaktion", Type = "radio", Value = "", TestungChapterId = 8 },
                    new { Id = 90, Label = "Fußdominanz - Ball schießen", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 91, Label = "Fußdominanz - Aufstampfen mit einem Fuß", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 92, Label = "Fußdominanz - Auf einen Stuhl steigen", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 93, Label = "Fußdominanz - Auf einem Bein hüpfen", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 94, Label = "Handdominanz - Einen Ball fangen", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 95, Label = "Handdominanz - Klatschen in eine Hand", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 96, Label = "Handdominanz - Schreibhand", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 97, Label = "Handdominanz - Teleskop", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 98, Label = "Augendominanz (Entfernung) - Teleskop", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 99, Label = "Augendominanz (Entfernung) - Ring", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 100, Label = "Augendominanz (Nähe) - Lochkarte", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 101, Label = "Augendominanz (Nähe) - Ring", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 102, Label = "Ohrdominanz - Muschel", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 103, Label = "Ohrdominanz - Lauschen", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 104, Label = "Ohrdominanz - Rufen (Hinweis auf Hemisphärendominanz)", Type = "radioLeftRight", Value = "", TestungChapterId = 9 },
                    new { Id = 105, Label = "Fixierungsschwierigkeiten", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 106, Label = "Beeinträchtigte Folgebewegungen (tracking- horizontal)", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 107, Label = "Beeinträchtigte Folgebewegungen (tracking-vertikal)", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 108, Label = "Verfolgen der Hand mit den Augen (eye-hand-tracking)", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 109, Label = "Augenzittern (Nystagmus)", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 110, Label = "Latente Konvergenz - links", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 111, Label = "Latente Konvergenz - rechts", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 112, Label = "Latente Divergenz - links", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 113, Label = "Latente Divergenz - rechts", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 114, Label = "Konvergenzschwierigkeiten - linkes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 115, Label = "Konvergenzschwierigkeiten - rechtes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 116, Label = "Schwierigkeit, die Augen unabhängig voneinander zu schließen - linkes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 117, Label = "Schwierigkeit, die Augen unabhängig voneinander zu schließen - rechtes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 118, Label = "Beeinträchtigung synchroner Augenbewegungen - linkes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 119, Label = "Beeinträchtigung synchroner Augenbewegungen - rechtes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 120, Label = "Erweiterte periphere Sicht - linkes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 121, Label = "Erweiterte periphere Sicht - rechtes Auge", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 122, Label = "Akkommodationsfähigkeit", Type = "radio", Value = "", TestungChapterId = 10 },
                    new { Id = 123, Label = "Pupillenreaktion auf Licht (optional) - linkes Auge", Type = "input", Value = "", TestungChapterId = 10 },
                    new { Id = 124, Label = "Pupillenreaktion auf Licht (optional) - rechtes Auge", Type = "input", Value = "", TestungChapterId = 10 },
                    new { Id = 125, Label = "Pupillenreaktion auf Licht (optional) - linkes Auge", Type = "input", Value = "", TestungChapterId = 10 },
                    new { Id = 126, Label = "Pupillenreaktion auf Licht (optional) - rechtes Auge", Type = "input", Value = "", TestungChapterId = 10 },
                    new { Id = 127, Label = "Visuelle Unterscheidungsprobleme - Tansley Standard Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 128, Label = "Visuelle Unterscheidungsprobleme - Daniels und Diack Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 129, Label = "Visuelle Unterscheidungsprobleme - Bender Visual Gestalt Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 130, Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Tansley Standard Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 131, Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Daniels und Diack Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 132, Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Bender Visual Gestalt Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 133, Label = "Räumliche Probleme - Tansley Standard Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 134, Label = "Räumliche Probleme - Daniels und Diack Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 135, Label = "Räumliche Probleme - Bender Visual Gestalt Figuren", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 136, Label = "Hinweis auf ‘Stimulusgebundenheit’", Type = "radio", Value = "", TestungChapterId = 11 },
                    new { Id = 137, Label = "Abschreiben eines kurzen Textes", Type = "input", Value = "", TestungChapterId = 11 },
                    new { Id = 138, Label = "Mann-Zeichnen-Test Test (Aston Index) - Entwicklungsalter", Type = "input", Value = "", TestungChapterId = 11 },
                    new { Id = 139, Label = "Mann-Zeichnen-Test Test (Aston Index) - Chronologisches Alter", Type = "input", Value = "", TestungChapterId = 11 },
                    new { Id = 140, Label = "Stiftgriff", Type = "textarea", Value = "", TestungChapterId = 12 },
                    new { Id = 141, Label = "Sitzposition", Type = "textarea", Value = "", TestungChapterId = 12 },
                    new { Id = 142, Label = "Schnelle Ermüdbarkeit", Type = "textarea", Value = "", TestungChapterId = 12 },
                    new { Id = 143, Label = "Kind ist ängstlich und besorgt und mit seinen Ergebnissen nicht zufrieden", Type = "textarea", Value = "", TestungChapterId = 12 },
                    new { Id = 144, Label = "Index der Dysfunktion", Type = "input", Value = "", TestungChapterId = 13 },
                    new { Id = 145, Label = "Grobmotorische Koordination und Gleichgewicht", Type = "textarea", Value = "", TestungChapterId = 13 },
                    new { Id = 146, Label = "Kleinhirnfunktionen", Type = "textarea", Value = "", TestungChapterId = 13 },
                    new { Id = 147, Label = "Dysdiadochokinese", Type = "textarea", Value = "", TestungChapterId = 13 },
                    new { Id = 148, Label = "Aberrante Reflexe", Type = "textarea", Value = "", TestungChapterId = 13 },
                    new { Id = 149, Label = "Okulomotorische Funktionen", Type = "textarea", Value = "", TestungChapterId = 13 },
                    new { Id = 150, Label = "Visuelle Wahrnehmungsfunktionen", Type = "textarea", Value = "", TestungChapterId = 13 }


                );
            });

            addAnamnese(modelBuilder);


        }

        private void addDocuments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(new Document()
                {
                    Id = 1,
                    Name = "Anamnese",
                }, 
                new Document()
                {
                    Id = 2,
                    Name ="Testung"
                },
                new Document()
                {
                    Id = 3,
                    Name = "Report"
                });
        }

        private void addAnamnese(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anamnese>(r =>
            {
                r.HasData(new
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Name = "Anamnese (Fragebogen / Kinder)",
                    CountOfPositivAnswers = -1,
                    PatientId = 1
                }
                );
            });

            modelBuilder.Entity<AnamneseChapter>(r =>
            {
                r.HasData(
                    new { Id = 1,  Name = "0. Allgemeines", AnamneseId = 1 },
                    new { Id = 2,  Name = "I. Schwangerschaft", AnamneseId = 1 },
                    new { Id = 3,  Name = "II. Geburt", AnamneseId = 1 },
                    new { Id = 4, Name = "- ENDE 1 Teil(7 - 8 JA Antworten) + Frage 18./ 22.a) / 24. -> Bitte beantworten Sie die folgenden Fragen, bis sie altersgemäß nicht mehr zutreffen - ", AnamneseId = 1 },
                    new { Id = 5,  Name = "III. Schulzeit: 6. – 8. Lebensjahr", AnamneseId = 1 },
                    new { Id = 6,  Name = "IV. 8. - 10. Lebensjahr", AnamneseId = 1 },
                    new { Id = 7,  Name = "V. Zusätzliche Angaben", AnamneseId = 1 }
                );
            });

            modelBuilder.Entity<AnamneseQuestion>(r =>
            {
                r.HasData(
                    new { Id = 1, Label = "Leidensdruck beim Kind?", Type = "textarea", Value = "", AnamneseChapterId = 1 },
                    new { Id = 2, Label = "Was wurde/ wird unternommen um den Leidensdruck zu verbessern?", Type = "textarea", Value = "", AnamneseChapterId = 1 },
                    new { Id = 3, Label = "Wie viele Kinder haben Sie entbunden?", Type = "input", Value = "", AnamneseChapterId = 1 },
                    new { Id = 4, Label = "Welches Kind bei mehreren?", Type = "input", Value = "", AnamneseChapterId = 1 },
                    new { Id = 5, Label = "Hatten Sie vorher Fehlgeburten? Warum?", Type = "input", Value = "", AnamneseChapterId = 1 },
                    new { Id = 6, Label = "Wurden Sie mit ihrem Kind aufgrund von IVF (künstliche Befruchtung) schwanger?", Type = "radioYesNo", Value = "", AnamneseChapterId = 1 },
                    new { Id = 7, Label = "Als Sie schwanger waren, hatten Sie irgendwelche medizinischen Probleme?", Type = "radioYesNo", Value = "", AnamneseChapterId = 2, MetaInfo = "hoher Blutdruck, Hyperemesis gravidarum, drohende Fehlgeburt, vorzeitige Wehen, verordnete Bettruhe, Zwischenblutungen, Eisenmangel, Erkrankungen / Infektionen, Alkohol, Drogen, Umweltgifte, Medikamente, Ernährung, Flugreise, Sauna", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 8, Label = "Hatten Sie eine starke Virusinfektion in den ersten 13. Wochen Ihrer Schwangerschaft?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 2 },
                    new { Id = 9, Label = "Standen Sie während Ihrer Schwangerschaft (besonders im 6. Monat) unter starkem emotionalen Stress?", Type = "radioYesNo", Value = "", AnamneseChapterId = 2 },
                    new { Id = 10, Label = "Sind während der Schwangerschaft diagnostische Verfahren durchgeführt worden?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 2, MetaInfo = "Ultraschall, Sonografie, Röntgen, Fruchtwasseruntersuchung o.ä.", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 11, Label = "Erfolgte vor oder während der Schwangerschaft eine Hormonbehandlung", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 2, MetaInfo = "z.B. Progesteron Gabe in der 6. Woche", TextPrefix = "Falls ja, welche und wann:" },
                    new { Id = 12, Label = "Wurde Ihr Kind früher (vor der 37. SSW) oder später (nach der 42. SSW) als zum errechneten Termin(+/ -2 Tage) geboren?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 2, TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 13, Label = "War der Geburtsprozess ungewöhnlich oder besonders schwierig?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "Wehen, Geburtsdauer, Medikamente, Kaiserschnitt, Zange, Saugglocke, Sturzgeburt, Steißlage, Fruchtwasser, Kristellern, gerissen/ geschnitten", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 14, Label = "War Ihr Kind klein bezogen auf den Geburtszeitpunkt?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "< 2500g oder > 4000 - 4300g", TextPrefix = "Geben Sie bitte das Geburtsgewicht die Geburtslänge und den Kopfumfang an." },
                    new { Id = 15, Label = "Gab es irgendwelche Besonderheiten an Ihrem Baby nach der Geburt? Brauchte es Intensivpflege? Kam es dadurch zu einer längeren Trennung? Wochenbettdepression?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "Schädelverformung, viele blaue Flecken, Nabelschnur um den Hals, deutlich blaue Verfärbung, schwere Neugeborenengelbsucht, Lanugo-Behaarung, stark mit Käseschmiere bedeckt, Gelbsucht, Fußdeformitäten, Hüftdysplasie", TextPrefix = ":Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 16, Label = "Wie waren die APGAR – Werte Ihres Kindes? (siehe Mutterpass ……/……/……) und der pH-Wert:", Type = "textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "", TextPrefix = "Zusätzliche Angaben zur Schwangerschaft und Geburt (z.B. Einnahme der Pille ?) " },
                    new { Id = 17, Label = "Hatte Ihr Kind in den ersten 13 Lebenswochen Schwierigkeiten beim Saugen an der Brust, beim Trinken aus der Flasche ? Hat es viel gespuckt ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "Wie lange gestillt, Dauer einer Mahlzeit, Zeitabstände zwischen den Mahlzeiten, Saugbewegungen", TextPrefix = "" },
                    new { Id = 18, Label = "Dauerte es auffallend lange, bis es seinen Kopf hochhalten konnte? Oder hat es den Kopf  sehr früh überstreckt gehalten?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "> 4 Monat", TextPrefix = "" },
                    new { Id = 19, Label = "War Ihr Kind in den ersten 6 Lebensmonaten ein auffallend ruhiges Baby, so ruhig, dass Sie manchmal befürchteten,es sei in seinem Bettchen gestorben?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "", TextPrefix = "" },
                    new { Id = 20, Label = "War Ihr Kind zwischen dem 6. und 18. Lebensmonat sehr aktiv und fordernd? Schlief es wenig und schrie es ständig?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "verstopfte Nase, Mundatmer, schnarchendes Baby, Schlafprobleme, mochte nicht liegen → Druck im Mittelohr (unreife Schluckmuster), ADHS", TextPrefix = "" },
                    new { Id = 21, Label = "Als Ihr Kind alt genug war, in der Karre zu sitzen oder sich im Kinderbett zum Stand hochzuziehen, bewegte es sich dort heftig schaukelnd hin und her, so dass sich Karre oder Bett mitbewegten?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "Wie ließ es sich beruhigen? → heftiges Schaukeln, Autofahren; Stereotype Bewegungen wie im Liegen den Kopf hin und her bewegen; Schreien beim Hinlegen?", TextPrefix = "" },
                    new { Id = 22, Label = "War Ihr Kind ein kleiner „Kopfstoßer“, d.h. stieß es absichtlich mit dem Kopf gegen feste Gegenstände ? Gibt es eine Vorgeschichte von Kopfverletzungen ? Wo waren die Kopfverletzungen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "(präfrontaler Cortex – kein INPP Kind)", TextPrefix = "" },
                    new { Id = 23, Label = "Hat Ihr Kind sich nicht zum richtigen Zeitpunkt (ca. ab 6. Monat) oder nur mit physiotherapeutischer Unterstützung vom Rücken auf den Bauch gedreht?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "vom Bauch auf den Rücken ab 8. Monat postnatal", TextPrefix = "" },
                    new { Id = 24, Label = "Hat ihr Kind nicht ausreichend/ wenig Zeit in BL verbracht bis zum Krabbeln?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "mochte es die BL, Tagesablauf des Babys, Maxi-Kosi, Wippe, Spreizwindel", TextPrefix = "" },
                    new { Id = 25, Label = "Hat Ihr Kind, anstatt zunächst auf dem Bauch zu kriechen und dann auf den Händen und Knien zu krabbeln, sich auf andere Weise fortbewegt(z.B.rollend, auf dem Po rutschend, im „Bärengang“ auf Händen und Füßen)?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "(Kriechen = ab 7. - 8. Monat, homolateral die ersten 2 – 4 Wochen; Robben = ab 8. Monat, kontralateral; Krabbeln = ab 8. - 9.Monat, ab 11.Monat flüssig; Sitzen = ab 8.Monat; Stand = ab 8.Monat mit festhalten, ab 10.bis 11.Monat frei)", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 26, Label = "Hat Ihr Kind auffallend spät (> 1,5 Jahre) oder früh (< 12 Monate) laufen gelernt?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "Lauflerngeräte, Babywippe, Maxi- Kosi, Hopser", TextPrefix = "" },
                    new { Id = 27, Label = "Hat Ihr Kind spät sprechen gelernt (Zwei- und Dreiwortsätze) (> 2,5 Jahre)?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "eigene Sprache, gesabbert, Schwierigkeiten bei bestimmten Lauten, Gesungene Sprache", TextPrefix = "" },
                    new { Id = 28, Label = "Hatte es während der ersten 18 Lebensmonate irgendwelche Krankheiten, die mit hohem Fieber und / oder Krämpfen verbunden waren ? ", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "sehr schnell, sehr hoch gefiebert, Narkose", TextPrefix = "Falls ja, bitte Einzelheiten angeben (konnte es nach der Krankheit etwas nicht mehr so gut wie vorher?)" },
                    new { Id = 29, Label = "Hatte es  auffällige Schwierigkeiten sich selber anziehen zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "Reihenfolge, falsch herum, Schleife (>5 bis 5,6Jahre), Knöpfe, Reißverschluss, „bequem / faul“, Hose fällt schwer anzuziehen", TextPrefix = "" },
                    new { Id = 30, Label = "Litt bzw. leidet Ihr Kind unter Hautproblemen (trockene Haut, Milchschorf,  Neurodermitis, Ekzeme) oder Asthma(Husten, Reizhusten)?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "", TextPrefix = "" },
                    new { Id = 31, Label = "Zeigt es irgendwelche allergische Reaktionen? ", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "Allergien in der Familie, Heuschnupfen, Auslöser: Milch/Eier/Weizen, ständig laufende Nase, Suchtmittel", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 32, Label = "Gab es irgendwelche auffälligen Reaktionen nach den  Impfungen?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "längeres schlafen, erhöhte Temperatur", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 33, Label = "Lutschte Ihr Kind bis etwa zum 5. Lebensjahr oder länger am Daumen? (Hatte es einen Schnuller (> 2 LJ?), Kuscheltier)", Type = "radioYesNo, input", Value = "", AnamneseChapterId = 3, MetaInfo = "", TextPrefix = "Falls ja, an welchem (links, rechts):" },
                    new { Id = 34, Label = "Machte oder macht  Ihr Kind auch noch nach dem Alter von 5 Jahren gelegentlich ins Bett?", Type = "radioYesNo", Value = "", AnamneseChapterId = 3, MetaInfo = "Wann war es trocken? Toilettentraining, spinaler Galant, gehäufte Mittelohrentzündungen, Hypoglykämie, nächtlicher O² Mangel, viel Milch Trinker", TextPrefix = "" },
                    new { Id = 35, Label = "Zusätzliche Angaben zum Vorschulalter (Auffälligkeiten in der Motorik, Sport, externe Betreuung, Trennungsangst, Schreckhaft, Ängstlich, viele Freunde, Einzelgänger, Streit, Reaktionen auf Veränderungen, emotionale Auffälligkeiten, was sagen die Erzieher über das Kind, schneiden, malen, puzzeln, Körperhaltung, Spielverhalten, zündeln, Beziehung zu Tieren, Verletzungen)", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 3, MetaInfo = "", TextPrefix = "" },
                    new { Id = 36, Label = "Leidet Ihr Kind unter Reiseübelkeit?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "> 8Jahre, Auto fahren und lesen, Schiffe, Trampolin, Schaukeln, Höhenangst, Karussell, Fahrstuhl, Klettern", TextPrefix = "" },
                    new { Id = 37, Label = "Hatte Ihr Kind in den ersten zwei Grundschuljahren Schwierigkeiten, das Lesen zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "Tempo, Betonung, Motivation, spezielle Bücher, Comics", TextPrefix = "" },
                    new { Id = 38, Label = "Hatte es Schwierigkeiten beim Schreiben lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "Sitzhaltung, Stifthaltung, Rechtschreibung, Grammatik, Motorik beim Schreiben, schreiben anstrengend, kann es die Linien halten, drückt es stark auf, sehr kleine Schrift", TextPrefix = "" },
                    new { Id = 39, Label = "Falls es zunächst Druckschrift erlernte, hatte es Probleme mit der Schreibschrift?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "", TextPrefix = "" },
                    new { Id = 40, Label = "Hatte Schwierigkeiten, die Uhrzeit ablesen zu lernen (nicht Digitaluhr) bzw. sich insgesamt in der Zeit(Wochentage, Monate etc.) zurecht zu finden ? > 8.LJ", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "räumliche Wahrnehmung, Morgens/ Mittags/ Abends, Gestern/ Morgen, Jahreszeiten, Orientierungssinn, Orientierung im Raum, Geschwindigkeit beim Anziehen, Aufräumen", TextPrefix = "" },
                    new { Id = 41, Label = "Hatte es Schwierigkeiten, Fahrradfahren (ohne Stützräder) zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "kann es fahren, seit wann, langsam fahren möglich, wie fährt es", TextPrefix = "" },
                    new { Id = 42, Label = "Hatte es Schwierigkeiten, Schwimmen zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "Verhältnis zum Wasser", TextPrefix = "" },
                    new { Id = 43, Label = "Konnte es besser unter als über Wasser schwimmen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "", TextPrefix = "" },
                    new { Id = 44, Label = "Hatte Ihr Kind im Verlauf der ersten 8 Lebensjahre (ausgenommen die ersten 18 Lebensmonate) Krankheiten mit sehr hohem Fieber, Bewusstlosigkeit oder Krämpfen ? ", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = 5, MetaInfo = "Vollnarkose, Infektanfälligkeit, Antibiotika, Meningitis, Tumore, Frakturen", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                    new { Id = 45, Label = "War / ist Ihr Kind ein „Hals- Nasen- und Ohren“ Kind, d.h. litt / leidet es an häufigen Infektionen im Hals -, Nasen - und Ohrenbereich ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "Schnupfnase, Mittelohrentzündungen → Hörminderung → Ursache sind dann nicht die Reflexe", TextPrefix = "" },
                    new { Id = 46, Label = "Hatte bzw. hat Ihr Kind Schwierigkeiten, einen (kleinen) Ball zu fangen oder andere Auge- / Hand - Koordinationsprobleme ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = 5, MetaInfo = "Dyspraxie → kleckern beim Essen und Trinken, Fixierungsprobleme, Raum – Zeit – Gefühl gering, Schreckhaft, Verhältnis zu Bällen, Tollpatschig, zu langsam, zu schnell, greift gar nicht, beim hinfallen abgestützt", TextPrefix = "" },
                    new { Id = 47, Label = "Hat Ihr Kind Schwierigkeiten still zu sitzen und wird es deswegen ständig vom Lehrer ermahnt ? Bevorzugt es auffällige Sitzpositionen?", Type = "radioYesNo", Value = "", AnamneseChapterId = 6, MetaInfo = "spinaler Galant (Stuhllehne/Gürtel), W – Sitz, Körperhaltung, Beinhaltung (um den Stuhl geschlungen/ hochgezogenes Bein),Sitzdauer, kippeln, liegend schreibend, Geräuscherzeugung", TextPrefix = "" },
                    new { Id = 48, Label = "Macht Ihr Kind zahlreiche Fehler, wenn es aus einem Buch oder von der Tafel abschreibt?", Type = "radioYesNo", Value = "", AnamneseChapterId = 6, MetaInfo = "", TextPrefix = "" },
                    new { Id = 49, Label = "Wenn Ihr Kind in Schule einen Aufsatz schreibt, verdreht es dabei gelegentlich Buchstaben oder lässt einzelne Buchstaben oder Wörter aus(auch evtl.Zahlendreher) ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = 6, MetaInfo = "lesen von rechts nach links, häufige Dreher > 8LJ", TextPrefix = "" },
                    new { Id = 50, Label = "Reagiert Ihr Kind bei plötzlichen, unerwarteten Geräuschen oder Bewegungen auffallend stark?", Type = "radioYesNo", Value = "", AnamneseChapterId = 6, MetaInfo = "Silvester, Staubsauger, Gewitter, Luftballons, Sportarten, Hobby, bewegt es sich gern, Traumverhalten / Albträume, Schlafhaltung", TextPrefix = "" },
                    new { Id = 51, Label = "Zusätzliche Angaben  (z.B. Ernährungsverhalten: Süßigkeiten, Fleisch, Gemüse, Milch; vorangegangene oder andauernde Behandlungen bzw.Therapien(Therapiemüdigkeit), besondere Familiensituationen, Belastungen für das Kind, Kopfschmerzen, Schlafhaltung, Erschöpfung, Hypersensitivität: Geruch / Sonne / Kuscheln / Material / Geschmack / vestibulär):", Type = "textarea", Value = "", AnamneseChapterId = 7, MetaInfo = "", TextPrefix = "" }
                );
            });
        }

        /*private void createGenericQuestionLib(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericChapter>(r =>
            {
                r.HasData(
                    new { Id = 1, Name = "I. TESTS ZUR ÜBERPRÜFUNG DER GROBMOTORISCHEN KOORDINAION UND DES GLEICHGEWICHTS"},
                    new { Id = 2, Name = "II. TESTS ZUR MOTORISCHEN ENTWICKLUNG"},
                    new { Id = 3, Name = "III. TESTS ZUR ÜBERPRÜFUNG VON KLEINHIRNFUNKTIONEN"},
                    new { Id = 4, Name = "IV. TESTS ZUR DYSDIADOCHOKINESE"},
                    new { Id = 5, Name = "V. LINKS-RECHTS-DISKRIMINIERUNGSPROBLEME"},
                    new { Id = 6, Name = "VI. ORIENTIERUNGSPROBLEME"},
                    new { Id = 7, Name = "VII. RÄUMLICHE WAHRNEHMUNGSPROBLEME"},
                    new { Id = 8, Name = "VIII. TESTS ZU ABERRANTEN REFLEXEN"},
                    new { Id = 9, Name = "IX. TESTS ZUR SEITIGKEITSÜBERPRÜFUNG"},
                    new { Id = 10, Name = "X. ÜBERPRÜFUNG DER AUGENMUSKELMOTORIK"},
                    new { Id = 11, Name = "XI. VISUELLE WAHRNEHMUNGSÜBERPRÜFUNG"},
                    new { Id = 12, Name = "ZUSÄTZLICHE BEOBACHTUNGEN UND NOTIZEN"},
                    new { Id = 13, Name = "ERGEBNISZUSAMMENFASSUNG"}
                );
            });

            modelBuilder.Entity<GenericQuestion>(r =>
            {
                r.HasData(
                    new { Id = 1, Label = "Aufrichten aus Rückenlage in den Stand", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 2, Label = "Aufrichten aus Bauchlage in den Stand", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 3, Label = "Romberg Test (Augen geöffnet)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 4, Label = "Romberg Test (Augen geschlossen)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 5, Label = "Mann Test (Augen geöffnet)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 6, Label = "Mann Test (Augen geschlossen)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 7, Label = "Einbeinstand", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 8, Label = "Marschieren und Umdrehen", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 9, Label = "Zehenspitzengang (vorwärts) 0 1", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 10, Label = "Zehenspitzengang (rückwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 11, Label = "Tandem Gang (vorwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 12, Label = "Tandem Gang (rückwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 13, Label = "Fog Walk (vorwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 14, Label = "Fog Walk (rückwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 15, Label = "Slalom Gang (vorwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 16, Label = "Slalom Gang (rückwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 17, Label = "Fersengang (nur vorwärts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 18, Label = "Hüpfen auf einem Bein (links oder rechts)", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 19, Label = "Hopserlauf", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 20, Label = "Windmühle", Type = "radio", Value = "", GenericChapterId = 1 },
                    new { Id = 21, Label = "Kriechen auf dem Bauch", Type = "radio2", Value = "", GenericChapterId = 2 },
                    new { Id = 22, Label = "Krabbeln auf Händen und Knien", Type = "radio3", Value = "", GenericChapterId = 2 },
                    new { Id = 23, Label = "Ferse auf Schienbein (linke Ferse auf rechtes Schienbein)", Type = "radio", Value = "", GenericChapterId = 3 },
                    new { Id = 24, Label = "Ferse auf Schienbein (rechte Ferse auf linkes Schienbein)", Type = "radio", Value = "", GenericChapterId = 3 },
                    new { Id = 25, Label = "Zeigefinger-Annäherung (Augen offen)", Type = "radio", Value = "", GenericChapterId = 3 },
                    new { Id = 26, Label = "Zeigefinger-Annäherung (Augen geschlossen)", Type = "radio", Value = "", GenericChapterId = 3 },
                    new { Id = 27, Label = "Finger an die Nase (Augen offen)", Type = "radio", Value = "", GenericChapterId = 3 },
                    new { Id = 28, Label = "Finger an die Nase (Augen geschlossen)", Type = "radio", Value = "", GenericChapterId = 3 },
                    new { Id = 29, Label = "Finger (linke Hand)", Type = "radio", Value = "", GenericChapterId = 4 },
                    new { Id = 30, Label = "Finger (rechte Hand)", Type = "radio", Value = "", GenericChapterId = 4 },
                    new { Id = 31, Label = "Hand (links)", Type = "radio", Value = "", GenericChapterId = 4 },
                    new { Id = 32, Label = "Hand (rechts)", Type = "radio", Value = "", GenericChapterId = 4 },
                    new { Id = 33, Label = "Fuß (links)", Type = "radio", Value = "", GenericChapterId = 4 },
                    new { Id = 34, Label = "Fuß (rechts)", Type = "radio", Value = "", GenericChapterId = 4 },
                    new { Id = 35, Label = "Links-Rechts-Diskriminierungsprobleme", Type = "radioYesNo", Value = "", GenericChapterId = 5 },
                    new { Id = 36, Label = "Orientierungsprobleme", Type = "radioYesNo", Value = "", GenericChapterId = 6 },
                    new { Id = 37, Label = "Räumliche Wahrnehmungsprobleme", Type = "radioYesNo", Value = "", GenericChapterId = 7 },
                    new { Id = 38, Label = "Standard Test - linker Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 39, Label = "Standard Test - linkes Bein", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 40, Label = "Standard Test - rechter Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 41, Label = "Standard Test - rechtes Bein", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 42, Label = "Ayres Test Nr. 1 - linker Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 43, Label = "Ayres Test Nr. 1 - rechter Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 44, Label = "Ayres Test Nr. 2 - linker Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 45, Label = "Ayres Test Nr. 2 - rechter Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 46, Label = "Schilder Test - linker Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 47, Label = "Schilder Test - rechter Arm", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 48, Label = "TTNR - von rechts nach links", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 49, Label = "TTNR - von links nach rechts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 50, Label = "STNR - Füße oder Rumpf", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 51, Label = "STNR - Arme", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 52, Label = "STNR - Krabbeltest", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 53, Label = "Spinaler Galant-Reflex - linke Seite", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 54, Label = "Spinaler Galant-Reflex - rechte Seite", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 55, Label = "TLR - Standard Test", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 56, Label = "TLR - Aufrechttest - Beugung", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 57, Label = "TLR - Aufrechttest – Streckung", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 58, Label = "Moro Reflex / FPR - Standard Test", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 59, Label = "Moro Reflex / FPR - Aufrecht: TT", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 60, Label = "Moro Reflex / FPR - Aufrecht: ZT", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 61, Label = "Moro Reflex / FPR - Aufrecht: FF", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 62, Label = "Augenkopfstellreaktionen - nach links", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 63, Label = "Augenkopfstellreaktionen - nach rechts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 64, Label = "Augenkopfstellreaktionen - vorwärts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 65, Label = "Augenkopfstellreaktionen - rückwärts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 66, Label = "Labyrinthkopfstellreaktionen - nach links", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 67, Label = "Labyrinthkopfstellreaktionen - nach rechts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 68, Label = "Labyrinthkopfstellreaktionen - rückwärts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 69, Label = "Labyrinthkopfstellreaktionen - vorwärts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 70, Label = "Amphibien Reaktion - linke Seite (Rückenlage)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 71, Label = "Amphibien Reaktion - rechte Seite (Rückenlage)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 72, Label = "Amphibien Reaktion - linke Seite (Bauchlage)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 73, Label = "Amphibien Reaktion - rechte Seite (Bauchlage)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 74, Label = "Segmentäre Rollreaktion- von den Schultern (links)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 75, Label = "Segmentäre Rollreaktion- von den Schultern (rechts)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 76, Label = "Segmentäre Rollreaktion- von den Hüften (links)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 77, Label = "Segmentäre Rollreaktion- von den Hüften (rechts)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 78, Label = "Babinski Reflex - linker Fuß", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 79, Label = "Babinski Reflex - rechter Fuß", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 80, Label = "Abdominal Reflex (optional)", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 81, Label = "Such-Reflex - links", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 82, Label = "Such-Reflex - rechts", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 83, Label = "Saug-Reflex", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 84, Label = "Erwachsener Saug-Reflex", Type = "radio4", Value = "", GenericChapterId = 8 },
                    new { Id = 85, Label = "Palmar-Reflex - linke Hand", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 86, Label = "Palmar-Reflex - rechte Hand", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 87, Label = "Plantar-Reflex - linker Fuß", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 88, Label = "Plantar-Reflex - rechter Fuß", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 89, Label = "Landau-Reaktion", Type = "radio", Value = "", GenericChapterId = 8 },
                    new { Id = 90, Label = "Fußdominanz - Ball schießen", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 91, Label = "Fußdominanz - Aufstampfen mit einem Fuß", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 92, Label = "Fußdominanz - Auf einen Stuhl steigen", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 93, Label = "Fußdominanz - Auf einem Bein hüpfen", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 94, Label = "Handdominanz - Einen Ball fangen", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 95, Label = "Handdominanz - Klatschen in eine Hand", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 96, Label = "Handdominanz - Schreibhand", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 97, Label = "Handdominanz - Teleskop", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 98, Label = "Augendominanz (Entfernung) - Teleskop", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 99, Label = "Augendominanz (Entfernung) - Ring", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 100, Label = "Augendominanz (Nähe) - Lochkarte", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 101, Label = "Augendominanz (Nähe) - Ring", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 102, Label = "Ohrdominanz - Muschel", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 103, Label = "Ohrdominanz - Lauschen", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 104, Label = "Ohrdominanz - Rufen (Hinweis auf Hemisphärendominanz)", Type = "radioLeftRight", Value = "", GenericChapterId = 9 },
                    new { Id = 105, Label = "Fixierungsschwierigkeiten", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 106, Label = "Beeinträchtigte Folgebewegungen (tracking- horizontal)", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 107, Label = "Beeinträchtigte Folgebewegungen (tracking-vertikal)", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 108, Label = "Verfolgen der Hand mit den Augen (eye-hand-tracking)", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 109, Label = "Augenzittern (Nystagmus)", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 110, Label = "Latente Konvergenz - links", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 111, Label = "Latente Konvergenz - rechts", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 112, Label = "Latente Divergenz - links", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 113, Label = "Latente Divergenz - rechts", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 114, Label = "Konvergenzschwierigkeiten - linkes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 115, Label = "Konvergenzschwierigkeiten - rechtes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 116, Label = "Schwierigkeit, die Augen unabhängig voneinander zu schließen - linkes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 117, Label = "Schwierigkeit, die Augen unabhängig voneinander zu schließen - rechtes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 118, Label = "Beeinträchtigung synchroner Augenbewegungen - linkes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 119, Label = "Beeinträchtigung synchroner Augenbewegungen - rechtes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 120, Label = "Erweiterte periphere Sicht - linkes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 121, Label = "Erweiterte periphere Sicht - rechtes Auge", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 122, Label = "Akkommodationsfähigkeit", Type = "radio", Value = "", GenericChapterId = 10 },
                    new { Id = 123, Label = "Pupillenreaktion auf Licht (optional) - linkes Auge", Type = "input", Value = "", GenericChapterId = 10 },
                    new { Id = 124, Label = "Pupillenreaktion auf Licht (optional) - rechtes Auge", Type = "input", Value = "", GenericChapterId = 10 },
                    new { Id = 125, Label = "Pupillenreaktion auf Licht (optional) - linkes Auge", Type = "input", Value = "", GenericChapterId = 10 },
                    new { Id = 126, Label = "Pupillenreaktion auf Licht (optional) - rechtes Auge", Type = "input", Value = "", GenericChapterId = 10 },
                    new { Id = 127, Label = "Visuelle Unterscheidungsprobleme - Tansley Standard Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 128, Label = "Visuelle Unterscheidungsprobleme - Daniels und Diack Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 129, Label = "Visuelle Unterscheidungsprobleme - Bender Visual Gestalt Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 130, Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Tansley Standard Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 131, Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Daniels und Diack Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 132, Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Bender Visual Gestalt Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 133, Label = "Räumliche Probleme - Tansley Standard Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 134, Label = "Räumliche Probleme - Daniels und Diack Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 135, Label = "Räumliche Probleme - Bender Visual Gestalt Figuren", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 136, Label = "Hinweis auf ‘Stimulusgebundenheit’", Type = "radio", Value = "", GenericChapterId = 11 },
                    new { Id = 137, Label = "Abschreiben eines kurzen Textes", Type = "input", Value = "", GenericChapterId = 11 },
                    new { Id = 138, Label = "Mann-Zeichnen-Test Test (Aston Index) - Entwicklungsalter", Type = "input", Value = "", GenericChapterId = 11 },
                    new { Id = 139, Label = "Mann-Zeichnen-Test Test (Aston Index) - Chronologisches Alter", Type = "input", Value = "", GenericChapterId = 11 },
                    new { Id = 140, Label = "Stiftgriff", Type = "textarea", Value = "", GenericChapterId = 12 },
                    new { Id = 141, Label = "Sitzposition", Type = "textarea", Value = "", GenericChapterId = 12 },
                    new { Id = 142, Label = "Schnelle Ermüdbarkeit", Type = "textarea", Value = "", GenericChapterId = 12 },
                    new { Id = 143, Label = "Kind ist ängstlich und besorgt und mit seinen Ergebnissen nicht zufrieden", Type = "textarea", Value = "", GenericChapterId = 12 },
                    new { Id = 144, Label = "Index der Dysfunktion", Type = "input", Value = "", GenericChapterId = 13 },
                    new { Id = 145, Label = "Grobmotorische Koordination und Gleichgewicht", Type = "textarea", Value = "", GenericChapterId = 13 },
                    new { Id = 146, Label = "Kleinhirnfunktionen", Type = "textarea", Value = "", GenericChapterId = 13 },
                    new { Id = 147, Label = "Dysdiadochokinese", Type = "textarea", Value = "", GenericChapterId = 13 },
                    new { Id = 148, Label = "Aberrante Reflexe", Type = "textarea", Value = "", GenericChapterId = 13 },
                    new { Id = 149, Label = "Okulomotorische Funktionen", Type = "textarea", Value = "", GenericChapterId = 13 },
                    new { Id = 150, Label = "Visuelle Wahrnehmungsfunktionen", Type = "textarea", Value = "", GenericChapterId = 13 }


                );
            });
        }*/
    }
}
