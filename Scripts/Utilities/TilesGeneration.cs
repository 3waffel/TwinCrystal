using System;
using System.Collections.Generic;
using System.Linq;
using GoRogue;
using GoRogue.MapGeneration;
using GoRogue.MapGeneration.Generators;
using GoRogue.MapViews;
using Godot;
using Troschuetz.Random.Generators;

public class TilesGeneration : Node
{
    public static Godot.Collections.Array BasicGen(int height, int width)
    {
        var random = new StandardGenerator();

        var map = new ArrayMap<bool>(height, width);
        for (int i = 0; i < 200; i++)
        {
            QuickGenerators.GenerateCellularAutomataMap(map, random, 40, 10, 7);
        }

        var mapList = new Godot.Collections.Array();
        map.Positions<bool>().ToList().ForEach(pos => mapList.Add(map[pos]));
        return mapList;
    }

    public static (TileMap, Vector2) TilesGen()
    {
        int
            height = 100,
            width = 100;
        var list = BasicGen(height, width);
        TileMap tileMap = new TileMap();
        tileMap.TileSet =
            ResourceLoader.Load<TileSet>("res://Assets/Tiles/Default.tres");
        tileMap.CellSize = new Vector2(32, 32);
        tileMap.ZIndex = -1;

        Vector2 spawnPoint = new Vector2(0, 0);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tileMap.SetCell(i, j, ((bool) list[i * width + j] ? 1 : 0));
                if ((bool) list[i * width + j])
                {
                    spawnPoint = new Vector2(i * 32, j * 32);
                }
            }
        }
        return (tileMap, spawnPoint);
    }

    private static void displayMap(IMapView<bool> map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            String row = "";
            for (int x = 0; x < map.Width; x++)
            {
                row += map[x, y] ? "." : "#";
            }
            GD.Print (row);
        }
    }
}
