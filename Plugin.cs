using System.Reflection;

using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using HarmonyLib;
namespace CheatConsole;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        ClassInjector.RegisterTypeInIl2Cpp<BruhBehavour>();

        var go = new GameObject();
        go.AddComponent<BruhBehavour>();
        UnityEngine.Object.DontDestroyOnLoad(go);

        Harmony harmony = new Harmony("stupidfuckingshit");
        MethodInfo original = AccessTools.Method(typeof(Sons.VitalsGui), "Update");
        MethodInfo real = AccessTools.Method(typeof(BruhBehavour), "Update");
        harmony.Patch(original, postfix: new HarmonyMethod(real));
    }
}

/*
[02:38]YUO HAVE NO PERSONALITY: so you can inherit the RumpBehavior?
[02:38]YUO HAVE NO PERSONALITY: the ThugBehavior? give me the ThugBehavior
[02:38]YUO HAVE NO PERSONALITY: get your il2cpp off that and Update() that shit
*/

public class BruhBehavour : MonoBehaviour
{
    private static bool flip = false;
    private static bool show = true;


    [HarmonyPostfix]
    public static void Update()
    {
        bool key = Input.GetKeyDown(KeyCode.F1); // or smth
        bool oldFlip = flip;
        flip = key;

        if (flip && !oldFlip)
          show = !show;

        if(TheForest.DebugConsole.Instance && flip && !oldFlip)
             TheForest.DebugConsole.Instance.ShowConsole(show);
    }
}