using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using rl_bl.Context;
using rl_contract.Models;
using rl_contract.Models.Review;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using IMapper = AutoMapper.IMapper;

namespace rl_bl
{
    public class PatientBL
    {
        public PatientBL()
        {

        }

        public void addRelevantPatientData(Patient patient, DBContext context)
        {
            // Reviews zufügen
            addReviews(patient);

            // Testung zufügen
            addTestung(patient, context);

            addAnamnese(patient, context);

        }

        private void addAnamnese(Patient patient, DBContext context)
        {
            Anamnese item = new Anamnese()
            {
                Date = DateTime.Now,
                Name = "Anamnese (Fragebogen / Kinder)",
                PatientId = patient.Id,
                CountOfPositivAnswers = -1
            };
            context.Anamnesen.Add(item);
            context.SaveChanges();


            // Add Chapters
            int itemId = item.Id.Value;

            AnamneseChapter firstChapter = new AnamneseChapter()
            {
                Name = "0. Allgemeines",
                AnamneseId = itemId
            };

            AnamneseChapter[] chapters = new AnamneseChapter[]
            {
                new AnamneseChapter { Name = "II. TESTS ZUR MOTORISCHEN ENTWICKLUNG", AnamneseId = itemId},
                new AnamneseChapter { Name = "III. TESTS ZUR ÜBERPRÜFUNG VON KLEINHIRNFUNKTIONEN", AnamneseId = itemId },
                new AnamneseChapter { Name = "IV. TESTS ZUR DYSDIADOCHOKINESE", AnamneseId = itemId },
                new AnamneseChapter { Name = "V. LINKS-RECHTS-DISKRIMINIERUNGSPROBLEME", AnamneseId = itemId },
                new AnamneseChapter { Name = "VI. ORIENTIERUNGSPROBLEME", AnamneseId = itemId },
                new AnamneseChapter { Name = "VII. RÄUMLICHE WAHRNEHMUNGSPROBLEME", AnamneseId = itemId },
                new AnamneseChapter { Name = "VIII. TESTS ZU ABERRANTEN REFLEXEN", AnamneseId = itemId },
                new AnamneseChapter { Name = "IX. TESTS ZUR SEITIGKEITSÜBERPRÜFUNG", AnamneseId = itemId },
                new AnamneseChapter { Name = "X. ÜBERPRÜFUNG DER AUGENMUSKELMOTORIK", AnamneseId = itemId },
                new AnamneseChapter { Name = "XI. VISUELLE WAHRNEHMUNGSÜBERPRÜFUNG", AnamneseId = itemId },
                new AnamneseChapter { Name = "ZUSÄTZLICHE BEOBACHTUNGEN UND NOTIZEN", AnamneseId = itemId },
                new AnamneseChapter { Name = "ERGEBNISZUSAMMENFASSUNG", AnamneseId = itemId }
            };

            item.Chapters = new List<AnamneseChapter>();
            item.Chapters.Add(firstChapter);
            item.Chapters.AddRange(chapters);
            context.SaveChanges();

            int firstChapterId = firstChapter.Id.Value;

            AnamneseQuestion[] listQuestions = new AnamneseQuestion[]
            {
                new AnamneseQuestion { Label = "Leidensdruck beim Kind?", Type = "textarea", Value = "", AnamneseChapterId = firstChapterId },
                new AnamneseQuestion { Label = "Was wurde/ wird unternommen um den Leidensdruck zu verbessern?", Type = "textarea", Value = "", AnamneseChapterId = firstChapterId },
                new AnamneseQuestion { Label = "Wie viele Kinder haben Sie entbunden?", Type = "input", Value = "", AnamneseChapterId = firstChapterId },
                new AnamneseQuestion { Label = "Welches Kind bei mehreren?", Type = "input", Value = "", AnamneseChapterId = firstChapterId },
                new AnamneseQuestion { Label = "Hatten Sie vorher Fehlgeburten? Warum?", Type = "input", Value = "", AnamneseChapterId = firstChapterId },
                new AnamneseQuestion { Label = "Wurden Sie mit ihrem Kind aufgrund von IVF (künstliche Befruchtung) schwanger?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId },
                new AnamneseQuestion { Label = "Als Sie schwanger waren, hatten Sie irgendwelche medizinischen Probleme?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+1, MetaInfo = "hoher Blutdruck, Hyperemesis gravidarum, drohende Fehlgeburt, vorzeitige Wehen, verordnete Bettruhe, Zwischenblutungen, Eisenmangel, Erkrankungen / Infektionen, Alkohol, Drogen, Umweltgifte, Medikamente, Ernährung, Flugreise, Sauna", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "Hatten Sie eine starke Virusinfektion in den ersten 13. Wochen Ihrer Schwangerschaft?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+1 },
                new AnamneseQuestion { Label = "Standen Sie während Ihrer Schwangerschaft (besonders im 6. Monat) unter starkem emotionalen Stress?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+1 },
                new AnamneseQuestion { Label = "Sind während der Schwangerschaft diagnostische Verfahren durchgeführt worden?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+1, MetaInfo = "Ultraschall, Sonografie, Röntgen, Fruchtwasseruntersuchung o.ä.", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "Erfolgte vor oder während der Schwangerschaft eine Hormonbehandlung", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+1, MetaInfo = "z.B. Progesteron Gabe in der 6. Woche", TextPrefix = "Falls ja, welche und wann:" },
                new AnamneseQuestion { Label = "Wurde Ihr Kind früher (vor der 37. SSW) oder später (nach der 42. SSW) als zum errechneten Termin(+/ -2 Tage) geboren?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+1, TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "War der Geburtsprozess ungewöhnlich oder besonders schwierig?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Wehen, Geburtsdauer, Medikamente, Kaiserschnitt, Zange, Saugglocke, Sturzgeburt, Steißlage, Fruchtwasser, Kristellern, gerissen/ geschnitten", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "War Ihr Kind klein bezogen auf den Geburtszeitpunkt?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "< 2500g oder > 4000 - 4300g", TextPrefix = "Geben Sie bitte das Geburtsgewicht die Geburtslänge und den Kopfumfang an." },
                new AnamneseQuestion { Label = "Gab es irgendwelche Besonderheiten an Ihrem Baby nach der Geburt? Brauchte es Intensivpflege? Kam es dadurch zu einer längeren Trennung? Wochenbettdepression?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Schädelverformung, viele blaue Flecken, Nabelschnur um den Hals, deutlich blaue Verfärbung, schwere Neugeborenengelbsucht, Lanugo-Behaarung, stark mit Käseschmiere bedeckt, Gelbsucht, Fußdeformitäten, Hüftdysplasie", TextPrefix = ":Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "Wie waren die APGAR – Werte Ihres Kindes? (siehe Mutterpass ……/……/……) und der pH-Wert:", Type = "textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "", TextPrefix = "Zusätzliche Angaben zur Schwangerschaft und Geburt (z.B. Einnahme der Pille ?) " },
                new AnamneseQuestion { Label = "Hatte Ihr Kind in den ersten 13 Lebenswochen Schwierigkeiten beim Saugen an der Brust, beim Trinken aus der Flasche ? Hat es viel gespuckt ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Wie lange gestillt, Dauer einer Mahlzeit, Zeitabstände zwischen den Mahlzeiten, Saugbewegungen", TextPrefix = "" },
                new AnamneseQuestion { Label = "Dauerte es auffallend lange, bis es seinen Kopf hochhalten konnte? Oder hat es den Kopf  sehr früh überstreckt gehalten?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "> 4 Monat", TextPrefix = "" },
                new AnamneseQuestion { Label = "War Ihr Kind in den ersten 6 Lebensmonaten ein auffallend ruhiges Baby, so ruhig, dass Sie manchmal befürchteten,es sei in seinem Bettchen gestorben?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "", TextPrefix = "" },
                new AnamneseQuestion { Label = "War Ihr Kind zwischen dem 6. und 18. Lebensmonat sehr aktiv und fordernd? Schlief es wenig und schrie es ständig?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "verstopfte Nase, Mundatmer, schnarchendes Baby, Schlafprobleme, mochte nicht liegen → Druck im Mittelohr (unreife Schluckmuster), ADHS", TextPrefix = "" },
                new AnamneseQuestion { Label = "Als Ihr Kind alt genug war, in der Karre zu sitzen oder sich im Kinderbett zum Stand hochzuziehen, bewegte es sich dort heftig schaukelnd hin und her, so dass sich Karre oder Bett mitbewegten?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Wie ließ es sich beruhigen? → heftiges Schaukeln, Autofahren; Stereotype Bewegungen wie im Liegen den Kopf hin und her bewegen; Schreien beim Hinlegen?", TextPrefix = "" },
                new AnamneseQuestion { Label = "War Ihr Kind ein kleiner „Kopfstoßer“, d.h. stieß es absichtlich mit dem Kopf gegen feste Gegenstände ? Gibt es eine Vorgeschichte von Kopfverletzungen ? Wo waren die Kopfverletzungen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "(präfrontaler Cortex – kein INPP Kind)", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hat Ihr Kind sich nicht zum richtigen Zeitpunkt (ca. ab 6. Monat) oder nur mit physiotherapeutischer Unterstützung vom Rücken auf den Bauch gedreht?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "vom Bauch auf den Rücken ab 8. Monat postnatal", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hat ihr Kind nicht ausreichend/ wenig Zeit in BL verbracht bis zum Krabbeln?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "mochte es die BL, Tagesablauf des Babys, Maxi-Kosi, Wippe, Spreizwindel", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hat Ihr Kind, anstatt zunächst auf dem Bauch zu kriechen und dann auf den Händen und Knien zu krabbeln, sich auf andere Weise fortbewegt(z.B.rollend, auf dem Po rutschend, im „Bärengang“ auf Händen und Füßen)?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "(Kriechen = ab 7. - 8. Monat, homolateral die ersten 2 – 4 Wochen; Robben = ab 8. Monat, kontralateral; Krabbeln = ab 8. - 9.Monat, ab 11.Monat flüssig; Sitzen = ab 8.Monat; Stand = ab 8.Monat mit festhalten, ab 10.bis 11.Monat frei)", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "Hat Ihr Kind auffallend spät (> 1,5 Jahre) oder früh (< 12 Monate) laufen gelernt?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Lauflerngeräte, Babywippe, Maxi- Kosi, Hopser", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hat Ihr Kind spät sprechen gelernt (Zwei- und Dreiwortsätze) (> 2,5 Jahre)?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "eigene Sprache, gesabbert, Schwierigkeiten bei bestimmten Lauten, Gesungene Sprache", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte es während der ersten 18 Lebensmonate irgendwelche Krankheiten, die mit hohem Fieber und / oder Krämpfen verbunden waren ? ", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "sehr schnell, sehr hoch gefiebert, Narkose", TextPrefix = "Falls ja, bitte Einzelheiten angeben (konnte es nach der Krankheit etwas nicht mehr so gut wie vorher?)" },
                new AnamneseQuestion { Label = "Hatte es  auffällige Schwierigkeiten sich selber anziehen zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Reihenfolge, falsch herum, Schleife (>5 bis 5,6Jahre), Knöpfe, Reißverschluss, „bequem / faul“, Hose fällt schwer anzuziehen", TextPrefix = "" },
                new AnamneseQuestion { Label = "Litt bzw. leidet Ihr Kind unter Hautproblemen (trockene Haut, Milchschorf,  Neurodermitis, Ekzeme) oder Asthma(Husten, Reizhusten)?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "", TextPrefix = "" },
                new AnamneseQuestion { Label = "Zeigt es irgendwelche allergische Reaktionen? ", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Allergien in der Familie, Heuschnupfen, Auslöser: Milch/Eier/Weizen, ständig laufende Nase, Suchtmittel", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "Gab es irgendwelche auffälligen Reaktionen nach den  Impfungen?", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "längeres schlafen, erhöhte Temperatur", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "Lutschte Ihr Kind bis etwa zum 5. Lebensjahr oder länger am Daumen? (Hatte es einen Schnuller (> 2 LJ?), Kuscheltier)", Type = "radioYesNo, input", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "", TextPrefix = "Falls ja, an welchem (links, rechts):" },
                new AnamneseQuestion { Label = "Machte oder macht  Ihr Kind auch noch nach dem Alter von 5 Jahren gelegentlich ins Bett?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "Wann war es trocken? Toilettentraining, spinaler Galant, gehäufte Mittelohrentzündungen, Hypoglykämie, nächtlicher O² Mangel, viel Milch Trinker", TextPrefix = "" },
                new AnamneseQuestion { Label = "Zusätzliche Angaben zum Vorschulalter (Auffälligkeiten in der Motorik, Sport, externe Betreuung, Trennungsangst, Schreckhaft, Ängstlich, viele Freunde, Einzelgänger, Streit, Reaktionen auf Veränderungen, emotionale Auffälligkeiten, was sagen die Erzieher über das Kind, schneiden, malen, puzzeln, Körperhaltung, Spielverhalten, zündeln, Beziehung zu Tieren, Verletzungen)", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+2, MetaInfo = "", TextPrefix = "" },
                new AnamneseQuestion { Label = "Leidet Ihr Kind unter Reiseübelkeit?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "> 8Jahre, Auto fahren und lesen, Schiffe, Trampolin, Schaukeln, Höhenangst, Karussell, Fahrstuhl, Klettern", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte Ihr Kind in den ersten zwei Grundschuljahren Schwierigkeiten, das Lesen zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "Tempo, Betonung, Motivation, spezielle Bücher, Comics", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte es Schwierigkeiten beim Schreiben lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "Sitzhaltung, Stifthaltung, Rechtschreibung, Grammatik, Motorik beim Schreiben, schreiben anstrengend, kann es die Linien halten, drückt es stark auf, sehr kleine Schrift", TextPrefix = "" },
                new AnamneseQuestion { Label = "Falls es zunächst Druckschrift erlernte, hatte es Probleme mit der Schreibschrift?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte Schwierigkeiten, die Uhrzeit ablesen zu lernen (nicht Digitaluhr) bzw. sich insgesamt in der Zeit(Wochentage, Monate etc.) zurecht zu finden ? > 8.LJ", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "räumliche Wahrnehmung, Morgens/ Mittags/ Abends, Gestern/ Morgen, Jahreszeiten, Orientierungssinn, Orientierung im Raum, Geschwindigkeit beim Anziehen, Aufräumen", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte es Schwierigkeiten, Fahrradfahren (ohne Stützräder) zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "kann es fahren, seit wann, langsam fahren möglich, wie fährt es", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte es Schwierigkeiten, Schwimmen zu lernen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "Verhältnis zum Wasser", TextPrefix = "" },
                new AnamneseQuestion { Label = "Konnte es besser unter als über Wasser schwimmen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte Ihr Kind im Verlauf der ersten 8 Lebensjahre (ausgenommen die ersten 18 Lebensmonate) Krankheiten mit sehr hohem Fieber, Bewusstlosigkeit oder Krämpfen ? ", Type = "radioYesNo, textarea", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "Vollnarkose, Infektanfälligkeit, Antibiotika, Meningitis, Tumore, Frakturen", TextPrefix = "Falls ja, bitte Einzelheiten angeben: " },
                new AnamneseQuestion { Label = "War / ist Ihr Kind ein „Hals- Nasen- und Ohren“ Kind, d.h. litt / leidet es an häufigen Infektionen im Hals -, Nasen - und Ohrenbereich ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "Schnupfnase, Mittelohrentzündungen → Hörminderung → Ursache sind dann nicht die Reflexe", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hatte bzw. hat Ihr Kind Schwierigkeiten, einen (kleinen) Ball zu fangen oder andere Auge- / Hand - Koordinationsprobleme ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+4, MetaInfo = "Dyspraxie → kleckern beim Essen und Trinken, Fixierungsprobleme, Raum – Zeit – Gefühl gering, Schreckhaft, Verhältnis zu Bällen, Tollpatschig, zu langsam, zu schnell, greift gar nicht, beim hinfallen abgestützt", TextPrefix = "" },
                new AnamneseQuestion { Label = "Hat Ihr Kind Schwierigkeiten still zu sitzen und wird es deswegen ständig vom Lehrer ermahnt ? Bevorzugt es auffällige Sitzpositionen?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+5, MetaInfo = "spinaler Galant (Stuhllehne/Gürtel), W – Sitz, Körperhaltung, Beinhaltung (um den Stuhl geschlungen/ hochgezogenes Bein),Sitzdauer, kippeln, liegend schreibend, Geräuscherzeugung", TextPrefix = "" },
                new AnamneseQuestion { Label = "Macht Ihr Kind zahlreiche Fehler, wenn es aus einem Buch oder von der Tafel abschreibt?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+5, MetaInfo = "", TextPrefix = "" },
                new AnamneseQuestion { Label = "Wenn Ihr Kind in Schule einen Aufsatz schreibt, verdreht es dabei gelegentlich Buchstaben oder lässt einzelne Buchstaben oder Wörter aus(auch evtl.Zahlendreher) ? ", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+5, MetaInfo = "lesen von rechts nach links, häufige Dreher > 8LJ", TextPrefix = "" },
                new AnamneseQuestion { Label = "Reagiert Ihr Kind bei plötzlichen, unerwarteten Geräuschen oder Bewegungen auffallend stark?", Type = "radioYesNo", Value = "", AnamneseChapterId = firstChapterId+5, MetaInfo = "Silvester, Staubsauger, Gewitter, Luftballons, Sportarten, Hobby, bewegt es sich gern, Traumverhalten / Albträume, Schlafhaltung", TextPrefix = "" },
                new AnamneseQuestion { Label = "Zusätzliche Angaben  (z.B. Ernährungsverhalten: Süßigkeiten, Fleisch, Gemüse, Milch; vorangegangene oder andauernde Behandlungen bzw.Therapien(Therapiemüdigkeit), besondere Familiensituationen, Belastungen für das Kind, Kopfschmerzen, Schlafhaltung, Erschöpfung, Hypersensitivität: Geruch / Sonne / Kuscheln / Material / Geschmack / vestibulär):", Type = "textarea", Value = "", AnamneseChapterId = firstChapterId+6, MetaInfo = "", TextPrefix = "" }
            };

            context.AnamneseQuestions.AddRange(listQuestions);
            patient.Anamnese = item;

        }

        private void addTestung(Patient patient, DBContext context)
        {
            Testung testung = new Testung
            {
                Date = DateTime.Now,
                Name = "Testung - " + patient.Firstname + " " + patient.Lastname,
                PatientId = patient.Id
            };
            context.Testungen.Add(testung);
            context.SaveChanges();


            // Add Chapters
            int testungId = testung.Id.Value;

            TestungChapter firstChapter = new TestungChapter
            {
                Name = "I. TESTS ZUR ÜBERPRÜFUNG DER GROBMOTORISCHEN KOORDINAION UND DES GLEICHGEWICHTS",
                TestungId = testungId
            };
            TestungChapter[] chapters = new TestungChapter[]
            {
                new TestungChapter { Name = "II. TESTS ZUR MOTORISCHEN ENTWICKLUNG", TestungId = testungId, Score = -1},
                new TestungChapter { Name = "III. TESTS ZUR ÜBERPRÜFUNG VON KLEINHIRNFUNKTIONEN", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "IV. TESTS ZUR DYSDIADOCHOKINESE", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "V. LINKS-RECHTS-DISKRIMINIERUNGSPROBLEME", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "VI. ORIENTIERUNGSPROBLEME", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "VII. RÄUMLICHE WAHRNEHMUNGSPROBLEME", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "VIII. TESTS ZU ABERRANTEN REFLEXEN", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "IX. TESTS ZUR SEITIGKEITSÜBERPRÜFUNG", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "X. ÜBERPRÜFUNG DER AUGENMUSKELMOTORIK", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "XI. VISUELLE WAHRNEHMUNGSÜBERPRÜFUNG", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "ZUSÄTZLICHE BEOBACHTUNGEN UND NOTIZEN", TestungId = testungId, Score = -1 },
                new TestungChapter { Name = "ERGEBNISZUSAMMENFASSUNG", TestungId = testungId, Score = -1 }
            };

            testung.Chapters = new List<TestungChapter>();
            testung.Chapters.Add(firstChapter);
            testung.Chapters.AddRange(chapters);
            context.SaveChanges();

            int firstChapterId = firstChapter.Id.Value;

            // Add Questions
            TestungQuestion[] listQuestions = new TestungQuestion[]
            {
                new TestungQuestion { Label = "Aufrichten aus Rückenlage in den Stand", Type = "radio", Value = "", TestungChapterId = firstChapterId  },
                new TestungQuestion { Label = "Aufrichten aus Bauchlage in den Stand", Type = "radio",  Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Romberg Test (Augen geöffnet)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Romberg Test (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Mann Test (Augen geöffnet)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Mann Test (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Einbeinstand", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Marschieren und Umdrehen", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Zehenspitzengang (vorwärts) 0 1", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Zehenspitzengang (rückwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Tandem Gang (vorwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Tandem Gang (rückwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Fog Walk (vorwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Fog Walk (rückwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Slalom Gang (vorwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Slalom Gang (rückwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Fersengang (nur vorwärts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Hüpfen auf einem Bein (links oder rechts)", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Hopserlauf", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Windmühle", Type = "radio", Value = "", TestungChapterId = firstChapterId },
                new TestungQuestion { Label = "Kriechen auf dem Bauch", Type = "radio2", Value = "", TestungChapterId = firstChapterId+1 },
                new TestungQuestion { Label = "Krabbeln auf Händen und Knien", Type = "radio3", Value = "", TestungChapterId = firstChapterId+1 },
                new TestungQuestion { Label = "Ferse auf Schienbein (linke Ferse auf rechtes Schienbein)", Type = "radio", Value = "", TestungChapterId = firstChapterId+2 },
                new TestungQuestion { Label = "Ferse auf Schienbein (rechte Ferse auf linkes Schienbein)", Type = "radio", Value = "", TestungChapterId = firstChapterId+2 },
                new TestungQuestion { Label = "Zeigefinger-Annäherung (Augen offen)", Type = "radio", Value = "", TestungChapterId = firstChapterId+2 },
                new TestungQuestion { Label = "Zeigefinger-Annäherung (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = firstChapterId+2 },
                new TestungQuestion { Label = "Finger an die Nase (Augen offen)", Type = "radio", Value = "", TestungChapterId = firstChapterId+2 },
                new TestungQuestion { Label = "Finger an die Nase (Augen geschlossen)", Type = "radio", Value = "", TestungChapterId = firstChapterId+2 },
                new TestungQuestion { Label = "Finger (linke Hand)", Type = "radio", Value = "", TestungChapterId = firstChapterId+3 },
                new TestungQuestion { Label = "Finger (rechte Hand)", Type = "radio", Value = "", TestungChapterId = firstChapterId+3 },
                new TestungQuestion { Label = "Hand (links)", Type = "radio", Value = "", TestungChapterId = firstChapterId+3 },
                new TestungQuestion { Label = "Hand (rechts)", Type = "radio", Value = "", TestungChapterId = firstChapterId+3 },
                new TestungQuestion { Label = "Fuß (links)", Type = "radio", Value = "", TestungChapterId = firstChapterId+3 },
                new TestungQuestion { Label = "Fuß (rechts)", Type = "radio", Value = "", TestungChapterId = firstChapterId+3 },
                new TestungQuestion { Label = "Links-Rechts-Diskriminierungsprobleme", Type = "radioYesNo", Value = "", TestungChapterId = firstChapterId+4 },
                new TestungQuestion { Label = "Orientierungsprobleme", Type = "radioYesNo", Value = "", TestungChapterId = firstChapterId+5 },
                new TestungQuestion { Label = "Räumliche Wahrnehmungsprobleme", Type = "radioYesNo", Value = "", TestungChapterId = firstChapterId+6 },
                new TestungQuestion { Label = "Standard Test - linker Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Standard Test - linkes Bein", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Standard Test - rechter Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Standard Test - rechtes Bein", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Ayres Test Nr. 1 - linker Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Ayres Test Nr. 1 - rechter Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Ayres Test Nr. 2 - linker Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Ayres Test Nr. 2 - rechter Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Schilder Test - linker Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Schilder Test - rechter Arm", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "TTNR - von rechts nach links", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "TTNR - von links nach rechts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "STNR - Füße oder Rumpf", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "STNR - Arme", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "STNR - Krabbeltest", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Spinaler Galant-Reflex - linke Seite", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Spinaler Galant-Reflex - rechte Seite", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "TLR - Standard Test", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "TLR - Aufrechttest - Beugung", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "TLR - Aufrechttest – Streckung", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Moro Reflex / FPR - Standard Test", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Moro Reflex / FPR - Aufrecht: TT", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Moro Reflex / FPR - Aufrecht: ZT", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Moro Reflex / FPR - Aufrecht: FF", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Augenkopfstellreaktionen - nach links", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Augenkopfstellreaktionen - nach rechts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Augenkopfstellreaktionen - vorwärts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Augenkopfstellreaktionen - rückwärts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Labyrinthkopfstellreaktionen - nach links", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Labyrinthkopfstellreaktionen - nach rechts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Labyrinthkopfstellreaktionen - rückwärts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Labyrinthkopfstellreaktionen - vorwärts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Amphibien Reaktion - linke Seite (Rückenlage)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Amphibien Reaktion - rechte Seite (Rückenlage)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Amphibien Reaktion - linke Seite (Bauchlage)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Amphibien Reaktion - rechte Seite (Bauchlage)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Segmentäre Rollreaktion- von den Schultern (links)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Segmentäre Rollreaktion- von den Schultern (rechts)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Segmentäre Rollreaktion- von den Hüften (links)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Segmentäre Rollreaktion- von den Hüften (rechts)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Babinski Reflex - linker Fuß", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Babinski Reflex - rechter Fuß", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Abdominal Reflex (optional)", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Such-Reflex - links", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Such-Reflex - rechts", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Saug-Reflex", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Erwachsener Saug-Reflex", Type = "radio4", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Palmar-Reflex - linke Hand", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Palmar-Reflex - rechte Hand", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Plantar-Reflex - linker Fuß", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Plantar-Reflex - rechter Fuß", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Landau-Reaktion", Type = "radio", Value = "", TestungChapterId = firstChapterId+7 },
                new TestungQuestion { Label = "Fußdominanz - Ball schießen", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Fußdominanz - Aufstampfen mit einem Fuß", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Fußdominanz - Auf einen Stuhl steigen", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Fußdominanz - Auf einem Bein hüpfen", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Handdominanz - Einen Ball fangen", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Handdominanz - Klatschen in eine Hand", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Handdominanz - Schreibhand", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Handdominanz - Teleskop", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Augendominanz (Entfernung) - Teleskop", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Augendominanz (Entfernung) - Ring", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Augendominanz (Nähe) - Lochkarte", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Augendominanz (Nähe) - Ring", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Ohrdominanz - Muschel", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Ohrdominanz - Lauschen", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Ohrdominanz - Rufen (Hinweis auf Hemisphärendominanz)", Type = "radioLeftRight", Value = "", TestungChapterId = firstChapterId+8 },
                new TestungQuestion { Label = "Fixierungsschwierigkeiten", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Beeinträchtigte Folgebewegungen (tracking- horizontal)", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Beeinträchtigte Folgebewegungen (tracking-vertikal)", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Verfolgen der Hand mit den Augen (eye-hand-tracking)", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Augenzittern (Nystagmus)", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Latente Konvergenz - links", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Latente Konvergenz - rechts", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Latente Divergenz - links", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Latente Divergenz - rechts", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Konvergenzschwierigkeiten - linkes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Konvergenzschwierigkeiten - rechtes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Schwierigkeit, die Augen unabhängig voneinander zu schließen - linkes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Schwierigkeit, die Augen unabhängig voneinander zu schließen - rechtes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Beeinträchtigung synchroner Augenbewegungen - linkes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Beeinträchtigung synchroner Augenbewegungen - rechtes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Erweiterte periphere Sicht - linkes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Erweiterte periphere Sicht - rechtes Auge", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Akkommodationsfähigkeit", Type = "radio", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Pupillenreaktion auf Licht (optional) - linkes Auge", Type = "input", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Pupillenreaktion auf Licht (optional) - rechtes Auge", Type = "input", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Pupillenreaktion auf Licht (optional) - linkes Auge", Type = "input", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Pupillenreaktion auf Licht (optional) - rechtes Auge", Type = "input", Value = "", TestungChapterId = firstChapterId+9 },
                new TestungQuestion { Label = "Visuelle Unterscheidungsprobleme - Tansley Standard Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Visuelle Unterscheidungsprobleme - Daniels und Diack Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Visuelle Unterscheidungsprobleme - Bender Visual Gestalt Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Tansley Standard Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Daniels und Diack Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Visuo-motorische Integrationsschwierigkeit (Auge-Hand-Koordination) - Bender Visual Gestalt Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Räumliche Probleme - Tansley Standard Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Räumliche Probleme - Daniels und Diack Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Räumliche Probleme - Bender Visual Gestalt Figuren", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Hinweis auf ‘Stimulusgebundenheit’", Type = "radio", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Abschreiben eines kurzen Textes", Type = "input", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Mann-Zeichnen-Test Test (Aston Index) - Entwicklungsalter", Type = "input", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Mann-Zeichnen-Test Test (Aston Index) - Chronologisches Alter", Type = "input", Value = "", TestungChapterId = firstChapterId+10 },
                new TestungQuestion { Label = "Stiftgriff", Type = "textarea", Value = "", TestungChapterId = firstChapterId+11 },
                new TestungQuestion { Label = "Sitzposition", Type = "textarea", Value = "", TestungChapterId = firstChapterId+11 },
                new TestungQuestion { Label = "Schnelle Ermüdbarkeit", Type = "textarea", Value = "", TestungChapterId = firstChapterId+11 },
                new TestungQuestion { Label = "Kind ist ängstlich und besorgt und mit seinen Ergebnissen nicht zufrieden", Type = "textarea", Value = "", TestungChapterId = firstChapterId+11 },
                new TestungQuestion { Label = "Index der Dysfunktion", Type = "input", Value = "", TestungChapterId = firstChapterId+12 },
                new TestungQuestion { Label = "Grobmotorische Koordination und Gleichgewicht", Type = "textarea", Value = "", TestungChapterId = firstChapterId+12 },
                new TestungQuestion { Label = "Kleinhirnfunktionen", Type = "textarea", Value = "", TestungChapterId = firstChapterId+12 },
                new TestungQuestion { Label = "Dysdiadochokinese", Type = "textarea", Value = "", TestungChapterId = firstChapterId+12 },
                new TestungQuestion { Label = "Aberrante Reflexe", Type = "textarea", Value = "", TestungChapterId = firstChapterId+12 },
                new TestungQuestion { Label = "Okulomotorische Funktionen", Type = "textarea", Value = "", TestungChapterId = firstChapterId+12 },
                new TestungQuestion { Label = "Visuelle Wahrnehmungsfunktionen", Type = "textarea", Value = "", TestungChapterId = firstChapterId+12 }
            };
            context.TestungQuestions.AddRange(listQuestions);
            patient.Testung = testung;
        }

        private void addReviews(Patient patient)
        {
            string[] list = new[] { "Übungseinweisung" };
            patient.Reviews = new List<Review>();
            List<ProblemHierarchie> problemHierarchies = new List<ProblemHierarchie>();

            int i = 0;
            foreach (string entry in list)
            {
                for (int j = 0; j < 5; j++)
                {
                    problemHierarchies.Add(
                        new ProblemHierarchie
                        {
                            InitialValue = "",
                            ChangedValue = "",
                        }
                    );
                }

                var review = new Review
                {
                    Date = DateTime.Now.Date.AddMonths(i),
                    Name = entry,
                    Payed = false,
                    Exercises = "",
                    Reasons = "",
                    PatientId = patient.Id,
                    ObservationsParents = "",
                    ObservationsChild = "",
                    ExerciseAccomplishment = "",
                    ProblemHierarchies = problemHierarchies,
                };
                patient.Reviews.Add(review);
                i++;
            }
        }

        public Review addReview(Patient patient)
        {
            List<ProblemHierarchie> problemHierarchies = new List<ProblemHierarchie>();
            for (int j = 0; j < 5; j++)
            {
                problemHierarchies.Add(
                    new ProblemHierarchie
                    {
                        InitialValue = "",
                        ChangedValue = "",
                    }
                );
            }

            var review = new Review
            {
                Date = DateTime.Now.Date,
                Name = "Review",
                Payed = false,
                Exercises = "",
                Reasons = "",
                PatientId = patient.Id,
                ObservationsParents = "",
                ObservationsChild = "",
                ExerciseAccomplishment = "",
                ProblemHierarchies = problemHierarchies,
            };
            /*patient.Reviews.Add(review);*/
            return review;
        }

        public void addReviewTests(Review review, List<TestungChapter> chapters, DBContext context)
        {
            chapters.ForEach(chapter =>
            {
                ReviewChapter tmpChapter = review.Chapters.Find(x => x.Name == chapter.Name);
                int itemId = -1;
                if (tmpChapter == null)
                {
                    ReviewChapter item = new ReviewChapter()
                    {
                        Name = chapter.Name,
                        Score = chapter.Score,
                        ReviewId = review.Id
                    };
                    context.ReviewChapters.Add(item);
                    context.SaveChanges();

                    /*mapper.Map(chapter, revChapter);*/
                    itemId = item.Id.Value;
                }
                else
                {
                    itemId = tmpChapter.Id.Value;
                }

                chapter.Questions.ForEach(question =>
                {
                    bool bFound = false;
                    review.Chapters.ForEach(c =>
                    {
                        var tmpQuestion = c.Questions.Find(q => q.Label == question.Label);
                        if (tmpQuestion != null && !bFound)
                            bFound = true;

                    });
                    
                    if (!bFound)
                    {
                        ReviewQuestion revQ = new ReviewQuestion()
                        {
                            Type = question.Type,
                            Label = question.Label,
                            Value = question.Value,
                            ReviewChapterId = itemId
                        };
                        /*mapper.Map(question, revQuestion);*/
                        /*revQuestion.ReviewChapterId = itemId;
                        revChapter.Questions.Add(revQuestion);*/
                        context.ReviewQuestion.Add(revQ);
                    }

                });
                    context.SaveChanges();
            });

            /*chapters.ForEach(chapter =>
            {
                ReviewChapter revChapter = new ReviewChapter();
                mapper.Map(chapter, revChapter);
                questions.ForEach(question =>
                {
                    ReviewQuestion revQuestion = new ReviewQuestion();
                    mapper.Map(question, revQuestion);
                    revQuestion.ReviewChapterId = revChapter.Id;
                    revChapter.Questions.Add(revQuestion);
                });
                revChapter.ReviewId = review.Id;
                review.Chapters.Add(revChapter);
            });*/
        }

    }
}
