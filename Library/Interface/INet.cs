using Library.Entity;

namespace Library.Interface
{
    public interface INet
    {
        /// <summary>
        /// 载入好友
        /// </summary>
        /// <returns></returns>
        Userfriends2SuccessResult LoadFriends();

        /// <summary>
        /// 载入所有群
        /// </summary>
        /// <returns></returns>
        Groups2SuccessResult LoadGroups();

        /// <summary>
        /// 取得群的详细信息
        /// </summary>
        /// <param name="gcode">Group2Gnamelist的code</param>
        /// <returns></returns>
        GroupInfoExt2 LoadGroupDetail(string gcode);

        /// <summary>
        /// 将临时uin转换为qq号
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        FriendUin Uin2Qq(string uin);

        /// <summary>
        /// 发送群组消息
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        SendMsgSuccessResult SendGroupMsg(long gid, string message);

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        SendMsgSuccessResult SendMessage(string uin, string message);

        /// <summary>
        /// 经常被修改的Hash加密代码（获取好友和群组会用到）
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="ptwebqq"></param>
        /// <returns></returns>
        string Hash(string qq, string ptwebqq);

        /// <summary>
        /// 预登录，检查帐号是否需要验证码，同时获取一系列运行时参数
        /// </summary>
        /// <returns></returns>
        bool CheckLogin();

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        byte[] FetchYzm();

        /// <summary>
        /// 正式登录，返回登录结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool Login(out string msg);

        /// <summary>
        /// 进行一次心跳
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Poll2SuccessResult Poll2(Poll2Params source);
    }
}
