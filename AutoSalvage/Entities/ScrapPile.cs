namespace AutoSalvage.Entities
{
    public class ScrapPile : Entity, ISalvagable
    {
        public override bool IsObstruction => false;

        public override string Name => "scrap";

        public int Scrap { get; internal set; } = 1;
    }
}
