Typpo's DCA
====================

This software was a project that I started and mostly wrote starting in late middle school and through high school (~2005+).  It automates a then-popular browser-based MMORPG by navigating a virtual city and killing things.

The client was used millions of times by tens of thousands of different users.  For a period of time, it ran with user authentication (there are still leftovers in the code) and made good money.

Since writing it, my interest and skill in software engineering has come a long way.  Now that I'm shutting the project down, I figured I'd post the old source for posterity.  I wrote it learning as I went, so it's an understatement to say that the code is not beautiful.  It was also written for .NET 2.0, so it's missing some of the nicer parts of .NET.

Setup and Usage
-----

You'll find everything you need to run your own server in `server/`.  With authentication stripped away, the server side is very simple and mostly just serves up map info when the client requests it.

You should easily be able to import `src/` into Visual Studio.  From there I wish you luck.  The program is mostly updated for the August 2011 changes, but mob parsing needs to be fixed.

License (GPL v3)
-----
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
