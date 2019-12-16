using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Threading;

using eRepublik;
using eRepublik.Citizens;

namespace eRepublik_Wiki_Citizen_Page_Creator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            LoadDraftList();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (cmbDrafts.Text != "" && File.Exists("Drafts//" + cmbDrafts.Text))
                {
                    Enabled = false;
                    CreatePage(Convert.ToInt32(txtID.Text), cmbDrafts.Text);
                    Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please select a Draft!", "Draft", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoadDraftList();
                }
            }
            else
                MessageBox.Show("Please select an ID!", "ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void CreatePage(int ctzId, string draft)
        {
            Citizen ctz = new Citizen();
            ctz.ID = ctzId;
            ctz.Scan();

            string avatar = "";

            #region Military Unit Avatars
            switch (ctz.MilitaryUnit.Name)
            {
                case "Dracones":
                    avatar = "Dracones_Avatar.jpg";
                    break;
                case "Legiunea Umbrelor":
                    avatar = "LU-Uniforma pt. baieti.jpg";
                    break;
                case "Fortele Aeriene Romane":
                    avatar = "FAR.jpg";
                    break;
                case "FSR - Chine":
                case "Fortele Speciale Romane":
                    avatar = "FSR.jpg";
                    break;
                case "Fortele Terestre Romane":
                    avatar = "FTR.jpg";
                    break;
                case "Bratul de Fier":
                    avatar = "BF.jpg";
                    break;
                case "Nemesis":
                case "Nemesis Galaxy":
                    avatar = "Nemesisbig.jpg";
                    break;
                case "Vanatorii de Munte":
                    avatar = "Mountain Hunters Avatar.jpg";
                    break;
                case "Praetorian Guard":
                    avatar = "Noua uniforma PG.jpg";
                    break;
                case "Soimii Patriei":
                    avatar = "SoimiiPatriei.jpg";
                    break;
                case "Ordinul Dragonului":
                    avatar = "OD.jpg";
                    break;
                case "Haiducii":
                    avatar = "Haiducii.jpg";
                    break;
                case "Gryphon Power Soldiers":
                    avatar = "Gryphon Power Soldiers.png";
                    break;
                case "Garda Nationala":
                    avatar = "Romanian_National_Guard_Avatar.jpg";
                    break;
                case "Vulturul Negru":
                    avatar = "VN.jpg";
                    break;
                case "Zbang Army":
                    avatar = "ZA.jpg";
                    break;
                case "Titanium Sword":
                    avatar = "TS.jpg";
                    break;
                case "Iron Cross":
                    avatar = "IC.jpg";
                    break;
                case "Acvila":
                    avatar = "Acvila.jpg";
                    break;
                case "Air Tanks":
                    avatar = "Air Tanks.jpg";
                    break;
                case "Garda de Fier - eLAM":
                    avatar = "GDF-eLAM.jpg";
                    break;
                case "Semper Pro Patria":
                    avatar = "spp.jpg";
                    break;
                case "Cruciatii":
                    avatar = "The_Crusaders_Uniform.jpg";
                    break;
                default:
                    avatar = "";
                    break;
            }

            if (ctz.MilitaryUnit.Name == "Dracones" && ctz.PoliticalParty.Name == "The Matza Party")
                avatar = "Dracones Matza.JPG";
            #endregion

            string file = "";

            string nationality;
            if (chkRomania.Checked)
            {
                file += "{{RomaniaFlagCorner}}\n";
                nationality = "Romania";
            }
            else
                nationality = ctz.Citizenship;

            if (ctz.PoliticalParty.ID == 2862)
                file += "{{MatzaFlagCorner}}\n";

            #region Categories
            if (ctz.MilitaryUnit.ID != -1)
            {
                if (ctz.MilitaryUnit.Position == "Commander" ||
                    ctz.MilitaryUnit.Position == "2nd Commander")
                    file += "[[Category:Commanders of " + ctz.MilitaryUnit.Name + "]]\n";

                file += "[[Category:Members of " + ctz.MilitaryUnit.Name + "]]\n";
            }

            if (ctz.PoliticalParty.ID != -1)
                file += "[[Category:Members of " + ctz.PoliticalParty.Name + "]]\n";

            if (ctz.Newspaper.ID != -1)
                file += "[[Category:Press Directors of " + nationality + "]]\n";

            if (ctz.EliteCitizen)
                file += "[[Category:Elite Citizens of " + nationality + "]]\n";

            if (ctz.RankValue > 65)
                file += "[[Category:Titans of " + nationality + "]]\n";

            if (ctz.Medals.TopFighter > 0)
                file += "[[Category:Top Fighters of " + nationality + "]]\n";

            if (ctz.Medals.SocietyBuilder > 0)
                file += "[[Category:Society Builders of " + nationality + "]]\n";
            
            if (ctz.Medals.CountryPresident > 0)
                file += "[[Category:Presidents]]\n";

            new Thread((ThreadStart)delegate
            {
                if (GnGraduate(ctz))
                    file += "[[Category:Graduates of Garda Nationala]]\n";
            }).Start();

            file += "[[Category:People of " + nationality + "]]\n";

            file += "[[Category:Citizens]]\n";
            #endregion

            #region Replace
            file += File.ReadAllText("Drafts//" + draft)
                .Replace("%%%userName%%%", ctz.Name)
                .Replace("%%%userId%%%", ctz.ID.ToString())
                .Replace("%%%citizenship%%%", ctz.Citizenship)
                .Replace("%%%nationality%%%", nationality)
                .Replace("%%%avatar%%%", avatar)
                .Replace("%%%citizenStatus%%%", ctz.CitizenStatus.ToLower())
                .Replace("%%%experiencePoints%%%", ctz.Experience.ToString("#,##0"))
                .Replace("%%%experiencePointsRaw%%%", ctz.Experience.ToString())
                .Replace("%%%level%%%", ctz.Level.ToString())
                .Replace("%%%elite%%%", ctz.EliteCitizen.ToString().Replace("True", "yes").Replace("False", "no"))
                .Replace("%%%ambassador%%%", ctz.Ambassador.ToString().Replace("True", "yes").Replace("False", "no"))
                .Replace("%%%moderator%%%", ctz.Moderator.ToString().Replace("True", "yes").Replace("False", "no"))
                .Replace("%%%nationalRank%%%", ctz.NationalRank.ToString())
                .Replace("%%%birthDate%%%", ctz.BirthDay.Date.ToString(GameInfo.Culture.DateTimeFormat.FullDateTimePattern, GameInfo.Culture))
                .Replace("%%%birthEday%%%", ctz.BirthDay.eDay.ToString())
                .Replace("%%%age%%%", ctz.BirthDay.Age.ToString())
                .Replace("%%%friends%%%", ctz.Friends.ToString("#,##0"))
                .Replace("%%%firstFriendName%%%", ctz.FirstFriendName)
                .Replace("%%%residenceCountry%%%", ctz.Residence.Country)
                .Replace("%%%residenceRegion%%%", ctz.Residence.Region)
                .Replace("%%%partyId%%%", ctz.PoliticalParty.ID.ToString())
                .Replace("%%%partyName%%%", ctz.PoliticalParty.Name)
                .Replace("%%%partyPosition%%%", ctz.PoliticalParty.Position)
                .Replace("%%%newspaperId%%%", ctz.Newspaper.ID.ToString())
                .Replace("%%%newspaperName%%%", ctz.Newspaper.Name)
                .Replace("%%%newspaperPosition%%%", ctz.Newspaper.Position)
                .Replace("%%%militaryUnit%%%", ctz.MilitaryUnit.Name)
                .Replace("%%%militaryUnitPosition%%%", ctz.MilitaryUnit.Position)
                .Replace("%%%militaryRank%%%", ctz.Rank.Replace(" *", "*"))
                .Replace("%%%division%%%", ctz.Division.ToString())
                .Replace("%%%strength%%%", ctz.Strength.ToString("#,##0"))
                .Replace("%%%strengthRaw%%%", Math.Floor(Convert.ToSingle(ctz.Strength.ToString().Replace('.', ','))).ToString())
                .Replace("%%%rankPointsRaw%%%", ctz.RankPoints.ToString())
                .Replace("%%%today%%%", DateTime.Now.ToString(GameInfo.Culture.DateTimeFormat.FullDateTimePattern, GameInfo.Culture))
                .Replace("%%%topDamage%%%", ctz.TopCampaignDamage.Damage.ToString("#,##0"))
                .Replace("%%%topDamageCountry%%%", ctz.TopCampaignDamage.Country)
                .Replace("%%%truePatriotDamage%%%", ctz.TruePatriotDamage.Damage.ToString("#,##0"))
                .Replace("%%%truePatriotCountry%%%", ctz.TruePatriotDamage.Country)
                .Replace("%%%guerillaWon%%%", ctz.GuerillaScore.Won.ToString())
                .Replace("%%%guerillaLost%%%", ctz.GuerillaScore.Lost.ToString())
                .Replace("%%%monthlyDamage%%%", ctz.MonthlyDamage.ToString("#,##0"))
                .Replace("%%%smallBombsUsed%%%", ctz.BombsUsed.Small.ToString())
                .Replace("%%%bigBombsUsed%%%", ctz.BombsUsed.Big.ToString())
                .Replace("%%%lastBombUsed%%%", ctz.BombsUsed.LastBombUsed)
                .Replace("%%%medalsTotal%%%", ctz.Medals.Count.ToString())
                .Replace("%%%medalsFF%%%", ctz.Medals.FreedomFighter.ToString())
                .Replace("%%%medalsHW%%%", ctz.Medals.HardWorker.ToString())
                .Replace("%%%medalsCM%%%", ctz.Medals.CongressMember.ToString())
                .Replace("%%%medalsCP%%%", ctz.Medals.CountryPresident.ToString())
                .Replace("%%%medalsMM%%%", ctz.Medals.MediaMogul.ToString())
                .Replace("%%%medalsBH%%%", ctz.Medals.BattleHero.ToString())
                .Replace("%%%medalsCH%%%", ctz.Medals.CampaignHero.ToString())
                .Replace("%%%medalsRH%%%", ctz.Medals.ResistanceHero.ToString())
                .Replace("%%%medalsSS%%%", ctz.Medals.SuperSoldier.ToString())
                .Replace("%%%medalsSB%%%", ctz.Medals.SocietyBuilder.ToString())
                .Replace("%%%medalsMR%%%", ctz.Medals.Mercenary.ToString())
                .Replace("%%%medalsTF%%%", ctz.Medals.TopFighter.ToString())
                .Replace("%%%medalsTP%%%", ctz.Medals.TruePatriot.ToString())
                .Replace("%%%decorationsTotal%%%", ctz.DecorationsTotal.ToString())
                .Replace("%%%decorations%%%", GetDecorationsList(ctz))
                .Replace("%%%rankingWorldXp%%%", ctz.WorldRanking.Experience.ToString())
                .Replace("%%%rankingWorldStr%%%", ctz.WorldRanking.Strength.ToString())
                .Replace("%%%rankingWorldRank%%%", ctz.WorldRanking.Rank.ToString())
                .Replace("%%%rankingWorldHit%%%", ctz.WorldRanking.Hit.ToString())
                .Replace("%%%rankingWorldMedals%%%", ctz.WorldRanking.Medals.ToString())
                .Replace("%%%rankingCountryXp%%%", ctz.CountryRanking.Experience.ToString())
                .Replace("%%%rankingCountryStr%%%", ctz.CountryRanking.Strength.ToString())
                .Replace("%%%rankingCountryRank%%%", ctz.CountryRanking.Rank.ToString())
                .Replace("%%%rankingCountryHit%%%", ctz.CountryRanking.Hit.ToString())
                .Replace("%%%rankingCountryMedals%%%", ctz.CountryRanking.Medals.ToString())
                .Replace("%%%rankingUnitXp%%%", ctz.UnitRanking.Experience.ToString())
                .Replace("%%%rankingUnitStr%%%", ctz.UnitRanking.Strength.ToString())
                .Replace("%%%rankingUnitRank%%%", ctz.UnitRanking.Rank.ToString())
                .Replace("%%%rankingUnitHit%%%", ctz.UnitRanking.Hit.ToString())
                .Replace("%%%rankingUnitMedals%%%", ctz.UnitRanking.Medals.ToString());
            #endregion

            if (file.Contains("###NON-ENGLISH###"))
                file = Regex.Replace(file,
                    "\\[\\[Category:([\\s\\S]*?)\\]\\]",
                    "<!-- [[Category:$1]] -->")
                    .Replace("###NON-ENGLISH###", "<!-- ###NON-ENGLISH### -->");

            Clipboard.SetText(file);
            MessageBox.Show("Done");
        }
        private void LoadDraftList()
        {
            DirectoryInfo dInfo = new DirectoryInfo("Drafts\\");
            FileInfo[] drafts = dInfo.GetFiles("*.TXT");

            cmbDrafts.Items.Clear();
            for (int i = 0; i < drafts.Length; i++)
                cmbDrafts.Items.Add(drafts[i].Name);
        }
        private string GetDecorationsList(Citizen citizen)
        {
            string s = "";

            for (int i = 0; i < citizen.Decoration.Length; i++)
            {
                s += "'''" + citizen.Decoration[i].Count + "'''x " +
                    "[[File:" + citizen.Decoration[i].ImageLink.RemoveNewLines().Split('/')[5] + "]] " +
                    "''" + citizen.Decoration[i].Text + "''\n\n";
            }
            return s;
        }

        private bool GnGraduate(Citizen ctz)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.OptionFixNestedTags = true;

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                doc.LoadHtml(wc.DownloadString("https://docs.google.com/spreadsheet/pub?key=0Am6tel9lYl4ydDg2M1lMSVZvTmNZRnBHaDNib1Y4dmc&single=true&gid=2&output=html"));
            }

            for (int i = 2; i <= doc.DocumentNode.SelectNodes("//table[@id='tblMain']/tr[@dir='ltr']").Count; i++)
                if (doc.DocumentNode.SelectSingleNode("//table[@id='tblMain']/tr[@dir='ltr'][" + i + "]/td[contains(@class, 's')][1]").InnerText == ctz.Name)
                    return true;
            return false;
        }
    }
}
