using System;
using NCMS.Utils;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using ReflectionUtility;

namespace ExampleMod
{
    class TilesWindow
    {
        private static ScrollWindow window;
        private static GameObject content;

        // Defining as internal static, because we using it in TilesPowerButton class
        internal static PowerButtonSelector pbsInstance;

        // Initializing Tiles Window
        public static void init()
        {
            // Creating new window
            window = Windows.CreateNewWindow("tilesWindow", "Select Tile");

            // Activating Scroll View object
            var scrollView = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            scrollView.gameObject.SetActive(true);


            // Fixing size to fit
            var viewport = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport");
            var viewportRect = viewport.GetComponent<RectTransform>();
            viewportRect.sizeDelta = new Vector2(0, 17);

            // Getting Content object
            content = GameObject.Find($"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");

            // Getting power button selector using reflections with ReflectionUtility
            pbsInstance = Reflection.GetField(typeof(PowerButtonSelector), null, "instance") as PowerButtonSelector;

            initTiles(); // Initializing tiles buttons inside window
        }

        private static void initTiles()
        {

            // Getting tiles sprites from game resources
            var sprites = Resources.LoadAll($"tilemap/tiles", typeof(Sprite)).Cast<Sprite>().ToList();

            //tilesDictBoth.AddRange(AssetManager.tiles.list);
            //tilesListBoth.AddRange(AssetManager.topTiles.list);

            // Preparing list of valid sprites
            var preparedTiles = new List<Sprite>();
            foreach (var pair in AssetManager.tiles.dict)
            {
                //var sprite = sprites.Find(c => c.name == $"{pair.Key}_0");
                var sprite = Resources.Load<Sprite>($"tiles/{pair.Key}/tile_0");

                if (sprite == null)
                {
                    continue;
                }

                preparedTiles.Add(sprite);
            }

            foreach (var pair in AssetManager.topTiles.dict)
            {
                //var sprite = sprites.Find(c => c.name == $"{pair.Key}_0");
                var sprite = Resources.Load<Sprite>($"tiles/{pair.Key}/tile_0");

                if (sprite == null)
                {
                    continue;
                }

                preparedTiles.Add(sprite);
            }


            // To make our window content scrollable, we need to change its RectTransform.sizeDelta
            var rect = content.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 1);
            rect.sizeDelta = new Vector2(0, Mathf.Abs(getPositionByIndex(preparedTiles.Count).y) + 40);


            // Adding tile buttons to window
            int i = 0;
            foreach (var pair in AssetManager.tiles.dict)
            {
                //var sprite = preparedTiles.Find(c => c.name == $"{pair.Key}_0");
                var sprite = Resources.Load<Sprite>($"tiles/{pair.Key}/tile_0");
                if (sprite == null)
                {
                    continue;
                }

                createTileButton(pair.Key, "tileType", sprite, i); // Creating tile button

                i++;
            }

            foreach (var pair in AssetManager.topTiles.dict)
            {
                //var sprite = preparedTiles.Find(c => c.name == $"{pair.Key}_0");
                var sprite = Resources.Load<Sprite>($"tiles/{pair.Key}/tile_0");
                if (sprite == null)
                {
                    continue;
                }

                createTileButton(pair.Value.id, "topTileType", sprite, i); // Creating tile button

                i++;
            }
        }

        private static void createTileButton(string tileName, string tileTypeType, Sprite sprite, int index)
        {
            // Making all first char in each string upper
            string name = String.Join(" ", tileName.Split('_').ToList().Select(c => { c = $"{c[0].ToString().ToUpper()}{c.Substring(1)}"; return c; }).ToList());

            // Creating new button
            var button = PowerButtons.CreateButton(
                tileName,
                sprite,
                name,
                "",

                // Getting button position by its index
                getPositionByIndex(index),

                ButtonType.Click,
                content.transform,

                // Setting on click callback with parameter
                () => tileButtonClick(tileName, tileTypeType)
            );
        }

        /*
        Getting button position by its index
        By this formules we dont need any additional if-else constructions. 
        */
        private static Vector2 getPositionByIndex(int index)
        {

            // Starting position by x
            float startX = 50;

            // Starting position by y
            float startY = -20;

            // Buttons size + gap between
            float sizeWithGap = 40;

            // Buttons per row
            int buttonsPerRow = 5;

            // Calculating points
            float positionX = startX + (index * sizeWithGap) - ((Mathf.Floor(index / buttonsPerRow) * sizeWithGap) * buttonsPerRow);
            float positionY = startY - (Mathf.Floor(index / buttonsPerRow) * sizeWithGap);

            return new Vector2(positionX, positionY);
        }

        // On click callback with parameter
        private static void tileButtonClick(string tileName, string tileTypeType)
        {
            // Setting new tile type
            if (tileTypeType == "tileType")
            {
                TilesPowerButton.power.tileType = tileName;
                TilesPowerButton.power.topTileType = String.Empty;
            }
            else
            {
                TilesPowerButton.power.tileType = String.Empty;
                TilesPowerButton.power.topTileType = tileName;
            }


            // Setting actions for power in our button
            //TilesPowerButton.power.click_action = null;
            TilesPowerButton.power.click_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("drawTiles", pTile, pPower); });
            TilesPowerButton.power.click_action += new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("cleanBurnedTile", pTile, pPower); });
            TilesPowerButton.power.click_action += new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("stopFire", pTile, pPower); });
            TilesPowerButton.power.click_action += new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("destroyBuildings", pTile, pPower); });
            TilesPowerButton.power.click_brush_action = new PowerActionWithID((WorldTile pTile, string pPower) => { return (bool)AssetManager.powers.CallMethod("loopWithCurrentBrush", pTile, pPower); });

            //Setting cached_tile_type_asset
            AssetManager.powers.checkCache();

            // When tile button clicked, hidding current window
            window.clickHide();



            // Activating power
            pbsInstance.clickPowerButton(TilesPowerButton.button);
        }
    }
}