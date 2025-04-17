using System.Collections.Generic;

public sealed class EnemyManager
{
    private List<Entity> _enemies = new();

    public void SetEnemies(List<Entity> enemies) 
        => _enemies = enemies;

    public List<Entity> GetEnemies() 
        => _enemies;
}
