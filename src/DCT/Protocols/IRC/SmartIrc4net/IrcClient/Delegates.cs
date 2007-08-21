/*
 * $Id: Delegates.cs 203 2005-06-10 01:42:42Z meebey $
 * $URL: svn://svn.qnetp.net/smartirc/SmartIrc4net/tags/0.4.0/src/IrcClient/Delegates.cs $
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

namespace Meebey.SmartIrc4net
{
    internal delegate void IrcEventHandler(object sender, IrcEventArgs e);
    internal delegate void CtcpEventHandler(object sender, CtcpEventArgs e);
    internal delegate void ActionEventHandler(object sender, ActionEventArgs e);
    internal delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
    internal delegate void PingEventHandler(object sender, PingEventArgs e);
    internal delegate void KickEventHandler(object sender, KickEventArgs e);
    internal delegate void JoinEventHandler(object sender, JoinEventArgs e);
    internal delegate void NamesEventHandler(object sender, NamesEventArgs e);
    internal delegate void PartEventHandler(object sender, PartEventArgs e);
    internal delegate void InviteEventHandler(object sender, InviteEventArgs e);
    internal delegate void OpEventHandler(object sender, OpEventArgs e);
    internal delegate void DeopEventHandler(object sender, DeopEventArgs e);
    internal delegate void HalfopEventHandler(object sender, HalfopEventArgs e);
    internal delegate void DehalfopEventHandler(object sender, DehalfopEventArgs e);
    internal delegate void VoiceEventHandler(object sender, VoiceEventArgs e);
    internal delegate void DevoiceEventHandler(object sender, DevoiceEventArgs e);
    internal delegate void BanEventHandler(object sender, BanEventArgs e);
    internal delegate void UnbanEventHandler(object sender, UnbanEventArgs e);
    internal delegate void TopicEventHandler(object sender, TopicEventArgs e);
    internal delegate void TopicChangeEventHandler(object sender, TopicChangeEventArgs e);
    internal delegate void NickChangeEventHandler(object sender, NickChangeEventArgs e);
    internal delegate void QuitEventHandler(object sender, QuitEventArgs e);
    internal delegate void AwayEventHandler(object sender, AwayEventArgs e);
    internal delegate void WhoEventHandler(object sender, WhoEventArgs e);
    internal delegate void MotdEventHandler(object sender, MotdEventArgs e);
    internal delegate void PongEventHandler(object sender, PongEventArgs e);
}
