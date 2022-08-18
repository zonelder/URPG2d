using UnityEngine;

public class PlayerEnemyDisplay : MonoBehaviour
{
    private bool _isFighting = false;
    private UnitEntity _enemy;

    private float _timer = 0;
    private static float s_showingTime = 5;
    [SerializeField] private GUISkin _enemyHPbar;
    [SerializeField] private UnitEntity _targetPlayer;

    [SerializeField] private float _size = 125;
    private void FightWith(UnitEntity Unit)
    {
        _enemy = Unit;
        _isFighting = true;
        _timer = 0;
    }
    private void Start()
    {
        _targetPlayer.OnDoneDamage.AddSubscriber(FightWith);
    }
    private void OnDisable()
    {
        _targetPlayer.OnDoneDamage.RemoveSubscriber(FightWith);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > s_showingTime)
        {
            _isFighting = false;
            _enemy = null;
        }
    }
    private void OnGUI()
    {
        if (_isFighting)
        {
            float HealthBarLen = _enemy.Stats.HP.RemainingPercent;
            GUI.Box(new Rect(Screen.width / 2 - 127, 15, 254, 15), " ", _enemyHPbar.GetStyle("FullHPBar"));
            GUI.Box(new Rect(Screen.width / 2 - 127, 15, 254 * HealthBarLen, 15), " ", _enemyHPbar.GetStyle("CurHPBar"));
            GUI.Box(new Rect(Screen.width / 2 - 127, 15, 254, 15), _enemy.name, _enemyHPbar.GetStyle("EnemyName"));
        }
    }
}
