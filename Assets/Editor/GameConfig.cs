//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-10-19 12:13
//================================

using UnityEditor;
using UnityEngine;
public class GameConfig : EditorWindow
{
    public enum OS
    {
        Windows,
        Linux
    }

    public enum Platform
    {
        Android,
        Ios,
        Win,
        Linux,
    }

    public enum GameServerHost
    {
        Release,
        Debug,
    }

    public enum ResourceServerHost
    {
        Release,
        Debug,
    }

    private bool _isEditorMode;
    private bool _isHotfixEnable;

    private OS _os;
    private Platform _platform;
    private GameServerHost _gameServerHost;
    private ResourceServerHost _resourceServerHost;

    [MenuItem("GameConfig/Open")]
    public static void ShowWindow()
    {
        var window = (GameConfig)EditorWindow.GetWindow(typeof(GameConfig));
        window.LoadConfig();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings:", EditorStyles.boldLabel);

        _isEditorMode = EditorGUILayout.Toggle("Is Editor Mode", _isEditorMode);
        _isHotfixEnable = EditorGUILayout.Toggle("Is Hotfix Enable", _isHotfixEnable);

        _os = (OS)EditorGUILayout.EnumPopup("OS", _os);
        _platform = (Platform)EditorGUILayout.EnumPopup("Platform", _platform);
        _gameServerHost = (GameServerHost)EditorGUILayout.EnumPopup("GameServerHost", _gameServerHost);
        _resourceServerHost = (ResourceServerHost)EditorGUILayout.EnumPopup("ResourceServerHost", _resourceServerHost);


        if(GUILayout.Button("Save"))
        {
            this.SaveConfig();
            this.SetSymbols();
        }
    }

    private void LoadConfig()
    {
        _isEditorMode = EditorPrefs.GetBool("GameConfig.IsEditorMode", false);
        _isHotfixEnable = EditorPrefs.GetBool("GameConfig.IsHotfixEnable", false);

        _os = (OS)EditorPrefs.GetInt("GameConfig.OS", (int)OS.Windows);
        _platform = (Platform)EditorPrefs.GetInt("GameConfig.Platform", (int)Platform.Android);
        _gameServerHost = (GameServerHost)EditorPrefs.GetInt("GameConfig.GameServerHost", (int)GameServerHost.Debug);
        _resourceServerHost = (ResourceServerHost)EditorPrefs.GetInt("GameConfig.ResourceServerHost", (int)ResourceServerHost.Debug);
    }

    private void SaveConfig()
    {
        EditorPrefs.SetBool("GameConfig.IsEditorMode", _isEditorMode);
        EditorPrefs.SetBool("GameConfig.IsHotfixEnable", _isHotfixEnable);

        EditorPrefs.SetInt("GameConfig.OS", (int)_os);
        EditorPrefs.SetInt("GameConfig.Platform", (int)_platform);
        EditorPrefs.SetInt("GameConfig.GameServerHost", (int)_gameServerHost);
        EditorPrefs.SetInt("GameConfig.ResourceServerHost", (int)_resourceServerHost);
    }

    private void SetSymbols()
    {
        string symbols = string.Empty;
        symbols += _isEditorMode == true ? "EDITOR_MODE;" : "";
        symbols += _isHotfixEnable == true ? "HOTFIX_ENABLE;INJECT_WITHOUT_TOOL;" : "";

        this.AddSymbolOS(ref symbols);
        this.AddSymbolPlatform(ref symbols);
        this.AddSymbolGameServerHost(ref symbols);
        this.AddSymbolResourceServerHost(ref symbols);

        this.SetDefineSymbols(BuildTargetGroup.Android, symbols);
        this.SetDefineSymbols(BuildTargetGroup.Standalone, symbols);
        this.SetDefineSymbols(BuildTargetGroup.iOS, symbols);
    }

    private void AddSymbolOS(ref string symbols)
    {
        switch(_os)
        {
            case OS.Windows:
                symbols += "WIN_EDITOR;";
                break;
            case OS.Linux:
                symbols += "LINUX_EDITOR;";
                break;
        }
    }

    private void AddSymbolPlatform(ref string symbols)
    {
        switch(_platform)
        {
            case Platform.Android:
                symbols += "PLATFORM_ANDROID;";
                break;
            case Platform.Ios:
                symbols += "PLATFORM_IOS;";
                break;
            case Platform.Win:
                symbols += "PLATFORM_WIN;";
                break;
            case Platform.Linux:
                symbols += "PLATFORM_LINUX;";
                break;
        }
    }

    private void AddSymbolGameServerHost(ref string symbols)
    {
        switch(_gameServerHost)
        {
            case GameServerHost.Debug:
                symbols += "GAMESERVER_HOST_DEBUG;";
                break;
            case GameServerHost.Release:
                symbols += "GAMESERVER_HOST_RELEASE;";
                break;
        }
    }

    private void AddSymbolResourceServerHost(ref string symbols)
    {
        switch(_resourceServerHost)
        {
            case ResourceServerHost.Debug:
                symbols += "RESOURCESERVER_HOST_DEBUG;";
                break;
            case ResourceServerHost.Release:
                symbols += "RESOURCESERVER_HOST_RELEASE;";
                break;
        }
    }

    private void SetDefineSymbols(BuildTargetGroup target, string symbols)
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(target, symbols);
    }
}
