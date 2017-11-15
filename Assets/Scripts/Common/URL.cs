using UnityEngine;
using System.Collections;

public class URL
{
    #region Server
    public static readonly int GAME_SERVER_PORT = 9001;
    public static readonly string GAME_SERVER_HOST =
#if GAMESERVER_HOST_DEBUG
		"127.0.0.1";
#elif GAMESERVER_HOST_RELEASE
        "120.26.119.202";
#else
        "";
#endif

    public static readonly string RESOURCE_SERVER_HOST = 
#if RESOURCESERVER_HOST_DEBUG
        "127.0.0.1";
#elif RESOURCESERVER_HOST_RELEASE
        "120.26.119.202";
#else
        "";
#endif
    #endregion

    #region Hotfix 
    #region Export
    public static readonly string HOTFIXEXPORT_OUTPUT_URL = "HotfixExport/";
    public static readonly string HOTFIXEXPORT_INPUT_URL = Application.dataPath + "/Hotfix/";
    public static readonly string ASSETBUNDLE_INPUT_URL = HOTFIXEXPORT_INPUT_URL + "Bundle/";
    public static readonly string ASSETBUNDLE_OUTPUT_URL = HOTFIXEXPORT_OUTPUT_URL + "bundle/";
    public static readonly string ASSETBUNDLE_PERSONAL_URL = Application.dataPath + "/Hotfix/Bundle/Personal/";
    public static readonly string ASSETBUNDLE_SHARE_URL = Application.dataPath + "/Hotfix/Bundle/Share/";

    public static readonly string LUA_INPUT_URL = HOTFIXEXPORT_INPUT_URL + "Lua/";
    public static readonly string LUA_OUTPUT_URL = HOTFIXEXPORT_OUTPUT_URL + "lua/";
    #endregion

    public static readonly string PERSISTENTDATA_URL = Application.persistentDataPath + "/";
    public static readonly string RESOURCE_FILE_LIST_FILENAME = "ResourceFileList.json";
    #region AssetBundle
    public static readonly string ASSETBUNDLE_HOST_URL = 
#if UNITY_ANDROID
        "http://" + RESOURCE_SERVER_HOST + "/luwanzhong/resource/pk/bundle/android/";
#elif UNITY_IPHONE
        "http://" + RESOURCE_SERVER_HOST + "/luwanzhong/resource/pk/bundle/ios/";
#elif UNITY_STANDALONE_WIN
        "http://" + RESOURCE_SERVER_HOST + "/luwanzhong/resource/pk/bundle/win/";
#elif UNITY_STANDALONE_LINUX
        "http://" + RESOURCE_SERVER_HOST + "/luwanzhong/resource/pk/bundle/linux/";
#elif UNITY_STANDALONE_OSX
        "http://" + RESOURCE_SERVER_HOST + "/luwanzhong/resource/pk/bundle/osx/";
#else
		"";
#endif

    public static readonly string ASSETBUNDLE_LOCAL_URL =
#if UNITY_EDITOR && EDITOR_MODE
        "Assets/Hotfix/Bundle/";
#else
        PERSISTENTDATA_URL;
#endif

    public static readonly string FILE_ASSETBUNDLE_LOCAL_URL = "file:///" + ASSETBUNDLE_LOCAL_URL;
    #endregion

    #region Lua
    public static readonly string LUA_HOST_URL =
        "http://" + RESOURCE_SERVER_HOST + "/luwanzhong/resource/pk/lua/";
		
    public static readonly string LUA_LOCAL_URL =
#if UNITY_EDITOR && EDITOR_MODE
        Application.dataPath + "/Hotfix/Lua/";
#else
        PERSISTENTDATA_URL;
#endif

    public static readonly string HOTFIX_MAIN_URL = LUA_LOCAL_URL + "Hotfix.lua";
    #endregion
    #endregion

    public static readonly string DEBUG_CONFIG = 
#if UNITY_ANDROID && !UNITY_EDITOR
		PERSISTENTDATA_URL;
#else
		"Assets/DebugConfig/";
#endif
}
