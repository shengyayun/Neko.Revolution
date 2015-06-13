using System;
using Library.Common;
using Library.Components;
using Library.Entity;
using Library.Interface;

namespace Library.Manager
{
    public class NetMgr
    {
        private static readonly INet Net = new NetDao();

        /// <summary>
        /// 载入好友
        /// </summary>
        /// <returns></returns>
        public Userfriends2SuccessResult LoadFriends()
        {
            try
            {
                return Net.LoadFriends();
            }
            catch (Exception e)
            {
                LogHelper.Info("载入好友失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 载入所有群
        /// </summary>
        /// <returns></returns>
        public Groups2SuccessResult LoadGroups()
        {
            try
            {
                return Net.LoadGroups();
            }
            catch (Exception e)
            {
                LogHelper.Info("载入所有群失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 取得群的详细信息
        /// </summary>
        /// <param name="gcode">Group2Gnamelist的code</param>
        /// <returns></returns>
        public GroupInfoExt2 LoadGroupDetail(string gcode)
        {
            try
            {
                return Net.LoadGroupDetail(gcode);
            }
            catch (Exception e)
            {
                LogHelper.Info("取得群的详细信息失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 将临时uin转换为qq号
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        public FriendUin Uin2Qq(string uin)
        {
            try
            {
                return Net.Uin2Qq(uin);
            }
            catch (Exception e)
            {
                LogHelper.Info("将临时uin转换为qq号失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 发送群组消息
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SendMsgSuccessResult SendGroupMsg(long gid, string message)
        {
            try
            {
                return Net.SendGroupMsg(gid, message);
            }
            catch (Exception e)
            {
                LogHelper.Info("发送群组消息失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SendMsgSuccessResult SendMessage(string uin, string message)
        {
            try
            {
                return Net.SendMessage(uin, message);
            }
            catch (Exception e)
            {
                LogHelper.Info("发送私聊消息失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 经常被修改的Hash加密代码（获取好友和群组会用到）
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="ptwebqq"></param>
        /// <returns></returns>
        public string Hash(string qq, string ptwebqq)
        {
            try
            {
                return Net.Hash(qq, ptwebqq);
            }
            catch (Exception e)
            {
                LogHelper.Info("Hash加密失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 预登录，检查帐号是否需要验证码，同时获取一系列运行时参数
        /// </summary>
        /// <returns>不需要验证码为true</returns>
        public bool CheckLogin()
        {
            try
            {
                return Net.CheckLogin();
            }
            catch (Exception e)
            {
                LogHelper.Info("预登录失败：{0}", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public byte[] FetchYzm()
        {
            try
            {
                return Net.FetchYzm();
            }
            catch (Exception e)
            {
                LogHelper.Info("获取验证码失败：{0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 正式登录，返回登录结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool Login(out string msg)
        {
            try
            {
                return Net.Login(out msg);
            }
            catch (Exception e)
            {
                LogHelper.Info("正式登录失败：{0}", e.Message);
                msg = "未知错误";
                return false;
            }
        }

        /// <summary>
        /// 进行一次心跳
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Poll2SuccessResult Poll2(Poll2Params source)
        {
            try
            {
                return Net.Poll2(source);
            }
            catch (Exception e)
            {
                LogHelper.Info("心跳失败：{0}", e.Message);
                return null;
            }
        }
    }
}
