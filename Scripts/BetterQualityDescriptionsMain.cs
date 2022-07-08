// Project:         BetterQualityDescriptions mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2022 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    7/2/2022, 8:00 PM
// Last Edit:		7/8/2022, 7:00 PM
// Version:			1.00
// Special Thanks:  Cliffworms, Kab the Bird Ranger, Aaron Gimblet, Reconsile, Jehuty, Ralzar, Kokytos, Hazelnut, and Interkarma
// Modifier:

using System;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using UnityEngine;
using DaggerfallWorkshop.Game.Guilds;
using DaggerfallWorkshop.Game.Utility.ModSupport.ModSettings;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.UserInterfaceWindows;

namespace BetterQualityDescriptions
{
    public partial class BetterQualityDescriptionsMain : MonoBehaviour, IHasModSaveData
    {
        static BetterQualityDescriptionsMain instance;

        public static BetterQualityDescriptionsMain Instance
        {
            get { return instance ?? (instance = FindObjectOfType<BetterQualityDescriptionsMain>()); }
        }

        static Mod mod;

        // Options
        public static int TextDisplayType { get; set; }
        public static int TextComplexity { get; set; }
        public static int MinDisplayDuration { get; set; }
        public static int MaxDisplayDuration { get; set; }
        public static int ShopTextCooldown { get; set; }
        public static int TavernTextCooldown { get; set; }
        public static int ResidenceTextCooldown { get; set; }
        public static int BankTextCooldown { get; set; }
        public static int LibraryTextCooldown { get; set; }
        public static int TempleTextCooldown { get; set; }
        public static int MagesGuildTextCooldown { get; set; }
        public static int FightersGuildTextCooldown { get; set; }
        public static int KnightOrderTextCooldown { get; set; }
        public static int ThievesGuildTextCooldown { get; set; }
        public static int DarkBrotherhoodTextCooldown { get; set; }
        public static int PalaceTextCooldown { get; set; }

        // Attached To SaveData
        public static ulong lastSeenShopText = 0;
        public static ulong lastSeenTavernText = 0;
        public static ulong lastSeenResidenceText = 0;
        public static ulong lastSeenBankText = 0;
        public static ulong lastSeenLibraryText = 0;
        public static ulong lastSeenTempleText = 0;
        public static ulong lastSeenMagesGuildText = 0;
        public static ulong lastSeenFightersGuildText = 0;
        public static ulong lastSeenKnightOrderText = 0;
        public static ulong lastSeenThievesGuildText = 0;
        public static ulong lastSeenDarkBrotherhoodText = 0;
        public static ulong lastSeenPalaceText = 0;

        // Global Variables
        public static float TextDelay { get; set; }

        PlayerEnterExit playerEnterExit = GameManager.Instance.PlayerEnterExit;
        PlayerGPS playerGPS = GameManager.Instance.PlayerGPS;

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;
            instance = new GameObject("BetterQualityDescriptions").AddComponent<BetterQualityDescriptionsMain>(); // Add script to the scene.
            mod.SaveDataInterface = instance;

            mod.LoadSettingsCallback = LoadSettings; // To enable use of the "live settings changes" feature in-game.

            mod.IsReady = true;
        }

        private void Start()
        {
            Debug.Log("Begin mod init: Better Quality Descriptions");

            mod.LoadSettings();

            PlayerEnterExit.OnTransitionInterior += ShowQualityText_OnTransitionInterior;

            Debug.Log("Finished mod init: Better Quality Descriptions");
        }

        #region Settings

        static void LoadSettings(ModSettings modSettings, ModSettingsChange change)
        {
            TextDisplayType = mod.GetSettings().GetValue<int>("GeneralSettings", "DisplayType");
            TextComplexity = mod.GetSettings().GetValue<int>("GeneralSettings", "ComplexityType");
            MinDisplayDuration = mod.GetSettings().GetValue<int>("GeneralSettings", "MinDisplayTime");
            MaxDisplayDuration = mod.GetSettings().GetValue<int>("GeneralSettings", "MaxDisplayTime");
            ShopTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "ShopCooldown");
            TavernTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "TavernCooldown");
            ResidenceTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "ResidenceCooldown");
            BankTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "BankCooldown");
            LibraryTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "LibraryCooldown");
            TempleTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "TempleCooldown");
            MagesGuildTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "MagesGuildCooldown");
            FightersGuildTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "FightersGuildCooldown");
            KnightOrderTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "KnightOrderCooldown");
            ThievesGuildTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "ThievesGuildCooldown");
            DarkBrotherhoodTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "DarkBrotherhoodCooldown");
            PalaceTextCooldown = mod.GetSettings().GetValue<int>("QualityTextFrequency", "PalaceCooldown");
        }

        #endregion

        public static TextFile.Token[] TextTokenFromRawString(string rawString)
        {
            var listOfCompLines = new List<string>();
            int partLength = 115;
            if (!DaggerfallUnity.Settings.SDFFontRendering)
                partLength = 65;
            string sentence = rawString;
            string[] words = sentence.Split(' ');
            TextDelay = 3f + (words.Length * 0.25f);
            var parts = new Dictionary<int, string>();
            string part = string.Empty;
            int partCounter = 0;
            foreach (var word in words)
            {
                if (part.Length + word.Length < partLength)
                {
                    part += string.IsNullOrEmpty(part) ? word : " " + word;
                }
                else
                {
                    parts.Add(partCounter, part);
                    part = word;
                    partCounter++;
                }
            }
            parts.Add(partCounter, part);

            foreach (var item in parts)
            {
                listOfCompLines.Add(item.Value);
            }

            return DaggerfallUnity.Instance.TextProvider.CreateTokens(TextFile.Formatting.JustifyCenter, listOfCompLines.ToArray());
        }

        public void ShowQualityText_OnTransitionInterior(PlayerEnterExit.TransitionEventArgs args)
        {
            DFLocation.BuildingTypes buildingType = playerEnterExit.BuildingDiscoveryData.buildingType;
            PlayerGPS.DiscoveredBuilding buildingData = playerEnterExit.BuildingDiscoveryData;
            int quality = buildingData.quality;

            ulong currentTimeSeconds = DaggerfallUnity.Instance.WorldTime.DaggerfallDateTime.ToSeconds(); // 15 * 60 = Number of seconds in 15 minutes.

            TextFile.Token[] tokens = null;

            if (playerEnterExit.IsPlayerInside)
            {
                if (playerEnterExit.IsPlayerInsideOpenShop && (currentTimeSeconds - lastSeenShopText) > 60 * (uint)ShopTextCooldown)
                {
                    lastSeenShopText = currentTimeSeconds;

                    if (ShopTextCooldown >= 21) { return; }

                    tokens = AllShopQualityText(quality);
                }
                else if (playerEnterExit.IsPlayerInsideTavern && (currentTimeSeconds - lastSeenTavernText) > 60 * (uint)TavernTextCooldown)
                {
                    lastSeenTavernText = currentTimeSeconds;

                    if (TavernTextCooldown >= 21) { return; }

                    tokens = TavernQualityText(quality);
                }
                else if ((int)playerEnterExit.BuildingDiscoveryData.buildingType >= 17 && (int)playerEnterExit.BuildingDiscoveryData.buildingType < 23 && (currentTimeSeconds - lastSeenResidenceText) > 60 * (uint)ResidenceTextCooldown)
                {
                    lastSeenResidenceText = currentTimeSeconds;

                    if (ResidenceTextCooldown >= 21) { return; }

                    tokens = ResidenceQualityText(quality);
                }
                else if (buildingType == DFLocation.BuildingTypes.Bank && (currentTimeSeconds - lastSeenBankText) > 60 * (uint)BankTextCooldown)
                {
                    lastSeenBankText = currentTimeSeconds;

                    if (BankTextCooldown >= 21) { return; }

                    tokens = BankQualityText(quality);
                }
                else if (buildingType == DFLocation.BuildingTypes.Library && (currentTimeSeconds - lastSeenLibraryText) > 60 * (uint)LibraryTextCooldown)
                {
                    lastSeenLibraryText = currentTimeSeconds;

                    if (LibraryTextCooldown >= 21) { return; }

                    tokens = LibraryQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.Temple && (currentTimeSeconds - lastSeenTempleText) > 60 * (uint)TempleTextCooldown)
                {
                    lastSeenTempleText = currentTimeSeconds;

                    if (TempleTextCooldown >= 21) { return; }

                    tokens = TempleQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.GuildHall && playerEnterExit.BuildingDiscoveryData.factionID == (int)FactionFile.FactionIDs.The_Mages_Guild && BuildingOpenCheck(buildingType, buildingData) && (currentTimeSeconds - lastSeenMagesGuildText) > 60 * (uint)MagesGuildTextCooldown)
                {
                    lastSeenMagesGuildText = currentTimeSeconds;

                    if (MagesGuildTextCooldown >= 21) { return; }

                    tokens = MagesGuildQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.GuildHall && playerEnterExit.BuildingDiscoveryData.factionID == (int)FactionFile.FactionIDs.The_Fighters_Guild && BuildingOpenCheck(buildingType, buildingData) && (currentTimeSeconds - lastSeenFightersGuildText) > 60 * (uint)FightersGuildTextCooldown)
                {
                    lastSeenFightersGuildText = currentTimeSeconds;

                    if (FightersGuildTextCooldown >= 21) { return; }

                    tokens = FightersGuildQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.GuildHall && playerEnterExit.BuildingDiscoveryData.factionID == (int)FactionFile.FactionIDs.Generic_Knightly_Order && BuildingOpenCheck(buildingType, buildingData) && (currentTimeSeconds - lastSeenKnightOrderText) > 60 * (uint)KnightOrderTextCooldown)
                {
                    lastSeenKnightOrderText = currentTimeSeconds;

                    if (KnightOrderTextCooldown >= 21) { return; }

                    tokens = KnightOrderQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.GuildHall && playerEnterExit.BuildingDiscoveryData.factionID == (int)FactionFile.FactionIDs.The_Thieves_Guild && BuildingOpenCheck(buildingType, buildingData) && (currentTimeSeconds - lastSeenThievesGuildText) > 60 * (uint)ThievesGuildTextCooldown)
                {
                    lastSeenThievesGuildText = currentTimeSeconds;

                    if (ThievesGuildTextCooldown >= 21) { return; }

                    tokens = ThievesGuildQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.GuildHall && playerEnterExit.BuildingDiscoveryData.factionID == (int)FactionFile.FactionIDs.The_Dark_Brotherhood && BuildingOpenCheck(buildingType, buildingData) && (currentTimeSeconds - lastSeenDarkBrotherhoodText) > 60 * (uint)DarkBrotherhoodTextCooldown)
                {
                    lastSeenDarkBrotherhoodText = currentTimeSeconds;

                    if (DarkBrotherhoodTextCooldown >= 21) { return; }

                    tokens = DarkBrotherhoodQualityText(quality);
                }
                else if (playerEnterExit.BuildingDiscoveryData.buildingType == DFLocation.BuildingTypes.Palace && (currentTimeSeconds - lastSeenPalaceText) > 60 * (uint)PalaceTextCooldown) // Perhaps for places where breaking in is a thing that might be done/worth doing, add another set of text for breaking in compared to normally just going in to emphasize the wealth/value of the house etc.
                {
                    lastSeenPalaceText = currentTimeSeconds;

                    if (PalaceTextCooldown >= 21) { return; }

                    tokens = PalaceQualityText(quality);
                }
            }

            if (tokens != null)
            {
                if (MinDisplayDuration != 0 && TextDelay < MinDisplayDuration) // If not set to 0, the minimum number of seconds a message can show for
                    TextDelay = MinDisplayDuration;

                if (MaxDisplayDuration != 0 && TextDelay > MaxDisplayDuration) // If not set to 0, the maximum number of seconds a message can show for
                    TextDelay = MaxDisplayDuration;

                if (TextDisplayType == 0) // For HUD display of text
                {
                    DaggerfallUI.AddHUDText(tokens, TextDelay);
                }
                else // For MessageBox display of text
                {
                    DaggerfallMessageBox textBox = new DaggerfallMessageBox(DaggerfallUI.UIManager, DaggerfallUI.UIManager.TopWindow);
                    textBox.SetTextTokens(tokens);
                    textBox.ClickAnywhereToClose = true;
                    textBox.Show();
                }
            }
        }

        public bool BuildingOpenCheck(DFLocation.BuildingTypes buildingType, PlayerGPS.DiscoveredBuilding buildingData)
        {
            /*
             * Open Hours For Specific Places:
             * Temples, Dark Brotherhood, Thieves Guild: 24/7
             * All Other Guilds: 11:00 - 23:00
             * Fighters Guild & Mages Guild, Rank 6 = 24/7 Access
             * 
             * Alchemists: 07:00 - 22:00
             * Armorers: 09:00 - 19:00
             * Banks: 08:00 - 15:00
             * Bookstores: 	09:00 - 21:00
             * Clothing Stores: 10:00 - 19:00
             * Gem Stores: 09:00 - 18:00
             * General Stores + Furniture Stores: 06:00 - 23:00
             * Libraries: 09:00 - 23:00
             * Pawn Shops + Weapon Smiths: 09:00 - 20:00
            */

            int buildingInt = (int)buildingType;
            int hour = DaggerfallUnity.Instance.WorldTime.Now.Hour;
            IGuild guild = GameManager.Instance.GuildManager.GetGuild(buildingData.factionID);
            if (buildingType == DFLocation.BuildingTypes.GuildHall && (PlayerActivate.IsBuildingOpen(buildingType) || guild.HallAccessAnytime()))
                return true;
            if (buildingInt < 18)
                return PlayerActivate.IsBuildingOpen(buildingType);
            else if (buildingInt <= 22)
                return hour < 6 || hour > 18 ? false : true;
            else
                return true;
        }

        #region SaveData Junk

        public Type SaveDataType
        {
            get { return typeof(BetterQualityDescriptionsSaveData); }
        }

        public object NewSaveData()
        {
            return new BetterQualityDescriptionsSaveData
            {
                LastSeenShopText = 0,
                LastSeenTavernText = 0,
                LastSeenResidenceText = 0,
                LastSeenBankText = 0,
                LastSeenLibraryText = 0,
                LastSeenTempleText = 0,
                LastSeenMagesGuildText = 0,
                LastSeenFightersGuildText = 0,
                LastSeenKnightOrderText = 0,
                LastSeenThievesGuildText = 0,
                LastSeenDarkBrotherhoodText = 0,
                LastSeenPalaceText = 0,
            };
        }

        public object GetSaveData()
        {
            return new BetterQualityDescriptionsSaveData
            {
                LastSeenShopText = lastSeenShopText,
                LastSeenTavernText = lastSeenTavernText,
                LastSeenResidenceText = lastSeenResidenceText,
                LastSeenBankText = lastSeenBankText,
                LastSeenLibraryText = lastSeenLibraryText,
                LastSeenTempleText = lastSeenTempleText,
                LastSeenMagesGuildText = lastSeenMagesGuildText,
                LastSeenFightersGuildText = lastSeenFightersGuildText,
                LastSeenKnightOrderText = lastSeenKnightOrderText,
                LastSeenThievesGuildText = lastSeenThievesGuildText,
                LastSeenDarkBrotherhoodText = lastSeenDarkBrotherhoodText,
                LastSeenPalaceText = lastSeenPalaceText,
            };
        }

        public void RestoreSaveData(object saveData)
        {
            var BetterQualityDescriptionsSaveData = (BetterQualityDescriptionsSaveData)saveData;
            lastSeenShopText = BetterQualityDescriptionsSaveData.LastSeenShopText;
            lastSeenTavernText = BetterQualityDescriptionsSaveData.LastSeenTavernText;
            lastSeenResidenceText = BetterQualityDescriptionsSaveData.LastSeenResidenceText;
            lastSeenBankText = BetterQualityDescriptionsSaveData.LastSeenBankText;
            lastSeenLibraryText = BetterQualityDescriptionsSaveData.LastSeenLibraryText;
            lastSeenTempleText = BetterQualityDescriptionsSaveData.LastSeenTempleText;
            lastSeenMagesGuildText = BetterQualityDescriptionsSaveData.LastSeenMagesGuildText;
            lastSeenFightersGuildText = BetterQualityDescriptionsSaveData.LastSeenFightersGuildText;
            lastSeenKnightOrderText = BetterQualityDescriptionsSaveData.LastSeenKnightOrderText;
            lastSeenThievesGuildText = BetterQualityDescriptionsSaveData.LastSeenThievesGuildText;
            lastSeenDarkBrotherhoodText = BetterQualityDescriptionsSaveData.LastSeenDarkBrotherhoodText;
            lastSeenPalaceText = BetterQualityDescriptionsSaveData.LastSeenPalaceText;
        }
    }

    [FullSerializer.fsObject("v1")]
    public class BetterQualityDescriptionsSaveData
    {
        public ulong LastSeenShopText;
        public ulong LastSeenTavernText;
        public ulong LastSeenResidenceText;
        public ulong LastSeenBankText;
        public ulong LastSeenLibraryText;
        public ulong LastSeenTempleText;
        public ulong LastSeenMagesGuildText;
        public ulong LastSeenFightersGuildText;
        public ulong LastSeenKnightOrderText;
        public ulong LastSeenThievesGuildText;
        public ulong LastSeenDarkBrotherhoodText;
        public ulong LastSeenPalaceText;
    }

    #endregion
}