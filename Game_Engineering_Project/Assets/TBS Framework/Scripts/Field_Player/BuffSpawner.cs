using System.Collections.Generic;

/// <summary>
/// Applies buffs to units. It requires unit parent game object to be named "Units" to work.
/// </summary>
public class BuffSpawner
{
    private List<Unit> unitsList;


    public void SpawnBuff(Buff buff, Unit unit)
    {
            var _buff = buff.Clone();
            unit.Buffs.Add(_buff);
            _buff.Apply(unit);
    }
}
