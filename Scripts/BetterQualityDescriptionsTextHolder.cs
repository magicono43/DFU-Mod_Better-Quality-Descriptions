using DaggerfallWorkshop.Game;
using DaggerfallWorkshop;
using DaggerfallConnect.Arena2;

namespace BetterQualityDescriptions
{
    public partial class BetterQualityDescriptionsMain
    {
        public static string BuildName()
        {
            return GameManager.Instance.PlayerEnterExit.BuildingDiscoveryData.displayName;
        }

        public static string CityName()
        {   // %cn
            PlayerGPS gps = GameManager.Instance.PlayerGPS;
            if (gps.HasCurrentLocation)
                return gps.CurrentLocation.Name;
            else
                return gps.CurrentRegion.Name;
        }

        private static string CurrentRegion()
        {   // %crn going to use for %reg as well here
            return GameManager.Instance.PlayerGPS.CurrentRegion.Name;
        }

        public static string OneWordQuality(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 11);
            string word = "Bugged";

            if (quality <= 3) // 01 - 03
            {
                if (variant == 0) { word = "Dreadful"; }
                else if (variant == 1) { word = "Horrendous"; }
                else if (variant == 2) { word = "Horrible"; }
                else if (variant == 3) { word = "Atrocious"; }
                else if (variant == 4) { word = "Horrid"; }
                else if (variant == 5) { word = "Awful"; }
                else { word = "Terrible"; }
            }
            else if (quality <= 7) // 04 - 07
            {
                if (variant == 0) { word = "Bad"; }
                else if (variant == 1) { word = "Reduced"; }
                else if (variant == 2) { word = "Low"; }
                else if (variant == 3) { word = "Inferior"; }
                else if (variant == 4) { word = "Meager"; }
                else if (variant == 5) { word = "Scant"; }
                else { word = "Poor"; }
            }
            else if (quality <= 13) // 08 - 13
            {
                if (variant == 0) { word = "Modest"; }
                else if (variant == 1) { word = "Medium"; }
                else if (variant == 2) { word = "Moderate"; }
                else if (variant == 3) { word = "Passable"; }
                else if (variant == 4) { word = "Adequate"; }
                else if (variant == 5) { word = "Typical"; }
                else { word = "Average"; }
            }
            else if (quality <= 17) // 14 - 17
            {
                if (variant == 0) { word = "Reasonable"; }
                else if (variant == 1) { word = "Solid"; }
                else if (variant == 2) { word = "Honorable"; }
                else if (variant == 3) { word = "Nice"; }
                else if (variant == 4) { word = "True"; }
                else if (variant == 5) { word = "Worthy"; }
                else { word = "Good"; }
            }
            else // 18 - 20
            {
                if (variant == 0) { word = "Exemplary"; }
                else if (variant == 1) { word = "Esteemed"; }
                else if (variant == 2) { word = "Astonishing"; }
                else if (variant == 3) { word = "Marvelous"; }
                else if (variant == 4) { word = "Wonderful"; }
                else if (variant == 5) { word = "Spectacular"; }
                else if (variant == 6) { word = "Incredible"; }
                else { word = "Exceptional"; }
            }

            return word;
        }


        public static TextFile.Token[] AllShopQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Shop quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a shop of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "Rusty relics lie wherever they were last tossed. All the wares show the cracks and chips of shoddy workmanship. A mouse scampers over your feet before burrowing into a nearby sack.";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "Sturdy shelves, cobbled together out of scrap lumber hold the shops wares. The items are sound and functional, but little more than that.";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "The shop is laid out in a practical and straightforward manner. All the items seem to be of adequate construction.";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "The shop is better appointed than many. Its wares lie neatly on the shelves. Although not fit for a king, all are skillfully crafted.";
                }
                else // 18 - 20
                {
                    raw = "Incense and soft music soothe your nerves as you cross the threshold. Each item in this shop is carefully mounted and displayed. Not the slightest defect can be detected in any item within.";
                }
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] TavernQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Tavern quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a tavern of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    if (variant == 0)
                    {
                        raw = "Clean, cozy and welcoming are words that no one would ever use to describe this horrible cross between a penal colony and a swamp.";
                    }
                    if (variant == 1)
                    {
                        raw = "The beds are damp, the floors are unwashed, and the whole place smells rancid. Only the desperate would rent a room here.";
                    }
                    else
                        raw = "Upon entering, one of the first things you notice is what looks like rodent droppings peppered all around the creaky and splintering floorboards. The beds in the nearby rooms catch your attention, as they look like they have not been cleaned or had the linens changed in months, maybe years. Is this an inn for people or for vermin and bedbugs?";
                }
                else if (quality <= 7) // 04 - 07
                {
                    if (variant == 0)
                    {
                        raw = "Before even entering, you smelled the nondescript but alluring aroma of greasy batter fried meats. After entering however, you feel your nose may have led you astray. Seeing the dirty utensils and mugs, and especially the small invasion of rodents and flies, put a slight damper on your appetite.";
                    }
                    else
                        raw = "Tacky, discount decorations line the walls of this tavern. Everything in sight looks cheap, old, and faded.";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "While not luxurious by any stretch. This tavern seems like an entirely amicable place to stay for a few nights. The beds might be a bit lumpy, but at least they are clean and free of unwelcome guests.";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "Comfortable, soothing, well decorated, and most importantly pest free are some of the ways this tavern could be described. Certainly not fit for royalty, but a very good place to lay your head or fill your belly.";
                }
                else // 18 - 20
                {
                    if (variant == 0)
                    {
                        raw = "Your senses are soothed by soft music, the smell of mouth-watering food, and lavish decor as you enter. This place looks fit for a king or queen... with a fitting price.";
                    }
                    else
                        raw = "Upon your first step into the inn, your senses are greeted with soothing music, the smell of mouth-watering food, and lavish decor. This place looks fit for a king or queen, but expect to pay a noble fee for the privilege of lodging here.";
                }
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] ResidenceQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "House quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a House of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a House of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] BankQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Bank quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a bank of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "Crumpled pieces of parchment and voided letters of credit cover the floor in haphazard piles. A bank in this condition is a blemish on" + CurrentRegion() + "'s reputation.";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "You can tell a modest effort was made to hide the peeling and stained wallpaper. But the creaky and unlevel furnishings are a dead giveaway that this financial establishment is not the best " + CurrentRegion() + "has to show for itself.";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "Necessary documents and writing implements are laid out in a practical and organized manner. While not exactly fit for a noble, you can tell your deposits are unlikely to disappear mysteriously here.";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "While \"The Best Bank In " + CurrentRegion() + "\" is definitely a stretch, you can tell this bank is of higher quality than most. The clean polished floors and proudly displayed certifications are a testament to this.";
                }
                else // 18 - 20
                {
                    raw = "As you enter, the immaculate interior and skillfully arranged furnishings are an immediate sign that you have not entered just any normal bank. You feel almost embarrassed, as if you just accidentally stumbled into the office of a king's personal accountant.";
                }
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] LibraryQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Library quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a library of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "Various books and parchment lie strewn about, as if some literary tornado had suddenly swept through just before you walked in. This state of disarray can either be blamed on the owner, lack of city funding, or more than likely both.";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "A modest selection of books lay out of any logical order on poorly constructed lumber shelves. From the layers of dust caked about, you can tell the owner does alot more reading than cleaning...";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "The library seems to be laid out in a practical and straightfoward manner. The books appear to be mostly organized in their respective spot on the adequately constructed shelves. While nothing especially notable, it's clear the owner takes some degree of pride in their modest vocation.";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "The library is better appointed than many. Its large variety of books lie neatly on the shelves, organized alphabetically and by subject. While not fit for a king, at least the citizens can be assured their taxes are not being squandered by this establishment.";
                }
                else // 18 - 20
                {
                    raw = "Not a book, map, scroll, or stack of parchment is out of place. Every reading chair has strategically placed floral arrangements nearby, to fragrance the air and calm the guests nerves to allow for maximum concentration and enjoyment of the massive collection of tomes this library has under its care. This collection may rival some of the mages guild archives, the lesser ones at least.";
                }
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] TempleQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Temple quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a temple of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "An overpowering stench of incense assaults your nostrils, a poor attempt to mask another offensive odor coming from some unknown source within the chapel.";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "Once beautiful stained glass windows, now scratched and faded to a drab visage of their former glory, due to years of neglect from this temple's members.";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "A cracked tile here, an underwatered plant there, it's clear this temple is not the best maintained. But, you can feel an aura exhibited by the members that dispels any doubt of devotion to their god.";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "Upon entering the temple, you are greeted with the mild but soothing smell of incense. The sound of your footfalls on the clean tile echo in noticeable contrast to the soft whispered prayers from within the chapel.";
                }
                else // 18 - 20
                {
                    raw = "As you approach the temple's main chamber, you are taken aback by the almost life-like statue ahead of you. Perfectly arranged flowers and icons decorate the base, even a god would be proud of this shrine.";
                }
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] MagesGuildQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Mages guildhall quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a Mages guildhall of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a Mages guildhall of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] FightersGuildQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Fighters guildhall quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a Fighters guildhall of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a Fighters guildhall of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] KnightOrderQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Knightly Order guildhall quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a Knightly Order guildhall of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a Knightly Order guildhall of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] ThievesGuildQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Thieves guildhall quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a Thieves guildhall of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a Thieves guildhall of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] DarkBrotherhoodQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Dark Brotherhood guildhall quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a Dark Brotherhood guildhall of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a Dark Brotherhood guildhall of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }

        public static TextFile.Token[] PalaceQualityText(int quality)
        {
            int variant = UnityEngine.Random.Range(0, 3);
            string raw = "";

            if (TextComplexity == 2) // Minimal Text Setting
            {
                raw = "Palace quality is, " + quality;
            }
            else if (TextComplexity == 1) // Basic Text Setting
            {
                raw = "This is a Palace of " + OneWordQuality(quality) + " quality.";
            }
            else // Fancy Text Setting
            {
                if (quality <= 3) // 01 - 03
                {
                    raw = "";
                }
                else if (quality <= 7) // 04 - 07
                {
                    raw = "";
                }
                else if (quality <= 13) // 08 - 13
                {
                    raw = "";
                }
                else if (quality <= 17) // 14 - 17
                {
                    raw = "";
                }
                else // 18 - 20
                {
                    raw = "";
                }

                // Lazy Placeholder for now
                raw = "This is a Palace of " + OneWordQuality(quality) + " quality.";
            }

            return TextTokenFromRawString(raw);
        }
    }
}