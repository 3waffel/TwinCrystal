using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoRogue;
using GoRogue.MapGeneration;
using GoRogue.MapGeneration.Generators;
using GoRogue.MapViews;
using Godot;
using Troschuetz.Random.Generators;

public class TileMapGeneration : Node
{
    private TileMap _preloadTileMap;

    private Task<Vector2> _preloadTask;

    private CancellationTokenSource _preloadCancellationTokenSource;

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

    public static (TileMap, Vector2) TilesGen(int height = 100, int width = 100)
    {
        var list = BasicGen(height, width);
        TileMap tileMap = new TileMap();
        tileMap.TileSet =
            ResourceLoader.Load<TileSet>("res://Assets/Tiles/Wall1.tres");
        tileMap.CellSize = new Vector2(32, 32);
        tileMap.ZIndex = -1;

        Vector2 spawnPoint = new Vector2(0, 0);
        for (int i = 0; i < list.Count; i++)
        {
            if ((bool) list[i])
            {
                if (
                    i % width > 0 &&
                    (bool) list[i - 1] && // current tile to the left
                    i % width < width - 1 &&
                    (bool) list[i + 1] && // current tile to the right
                    i / width > 0 &&
                    (bool) list[i - width] && // current tile above
                    i / width < height - 1 &&
                    (bool) list[i + width] // current tile below
                )
                {
                    spawnPoint = new Vector2(i % width, i / width);
                    break;
                }
            }
        }
        spawnPoint.x *= -tileMap.CellSize.x;
        spawnPoint.y *= -tileMap.CellSize.y;

        for (int i = 0; i < list.Count; i++)
        {
            tileMap.SetCell(i % width, i / width, (bool) list[i] ? 1 : 0);
        }
        return (tileMap, spawnPoint);
    }

    private static void DisplayMap(IMapView<bool> map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            String row = "";
            for (int x = 0; x < map.Width; x++)
            {
                row += map[x, y] ? "." : "#";
            }
        }
    }

    public override void _Ready()
    {
        _preloadCancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken =
            _preloadCancellationTokenSource.Token;

        try
        {
            _preloadTask =
                Task
                    .Run(() =>
                    {
                        Vector2 spawnPoint = new Vector2(0, 0);
                        (_preloadTileMap, spawnPoint) =
                            TileMapGeneration.TilesGen();
                        return spawnPoint;
                    },
                    cancellationToken);
        }
        catch (OperationCanceledException)
        {
            GD.Print("Preload task cancelled");
        }
        finally
        {
            _preloadTask
                .ContinueWith(task =>
                {
                    _preloadTileMap.Position = task.Result;
                    GetParent().AddChild(_preloadTileMap);
                },
                TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(task =>
                {
                    QueueFree();
                    _preloadCancellationTokenSource.Dispose();  
                });
        }
    }
}
