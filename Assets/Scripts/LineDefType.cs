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

        public virtual int[] GetFloorMovementBound(WAD wad, SECTORS sector)
        {
            return new int[2] { sector.floorHeight, sector.floorHeight };
        }

        public virtual int[] GetCeilingMovementBound(WAD wad, SECTORS sector)
        {
            return new int[2] { sector.ceilingHeight, sector.ceilingHeight };
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

        public override int[] GetCeilingMovementBound(WAD wad, SECTORS sector)
        {
            int lowestNeighborCeiling = int.MaxValue;
            sector.neighbors.ForEach(n => lowestNeighborCeiling = Math.Min(lowestNeighborCeiling, n.ceilingHeight));
            if (lowestNeighborCeiling == int.MaxValue || (lowestNeighborCeiling - 4) < sector.floorHeight)
                return new int[2] { sector.floorHeight, sector.ceilingHeight };
            else
                return new int[2] { sector.floorHeight, lowestNeighborCeiling - 4 };
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
        public FloorTarget target;

        public LineDefFloorType(Trigger trigger, Repeatable repeatable, Direction direction, Speed speed, TextureChange textureChange, Model model, MonsterActivate monsterActivate, Crush crush, FloorTarget target) : base(Category.Floor)
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

        public override int[] GetFloorMovementBound(WAD wad, SECTORS sector)
        {
            int[] bound = new int[2];
            switch(target)
            {
                case FloorTarget.LowestNeighborFloor:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    sector.neighbors.ForEach(n => bound[0] = Math.Min(bound[0], n.floorHeight));
                    bound[1] = sector.floorHeight;
                    break;

                case FloorTarget.NextNeighborFloor:
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

                case FloorTarget.LowestNeighborCeiling:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    sector.neighbors.ForEach(n => bound[1] = Math.Min(bound[1], n.ceilingHeight));
                    break;

                case FloorTarget.LowestNeighborCeilingMinus8:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight - 8;
                    sector.neighbors.ForEach(n => bound[1] = Math.Min(bound[1], n.ceilingHeight - 8));
                    break;

                case FloorTarget.HighestNeighborFloor:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = -32000;
                    sector.neighbors.ForEach(n => bound[0] = Math.Max(bound[0], n.floorHeight));
                    bound[1] = sector.floorHeight;
                    break;

                case FloorTarget.HighestNeighborFloorPlus8:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = -32000 + 8;
                    sector.neighbors.ForEach(n => bound[0] = Math.Max(bound[0], n.floorHeight + 8));
                    bound[1] = sector.floorHeight;
                    break;

                case FloorTarget.Ceiling:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    break;

                case FloorTarget.Absolute24:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 24;
                    break;

                case FloorTarget.Absolute32:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 32;
                    break;

                case FloorTarget.Absolute512:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefFloorType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 512;
                    break;

                case FloorTarget.AbsShortestLowerTexture:
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

                case FloorTarget.None:
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    break;

                default:
                    throw new Exception("Unexpected LineDefFloorType target!");
            }

            return bound;
        }
    }

    public class LineDefLiftType : LineDefType
    {
        public Trigger trigger;
        public Repeatable repeatable;
        public Speed speed;
        public TextureChange textureChange;
        public Model model;
        public MonsterActivate monsterActivate;
        public LiftTarget target;

        public LineDefLiftType(Trigger trigger, Repeatable repeatable, int wait, Speed speed, TextureChange textureChange, Model model, MonsterActivate monsterActivate, LiftTarget target) : base(Category.Lift)
        {
            this.trigger = trigger;
            this.repeatable = repeatable;
            this.speed = speed;
            this.textureChange = textureChange;
            this.model = model;
            this.monsterActivate = monsterActivate;
            this.target = target;
        }

        public override int[] GetFloorMovementBound(WAD wad, SECTORS sector)
        {
            int[] bound = new int[2];
            switch (target)
            {
                case LiftTarget.LowestNeighborFloor:
                    bound[0] = sector.floorHeight;
                    sector.neighbors.ForEach(n => bound[0] = Math.Min(bound[0], n.floorHeight));
                    bound[1] = sector.floorHeight;
                    break;

                case LiftTarget.LowestAndHighestFloor:
                    bound[0] = sector.floorHeight;
                    sector.neighbors.ForEach(n => bound[0] = Math.Min(bound[0], n.floorHeight));
                    bound[1] = sector.floorHeight;
                    sector.neighbors.ForEach(n => bound[1] = Math.Max(bound[1], n.floorHeight));
                    break;

                case LiftTarget.RaiseNextFloor:
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
                    break;

                case LiftTarget.Ceiling:
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    break;

                case LiftTarget.Raise24:
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 24;
                    break;

                case LiftTarget.Raise32:
                    bound[0] = sector.floorHeight;
                    bound[1] = (repeatable == Repeatable.Multiple) ? sector.ceilingHeight : sector.floorHeight + 32;
                    break;

                case LiftTarget.Stop:
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.floorHeight;
                    break;

                default:
                    throw new Exception("Unexpected LineDefFloorType target!");
            }

            return bound;
        }
    }


    public class LineDefCeilingType : LineDefType
    {
        public Trigger trigger;
        public Repeatable repeatable;
        public Direction direction;
        public Speed speed;
        public TextureChange textureChange;
        public Model model;
        public MonsterActivate monsterActivate;
        public Crush crush;
        public CeilingTarget target;

        public LineDefCeilingType(Trigger trigger, Repeatable repeatable, Direction direction, Speed speed, TextureChange textureChange, Model model, MonsterActivate monsterActivate, Crush crush, CeilingTarget target) : base(Category.Ceiling)
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

        public override int[] GetCeilingMovementBound(WAD wad, SECTORS sector)
        {
            int[] bound = new int[2];
            switch (target)
            {
                case CeilingTarget.HighestNeighborCeiling:
                    if (direction == Direction.Down) { throw new Exception("Unexpected LineDefCeilingType direction!"); }
                    bound[0] = sector.ceilingHeight;
                    bound[1] = -32000;
                    sector.neighbors.ForEach(n => bound[1] = Math.Max(bound[1], n.ceilingHeight));
                    if (bound[1] < bound[0])
                    {
                        bound[0] = bound[1];
                        bound[1] = sector.ceilingHeight;
                    }
                    break;

                case CeilingTarget.LowestNeighborCeiling:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefCeilingType direction!"); }
                    bound[0] = 32000;
                    bound[1] = sector.ceilingHeight;
                    sector.neighbors.ForEach(n => bound[0] = Math.Min(bound[0], n.ceilingHeight));
                    if (bound[1] < bound[0])
                    {
                        bound[1] = bound[0];
                        bound[0] = sector.ceilingHeight;
                    }
                    break;

                case CeilingTarget.HighestNeighborFloor:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefCeilingType direction!"); }
                    bound[0] = Math.Max(sector.floorHeight, -32000);
                    sector.neighbors.ForEach(n => bound[0] = Math.Max(bound[0], n.floorHeight));
                    bound[1] = sector.ceilingHeight;
                    if (bound[1] < bound[0])
                    {
                        bound[1] = bound[0];
                        bound[0] = sector.ceilingHeight;
                    }
                    break;

                case CeilingTarget.Floor:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefCeilingType direction!"); }
                    bound[0] = sector.floorHeight;
                    bound[1] = sector.ceilingHeight;
                    if (bound[1] < bound[0])
                    {
                        bound[1] = bound[0];
                        bound[0] = sector.ceilingHeight;
                    }
                    break;

                case CeilingTarget.FloorPlus8:
                    if (direction == Direction.Up) { throw new Exception("Unexpected LineDefCeilingType direction!"); }
                    bound[0] = sector.floorHeight + 8;
                    bound[1] = sector.ceilingHeight;
                    if (bound[1] < bound[0])
                    {
                        bound[1] = bound[0];
                        bound[0] = sector.ceilingHeight;
                    }
                    break;

                default:
                    throw new Exception("Unexpected LineDefCeilingType target!");
            }

            return bound;
        }
    }

    public enum Category { Door, Floor, Lift, Ceiling };

    public enum Trigger { Push, Switch, Walk, Gun };
    public enum Repeatable { Multiple, Once };
    public enum Speed { None, Slow, Fast, Instant };
    public enum MonsterActivate { Yes, No };

    public enum Lock { Blue, Red, Yellow, None };
    public enum DoorAction { Open, OpenWaitClose, Close, CloseWaitOpen };

    public enum Direction { Up, Down, None };
    public enum TextureChange { Tx, Tx0, TxTy, None };
    public enum Model { Trigger, Numeric, None };

    public enum Crush { Yes, No };
    public enum FloorTarget
    {
        LowestNeighborFloor, NextNeighborFloor,
        LowestNeighborCeiling, LowestNeighborCeilingMinus8,
        HighestNeighborFloor, HighestNeighborFloorPlus8,
        Ceiling,
        Absolute24, Absolute32, Absolute512,
        AbsShortestLowerTexture,
        None
    }

    public enum LiftTarget
    {
        LowestNeighborFloor, LowestAndHighestFloor,
        RaiseNextFloor, Raise24, Raise32,
        Ceiling, Stop
    }

    public enum CeilingTarget
    {
        HighestNeighborCeiling, LowestNeighborCeiling,
        HighestNeighborFloor, Floor, FloorPlus8
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
        { 5,   new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborCeiling) },
        // TODO: 9 is missing because 'donuts' are not fun
        { 18,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 19,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloor) },
        { 23,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 24,  new LineDefFloorType(Trigger.Gun,    Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborCeiling) },
        { 30,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.AbsShortestLowerTexture) },
        { 36,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloorPlus8) },
        { 37,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 38,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 45,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloor) },
        { 55,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, FloorTarget.LowestNeighborCeilingMinus8) },
        { 56,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, FloorTarget.LowestNeighborCeilingMinus8) },
        { 58,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 59,  new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 60,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 64,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborCeiling) },
        { 65,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, FloorTarget.LowestNeighborCeilingMinus8) },
        { 69,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 70,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloorPlus8) },
        { 71,  new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloorPlus8) },
        { 78,  new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 82,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 83,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloor) },
        { 84,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 91,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborCeiling) },
        { 92,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 93,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 94,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.Yes, FloorTarget.LowestNeighborCeilingMinus8) },
        { 96,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.AbsShortestLowerTexture) },
        { 98,  new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloorPlus8) },
        { 101, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborCeiling) },
        { 102, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.HighestNeighborFloor) },
        { 119, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 128, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 129, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 130, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 131, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 132, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Fast, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 140, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute512) },
        { 142, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute512) },
        { 147, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute512) },
        { 153, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 154, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 158, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.AbsShortestLowerTexture) },
        { 159, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 160, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 161, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 176, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.AbsShortestLowerTexture) },
        { 177, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.LowestNeighborFloor) },
        { 178, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute512) },
        { 179, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 180, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.Absolute24) },
        { 189, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 190, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Trigger, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 219, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 220, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 221, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 222, new LineDefFloorType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None,    MonsterActivate.No, Crush.No,  FloorTarget.NextNeighborFloor) },
        { 239, new LineDefFloorType(Trigger.Walk,   Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 240, new LineDefFloorType(Trigger.Walk,   Repeatable.Multiple, Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.None) },
        { 241, new LineDefFloorType(Trigger.Switch, Repeatable.Once,     Direction.None, Speed.None, TextureChange.TxTy, Model.Numeric, MonsterActivate.No, Crush.No,  FloorTarget.None) },

        /* LIFTS */
        { 10,  new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 14,  new LineDefLiftType(Trigger.Switch, Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.Raise32) },
        { 15,  new LineDefLiftType(Trigger.Switch, Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx,   Model.Trigger, MonsterActivate.No, LiftTarget.Raise24) },
        { 20,  new LineDefLiftType(Trigger.Switch, Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.RaiseNextFloor) },
        { 21,  new LineDefLiftType(Trigger.Switch, Repeatable.Once,     3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 22,  new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.RaiseNextFloor) },
        { 47,  new LineDefLiftType(Trigger.Gun,    Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.RaiseNextFloor) },
        { 53,  new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestAndHighestFloor) },
        { 54,  new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     0, Speed.None,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.Stop) },
        { 62,  new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 66,  new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 0, Speed.Slow,    TextureChange.Tx,   Model.Trigger, MonsterActivate.No, LiftTarget.Raise24) },
        { 67,  new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.Raise32) },
        { 68,  new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.RaiseNextFloor) },
        { 87,  new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestAndHighestFloor) },
        { 88,  new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 89,  new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 0, Speed.None,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.Stop) },
        { 95,  new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.RaiseNextFloor) },
        { 120, new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 3, Speed.Fast,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 121, new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     3, Speed.Fast,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 122, new LineDefLiftType(Trigger.Switch, Repeatable.Once,     3, Speed.Fast,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 123, new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 3, Speed.Fast,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestNeighborFloor) },
        { 143, new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx,   Model.Trigger, MonsterActivate.No, LiftTarget.Raise24) },
        { 144, new LineDefLiftType(Trigger.Walk,   Repeatable.Once,     0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.Raise32) },
        { 148, new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 0, Speed.Slow,    TextureChange.Tx,   Model.Trigger, MonsterActivate.No, LiftTarget.Raise24) },
        { 149, new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 0, Speed.Slow,    TextureChange.Tx0,  Model.Trigger, MonsterActivate.No, LiftTarget.Raise32) },
        { 162, new LineDefLiftType(Trigger.Switch, Repeatable.Once,     3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestAndHighestFloor) },
        { 163, new LineDefLiftType(Trigger.Switch, Repeatable.Once,     0, Speed.None,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.Stop) },
        { 181, new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 3, Speed.Slow,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.LowestAndHighestFloor) },
        { 182, new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 0, Speed.None,    TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.Stop) },
        { 211, new LineDefLiftType(Trigger.Switch, Repeatable.Multiple, 0, Speed.Instant, TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.Ceiling) },
        { 212, new LineDefLiftType(Trigger.Walk,   Repeatable.Multiple, 0, Speed.Instant, TextureChange.None, Model.None,    MonsterActivate.No, LiftTarget.Ceiling) },

        /* CEILINGS */
        { 40,   new LineDefCeilingType(Trigger.Walk,   Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborCeiling) },
        { 41,   new LineDefCeilingType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Fast, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.Floor) },
        { 43,   new LineDefCeilingType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Fast, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.Floor) },
        { 44,   new LineDefCeilingType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.FloorPlus8) },
        { 72,   new LineDefCeilingType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.FloorPlus8) },
        { 145,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Fast, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.Floor) },
        { 151,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborCeiling) },
        { 152,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Fast, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.Floor) },
        { 166,  new LineDefCeilingType(Trigger.Switch, Repeatable.Once,     Direction.Up,   Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborCeiling) },
        { 167,  new LineDefCeilingType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.FloorPlus8) },
        { 186,  new LineDefCeilingType(Trigger.Switch, Repeatable.Multiple, Direction.Up,   Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborCeiling) },
        { 187,  new LineDefCeilingType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.FloorPlus8) },
        { 199,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.LowestNeighborCeiling) },
        { 200,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborFloor) },
        { 201,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.LowestNeighborCeiling) },
        { 202,  new LineDefCeilingType(Trigger.Walk,   Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborFloor) },
        { 203,  new LineDefCeilingType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.LowestNeighborCeiling) },
        { 204,  new LineDefCeilingType(Trigger.Switch, Repeatable.Once,     Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborFloor) },
        { 205,  new LineDefCeilingType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.LowestNeighborCeiling) },
        { 206,  new LineDefCeilingType(Trigger.Switch, Repeatable.Multiple, Direction.Down, Speed.Slow, TextureChange.None, Model.None, MonsterActivate.No, Crush.No,  CeilingTarget.HighestNeighborFloor) },
    };
}
