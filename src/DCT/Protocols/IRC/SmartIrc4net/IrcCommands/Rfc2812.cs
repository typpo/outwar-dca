/*
 * $Id: Rfc2812.cs 198 2005-06-08 16:50:11Z meebey $
 * $URL: svn://svn.qnetp.net/smartirc/SmartIrc4net/tags/0.4.0/src/IrcCommands/Rfc2812.cs $
 * $Rev: 198 $
 * $Author: meebey $
 * $Date: 2005-06-08 18:50:11 +0200 (Wed, 08 Jun 2005) $
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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Meebey.SmartIrc4net
{
    /// <summary>
    ///
    /// </summary>
    /// <threadsafety static="true" instance="true" />
    internal sealed class Rfc2812
    {
        // nickname   =  ( letter / special ) *8( letter / digit / special / "-" )
        // letter     =  %x41-5A / %x61-7A       ; A-Z / a-z
        // digit      =  %x30-39                 ; 0-9
        // special    =  %x5B-60 / %x7B-7D
        //                  ; "[", "]", "\", "`", "_", "^", "{", "|", "}"
        private static Regex _NicknameRegex = new Regex(@"^[A-Za-z\[\]\\`_^{|}][A-Za-z0-9\[\]\\`_\-^{|}]+$", RegexOptions.Compiled);
        
        private Rfc2812()
        {
        }
        
        /// <summary>
        /// Checks if the passed nickname is valid according to the RFC
        ///
        /// Use with caution, many IRC servers are not conform with this!
        /// </summary>
        internal static bool IsValidNickname(string nickname)
        {
            if ((nickname != null) &&
                (nickname.Length > 0) &&
                (_NicknameRegex.Match(nickname).Success)) {
                return true;
            }
            
            return false;
        }
        
        internal static string Pass(string password)
        {
            return "PASS "+password;
        }
        
        internal static string Nick(string nickname)
        {
            return "NICK "+nickname;
        }
        
        internal static string User(string username, int usermode, string realname)
        {
            return "USER "+username+" "+usermode.ToString()+" * :"+realname;
        }

        internal static string Oper(string name, string password)
        {
            return "OPER "+name+" "+password;
        }
        
        internal static string Privmsg(string destination, string message)
        {
            return "PRIVMSG "+destination+" :"+message;
        }

        internal static string Notice(string destination, string message)
        {
            return "NOTICE "+destination+" :"+message;
        }

        internal static string Join(string channel)
        {
            return "JOIN "+channel;
        }
        
        internal static string Join(string[] channels)
        {
            string channellist = String.Join(",", channels);
            return "JOIN "+channellist;
        }
        
        internal static string Join(string channel, string key)
        {
            return "JOIN "+channel+" "+key;
        }

        internal static string Join(string[] channels, string[] keys)
        {
            string channellist = String.Join(",", channels);
            string keylist = String.Join(",", keys);
            return "JOIN "+channellist+" "+keylist;
        }
        
        internal static string Part(string channel)
        {
            return "PART "+channel;
        }

        internal static string Part(string[] channels)
        {
            string channellist = String.Join(",", channels);
            return "PART "+channellist;
        }
        
        internal static string Part(string channel, string partmessage)
        {
            return "PART "+channel+" :"+partmessage;
        }

        internal static string Part(string[] channels, string partmessage)
        {
            string channellist = String.Join(",", channels);
            return "PART "+channellist+" :"+partmessage;
        }

        internal static string Kick(string channel, string nickname)
        {
            return "KICK "+channel+" "+nickname;
        }

        internal static string Kick(string channel, string nickname, string comment)
        {
            return "KICK "+channel+" "+nickname+" :"+comment;
        }
        
        internal static string Kick(string[] channels, string nickname)
        {
            string channellist = String.Join(",", channels);
            return "KICK "+channellist+" "+nickname;
        }

        internal static string Kick(string[] channels, string nickname, string comment)
        {
            string channellist = String.Join(",", channels);
            return "KICK "+channellist+" "+nickname+" :"+comment;
        }

        internal static string Kick(string channel, string[] nicknames)
        {
            string nicknamelist = String.Join(",", nicknames);
            return "KICK "+channel+" "+nicknamelist;
        }

        internal static string Kick(string channel, string[] nicknames, string comment)
        {
            string nicknamelist = String.Join(",", nicknames);
            return "KICK "+channel+" "+nicknamelist+" :"+comment;
        }

        internal static string Kick(string[] channels, string[] nicknames)
        {
            string channellist = String.Join(",", channels);
            string nicknamelist = String.Join(",", nicknames);
            return "KICK "+channellist+" "+nicknamelist;
        }

        internal static string Kick(string[] channels, string[] nicknames, string comment)
        {
            string channellist = String.Join(",", channels);
            string nicknamelist = String.Join(",", nicknames);
            return "KICK "+channellist+" "+nicknamelist+" :"+comment;
        }
        
        internal static string Motd()
        {
            return "MOTD";
        }

        internal static string Motd(string target)
        {
            return "MOTD "+target;
        }

        internal static string Luser()
        {
            return "LUSER";
        }

        internal static string Luser(string mask)
        {
            return "LUSER "+mask;
        }

        internal static string Luser(string mask, string target)
        {
            return "LUSER "+mask+" "+target;
        }
        
        internal static string Version()
        {
            return "VERSION";
        }

        internal static string Version(string target)
        {
            return "VERSION "+target;
        }

        internal static string Stats()
        {
            return "STATS";
        }

        internal static string Stats(string query)
        {
            return "STATS "+query;
        }

        internal static string Stats(string query, string target)
        {
            return "STATS "+query+" "+target;
        }

        internal static string Links()
        {
            return "LINKS";
        }
        
        internal static string Links(string servermask)
        {
            return "LINKS "+servermask;
        }
        
        internal static string Links(string remoteserver, string servermask)
        {
            return "LINKS "+remoteserver+" "+servermask;
        }
        
        internal static string Time()
        {
            return "TIME";
        }
        
        internal static string Time(string target)
        {
            return "TIME "+target;
        }
        
        internal static string Connect(string targetserver, string port)
        {
            return "CONNECT "+targetserver+" "+port;
        }
        
        internal static string Connect(string targetserver, string port, string remoteserver)
        {
            return "CONNECT "+targetserver+" "+port+" "+remoteserver;
        }
        
        internal static string Trace()
        {
            return "TRACE";
        }
        
        internal static string Trace(string target)
        {
            return "TRACE "+target;
        }
        
        internal static string Admin()
        {
            return "ADMIN";
        }
        
        internal static string Admin(string target)
        {
            return "ADMIN "+target;
        }
        
        internal static string Info()
        {
            return "INFO";
        }
        
        internal static string Info(string target)
        {
            return "INFO "+target;
        }
        
        internal static string Servlist()
        {
            return "SERVLIST";
        }
        
        internal static string Servlist(string mask)
        {
            return "SERVLIST "+mask;
        }
        
        internal static string Servlist(string mask, string type)
        {
            return "SERVLIST "+mask+" "+type;
        }
        
        internal static string Squery(string servicename, string servicetext)
        {
            return "SQUERY "+servicename+" :"+servicetext;
        }
        
        internal static string List()
        {
            return "LIST";
        }

        internal static string List(string channel)
        {
            return "LIST "+channel;
        }

        internal static string List(string[] channels)
        {
            string channellist = String.Join(",", channels);
            return "LIST "+channellist;
        }
        
        internal static string List(string channel, string target)
        {
            return "LIST "+channel+" "+target;
        }

        internal static string List(string[] channels, string target)
        {
            string channellist = String.Join(",", channels);
            return "LIST "+channellist+" "+target;
        }
        
        internal static string Names()
        {
            return "NAMES";
        }

        internal static string Names(string channel)
        {
            return "NAMES "+channel;
        }

        internal static string Names(string[] channels)
        {
            string channellist = String.Join(",", channels);
            return "NAMES "+channellist;
        }
        
        internal static string Names(string channel, string target)
        {
            return "NAMES "+channel+" "+target;
        }
        
        internal static string Names(string[] channels, string target)
        {
            string channellist = String.Join(",", channels);
            return "NAMES "+channellist+" "+target;
        }
        
        internal static string Topic(string channel)
        {
            return "TOPIC "+channel;
        }

        internal static string Topic(string channel, string newtopic)
        {
            return "TOPIC "+channel+" :"+newtopic;
        }

        internal static string Mode(string target)
        {
            return "MODE "+target;
        }

        internal static string Mode(string target, string newmode)
        {
            return "MODE "+target+" "+newmode;
        }

        internal static string Service(string nickname, string distribution, string info)
        {
            return "SERVICE "+nickname+" * "+distribution+" * * :"+info;
        }
        
        internal static string Invite(string nickname, string channel)
        {
            return "INVITE "+nickname+" "+channel;
        }

        internal static string Who()
        {
            return "WHO";
        }
        
        internal static string Who(string mask)
        {
            return "WHO "+mask;
        }
        
        internal static string Who(string mask, bool ircop)
        {
            if (ircop) {
                return "WHO "+mask+" o";
            } else {
                return "WHO "+mask;
            }
        }
        
        internal static string Whois(string mask)
        {
            return "WHOIS "+mask;
        }
        
        internal static string Whois(string[] masks)
        {
            string masklist = String.Join(",", masks);
            return "WHOIS "+masklist;
        }
        
        internal static string Whois(string target, string mask)
        {
            return "WHOIS "+target+" "+mask;
        }
        
        internal static string Whois(string target, string[] masks)
        {
            string masklist = String.Join(",", masks);
            return "WHOIS "+target+" "+masklist;
        }
        
        internal static string Whowas(string nickname)
        {
            return "WHOWAS "+nickname;
        }
        
        internal static string Whowas(string[] nicknames)
        {
            string nicknamelist = String.Join(",", nicknames);
            return "WHOWAS "+nicknamelist;
        }

        internal static string Whowas(string nickname, string count)
        {
            return "WHOWAS "+nickname+" "+count+" ";
        }
        
        internal static string Whowas(string[] nicknames, string count)
        {
            string nicknamelist = String.Join(",", nicknames);
            return "WHOWAS "+nicknamelist+" "+count+" ";
        }
        
        internal static string Whowas(string nickname, string count, string target)
        {
            return "WHOWAS "+nickname+" "+count+" "+target;
        }
        
        internal static string Whowas(string[] nicknames, string count, string target)
        {
            string nicknamelist = String.Join(",", nicknames);
            return "WHOWAS "+nicknamelist+" "+count+" "+target;
        }
        
        internal static string Kill(string nickname, string comment)
        {
            return "KILL "+nickname+" :"+comment;
        }
        
        internal static string Ping(string server)
        {
            return "PING "+server;
        }
        
        internal static string Ping(string server, string server2)
        {
            return "PING "+server+" "+server2;
        }
        
        internal static string Pong(string server)
        {
            return "PONG "+server;
        }
        
        internal static string Pong(string server, string server2)
        {
            return "PONG "+server+" "+server2;
        }

        internal static string Error(string errormessage)
        {
            return "ERROR :"+errormessage;
        }
        
        internal static string Away()
        {
            return "AWAY";
        }
        
        internal static string Away(string awaytext)
        {
            return "AWAY :"+awaytext;
        }
        
        internal static string Rehash()
        {
            return "REHASH";
        }
        
        internal static string Die()
        {
            return "DIE";
        }
        
        internal static string Restart()
        {
            return "RESTART";
        }
        
        internal static string Summon(string user)
        {
            return "SUMMON "+user;
        }
        
        internal static string Summon(string user, string target)
        {
            return "SUMMON "+user+" "+target;
        }
        
        internal static string Summon(string user, string target, string channel)
        {
            return "SUMMON "+user+" "+target+" "+channel;
        }
        
        internal static string Users()
        {
            return "USERS";
        }
        
        internal static string Users(string target)
        {
            return "USERS "+target;
        }
        
        internal static string Wallops(string wallopstext)
        {
            return "WALLOPS :"+wallopstext;
        }
        
        internal static string Userhost(string nickname)
        {
            return "USERHOST "+nickname;
        }
        
        internal static string Userhost(string[] nicknames)
        {
            string nicknamelist = String.Join(" ", nicknames);
            return "USERHOST "+nicknamelist;
        }
        
        internal static string Ison(string nickname)
        {
            return "ISON "+nickname;
        }
        
        internal static string Ison(string[] nicknames)
        {
            string nicknamelist = String.Join(" ", nicknames);
            return "ISON "+nicknamelist;
        }
        
        internal static string Quit()
        {
            return "QUIT";
        }
        
        internal static string Quit(string quitmessage)
        {
            return "QUIT :"+quitmessage;
        }
        
        internal static string Squit(string server, string comment)
        {
            return "SQUIT "+server+" :"+comment;
        }
    }
}
