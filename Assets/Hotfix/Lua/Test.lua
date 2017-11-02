require("ExtendGlobal")
TestLua = class()

function TestLua:ctor()
end

function TestLua:pack()
end

function TestLua:unpack()
end

function TestLua:test()
    CS.Easy.FrameUnity.Net.Net.InvokeAsyncForCS(function()
        print("Test lua")
    end)
end

TestLua:test()
