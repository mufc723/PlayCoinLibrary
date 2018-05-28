namespace PlayCoinLibrary
{
    using System;
    using System.Collections.Generic;


    public partial class User
    {

        public int Id { get; set; }

        public string Username { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string Account_type { get; set; }

        public string Image { get; set; }

        public string Game_description { get; set; }

        public string Game_type { get; set; }

        public int? Enabled { get; set; }

       
        public string Private_key { get; set; }

     
    }
}
