using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineDefTypes
{
    public class LineDefType
    {
        public Category category;

        public LineDefType(Category category)
        {
            this.category = category;
        }

        public virtual int[] GetMovementBoundY(WAD wad, SECTORS sector)
        {
            return new int[2] { sector.floorHeight, sector.floorHeight };
        }
    }

    public class LineDefDoorType : LineDefType
    {
        public Trigger trigger;
        public Repeatable repeatable;
        public Lock lockType;
        public Speed speed;
        public int wait;
        public MonsterActivate monsterActivate;
        public DoorAction doorAction;

        public LineDefDoorType(Trigger trigger, Repeatable repeatable, Lock lockType, Speed speed, int wait, MonsterActivate monsterActivate, DoorAction doorAction) : base(Category.Door)
        {
            this.trigger = trigger;
            this.repeatable = repeatable;
            this.lockType = lockType;
            this.speed = speed;
            this.wait = wait;
            this.monsterActivate = monsterActivate;
            this.doorAction = doorAction;
        }

        public bool isLocal()
        {
            return (trigger == Trigger.Gun || trigger == Trigger.Push);
        }
    }

    public class LineDefFloorType : LineDefType
    {
        public Trigger trigger;
        public Repeatable repeatable;
        public Direction direction;
        public Speed speed;
        public TextureChange textureChange;
        public Model model;
        public MonsterActivate monsterActivate;
        public Crush crush;
        public Target target;

        public LineDefFloorType(Trigger trigger, Repeatable repeatable, Direction direction, Speed speed, TextureChange textureChange, Model model, MonsterActivate monsterActivate, Crush crush, Target target) : base(Category.Floor)
        {
            this.trigger = trigger;
            this.repeatable = repeatable;
            this.direction = direction;
            this.speed = speed;
            this.textureChange = textureChange;
            this.model = model;
            this.monsterActivate = monsterActivate;
            this.crush = crush;
            this.target = target;
        }

        public override int[] GetMovementBoundY(WAD wad, SECTORS sector)
        {
            int[] bound = new int[2];
            switch(target)
            {
                case Target.LowestNeighborFloor:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    sector.neighbors.ForEach(n => bound[0] = Math.Min(bound[0], n.floorHeight));
                    bound[1] = sector.floorHeight;
                    break;

                case Target.NextNeighborFloor:
                    if (direction == Direction.Up)
                    {
                        bound[0] = sector.floorHeight;
                        bound[1] = sector.floorHeight;
                        if (repeatable == Repeatable.Multiple)
                        {
                            sector.neighbors.ForEach(n => bound[1] = Math.Max(bound[1], n.floorHeight));
                        }
                        else
                        {
                            int height = int.MaxValue;
                            sector.neighbors.ForEach(n => height = (n.floorHeight > sector.floorHeight) ? Math.Min(height, n.floorHeight) : height);
                            if (height < int.MaxValue) { bound[1] = height; }
                        }
                    }
                    else if(direction == Direction.Down)
                    {
                        bound[0] = sector.floorHeight;
                        bound[1] = sector.floorHeight;
                        if (repeatable == Repeatable.Multiple)
                        {
                            sector.neighbors.ForEach(n => bound[0] = Math.Min(bound[0], n.floorHeight));
                        }
                        else
                        {
                            int height = int.MaxValue;
                            sector.neighbors.ForEach(n => height = (n.floorHeight < sector.floorHeight) ? Math.Max(height, n.floorHeight) : height);
                            if (height < int.MaxValue) { bound[0] = height; }
                        }
                    }
                    break;

                case Target.LowestNeighborCeiling:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    sector.neighbors.ForEach(n => bound[1] = Math.Min(bound[1], n.ceilingHeight));
                    break;

                case Target.LowestNeighborCeilingMinus8:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight - 8;
                    sector.neighbors.ForEach(n => bound[1] = Math.Min(bound[1], n.ceilingHeight - 8));
                    break;

                case Target.HighestNeighborFloor:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = -32000;
                    sector.neighbors.ForEach(n => bound[0] = Math.Max(bound[0], n.floorHeight));
                    bound[1] = sector.floorHeight;
                    break;

                case Target.HighestNeighborFloorPlus8:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = -32000 + 8;
                    sector.neighbors.ForEach(n => bound[0] = Math.Max(bound[0], n.floorHeight + 8));
                    bound[1] = sector.floorHeight;
                    break;

                case Target.Ceiling:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    break;

                case Target.Absolute24:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 24;
                    break;

                case Target.Absolute32:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 32;
                    break;

                case Target.Absolute512:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 512;
                    break;

                case Target.AbsShortestLowerTexture:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.floorHeight;
                    int texHeight = int.MaxValue;
                    foreach(LINEDEFS line in sector.lines)
                    {
                        if (line.side1 != null && wad.textures.ContainsKey(line.side1.lowTex))
                            texHeight = Math.Min(texHeight, wad.textures[line.side1.lowTex].mainTexture.height);

                        if (line.side2 != null && wad.textures.ContainsKey(line.side2.lowTex))
                            texHeight = Math.Min(texHeight, wad.textures[line.side2.lowTex].mainTexture.height);
                    }
                    if (texHeight < int.MaxValue)
                        bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + texHeight;
                    break;

                case Target.None:
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    break;

                default:
                    throw new Exception("Unexpected LineDefFloorType target!");
            }

            return bound;
        }
    }

    public enum Category { Door, Floor };
    public enum Trigger { Push, Switch, Walk, Gun };
    public enum Repeatable { Multiple, Once };
    public enum Speed { Slow, Fast, None };
    public enum MonsterActivate { Yes, No };

    public enum Lock { Blue, Red, Yellow, None };
    public enum DoorAction { Open, OpenWaitClose, Close, CloseWaitOpen };

    public enum Direction { Up, Down, None };
    public enum TextureChange { Tx, Tx0, TxTy, None };
    public enum Model { Trigger, Numeric, None };
    public enum Crush { Yes, No };
    public enum Target {
        LowestNeighborFloor, NextNeighborFloor,
        LowestNeighborCeiling, LowestNeighborCeilingMinus8,
        HighestNeighborFloor, HighestNeighborFloorPlus8,
        Ceiling,
        Absolute24, Absolute32, Absolute512,
        AbsShortestLowerTexture,
        None
    }

    public static Dictionary<int, LineDefType> types = new Dictionary<int, LineDefType>
    {
        /* DOORS */
        { 1,   new LineDefDoorType(Trigger.Push,   Repeatable.Multiple, Lock.None,   Speed.Slow, 4,  MonsterActivate.Yes, DoorAction.OpenWaitClose) },
        { 2,   new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 3,   new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 4,   new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Slow, 4,  MonsterActivate.Yes, DoorAction.OpenWaitClose) },
        { 16,  new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Slow, 30, MonsterActivate.No,  DoorAction.CloseWaitOpen) },
        { 26,  new LineDefDoorType(Trigger.Push,   Repeatable.Multiple, Lock.Blue,   Speed.Slow, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 27,  new LineDefDoorType(Trigger.Push,   Repeatable.Multiple, Lock.Yellow, Speed.Slow, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 28,  new LineDefDoorType(Trigger.Push,   Repeatable.Multiple, Lock.Red,    Speed.Slow, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 29,  new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Slow, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 31,  new LineDefDoorType(Trigger.Push,   Repeatable.Once,     Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 32,  new LineDefDoorType(Trigger.Push,   Repeatable.Once,     Lock.Blue,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 33,  new LineDefDoorType(Trigger.Push,   Repeatable.Once,     Lock.Red,    Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 34,  new LineDefDoorType(Trigger.Push,   Repeatable.Once,     Lock.Yellow, Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 42,  new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 46,  new LineDefDoorType(Trigger.Gun,    Repeatable.Multiple, Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 50,  new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 61,  new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 63,  new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Slow, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 75,  new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 76,  new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Slow, 30, MonsterActivate.No,  DoorAction.CloseWaitOpen) },
        { 86,  new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 90,  new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Slow, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 99,  new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.Blue,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 103, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Slow, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 105, new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Fast, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 106, new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 107, new LineDefDoorType(Trigger.Walk,   Repeatable.Multiple, Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 108, new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Fast, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 109, new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 110, new LineDefDoorType(Trigger.Walk,   Repeatable.Once,     Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 111, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Fast, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 112, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 113, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 114, new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Fast, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 115, new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 116, new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Close) },
        { 117, new LineDefDoorType(Trigger.Push,   Repeatable.Multiple, Lock.None,   Speed.Fast, 4,  MonsterActivate.No,  DoorAction.OpenWaitClose) },
        { 118, new LineDefDoorType(Trigger.Push,   Repeatable.Once,     Lock.None,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 133, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.Blue,   Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 134, new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.Red,    Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 135, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.Red,    Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 136, new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.Yellow, Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 137, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.Yellow, Speed.Fast, 0,  MonsterActivate.No,  DoorAction.Open) },
        { 175, new LineDefDoorType(Trigger.Switch, Repeatable.Once,     Lock.None,   Speed.Slow, 30, MonsterActivate.No,  DoorAction.CloseWaitOpen) },
        { 196, new LineDefDoorType(Trigger.Switch, Repeatable.Multiple, Lock.None,   Speed.Slow, 30, MonsterActivate.No,  DoorAction.CloseWaitOpen) },

        /* FLOORS */
        { 5,   new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborCeiling) },
        // TODO: 9 is missing because 'donuts' are not fun
        { 14,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute32) },
        { 15,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 18,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 19,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloor) },
        { 20,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.Tx,   Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 22,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.Tx,   Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 23,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 24,  new LineDefFloorType(Trigger.Gun,    Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborCeiling) },
        { 30,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.AbsShortestLowerTexture) },
        { 36,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloorPlus8) },
        { 37,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 38,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 45,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloor) },
        { 47,  new LineDefFloorType(Trigger.Gun,    Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 55,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, Target.LowestNeighborCeilingMinus8) },
        { 56,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, Target.LowestNeighborCeilingMinus8) },
        { 58,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 59,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 60,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 64,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborCeiling) },
        { 65,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, Target.LowestNeighborCeilingMinus8) },
        { 66,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 67,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No, Target.Absolute32) },
        { 68,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.Tx,   Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 69,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 70,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloorPlus8) },
        { 71,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloorPlus8) },
        { 78,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.None) },
        { 82,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 83,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloor) },
        { 84,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 91,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborCeiling) },
        { 92,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 93,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 94,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, Target.LowestNeighborCeilingMinus8) },
        { 95,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.Tx,   Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 96,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.AbsShortestLowerTexture) },
        { 98,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloorPlus8) },
        { 101, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.LowestNeighborCeiling) },
        { 102, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.HighestNeighborFloor) },
        { 119, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 128, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 129, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 130, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 131, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 132, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 140, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute512) },
        { 142, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute512) },
        { 147, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute512) },
        { 153, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.None) },
        { 154, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.None) },
        { 158, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.AbsShortestLowerTexture) },
        { 159, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 160, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 161, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 176, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.AbsShortestLowerTexture) },
        { 177, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.LowestNeighborFloor) },
        { 178, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute512) },
        { 179, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 180, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.Absolute24) },
        { 189, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.None) },
        { 190, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  Target.None) },
        { 219, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 220, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 221, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 222, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  Target.NextNeighborFloor) },
        { 239, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.None) },
        { 240, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.None) },
        { 241, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  Target.None) },
    };
}
