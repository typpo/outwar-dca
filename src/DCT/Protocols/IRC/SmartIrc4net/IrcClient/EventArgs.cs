/*
 * $Id: EventArgs.cs 203 2005-06-10 01:42:42Z meebey $
 * $URL: svn://svn.qnetp.net/smartirc/SmartIrc4net/tags/0.4.0/src/IrcClient/EventArgs.cs $
 * $Rev: 203 $
 * $Author: meebey $
 * $Date: 2005-06-10 03:42:42 +0200 (Fri, 10 Jun 2005) $
 *
 * SmartIrc4net - the IRC library for .NET/C# <http://smartirc4net.sf.net>
 *
 * Copyright (c) 2003-2005 Mirco Bauer <meebey@meebey.net> <http://www.meebey.net>
 * 
 * Full LGPL License: <http://www.gnu.org/licenses/lgpl.txt>
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General internal
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General internal License for more details.
 *
 * You should have received a copy of the GNU Lesser General internal
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Specialized;

namespace Meebey.SmartIrc4net
{
    /// <summary>
    ///
    /// </summary>
    internal class ActionEventArgs : CtcpEventArgs
    {
        private string _ActionMessage;
        
        internal string ActionMessage {
            get {
                return _ActionMessage;
            }
        }
         
        internal ActionEventArgs(IrcMessageData data, string actionmsg) : base(data, "ACTION", actionmsg)
        {
            _ActionMessage = actionmsg;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class CtcpEventArgs : IrcEventArgs
    {
        private string _CtcpCommand;
        private string _CtcpParameter;
        
        internal string CtcpCommand {
            get {
                return _CtcpCommand;
            }
        }
         
        internal string CtcpParameter {
            get {
                return _CtcpParameter;
            }
        }
         
        internal CtcpEventArgs(IrcMessageData data, string ctcpcmd, string ctcpparam) : base(data)
        {
            _CtcpCommand = ctcpcmd;
            _CtcpParameter = ctcpparam;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class ErrorEventArgs : IrcEventArgs
    {
        private string _ErrorMessage;
        
        internal string ErrorMessage {
            get {
                return _ErrorMessage;
            }
        }
         
        internal ErrorEventArgs(IrcMessageData data, string errormsg) : base(data)
        {
            _ErrorMessage = errormsg;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class MotdEventArgs : IrcEventArgs
    {
        private string _MotdMessage;
        
        internal string MotdMessage {
            get {
                return _MotdMessage;
            }
        }
         
        internal MotdEventArgs(IrcMessageData data, string motdmsg) : base(data)
        {
            _MotdMessage = motdmsg;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class PingEventArgs : IrcEventArgs
    {
        private string _PingData;
        
        internal string PingData {
            get {
                return _PingData;
            }
        }
         
        internal PingEventArgs(IrcMessageData data, string pingdata) : base(data)
        {
            _PingData = pingdata;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class PongEventArgs : IrcEventArgs
    {
        private TimeSpan _Lag;
        
        internal TimeSpan Lag {
            get {
                return _Lag;
            }
        }

        internal PongEventArgs(IrcMessageData data, TimeSpan lag) : base(data)
        {
            _Lag = lag;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class KickEventArgs : IrcEventArgs
    {
        private string _Channel;
        private string _Who;
        private string _Whom;
        private string _KickReason;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }
         
        internal string Whom {
            get {
                return _Whom;
            }
        }
         
        internal string KickReason {
            get {
                return _KickReason;
            }
        }
         
        internal KickEventArgs(IrcMessageData data, string channel, string who, string whom, string kickreason) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
            _KickReason = kickreason;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class JoinEventArgs : IrcEventArgs
    {
        private string _Channel;
        private string _Who;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }
         
        internal JoinEventArgs(IrcMessageData data, string channel, string who) : base(data)
        {
            _Channel = channel;
            _Who = who;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class NamesEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string[] _UserList;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string[] UserList {
            get {
                return _UserList;
            }
        }
         
        internal NamesEventArgs(IrcMessageData data, string channel, string[] userlist) : base(data)
        {
            _Channel = channel;
            _UserList = userlist;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class InviteEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }
         
        internal InviteEventArgs(IrcMessageData data, string channel, string who) : base(data)
        {
            _Channel = channel;
            _Who = who;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class PartEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _PartMessage;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }
         
        internal string PartMessage {
            get {
                return _PartMessage;
            }
        }
        
        internal PartEventArgs(IrcMessageData data, string channel, string who, string partmessage) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _PartMessage = partmessage;
        }
    }
    /// <summary>
    ///
    /// </summary>
    internal class WhoEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Nick;
        private string   _Ident;
        private string   _Host;
        private string   _Realname;
        private bool     _IsAway;
        private bool     _IsOp;
        private bool     _IsVoice;
        private bool     _IsIrcOp;
        private string   _Server;
        private int      _HopCount;

        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Nick {
            get {
                return _Nick;
            }
        }
         
        internal string Ident {
            get {
                return _Ident;
            }
        }
        
        internal string Host {
            get {
                return _Host;
            }
        }
        
        internal string Realname {
            get {
                return _Realname;
            }
        }
        
        internal bool IsAway {
            get {
                return _IsAway;
            }
        }
        
        internal bool IsOp {
            get {
                return _IsOp;
            }
        }
        
        internal bool IsVoice {
            get {
                return _IsVoice;
            }
        }
        
        internal bool IsIrcOp {
            get {
                return _IsIrcOp;
            }
        }
        
        internal string Server {
            get {
                return _Server;
            }
        }

        internal int HopCount {
            get {
                return _HopCount;
            }
        }

        internal WhoEventArgs(IrcMessageData data, string channel, string nick, string ident, string host, string realname, bool away, bool op, bool voice, bool ircop, string server, int hopcount) : base(data)
        {
            _Channel = channel;
            _Nick = nick;
            _Ident = ident;
            _Host = host;
            _Realname = realname;
            _IsAway = away;
            _IsOp = op;
            _IsVoice = voice;
            _IsIrcOp = ircop;
            _Server = server;
            _HopCount = hopcount;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class QuitEventArgs : IrcEventArgs
    {
        private string   _Who;
        private string   _QuitMessage;
        
        internal string Who {
            get {
                return _Who;
            }
        }

        internal string QuitMessage {
            get {
                return _QuitMessage;
            }
        }
        
        internal QuitEventArgs(IrcMessageData data, string who, string quitmessage) : base(data)
        {
            _Who = who;
            _QuitMessage = quitmessage;
        }
    }


    /// <summary>
    ///
    /// </summary>
    internal class AwayEventArgs : IrcEventArgs
    {
        private string   _Who;
        private string   _AwayMessage;
        
        internal string Who {
            get {
                return _Who;
            }
        }

        internal string AwayMessage{
            get {
                return _AwayMessage;
            }
        }
        
        internal AwayEventArgs(IrcMessageData data, string who, string awaymessage) : base(data)
        {
            _Who = who;
            _AwayMessage = awaymessage;
        }
    }
    /// <summary>
    ///
    /// </summary>
    internal class NickChangeEventArgs : IrcEventArgs
    {
        private string   _OldNickname;
        private string   _NewNickname;
        
        internal string OldNickname {
            get {
                return _OldNickname;
            }
        }

        internal string NewNickname {
            get {
                return _NewNickname;
            }
        }
        
        internal NickChangeEventArgs(IrcMessageData data, string oldnick, string newnick) : base(data)
        {
            _OldNickname = oldnick;
            _NewNickname = newnick;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class TopicEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Topic;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Topic {
            get {
                return _Topic;
            }
        }
        
        internal TopicEventArgs(IrcMessageData data, string channel, string topic) : base(data)
        {
            _Channel = channel;
            _Topic = topic;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class TopicChangeEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _NewTopic;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string NewTopic {
            get {
                return _NewTopic;
            }
        }
        
        internal TopicChangeEventArgs(IrcMessageData data, string channel, string who, string newtopic) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _NewTopic = newtopic;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class BanEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Hostmask;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Hostmask {
            get {
                return _Hostmask;
            }
        }
        
        internal BanEventArgs(IrcMessageData data, string channel, string who, string hostmask) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Hostmask = hostmask;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class UnbanEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Hostmask;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Hostmask {
            get {
                return _Hostmask;
            }
        }
        
        internal UnbanEventArgs(IrcMessageData data, string channel, string who, string hostmask) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Hostmask = hostmask;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class OpEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Whom;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Whom {
            get {
                return _Whom;
            }
        }
        
        internal OpEventArgs(IrcMessageData data, string channel, string who, string whom) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class DeopEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Whom;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Whom {
            get {
                return _Whom;
            }
        }
        
        internal DeopEventArgs(IrcMessageData data, string channel, string who, string whom) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class HalfopEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Whom;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Whom {
            get {
                return _Whom;
            }
        }
        
        internal HalfopEventArgs(IrcMessageData data, string channel, string who, string whom) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class DehalfopEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Whom;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Whom {
            get {
                return _Whom;
            }
        }
        
        internal DehalfopEventArgs(IrcMessageData data, string channel, string who, string whom) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
        }
    }
    
    /// <summary>
    ///
    /// </summary>
    internal class VoiceEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Whom;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Whom {
            get {
                return _Whom;
            }
        }
        
        internal VoiceEventArgs(IrcMessageData data, string channel, string who, string whom) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class DevoiceEventArgs : IrcEventArgs
    {
        private string   _Channel;
        private string   _Who;
        private string   _Whom;
        
        internal string Channel {
            get {
                return _Channel;
            }
        }

        internal string Who {
            get {
                return _Who;
            }
        }

        internal string Whom {
            get {
                return _Whom;
            }
        }
        
        internal DevoiceEventArgs(IrcMessageData data, string channel, string who, string whom) : base(data)
        {
            _Channel = channel;
            _Who = who;
            _Whom = whom;
        }
    }

}
