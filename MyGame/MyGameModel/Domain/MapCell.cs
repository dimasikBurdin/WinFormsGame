namespace MyGameModel.Domain
{
    public enum MapCell
    {
        Grass,
        Trail,
        Land,//можно ходить
        Rock,
        Forest,//нельзя ходить
        Water,
        Empty
    }
}
