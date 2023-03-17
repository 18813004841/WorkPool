using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Net
{

    public class BufferEntity
    {
        public int recurCount = 0;//�ط����� �����ڲ�ʹ�õ� ����ҵ������
        public IPEndPoint endPoint;//���͵�Ŀ���ն�


        public int protoSize;
        public int session;//�ỰID
        public int sn;//���
        public int moduleID;//ģ��ID
        public long time;//����ʱ��
        public int messageType;//Э������
        public int messageID;//Э��ID
        public byte[] proto;//ҵ����

        public byte[] buffer;//����Ҫ���͵����� ������ �յ�������

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="session"></param>
        /// <param name="sn"></param>
        /// <param name="moduleID"></param>
        /// <param name="messageType"></param>
        /// <param name="messageID"></param>
        /// <param name="proto"></param>
        public BufferEntity(IPEndPoint endPoint, int session, int sn, int moduleID, int messageType, int messageID, byte[] proto)
        {
            protoSize = proto.Length;//ҵ�����ݵĴ�С
            this.endPoint = endPoint;
            this.session = session;
            this.sn = sn;
            this.moduleID = moduleID;
            this.messageType = messageType;
            this.messageID = messageID;
            this.proto = proto;

        }

        //����Ľӿ� byte[] ACKȷ�ϱ��� ҵ����
        public byte[] Encoder(bool isAck)
        {
            byte[] data = new byte[32 + protoSize];
            if (isAck == true)
            {
                protoSize = 0;//���͵�ҵ�����ݵĴ�С
            }
            byte[] _length = BitConverter.GetBytes(protoSize);
            byte[] _session = BitConverter.GetBytes(session);
            byte[] _sn = BitConverter.GetBytes(sn);
            byte[] _moduleid = BitConverter.GetBytes(moduleID);
            byte[] _time = BitConverter.GetBytes(time);
            byte[] _messageType = BitConverter.GetBytes(messageType);
            byte[] _messageID = BitConverter.GetBytes(messageID);

            //Ҫ���ֽ����� д�뵽data
            Array.Copy(_length, 0, data, 0, 4);
            Array.Copy(_session, 0, data, 4, 4);
            Array.Copy(_sn, 0, data, 8, 4);
            Array.Copy(_moduleid, 0, data, 12, 4);
            Array.Copy(_time, 0, data, 16, 8);
            Array.Copy(_messageType, 0, data, 24, 4);
            Array.Copy(_messageID, 0, data, 28, 4);
            if (isAck)
            {

            }
            else
            {
                //ҵ������ ׷�ӽ���
                Array.Copy(proto, 0, data, 32, proto.Length);
            }
            buffer = data;
            return data;
        }

        /// <summary>
        /// �������յ��ı���ʵ��
        /// </summary>
        /// <param name="endPoint">�ն�IP�Ͷ˿�</param>
        /// <param name="buffer">�յ�������</param>
        public BufferEntity(IPEndPoint endPoint, byte[] buffer)
        {

            this.endPoint = endPoint;
            this.buffer = buffer;
            DeCode();
        }

        public bool isFull = false;

        //�����ķ����л� ��Ա
        private void DeCode()
        {
            if (buffer.Length >= 4)
            {
                //�ֽ����� ת���� int ������long
                protoSize = BitConverter.ToInt32(buffer, 0);//��0��λ�� ȡ4���ֽ�ת����int
                if (buffer.Length == protoSize + 32)
                {
                    isFull = true;
                }
            }
            else
            {
                isFull = false;
                return;
            }

            session = BitConverter.ToInt32(buffer, 4);//��4��λ�� ȡ4���ֽ�ת����int
            sn = BitConverter.ToInt32(buffer, 8); //��8��λ�� ȡ4���ֽ�ת����int
            moduleID = BitConverter.ToInt32(buffer, 12);

            time = BitConverter.ToInt64(buffer, 16);//��16��λ�� ȡ8���ֽ�ת����int

            messageType = BitConverter.ToInt32(buffer, 24);//
            messageID = BitConverter.ToInt32(buffer, 28);

            //BitConverter.ToInt64();//long
            if (messageType == 0)
            {

            }
            else
            {
                proto = new byte[protoSize];
                //��buffer��ʣ�µ����� ���Ƶ�proto �õ����յ�ҵ������
                Array.Copy(buffer, 32, proto, 0, protoSize);
            }
        }


        /// <summary>
        /// ����һ��ACK���ĵ�ʵ��
        /// </summary>
        /// <param name="package">�յ��ı���ʵ��</param>
        public BufferEntity(BufferEntity package)
        {
            protoSize = 0;
            this.endPoint = package.endPoint;
            this.session = package.session;
            this.sn = package.sn;
            this.moduleID = package.moduleID;
            this.time = 0;//
            this.messageType = 0;//
            this.messageID = package.messageID;

            //�ỰID ���

            buffer = Encoder(true);
        }

    }

}
