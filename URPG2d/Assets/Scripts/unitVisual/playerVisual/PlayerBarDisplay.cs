using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBarDisplay : MonoBehaviour
{
    [SerializeField] private GUISkin mySkin;
    [SerializeField] private UnitEntity Char;
    private bool _visible = true;


    private void Start()
    {
        Char.OnDead += OnDeadGUI;
    }

    private void OnDisable()
    {
        Char.OnDead -= OnDeadGUI;
    }
    private void OnGUI()
    {
        if (_visible)
        {
            GUI.skin = mySkin;
            UnitEntity PlayerSt = Char;


            float MaxHealth = PlayerSt.Stats.HP.Max;
            float CurHealth = PlayerSt.Stats.HP.Current();
            float MaxMana = PlayerSt.Stats.MP.Max;
            float CurMana = PlayerSt.Stats.MP.Current();


            float HealthBarLen = CurHealth / MaxHealth;
            float ManaBarLen = CurMana / MaxMana;
            float ExpBarLen = PlayerSt.Exp.DonePersent();

            GUI.Box(new Rect(10, 15, 254 * HealthBarLen, 15), " ", GUI.skin.GetStyle("HPbar"));
            GUI.Box(new Rect(10, 35, 254 * ManaBarLen, 15), " ", GUI.skin.GetStyle("MPbar"));
            GUI.Box(new Rect(10, 55, 254 * ExpBarLen, 15), " ", GUI.skin.GetStyle("EXPbar"));
            GUI.Box(new Rect(10, 10, 254, 64), " ", GUI.skin.GetStyle("PlayerBar"));
            GUI.Box(new Rect(Screen.width / 2 - 7, Screen.height / 2 - 7, 14, 14), " ", GUI.skin.GetStyle("Crosshair"));


        }
    }
    private void OnDeadGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 50), "Переиграть"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
