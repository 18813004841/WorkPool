1.BufferEntity 消息体
	1.1 结构
		recurCount	重发次数
		IpEndPoint	发送目标终端
		session		会话ID，客户端用于在服务器表示自己的唯一ID（可以是roleID）
		sn			消息序号（每条新得消息，消息序号递增）
		moduleID	模块ID，区分服务器消息处理模块
		messageType	协议类型（确认报文/业务逻辑，确认报文不需要消息体）
		messageID	协议ID（代表使用的是哪个proto的消息结构，）
		protoSize	消息内容字节数
		proto		业务报文（发送的消息内容）
		buffer		最终发送的数据体，包含上面所有的内容
	1.2 消息类型
		ACK=0,//确认报文
        Login=1,//业务逻辑的报文
