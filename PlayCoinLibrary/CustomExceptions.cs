using System;
using System.Collections.Generic;
using System.Text;

namespace PlayCoinLibrary
{
    public class UserNotSetException : Exception
    {
        public override string Message => "User Not Signed In";
    }
    public class GameNotSetException : Exception
    {
        public override string Message => "Game Not Signed In ";
    }
}
