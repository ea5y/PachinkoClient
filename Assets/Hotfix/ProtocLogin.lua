--================================
--===Author: easy
--===Email: gopiny@live.com
--===Date: 2017-11-03 11:07
--================================
local Net = require("Net")
local LoginReq = {}
local LoginRes = {}

ProtocLogin = class()

ProtocLogin.callback = false

function ProtocLogin:send(username, password)
    local package = CS.NetPackage()
    package.MsgId = CS.Net.MsgId;
    package.ActionId = Net.Net.protocActionType.login
    package.Data = self:getPacakgeData(username, password)
    local bytes = CS.Net.Pack(package)
    Net.CallbacksLUA.insert(package.MsgId, self.recv)
    CS.SocketClientNew.Send(bytes)
end

function ProtocLogin:getPacakgeData(username, password)
    LoginReq.username = username
    LoginReq.password = password
    local data
    --data = serialize(LoginReq)
    return data
end

function ProtocLogin:recv(str)
    local object
    --object = Serialize from string to object
    if self.callback then
        self.callback(object)
    end
end
