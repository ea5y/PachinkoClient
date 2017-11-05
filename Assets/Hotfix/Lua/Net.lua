--send:
--  pack->From NetPackage to byte[]
--      wirtehead:MsgId, ActionId, Sid, Uid
--      wirtedata:data,
--      ToByte[]
--  origin->
--      Create Callback(string){}
--      NetCallbacksCS.Add(MsgId, Callback)
--      CS.SocketClient.Send(byte[])
--  hotfix->
--      Create Callback(string){}
--      NetCallbacksLUA.Insert(MsgId, Callback)
--      CS.SocketClient.Send(byte[])
--
--recv:
--  unpack->From byte[] to NetPackage
--  check protoc type:
--      req&res
--          origin->CS.SocketClient.RecvResQueueForCS.Enqueue(NetPackage)
--              Find Callback From NetCallbacksCS by MsgId
--              Serialize From string to object
--              Callback(object)
--          hotfix->cS.SocketClient.RecvResQueueForLUA.Enqueue(NetPackage)
--              Transform to Lua
--              Find Callback From NetCallbacksLUA by MsgId
--              Serialize From string to object
--              Callback(object)
--      castbord
--          origin->CS.SocketClient.RecvCastQueueForCS.Enqueue(NetPackage)
--              Find Cast From CastsCS by CastId
--              Serialize From string to object
--              Cast
--          hotfix->cS.SocketClient.RecvCastQueueForLUA.Enqueue(NetPackage)
--              Transform to Lua
--              Find Cast From CastsLUA by CastId
--              Serialize From string to object
--              Cast
--
--
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
--
require("ExtendGlobal")
Net = class()
Net.protocActionId = {
    login = "1002"
}

Net.protocBrodcastId = {
}

function Net:send(data, protocId, callback)
	--1.local dataJson = Serialize(data)
	--2.local package = CS.NetPackage()
	--3.package.MsgId = CS.Net.MsgId
	--4.package.ProtocId = protocId
	--5.package.Data = dataJson
	--6.local bytes = CS.Net.Pack(package)
	--local action = function(res)
	--	local obj = UnSerialize(res)
	--	callback(obj)
	--end
	--7.CS.Net.AddActionToResponseCallbacksLUA(package.MsgId, action)
	--8.CS.SocketClient.Send(bytes)
end

function Net:login(username, password, callback)
	--local data = new LoginDataReq
	--self:send(data, self.actionId, callback)
end


