--================================
--===Author: easy
--===Email: gopiny@live.com
--===Date: 2017-11-06 12:25
--================================

require("ExtendGlobal")
PanelLogin = class()

function PanelLogin:ctor()

end

function PanelLogin:RegisterBtnEvent()
    local ok, btn = self.cs.HotfixUIDic:TryGetValue("BtnLogin")
    btn = btn:GetComponent("UIButton")
    CS.EventDelegate.Add(btn.onClick, self.onBtnLoginClick)
end

function PanelLogin:TransformGameObject(gameObject)
    print("===>TransformGameObject: " .. gameObject.name);
    self.gameObject = gameObject;
    self.cs = gameObject:GetComponent("PanelHotfix")
    self:RegisterBtnEvent()
end

function PanelLogin:onBtnLoginClick()
    print("Login")
end
