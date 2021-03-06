//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-10-16 18:01
//================================

using System.IO;
using System.Collections.Generic;
using XLua;
using Easy.FrameUnity.EsAssetBundle;
using Easy.FrameUnity.ScriptableObj;
using UnityEngine;
using Asobimo.Pachinko;

namespace Easy.FrameUnity.Manager
{
    [CSharpCallLuaAttribute]
    public interface ILuaTable : IPanel
    {
        void Awake();
        void Start();
        void OnEnable();
        void OnDisable();
        void Update();
        void OnDestroy();
        void TransformGameObject(GameObject gameObject);
    }

    public class HotfixManager : ManagerBase<HotfixManager>
    {
        public LuaEnv LuaEnv;

        private Dictionary<string, ILuaTable> _luaTableDic = new Dictionary<string, ILuaTable>();

        private void Awake()
        {
            base.GetInstance();
        }

        public override void Init()
        {
            this.GetLuaTable();
        }

        public void EnableHotFix()
        {
            if(File.Exists(URL.HOTFIX_MAIN_URL))
            {
                this.LuaEnv = new LuaEnv();
                //this.LuaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
                this.LuaEnv.AddLoader(CustomLoader);
                this.LuaEnv.DoString(File.ReadAllText(URL.HOTFIX_MAIN_URL));
            }
        }

        private byte[] CustomLoader(ref string fileName)
        {
            Debug.Log("Check Lua file: " + fileName);
            fileName = URL.LUA_LOCAL_URL + fileName.Replace('.', '/') + ".lua";
            if(File.Exists(fileName))
            {
                return File.ReadAllBytes(fileName);
            }
            else
            {
                return null;
            }
        }

        public void GetLuaTable()
        {
            /*
            AssetPoolManager.Inst.FindAsset<AssetScriptableObject, SObjPanelNames>("parameter", "PanelInfo", (obj) => {
            */
                /*
                foreach(var panelName in obj.PanelNameList)
                {
                    var table = _luaEnv.Global.Get<ILuaTable>(panelName);
                    _luaTableDic.Add(panelName, table);
                }
                */
                /*
                    var table = this.LuaEnv.Global.Get<ILuaTable>("PanelOther");
                    _luaTableDic.Add("PanelOther", table);
            });
            */
            var table = this.LuaEnv.Global.Get<ILuaTable>("PanelLogin");
            _luaTableDic.Add("PanelLogin", table);
        }

        public ILuaTable GetLuaTable(string key)
        {
            ILuaTable table;
            if(!_luaTableDic.TryGetValue(key, out table))
            {
                return null;
            }
            return table;
        }
    }
}
