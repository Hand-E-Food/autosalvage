namespace AutoSalvage.World.Generator
{
    internal interface IDoorGenerator
    {
        Door CreateDoor(Room room1, Room room2);
    }
}