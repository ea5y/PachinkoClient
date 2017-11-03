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
--  string ActionId,
--  string CastId,
--  string Sid,
--  string Uid,
--  string Type,
--  string Version,
--  string Data,
--}
--
require("ExtendGlobal")
Net = class()
Net.protocActionType = {
    login = "1002"
}

Net.protocCastType = {
}

Net.NetCallbacksLUA = {}


