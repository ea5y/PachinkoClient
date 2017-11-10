--send:
--  pack->From NetPackage to byte[]
--  origin->
--      Create Callback(string){}
--      NetCallbacksCS.Add(MsgId, Callback)
--      CS.SocketClient.Send(byte[])
--  hotfix->
--      Create Callback(string){}
--      NetCallbacksLUA.Add(MsgId, Callback)
--      CS.SocketClient.Send(byte[])
--
--recv:
--  unpack->From byte[] to NetPackage
--  check protoc type:
--      req&res
--          origin->Net.RecvResQueueForCS.Enqueue(NetPackage)
--              Find Callback From NetCallbacksCS by MsgId
--              Serialize From string to object
--              Callback(object)
--          hotfix->cS.SocketClient.RecvResQueueForLUA.Enqueue(NetPackage)
--              Find Callback From NetCallbacksLUA by MsgId
--              Serialize From string to object
--              Callback(object)
--      brodcast
--          origin->CS.SocketClient.RecvCastQueueForCS.Enqueue(NetPackage)
--              Find Cast From CastsCS by CastId
--              Serialize From string to object
--              Cast
--          hotfix->cS.SocketClient.RecvCastQueueForLUA.Enqueue(NetPackage)
--              Find Cast From CastsLUA by CastId
--              Serialize From string to object
--              Cast
--
--public class NetPackage
--{
--  string MsgId,
--  string ProtocId,
--  string Sid,
--  string Uid,
--  string Type,
--  string Version,
--  string Data,
--}
require("ExtendGlobal")
local rapidjson = require("rapidjson")
local CSNet = CS.Easy.FrameUnity.ESNetwork

Net = class()
Net.protocActionId = {
    login = "1002"
}

Net.protocBrodcastId = {
}

function Net:send(data, protocId, callback)
    local dataJson = rapidjson.encode(data)
    local package = CSNet.NetPackage()
    package.MsgId = CSNet.Net.MsgId
    package.ProtocId = protocId
    package.Data = dataJson
    local bytes = CSNet.Net.Pack(package)

    local action = function(res)
        local obj = rapidjson.decode(res)
        callback(obj)
    end
    CSNet.Net.AddActionToResponseCallbacksLUA(package.MsgId, action)

    CSNet.SocketClient.Send(bytes)
end

function Net:login(username, password, callback)
	local data = {Username = username, Password = password}
    self:send(data, self.protocActionId.login, callback)
end


